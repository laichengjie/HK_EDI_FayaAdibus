using HK_EDI.Core;
using HK_EDI.Core.Common;
using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Data;
using HK_EDI.Service.Orders;
using HK_EDI.Service.WMS;
using HK_EDI.Service.WMS.Best.EnumFolds;
using HK_EDI.Service.WMS.CommonModels;
using HK_EDI.Services.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace HK_EDI.Service.FayaAdibus.CreateReplenishmentOrder
{
    /// <summary>
    /// FRS补货单生成恒康补货单（调拨申请单）
    /// </summary>
    public class CreateReplenishmentOrderService : lockExtend, ICreateReplenishmentOrderService, IWMSService
    {
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly IOrderHandle _orderHandle;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly fayaadibusReplenishmentOrderSet _FayaAdibusReplenishmentOrderSet;

        public CreateReplenishmentOrderService(ILogger logger, IJobOrderTraceDetailService iJobOrderTraceDetailService, IJobOrderTraceService iJobOrderTraceService, IOrderHandle orderHandle, IDbContext dbContext, fayaadibusReplenishmentOrderSet fayaadibusReplenishmentOrderSet)
        {
            this._logger = logger;
            this._iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            this._iJobOrderTraceService = iJobOrderTraceService;
            this._orderHandle = orderHandle;
            this._dbContext = dbContext;
            this._FayaAdibusReplenishmentOrderSet = fayaadibusReplenishmentOrderSet;
        }

        /// <summary>
        /// 补货单生成恒康补货单
        /// </summary>
        /// <param name="config"></param>
        /// <param name="BillNo"></param>
        public void CreateReplenishmentOrderMethod(SD_Adibus_Interface config, string BillNo)
        {
            lock (GetPostSkuLock(config, "CreateReplenishmentOrderMethod"))
            { 
                try
                {
                    bool isCreateReplenishment = false;
                    DataTable dtMaster = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_AdibusGetReplenishmentOrder where UploadState = 0 ");
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        isCreateReplenishment = true;
                    }

                    if (isCreateReplenishment || !string.IsNullOrEmpty(BillNo))
                    {
                        //生成补货单(调拨申请单)
                        var outRetVal = new System.Data.SqlClient.SqlParameter("RetVal", "");
                        outRetVal.Direction = ParameterDirection.Output;
                        outRetVal.Size = 1000;
                        var outRetMsg = new System.Data.SqlClient.SqlParameter("RetValMS", "");
                        outRetMsg.Direction = ParameterDirection.Output;
                        outRetMsg.Size = 1000;
                        DBHelper.ExecuteNonQuery("sp_HK_FAYACreatAdibusReplenishment", CommandType.StoredProcedure,
                         new System.Data.SqlClient.SqlParameter[]
                           {  new System.Data.SqlClient.SqlParameter("CompanyID",_FayaAdibusReplenishmentOrderSet.CompanyId),
                                      new System.Data.SqlClient.SqlParameter("Type","0"),
                                      new System.Data.SqlClient.SqlParameter("BillNo",BillNo),
                                   outRetVal, outRetMsg
                           });

                        _logger.Information(string.Format("CreateReplenishmentOrder-Information: 生成补货单(调拨申请单)处理结束,处理时间:{0},公司ID: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd"), _FayaAdibusReplenishmentOrderSet.CompanyId));
                    }
                    else
                    {
                        _logger.Information(string.Format("CreateReplenishmentOrder-Information: 没有可生成补货单(调拨申请单)的数据,时间:{0},公司ID: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd"), _FayaAdibusReplenishmentOrderSet.CompanyId));
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("CreateReplenishmentOrder-Error:{0}", ex.Message));
                }
            }
        }

        public void DelOrderBySignCode(SD_Adibus_Interface config, string SignCode)
        {
            List<string> vs = new List<string>();
            string sql = "DELETE FROM dbo.HK_Adibus_JobOrderTraceDetail WHERE PId IN (SELECT Id FROM dbo.HK_Adibus_JobOrderTrace WHERE  OpResultStatus =-95 and   OSInterfaceID ='" + config.OSInterfaceID + "' AND  SignCode ='" + SignCode + "')" +
                          "DELETE FROM dbo.HK_Adibus_JobOrderTrace WHERE OpResultStatus =-95 and  OSInterfaceID = '" + config.OSInterfaceID + "' AND SignCode = '" + SignCode + "'";
            vs.Add(sql);
            DBHelper.ExecuteNonTranQuery(vs);
        }

        public void PostByOperator(SD_Adibus_Interface config)
        {
            throw new NotImplementedException();
        }

        public CommonReturnModel CancelOrder(SD_Adibus_Interface config, string SignCodes)
        {
            throw new NotImplementedException();
        }

        public CommonReturnModel CancelOrderList(SD_Adibus_Interface config, string SignCodes)
        {
            throw new NotImplementedException();
        }

        public void CompleteOrder(SD_Adibus_Interface config)
        {
            throw new NotImplementedException();
        }

        public CommonReturnModel ForceProcess(SD_Adibus_Interface config, string SignCodes)
        {
            throw new NotImplementedException();
        }

        public string GetOrderStatus(SD_Adibus_Interface config, string singCode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BaseItem> GetOrderStatusEnums()
        {
            return OrderOperationStatusContrastEnum.CancelOrder.ToEnumSelectList();
        }

        public bool IsCancelStatus(int status)
        {
            bool fs = false;
            if ((int)OrderOperationStatusContrastEnum.CancelOrder == status)
            {
                fs = true;
            }
            return fs;
        }

        public void PostInOrder(SD_Adibus_Interface config)
        {
            CreateReplenishmentOrderMethod(config, "");
        }
        public void PostOutOrder(SD_Adibus_Interface config)
        {
            throw new NotImplementedException();
        }

        public void PostSku(SD_Adibus_Interface config)
        {
            throw new NotImplementedException();
        }

        public void PostStockQuery(SD_Adibus_Interface config)
        {
            throw new NotImplementedException();
        }

        public void PostOrderByBillNo(SD_Adibus_Interface config, string BillNo)
        {
            CreateReplenishmentOrderMethod(config, BillNo);
        }
        public void PostOrderBySignCode(SD_Adibus_Interface config, string CompanyId, string SignCode)
        {
            CreateReplenishmentOrderMethod(config, "");
        }
        public string PostSummaryBillNo(SD_Adibus_Interface config, string CompanyId, string ShopID, string DateTime)
        {
            throw new NotImplementedException();
        }
    }
}
