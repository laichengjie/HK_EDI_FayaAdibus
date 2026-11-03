using BC.LogicPub;
using HK_EDI.Core;
using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Data;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Service.Tasks;
using HK_EDI.Service.WMS;
using HK_EDI.Service.WMS.CommonModels;
using HK_EDI.Service.WMS.SKU360.EnumFolds;
using HK_EDI.Service.WMS.SKU360.Models;
using HK_EDI.Services.Logging;
using LogicPub;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    /// <summary>
    /// 处理订单
    /// </summary>
    public class OrderHandle : IOrderHandle
    {
        private readonly IDbContext _dbContext;
        private readonly ITaskRunTimeService _iTaskRunTimeService;
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly ILogger _logger;
        private readonly IEDI_ConfigService _iedi_ConfigService;

        #region 构造函数

        public OrderHandle(IDbContext dbContext,
                        ITaskRunTimeService iTaskRunTimeService,
                        IJobOrderTraceService iJobOrderTraceService,
                        IJobOrderTraceDetailService iJobOrderTraceDetailService,
                        ILogger iLogger,
                        IEDI_ConfigService iedi_ConfigService
             )
        {
            _dbContext = dbContext;
            _iTaskRunTimeService = iTaskRunTimeService;
            _iJobOrderTraceService = iJobOrderTraceService;
            _iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            _logger = iLogger;
            this._iedi_ConfigService = iedi_ConfigService;
        }

        #endregion

        #region 公共方法

        public IList<BaseDataModel> GetProductData(Core.Domain.EDI_Config.SD_Adibus_Interface config, string key, ref string syncKey, ref TaskRunTime taskTime)
        {
            DateTime starttime = DateTime.Now.AddDays(-10000);
            DateTime endtime = DateTime.Now;
            syncKey = string.Format("HK_EDI-PostSku:{0}", key);
            taskTime = _iTaskRunTimeService.GetByKey(syncKey);
            starttime = taskTime == null ? starttime : taskTime.LastTime;
            //var listmodel = this._dbContext.ExecuteStoredProcedureListN<BaseDataModel>
            //    ("sp_HK_EDI_PostSku",
            //    new SqlParameter("StartTime", starttime),
            //    new SqlParameter("EndTime", endtime),
            //    new SqlParameter("OSInterfaceID", config.OSInterfaceID),
            //    new SqlParameter("InterfaceType", 1)
            //    );

            DataTable dtRead = DBHelper.ExecuteDataTable("sp_HK_EDI_PostSku", CommandType.StoredProcedure,
                    new System.Data.SqlClient.SqlParameter[]
                            {
                                new SqlParameter(){ ParameterName="StartTime", Value=starttime },
                                new SqlParameter(){ ParameterName="EndTime", Value=endtime },
                                new SqlParameter(){ ParameterName="OSInterfaceID", Value=config.OSInterfaceID },
                                new SqlParameter(){ ParameterName="InterfaceType", Value=1 }
                                },
                             true);
            List<BaseDataModel> listmodel = ConvertHelper.DataTableToList<BaseDataModel>(dtRead);

            return listmodel;
        }

        public IList<WMS.CommonModels.NormalOrderModel> GetOrderMasterData(Core.Domain.EDI_Config.SD_Adibus_Interface config, int OpertionType)
        {
            try
            {
                DateTime starttime = DateTime.Now.AddDays(-600);
                DateTime endtime = DateTime.Now;

                //var listmodel = this._dbContext.ExecuteStoredProcedureListN<NormalOrderModel>
                //                ("sp_HK_EDI_GetOrderDataPro",
                //                new SqlParameter("OSInterfaceID", config.OSInterfaceID),
                //                new SqlParameter("StartTime", starttime),
                //                new SqlParameter("EndTime", endtime),
                //                new SqlParameter("OutInFlag", OpertionType)
                //                );
                DataTable dtRead = DBHelper.ExecuteDataTable("sp_HK_EDI_GetOrderDataPro", CommandType.StoredProcedure,
                     new System.Data.SqlClient.SqlParameter[]
                    {
                                   new SqlParameter("OSInterfaceID", config.OSInterfaceID),
                                                new SqlParameter("StartTime", starttime),
                                                new SqlParameter("EndTime", endtime),
                                                new SqlParameter("OutInFlag", OpertionType)
                        },
                     true);

                List<NormalOrderModel> listmodel = ConvertHelper.DataTableToList<NormalOrderModel>(dtRead);

                return listmodel;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IList<WMS.CommonModels.NormalOrderDetailModel> GetOrderDetailData(Core.Domain.EDI_Config.SD_Adibus_Interface config, WMS.CommonModels.NormalOrderModel normalOrder, int OpertionType, ref JobOrderTrace orderTrace, ref int orderType)
        {
            try
            {
                orderType = GetOrderType(normalOrder.BillTypeID);
                orderTrace = _iJobOrderTraceService.GetJobOrderTrace(normalOrder.CompanyID, normalOrder.BillNo, config.OSInterfaceID,  orderType);
                //sp_HK_EDI_GetOrderDetailDataPro_SHInfo sp_HK_EDI_GetOrderDetailDataPro
                var orderDetail = this._dbContext.ExecuteStoredProcedureListN<NormalOrderDetailModel>
                      ("sp_HK_EDI_GetOrderDetailDataPro",
                      new SqlParameter("BillNo", normalOrder.BillNo),
                      new SqlParameter("CompanyID", normalOrder.CompanyID),
                      new SqlParameter("billTypeId", normalOrder.BillTypeID),
                      new SqlParameter("OutInFlag", OpertionType)
                      );

                return orderDetail;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IList<CompleteOrderGetModel> GetCompleteOrder(Core.Domain.EDI_Config.SD_Adibus_Interface config, int InterfaceType)
        {
            try
            {
                var completeOrders = this._dbContext.ExecuteStoredProcedureListN<CompleteOrderGetModel>
                                              ("sp_HK_EDI_GetOrderCompleteProc",
                                              new SqlParameter("OSInterfaceID", config.OSInterfaceID),
                                              new SqlParameter("InterfaceType", InterfaceType));
                return completeOrders;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ProcessOmsOrder<T>(string con, CompleteOrderGetModel Model, JobOrderTrace jobtrace, IRequestBase<T> result, SettingModel settingModel, bool uploadState = true) where T : IProductBase
        {
            try
            {
                #region 组装数据
                DataTable dt = new DataTable();
                DataTable detail = new DataTable();
                InitializeDataTable(dt, detail);
                var barCodes = string.Join("','", result.Products.Select(t => t.SkuCode).ToArray());
                var companybarcodeMode = _iJobOrderTraceService.GetCompleteOrderGetModel(barCodes);
                List<SkuModel> skuModel = new List<SkuModel>();
                foreach (var product in result.Products)  //遍历每条回执的商品数据
                {
                    DataRow dr = dt.NewRow();
                    var modelItem = companybarcodeMode.FirstOrDefault(t => t.BarCode.ToUpper() == product.SkuCode.ToUpper());
                    if (modelItem == null)
                    {
                        AddJobOrderTraceDetail(jobtrace, string.Format("没有找到商品编码:{0}", product.SkuCode));
                        return;
                    }

                    skuModel.Add(new SkuModel() { skuCode = modelItem.SKUCode, qty = product.Qty.ToDecimal() });
                    DataRow drs = detail.NewRow();
                    drs["Item"] = "";
                    drs["CustID"] = "";
                    drs["StockID"] = "";
                    drs["Sku_ProductNo"] = modelItem.BarCode;
                    drs["Sku_BatchNo"] = "";
                    drs["Qty"] = product.Qty.ToInt();
                    detail.Rows.Add(drs);
                }

                if (skuModel.Count == 0)
                    throw new Exception("skuList为空，没有货品明细");

                #endregion

                if (jobtrace.BillTypeID.ToUpper() == "BC_NETSALORDER")
                {
                    EDIReturn edi = new EDIReturn("");
                    bool fs = false;
                    string msg = "";
                    if (Model.SaleType == "0")
                    {
                        //发货
                        fs = edi.OrderSendGoodsForNew(con, result.ExpressNo, Model.BillNo, Model.CompanyID, settingModel.UserId, ref msg, result.ExpressCode, "CN", skuModel, "", true, uploadState);
                    }
                    else if (Model.SaleType == "1")
                    {
                       //退货
                      fs = edi.CreateRMAPos(con, Model.CompanyID, Model.BillNo, settingModel.UserId, "CN", ref msg, skuModel, isflow: settingModel.IsFlow);
                      
                    }

                    if (fs)
                    {
                        jobtrace.OpResultStatus = (int)OrderOperationStatusContrastEnum.ReceiveProcessedComplete;
                        jobtrace.ReadDate = DateTime.Now;
                        _iJobOrderTraceService.UpdateOrderTrace(jobtrace);
                        JobOrderTraceDetail jobdetail = new JobOrderTraceDetail();
                        jobdetail.PId = jobtrace.Id;
                        jobdetail.BillNo = Model.BillNo;
                        jobdetail.CompanyID = Model.CompanyID;
                        jobdetail.ContentNode = "";
                        jobdetail.ContentNodeRequest = "";
                        jobdetail.Description = "订单回执处理完成";
                        jobdetail.ContentType = (int)ContentType_OperationEnum.CompleteOrder_Success;
                        jobdetail.CreateTime = DateTime.Now;
                        jobdetail.ModifyTime = DateTime.Now;
                        _iJobOrderTraceDetailService.AddJobOrderTraceDetail(jobdetail);
                    }
                    else
                    {
                        JobOrderTraceDetail jobdetail = new JobOrderTraceDetail();
                        jobdetail.PId = jobtrace.Id;
                        jobdetail.BillNo = Model.BillNo;
                        jobdetail.CompanyID = Model.CompanyID;
                        jobdetail.ContentNode = "";
                        jobdetail.ContentNodeRequest = "";
                        jobdetail.Description = "订单回执处理错误:" + msg;
                        jobdetail.ContentType = (int)ContentType_OperationEnum.CompleteOrder_Fail;
                        jobdetail.CreateTime = DateTime.Now;
                        jobdetail.ModifyTime = DateTime.Now;
                        _iJobOrderTraceDetailService.AddJobOrderTraceDetail(jobdetail);
                        _logger.Warning(string.Format("HK_EDI-CompleteOrder(2){0}-{1}:{2}", "", Model.BillNo, msg));
                    }

                }
                else if (jobtrace.BillTypeID.Equals("Inv_RejectSaleIn", StringComparison.OrdinalIgnoreCase))
                {
                    //处理受托代销单


                }
                //处理网店零售订单
                else
                {
                    int outTag = 1;
                    

                    DBHelper.ExecuteNonQuery("sp_HK_EDI_WMSCreateBill", CommandType.StoredProcedure,
                    new System.Data.SqlClient.SqlParameter[]
                                    {
                                            new System.Data.SqlClient.SqlParameter("SignCode",jobtrace.SignCode),
                                            new System.Data.SqlClient.SqlParameter("skuDetail",detail),
                                            new System.Data.SqlClient.SqlParameter("OutInFlag",outTag)
                                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void BillCancel(string billNo)
        {
            try
            {
                DBHelper.ExecuteNonQuery("sp_HK_EDI_BillCancel", CommandType.StoredProcedure,
                                    new System.Data.SqlClient.SqlParameter[]
                                    {
                                            new System.Data.SqlClient.SqlParameter("BillNo",billNo)
                                    });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void UpdatePostSkuErrorRecords(DataTable dt)
        {
            try
            {
                DBHelper.ExecuteNonQuery("sp_HK_EDI_UpdatePostSkuErrorRecords", CommandType.StoredProcedure,
                                    new System.Data.SqlClient.SqlParameter[]
                                    {
                                            new System.Data.SqlClient.SqlParameter("Recordstable",dt)
                                    });
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public JobOrderTrace AddOrderTrace(JobOrderTrace orderTrace, NormalOrderModel model,  int orderType)
        {
            try
            {
                if (orderTrace == null)
                {
                    orderTrace = new JobOrderTrace();
                    orderTrace.CompanyID = model.CompanyID;
                    orderTrace.NoticeBillNo = model.BillNo;
                    orderTrace.BillTypeID = model.BillTypeID;
                    orderTrace.CreateDate = DateTime.Now;
                    orderTrace.InsertedDate = DateTime.Now;
                    orderTrace.OrderType = orderType;
                    orderTrace.OpResultStatus = (int)OrderOperationStatusContrastEnum.NoProcessing;
                    _iJobOrderTraceService.AddJobOrderTrace(orderTrace);
                    orderTrace.SignCode = DateTime.Now.ToString("yyyyMMdd") + orderTrace.Id;
                    _iJobOrderTraceService.UpdateOrderTrace(orderTrace);
                }
                else
                {
                    orderTrace.ReadDate = DateTime.Now;
                    orderTrace.OpResultStatus = (int)HK_EDI.Service.WMS.Best.EnumFolds.OrderOperationStatusContrastEnum.NoProcessing;
                    _iJobOrderTraceService.UpdateOrderTrace(orderTrace);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return orderTrace;

        }

        public JobOrderTrace AddOrderTrace(JobOrderTrace orderTrace, NormalOrderModel model, string OSInterfaceID, int orderType,string ShopID, string StockID, DateTime BillDate)
        {
            try
            {
                if (orderTrace == null)
                {
                    orderTrace = new JobOrderTrace();
                    orderTrace.OSInterfaceID = OSInterfaceID;
                    orderTrace.CompanyID = model.CompanyID;
                    orderTrace.NoticeBillNo = model.BillNo;
                    orderTrace.CgbillNo = model.SourcebillNo;
                    orderTrace.BillTypeID = model.BillTypeID;
                    orderTrace.CustomerCode = model.CustomerCode;//客户代码
                    orderTrace.CreateDate = DateTime.Now;
                    orderTrace.InsertedDate = DateTime.Now;
                    orderTrace.OrderType = orderType;
                    orderTrace.ShopID = ShopID;
                    orderTrace.StockID = StockID;
                    orderTrace.BillDate = BillDate;
                    orderTrace.SignCode = DateTime.Now.ToString("yyyyMMdd") + orderTrace.Id;
                    orderTrace.OpResultStatus = (int)OrderOperationStatusContrastEnum.NoProcessing;
                    _iJobOrderTraceService.AddJobOrderTrace(orderTrace);
                    orderTrace.SignCode = DateTime.Now.ToString("yyyyMMdd") + orderTrace.Id;
                    _iJobOrderTraceService.UpdateOrderTrace(orderTrace);
                }
                else
                {
                    orderTrace.ShopID = ShopID;
                    orderTrace.StockID = StockID;
                    orderTrace.BillDate = BillDate;
                    orderTrace.ReadDate = DateTime.Now;
                    orderTrace.OpResultStatus = (int)HK_EDI.Service.WMS.Best.EnumFolds.OrderOperationStatusContrastEnum.NoProcessing;
                    _iJobOrderTraceService.UpdateOrderTrace(orderTrace);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            return orderTrace;

        }
        public void InitializeDataTable(DataTable dt, DataTable detail)
        {
            dt.Columns.Add("Sku", System.Type.GetType("System.String"));//商品编码
            dt.Columns.Add("Qty", System.Type.GetType("System.Int32"));//数量
            dt.Columns.Add("lotNumber", System.Type.GetType("System.String"));
            dt.Columns.Add("uniqueId", System.Type.GetType("System.String"));


            detail.Columns.Add("Item", System.Type.GetType("System.String"));
            detail.Columns.Add("CustID", System.Type.GetType("System.String"));
            detail.Columns.Add("StockID", System.Type.GetType("System.String"));
            detail.Columns.Add("Sku_ProductNo", System.Type.GetType("System.String"));
            detail.Columns.Add("Sku_BatchNo", System.Type.GetType("System.String"));
            detail.Columns.Add("Qty", System.Type.GetType("System.Int32"));
        }
        public void AddJobOrderTraceDetail(JobOrderTrace jobtrace, string ex)
        {
            JobOrderTraceDetail jobdetail = new JobOrderTraceDetail();
            jobdetail.PId = jobtrace.Id;
            jobdetail.BillNo = jobtrace.NoticeBillNo;
            jobdetail.CompanyID = jobtrace.CompanyID;
            jobdetail.ContentNode = "";
            jobdetail.ContentNodeRequest = "";
            jobdetail.Description = "订单回执处理错误" + ex.ToString();
            jobdetail.ContentType = (int)ContentType_OperationEnum.CompleteOrder_Fail;
            jobdetail.CreateTime = DateTime.Now;
            jobdetail.ModifyTime = DateTime.Now;
            _iJobOrderTraceDetailService.AddJobOrderTraceDetail(jobdetail);
            _logger.Warning(string.Format("HK_EDI-CompleteOrder(3)-{0}:{1}:{2}", "", jobtrace.NoticeBillNo, ex.ToString()));
        }

        public IList<OrderValidityResult> CheckOrderValidity(string billNo, string ossStockID)
        {
            if (string.IsNullOrWhiteSpace(billNo))
            {
                throw new ArgumentException("不能为null或空白字符串", "billNo");
            }
            if (string.IsNullOrWhiteSpace(ossStockID))
            {
                throw new ArgumentException("不能为null或空白字符串", "ossStockID");
            }
            DataTable dtRead = DBHelper.ExecuteDataTable("sp_HK_EDI_CheckOrderValidity", CommandType.StoredProcedure,
                new SqlParameter[]
                {
                 new SqlParameter(){ ParameterName="BillNo", Value=billNo },
                 new SqlParameter(){ ParameterName="OSInterfaceID", Value=ossStockID }
                }, true);
            List<OrderValidityResult> listmodel = ConvertHelper.DataTableToList<OrderValidityResult>(dtRead);
            return listmodel;
        }
        #endregion

        #region 辅助方法

        /// <summary>
        /// 取的日志状态记录表的订单类型
        /// </summary>
        /// <param name="billTypeId">类型ID</param>
        /// <param name="opertionType">操作类型</param>
        /// <param name="MoreZero">是否大于0</param>
        /// <returns></returns>
        public int GetOrderType(string billTypeId)
        {
            int result = -1;

            switch (billTypeId.ToUpper())
            {
                case "GETESTIMATEDSHIPMENT":
                    result = (int)OrderTypeEnum.GETESTIMATEDSHIPMENT;
                    break;
                case "CREATECGORDER":
                    result = (int)OrderTypeEnum.CREATECGORDER;
                    break;
                case "GETPACKINGLIST":
                    result = (int)OrderTypeEnum.GETPACKINGLIST;
                    break;
                case "CREATERECEIPT":
                    result = (int)OrderTypeEnum.CREATERECEIPT;
                    break;

            }
            if (result < 0)
            {
                throw new Exception("没有找到类型");
            }
            return result;
        }


        #endregion
    }
}
