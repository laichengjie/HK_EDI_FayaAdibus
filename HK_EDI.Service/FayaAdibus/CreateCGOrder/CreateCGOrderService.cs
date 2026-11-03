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
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.FayaAdibus.CreateCGOrder
{
    public class CreateCGOrderService : lockExtend, ICreateCGOrderService, IWMSService
    {
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly IOrderHandle _orderHandle;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;
        private readonly FayaAdibusGetEstimatedShipmentSet _FayaAdibusGetEstimatedShipmentSet;

        public CreateCGOrderService(ILogger logger, IJobOrderTraceDetailService iJobOrderTraceDetailService, IJobOrderTraceService iJobOrderTraceService, IOrderHandle orderHandle, IDbContext dbContext, FayaAdibusGetEstimatedShipmentSet fayaAdibusGetEstimatedShipmentSet)
        {
            this._logger = logger;
            this._iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            this._iJobOrderTraceService = iJobOrderTraceService;
            this._orderHandle = orderHandle;
            this._dbContext = dbContext;
            this._FayaAdibusGetEstimatedShipmentSet = fayaAdibusGetEstimatedShipmentSet;
        }
        /// <summary>
        /// ad预计发货单生成erp期货采购订单
        /// </summary>
        /// <param name="config"></param>
        /// <param name="BillNo"></param>
        public void CreateCGOrderMethod(SD_Adibus_Interface config, string BillNo)
        {
            lock (GetPostSkuLock(config, "CreateCGOrderMethod"))
            {
                try
                {
                    bool isCreateReceipt = false;
                    DataTable dtMaster = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_AdibusEstimatedShipment where UploadState = 0 ");
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
                        DBHelper.ExecuteNonQuery("sp_HK_FAYACreatAdibusOrder", CommandType.StoredProcedure,
                         new System.Data.SqlClient.SqlParameter[]
                           {  new System.Data.SqlClient.SqlParameter("CompanyID",_FayaAdibusGetEstimatedShipmentSet.CompanyId),
                                      new System.Data.SqlClient.SqlParameter("Type","0"),
                                      new System.Data.SqlClient.SqlParameter("BillNo",BillNo),
                                   outRetVal, outRetMsg
                           });

                        _logger.Information(string.Format("CreateReceipt-Information: 生成采购订单处理结束,处理时间:{0},公司ID: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd"), _FayaAdibusGetEstimatedShipmentSet.CompanyId));

                    }
                    else
                    {
                        _logger.Information(string.Format("CreateReceipt-Information: 没有可生成采购订单的数据,时间:{0},公司ID: {1}", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:ffff dddd"), _FayaAdibusGetEstimatedShipmentSet.CompanyId));

                    }



                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("CreateReceipt-Error:{0}", ex.Message));
                }
            }

        }

        public static int AddDataMethod<T>(T nodel, string TabelName) where T : new()
        {
            //获取Type对象
            Type type = typeof(T);
            string strsql = string.Format("insert into {0}", TabelName);
            string column = "(";
            string val = "values(";
            string link = "";
            int index = 0;
            //获取Type对象所有公共属性
            PropertyInfo[] info = type.GetProperties();
            int count = 0;
            if (TabelName == "HK_Faya_AdibusGetPackingList")
            {
                count = info.Count() - 1;
            }
            else
            {
                count = info.Count();
            }
            SqlParameter[] p = new SqlParameter[count];
            foreach (PropertyInfo item in info)
            {
                if (item.Name.ToLower() == "detail")
                {
                    continue;
                }
               
       
                column += link + item.Name;//定义需要添加的字段
                val += link + " @" + item.Name;//定义字段变量
                //给字段变量赋值
                if ((item.GetValue(nodel) != null ? item.GetValue(nodel).ToString() : "") == "0000-00-00 00:00:00")
                {
                    p[index++] = new SqlParameter("@" + item.Name, null);
                }
                else
                {
                    if (item.Name.ToLower() == "send_datetime" || item.Name.ToLower() == "etd" || item.Name.ToLower() == "eta")//UTC时间转为本地时区时间
                    {
                        p[index++] = new SqlParameter("@" + item.Name, Convert.ToDateTime(item.GetValue(nodel).ToString().Replace(" ", "+")));
                    }
                    else
                    {
                        p[index++] = new SqlParameter("@" + item.Name, item.GetValue(nodel) != null ? item.GetValue(nodel) : DBNull.Value);
                    }
                }

                link = ",";
            }
            column += ")";
            val += ")";
            strsql += column + val;

            return DBHelper.ExecuteNonTranQuery(strsql, p);

        }
        public void DelOrderBySignCode(SD_Adibus_Interface config, string SignCode)
        {
            List<string> vs = new List<string>();
            string sql = "DELETE FROM dbo.HK_Adibus_JobOrderTraceDetail WHERE PId IN (SELECT Id FROM dbo.HK_Adibus_JobOrderTrace WHERE OSInterfaceID ='" + config.OSInterfaceID + "' AND  SignCode ='" + SignCode + "')" +
                          "DELETE FROM dbo.HK_Adibus_JobOrderTrace WHERE OSInterfaceID = '"+ config.OSInterfaceID+ "' AND SignCode = '"+ SignCode + "'";
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
            CreateCGOrderMethod(config, "");
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
            CreateCGOrderMethod(config, BillNo);
        }
        public void PostOrderBySignCode(SD_Adibus_Interface config, string CompanyId, string SignCode)
        {
            CreateCGOrderMethod(config, "");
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
    public class Response
    {
        [JsonProperty("error_code")]
        public string error_code { get; set; }
        [JsonProperty("error_message")]
        public string error_message { get; set; }
    }
}
