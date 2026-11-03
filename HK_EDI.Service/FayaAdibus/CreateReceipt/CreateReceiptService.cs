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
using HK_EDI.Service.XmlNodeClass;
using HK_EDI.Services.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HK_EDI.Service.FayaAdibus.CreateReceipt
{
    public class CreateReceiptService : lockExtend, ICreateReceiptService, IWMSService
    {
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly IAdibusPosSaleAsicsService _iAdibusPosSaleAsicsService;
        private readonly IOrderHandle _orderHandle;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly FayaAdibusGetPackingListSet _FayaAdibusGetPackingListSet;

        public CreateReceiptService(ILogger logger, IJobOrderTraceDetailService iJobOrderTraceDetailService, IJobOrderTraceService iJobOrderTraceService, IAdibusPosSaleAsicsService iAdibusPosSaleAsicsService, IOrderHandle orderHandle, IDbContext dbContext, FayaAdibusGetPackingListSet fayaAdibusGetPackingListSet)
        {
            this._logger = logger;
            this._iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            this._iJobOrderTraceService = iJobOrderTraceService;
            this._iAdibusPosSaleAsicsService = iAdibusPosSaleAsicsService;
            this._orderHandle = orderHandle;
            this._dbContext = dbContext;
            this._FayaAdibusGetPackingListSet = fayaAdibusGetPackingListSet;
        }
        /// <summary>
        /// ad装箱单生成erp采购到货单
        /// </summary>
        /// <param name="config"></param>
        /// <param name="BillNo"></param>
        public void CreateReceiptMethod(SD_Adibus_Interface config, string BillNo)
        {
            lock (GetPostSkuLock(config, "CreateReceiptMethod"))
            {
                try
                {
                    bool isCreateReceipt = false;
                    DataTable dtMaster = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_AdibusGetPackingList where UploadState = 0 ");
                    if (dtMaster != null && dtMaster.Rows.Count > 0)
                    {
                        isCreateReceipt = true;
                    }

                    if (isCreateReceipt || !string.IsNullOrEmpty(BillNo))
                    {
                        //生成POS销售单
                        var outRetVal = new System.Data.SqlClient.SqlParameter("RetVal", "");
                        outRetVal.Direction = ParameterDirection.Output;
                        outRetVal.Size = 1000;
                        var outRetMsg = new System.Data.SqlClient.SqlParameter("RetValMS", "");
                        outRetMsg.Direction = ParameterDirection.Output;
                        outRetMsg.Size = 1000;
                        DBHelper.ExecuteNonQuery("sp_HK_FAYACreatAdibusReceipt", CommandType.StoredProcedure,
                         new System.Data.SqlClient.SqlParameter[]
                           {  new System.Data.SqlClient.SqlParameter("CompanyID",_FayaAdibusGetPackingListSet.CompanyId),
                                      new System.Data.SqlClient.SqlParameter("Type","0"),
                                      new System.Data.SqlClient.SqlParameter("BillNo",BillNo),
                                   outRetVal, outRetMsg
                           });

                        _logger.Information(string.Format("CreateReceipt-Information: 生成期货采购到货单处理结束,处理时间:{0},公司ID: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd"), _FayaAdibusGetPackingListSet.CompanyId));

                    }else
                    {
                        _logger.Information(string.Format("CreateReceipt-Information: 没有可生成期货采购到货单的数据,时间:{0},公司ID: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd"), _FayaAdibusGetPackingListSet.CompanyId));

                    }



                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("CreateReceipt-Error:{0}", ex.Message));
                }
            }
        }

        private void PostData2Service(string data, List<JobOrderTrace> listorderTrace)
        {
           
        }
        private DataSet GetPosDataDetail(string CompanyID, string BillNo)
        {
            var dataSettingsManager = new HK_EDI.Core.Data.DataSettingsManager();
            var connectionString = dataSettingsManager.LoadSettings().DataConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "spNome_GetPosDataDetail";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter p1 = new SqlParameter("@CompanyID", CompanyID);
                SqlParameter p2 = new SqlParameter("@BillNo", BillNo);

                p1.Direction = ParameterDirection.Input;
                p2.Direction = ParameterDirection.Input;

                command.Parameters.Add(p1);
                command.Parameters.Add(p2);
                //返回结果集
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
        }
        public string CreateParam(Dictionary<string, string> dicReq, bool isLower = false)
        {
            string appsecret = System.Configuration.ConfigurationManager.AppSettings["appsecret"];
            //排序
            dicReq = dicReq.OrderBy(r => r.Key).ToDictionary(r => r.Key, r => r.Value);
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (var item in dicReq)
            {
                if (item.Key == "sign")
                    continue;
                if (i > 0)
                {
                    sb.Append(";");
                }
                i++;
                sb.Append(item.Key.Length.ToString("00"));
                sb.Append("-");
                sb.Append(item.Key);
                sb.Append(":");
                sb.Append(item.Value.Length.ToString("0000"));
                sb.Append("-");
                sb.Append(item.Value);
            }
            sb = new StringBuilder();
            i = 0;
            foreach (var item in dicReq)
            {
                if (i == 0)
                    sb.Append(string.Format("{0}={1}", item.Key, item.Value));
                else
                    sb.Append(string.Format("&{0}={1}", item.Key, item.Value));
                i++;
            }
            return sb.ToString();
        }
        public void DelOrderBySignCode(SD_Adibus_Interface config,  string SignCode)
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
            CreateReceiptMethod(config, "");
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
            CreateReceiptMethod(config, BillNo);
        }
        public void PostOrderBySignCode(SD_Adibus_Interface config, string CompanyId, string SignCode)
        {
            CreateReceiptMethod(config, "");
        }
        public string PostSummaryBillNo(SD_Adibus_Interface config, string CompanyId, string ShopID, string DateTime)
        {
            throw new NotImplementedException();
        }

        class ModelHandler<T> where T : new()
        {
            /// <summary>  
            /// 填充对象列表：用DataSet的第index个表填充实体类
            /// </summary>  
            public static List<T> FillModel(DataSet ds, int index)
            {
                if (ds == null || ds.Tables.Count <= index || ds.Tables[index].Rows.Count == 0)
                {
                    return null;
                }
                else
                {
                    return FillModel(ds.Tables[index]);
                }
            }
            /// <summary>  
            /// 填充对象列表：用DataTable填充实体类
            /// </summary>  
            public static List<T> FillModel(DataTable dt)
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    return null;
                }
                List<T> modelList = new List<T>();
                foreach (DataRow dr in dt.Rows)
                {

                    T model = new T();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                        if (propertyInfo != null && dr[i] != DBNull.Value)
                            propertyInfo.SetValue(model, dr[i], null);
                    }

                    modelList.Add(model);
                }
                return modelList;
            }
        }
    }
}
