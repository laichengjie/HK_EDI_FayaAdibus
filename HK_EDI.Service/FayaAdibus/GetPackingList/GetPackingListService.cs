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
using System.Web.Mvc;

namespace HK_EDI.Service.FayaAdibus.GetPackingList
{
    public class GetPackingListService : lockExtend, IGetPackingListService, IWMSService
    {
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly FayaAdibusGetPackingListSet _FayaAdibusGetPackingListSet;
        private readonly IOrderHandle _orderHandle;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public GetPackingListService(ILogger logger, IJobOrderTraceDetailService iJobOrderTraceDetailService, IJobOrderTraceService iJobOrderTraceService, FayaAdibusGetPackingListSet fayaAdibusGetPackingListSet, IOrderHandle orderHandle, IDbContext dbContext)
        {
            this._logger = logger;
            this._iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            this._iJobOrderTraceService = iJobOrderTraceService;
            this._FayaAdibusGetPackingListSet = fayaAdibusGetPackingListSet;
            this._orderHandle = orderHandle;
            this._dbContext = dbContext;
        }
        public void GetPackingListMethod(SD_Adibus_Interface config, string BillNo)
        {
            lock (GetPostSkuLock(config, "GetPackingList"))
            {
                try
                {
                    string password = System.Configuration.ConfigurationManager.AppSettings["password"];
                    string consumer_syscode = System.Configuration.ConfigurationManager.AppSettings["consumer_syscode"];

                    string[] customerCodelist = _FayaAdibusGetPackingListSet.SetcustomerCode.Split(",");
                    string servicecode = _FayaAdibusGetPackingListSet.SERVICECODE;

                    if (customerCodelist != null && customerCodelist.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(consumer_syscode))
                        {
                            string BeginDataTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");//计算开始时间
                            string EndDataTime = DateTime.Now.AddSeconds(-120).ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");

                            foreach (string customerCode in customerCodelist)
                            {
                                try
                                {
                                    _logger.Information("GetPackingListService - Information:获取装箱单数据,客户数量:" + customerCodelist.Length + ",当前客户:" + customerCode + "");

                                    if (string.IsNullOrEmpty(customerCode))
                                        continue;
                                    if (_FayaAdibusGetPackingListSet.GetDate >= 0)//获取几天数据
                                    {
                                        for (int i = _FayaAdibusGetPackingListSet.GetDate; i >= 0; i--)
                                        {
                                            BeginDataTime = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");//计算开始时间
                                            GetPacking(BeginDataTime, password, customerCode, consumer_syscode, servicecode);
                                        }
                                    }
                                    else
                                    {
                                        BeginDataTime = _FayaAdibusGetPackingListSet.BeginDate.ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");
                                        GetPacking(BeginDataTime, password, customerCode, consumer_syscode, servicecode);
                                    }
                                }
                                catch
                                {
                                    _logger.Error("GetPackingListService - Error:获取装箱单数据, 获取数据异常,当前客户:" + customerCode + "");
                                }
                            }
                        }
                        else
                        {
                            _logger.Information("GetPackingListService - Information:获取装箱单数据,处理失败:WebConfing配置数据为空");
                        }
                    }
                    else
                    {
                        _logger.Information("GetEstimatedShipmentService - Information:获取装箱单数据,请先配置客户代码");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("GetPackingListService - err:获取装箱单数据,处理失败:" + ex.ToString());

                }
            }

        }
        public void GetPacking(string getDateTime, string password, string customerCode, string consumer,string servicecode)
        {
            try
            {


                MD5Set mD5Set = new MD5Set();


                string[] GetPackingList = new string[3];
                PostHeader postHeader = new PostHeader();
                PostDATA postDATA = new PostDATA();
                PostHEAD postHEAD = new PostHEAD();
                postHeader.SERVICE_CODE = servicecode;
                postHeader.CONSUMER_SYSCODE = consumer;
                postHeader.VERSION = "1";
                postHeader.MESSAGE_ID = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                postHeader.DTSEND = DateTime.Now.ToString("yyyyMMddHHmmss");
                //请求数据
                postHEAD.KAName = _FayaAdibusGetPackingListSet.CompanyId;
                postHEAD.GetDateTime = getDateTime;
                postHEAD.CustomerCode = customerCode;
                postDATA.HEAD = postHEAD;
                #region 保存数据
                string postHeaderJson = JsonConvert.SerializeObject(postHeader);
                string postDATAJson = JsonConvert.SerializeObject(postDATA);
                GetPackingList[0] = postHeaderJson;
                GetPackingList[1] = mD5Set.MD5Encrypt(password + postHeaderJson + password);
                GetPackingList[2] = postDATAJson;
                HK_EDI.Service.Common.WebClient wc = new HK_EDI.Service.Common.WebClient();
                string Header = "HEADER=" + GetPackingList[0] + "&PASSWORD=" + GetPackingList[1] + "&DATA=" + GetPackingList[2];
                InsertConnextLog(Header, "");
                var response = wc.PostWebRequest(_FayaAdibusGetPackingListSet.GetPackingListUrl, Header);
                InsertConnextLog(Header, response);

                GetPackingListBase getPackingListBase = JsonConvert.DeserializeObject<GetPackingListBase>(response);

                if (getPackingListBase != null)
                {
                    if (getPackingListBase.HEAD[0].StatusCode == "200")
                    {

                        List<GetPackingListBaseBODY> getlistBODY = getPackingListBase.BODY;
                        var list_distinct = getlistBODY.GroupBy(c => c.ShipmentNo).Select(c => c.First());//去重
                        var ShipmentNoList = new SelectList(list_distinct, "ShipmentNo", null);
                        IList<SelectListItem> DisShipmentNoLists = ShipmentNoList.ToList();
                        foreach (var ShipmentNo in DisShipmentNoLists)
                        {
                            DataTable dtDetail = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_AdibusGetPackingList where ShipmentNo = '" + ShipmentNo.Value.ToString() + "' ");
                            if (dtDetail != null && dtDetail.Rows.Count > 0)
                            {
                                continue;
                            }
                            List<GetPackingListBaseBODY> vPackingList = getlistBODY.Where(p => p.ShipmentNo == ShipmentNo.Value.ToString()).ToList();
                            string _pnno = "";
                            foreach (GetPackingListBaseBODY vPacking in vPackingList)
                            {
                                AddDataMethod(vPacking, "HK_Faya_AdibusGetPackingList");
                                _pnno += vPacking.PNNo + ",";
                            }

                            #region 订单状态
                            List<JobOrderTrace> listorderTrace = new List<JobOrderTrace>();
                            int orderType = 0;
                            JobOrderTrace orderTrace = new JobOrderTrace();

                            var orderModel = new NormalOrderModel() { CompanyID = _FayaAdibusGetPackingListSet.CompanyId, BillNo = ShipmentNo.Value.ToString(), BillTypeID = "CREATERECEIPT", SourcebillNo = _pnno, CustomerCode = vPackingList[0].ShipTo };
                            orderType = _orderHandle.GetOrderType("CREATERECEIPT");
                            orderTrace = _iJobOrderTraceService.GetJobOrderTrace(_FayaAdibusGetPackingListSet.CompanyId, ShipmentNo.Value.ToString(), "AD0000004", orderType);
                            orderTrace = _orderHandle.AddOrderTrace(orderTrace, orderModel, "AD0000004", orderType, "", "", DateTime.Now);

                            listorderTrace.Add(orderTrace);

                            #endregion
                            _logger.Information("GetPackingListService - Information:获取装箱单数据,处理成功:单号:" + ShipmentNo.Value.ToString());
                        }

                    }
                    else
                    {
                        _logger.Error("GetPackingListService - err:获取装箱单数据,处理失败:" + getPackingListBase.HEAD[0].StatusCode + ";" + getPackingListBase.HEAD[0].MSG + ", content:" + response);

                    }
                    _logger.Information("GetPackingListService - Information:获取装箱单数据,客户代码"+ customerCode + "处理完成");
                }
                else
                {
                    _logger.Error("GetPackingListService - err:获取装箱单数据,处理失败:" + getPackingListBase.HEAD[0].StatusCode + ";" + getPackingListBase.HEAD[0].MSG + ", content:" + response);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _logger.Error("GetPackingListService - err:获取装箱单数据,处理失败:"+customerCode + ex.ToString());
            }
        }

        public static int  AddDataMethod<T>(T nodel, string TabelName) where T : new()
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
            //if (TabelName == "HK_Faya_AdibusGetPackingList")
            //{
            //    count = info.Count() - 1;
            //}
            //else
            //{
            //    count = info.Count();
            //}
            count = info.Count();
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
                    if (item.Name.ToLower() == "etd" || item.Name.ToLower() == "eta") //DateTime
                    {
                        p[index++] = new SqlParameter("@" + item.Name, StampToDateTime(item.GetValue(nodel).ToString()));
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
            strsql += column + val;;
            return DBHelper.ExecuteNonTranQuery(strsql, p);

        }
        private static DateTime StampToDateTime(string time)
        {

            time = time.Substring(0, 10);
            double timestamp = Convert.ToInt64(time);
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp).ToLocalTime();
            return dateTime;

        }
        private static void InsertConnextLog( string RequestData, string ResponseData)
        {
           
             try
             {
                 string sql = @"INSERT INTO [dbo].[RequestAdibusLog]
                                      ([InterfaceType]
                                      ,[RequestData]
                                      ,[ResponseData]
                                      ,[InsertData]
                                      ,[ModifyDTM])
                                VALUES
                                      ('AD0000003' ,'" + RequestData + "'  ,'"+ ResponseData + "'  ,getdate() ,getdate())";
           
                 DBHelper.ExecuteNonQuery(sql);
             }
             catch { }
          
        }
        public void DelOrderBySignCode(SD_Adibus_Interface config,  string SignCode)
        {

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
            GetPackingListMethod(config, "");
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
            GetPackingListMethod(config, BillNo);
        }
        public void PostOrderBySignCode(SD_Adibus_Interface config, string CompanyId, string SignCode)
        {
            GetPackingListMethod(config, "");
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
