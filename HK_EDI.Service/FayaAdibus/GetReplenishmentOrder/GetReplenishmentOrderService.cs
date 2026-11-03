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
using System.Web.Mvc;

namespace HK_EDI.Service.FayaAdibus.GetReplenishmentOrder
{
    /// <summary>
    /// 获取补货单
    /// </summary>
    public class GetReplenishmentOrderService : lockExtend, IGetReplenishmentOrderService, IWMSService
    {
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly fayaadibusReplenishmentOrderSet _FayaAdibusReplenishmentOrderSet;
        private readonly IOrderHandle _orderHandle;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public GetReplenishmentOrderService(ILogger logger, IJobOrderTraceDetailService iJobOrderTraceDetailService, IJobOrderTraceService iJobOrderTraceService, fayaadibusReplenishmentOrderSet fayaadibusReplenishmentOrderSet, IOrderHandle orderHandle, IDbContext dbContext)
        {
            this._logger = logger;
            this._iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            this._iJobOrderTraceService = iJobOrderTraceService;
            this._FayaAdibusReplenishmentOrderSet = fayaadibusReplenishmentOrderSet;
            this._orderHandle = orderHandle;
            this._dbContext = dbContext;
        }

        public void GetReplenishmentOrderMethod(SD_Adibus_Interface config, string BillNo)
        {
            lock (GetPostSkuLock(config, "GetReplenishmentOrder"))
            {
                try
                {
                    string password = System.Configuration.ConfigurationManager.AppSettings["password"];
                    string consumer_syscode = System.Configuration.ConfigurationManager.AppSettings["consumer_syscode"];

                    string[] customerCodelist = (_FayaAdibusReplenishmentOrderSet.SetcustomerCode ?? "").Split(",");
                    string servicecode = _FayaAdibusReplenishmentOrderSet.SERVICECODE;

                    if (customerCodelist != null && customerCodelist.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(consumer_syscode))
                        {
                            string BeginDataTime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");//计算开始时间

                            foreach (string customerCode in customerCodelist)
                            {
                                try
                                {
                                    _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,客户数量:" + customerCodelist.Length + ",当前客户:" + customerCode + "");

                                    if (string.IsNullOrEmpty(customerCode))
                                        continue;
                                    if (_FayaAdibusReplenishmentOrderSet.GetDate >= 0)//获取几天数据
                                    {
                                        for (int i = _FayaAdibusReplenishmentOrderSet.GetDate; i >= 0; i--)
                                        {
                                            BeginDataTime = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");//计算开始时间
                                            GetReplenishmentOrder(BeginDataTime, password, customerCode, consumer_syscode, servicecode);
                                        }
                                    }
                                    else
                                    {
                                        BeginDataTime = _FayaAdibusReplenishmentOrderSet.BeginDate.ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");
                                        GetReplenishmentOrder(BeginDataTime, password, customerCode, consumer_syscode, servicecode);
                                    }
                                }
                                catch
                                {
                                    _logger.Error("GetReplenishmentOrderService - Error:获取补货单数据, 获取数据异常,当前客户:" + customerCode + "");
                                }
                            }
                        }
                        else
                        {
                            _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,处理失败:WebConfing配置数据为空");
                        }
                    }
                    else
                    {
                        _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,请先配置客户代码");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:" + ex.ToString());
                }
            }
        }

        public void GetReplenishmentOrder(string getDateTime, string password, string customerCode, string consumer, string servicecode)
        {
            try
            {
                MD5Set mD5Set = new MD5Set();

                string[] postParams = new string[3];
                PostHeader postHeader = new PostHeader();
                PostDATA postDATA = new PostDATA();
                PostHEAD postHEAD = new PostHEAD();
                postHeader.SERVICE_CODE = servicecode;
                postHeader.CONSUMER_SYSCODE = consumer;
                postHeader.VERSION = "1";
                postHeader.MESSAGE_ID = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                postHeader.DTSEND = DateTime.Now.ToString("yyyyMMddHHmmss");
                //请求数据
                postHEAD.KAName = _FayaAdibusReplenishmentOrderSet.CompanyId;
                postHEAD.GetDateTime = getDateTime;
                postHEAD.CustomerCode = customerCode;
                postDATA.HEAD = postHEAD;
                #region 保存数据
                string postHeaderJson = JsonConvert.SerializeObject(postHeader);
                string postDATAJson = JsonConvert.SerializeObject(postDATA);
                postParams[0] = postHeaderJson;
                postParams[1] = mD5Set.MD5Encrypt(password + postHeaderJson + password);
                postParams[2] = postDATAJson;
                HK_EDI.Service.Common.WebClient wc = new HK_EDI.Service.Common.WebClient();
                string Header = "HEADER=" + postParams[0] + "&PASSWORD=" + postParams[1] + "&DATA=" + postParams[2];
                InsertConnextLog(Header, "");
                var response = wc.PostWebRequest(_FayaAdibusReplenishmentOrderSet.GetReplenishmentOrderUrl, Header);
                InsertConnextLog(Header, response);

                GetReplenishmentOrderBase getReplenishmentOrderBase = JsonConvert.DeserializeObject<GetReplenishmentOrderBase>(response);

                if (getReplenishmentOrderBase != null)
                {
                    if (getReplenishmentOrderBase.HEAD[0].StatusCode == "200")
                    {
                        List<GetReplenishmentOrderBaseBODY> getlistBODY = getReplenishmentOrderBase.BODY;
                        var list_distinct = getlistBODY.GroupBy(c => c.ReplenishmentNo).Select(c => c.First());//去重
                        var ReplenishmentNoList = new SelectList(list_distinct, "ReplenishmentNo", null);
                        IList<SelectListItem> DisReplenishmentNoLists = ReplenishmentNoList.ToList();
                        foreach (var ReplenishmentNo in DisReplenishmentNoLists)
                        {
                            DataTable dtDetail = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_AdibusGetReplenishmentOrder where ReplenishmentNo = '" + ReplenishmentNo.Value.ToString() + "' ");
                            if (dtDetail != null && dtDetail.Rows.Count > 0)
                            {
                                continue;
                            }
                            List<GetReplenishmentOrderBaseBODY> vReplenishmentOrder = getlistBODY.Where(p => p.ReplenishmentNo == ReplenishmentNo.Value.ToString()).ToList();
                            string _pnno = "";
                            foreach (GetReplenishmentOrderBaseBODY vReplenishment in vReplenishmentOrder)
                            {
                                AddDataMethod(vReplenishment, "HK_Faya_AdibusGetReplenishmentOrder");
                                _pnno += vReplenishment.PNNo + ",";
                            }

                            #region 订单状态
                            List<JobOrderTrace> listorderTrace = new List<JobOrderTrace>();
                            int orderType = 0;
                            JobOrderTrace orderTrace = new JobOrderTrace();

                            var orderModel = new NormalOrderModel() { CompanyID = _FayaAdibusReplenishmentOrderSet.CompanyId, BillNo = ReplenishmentNo.Value.ToString(), BillTypeID = "CREATERECEIPT", SourcebillNo = _pnno, CustomerCode = vReplenishmentOrder[0].ShipTo };
                            orderType = _orderHandle.GetOrderType("CREATERECEIPT");
                            orderTrace = _iJobOrderTraceService.GetJobOrderTrace(_FayaAdibusReplenishmentOrderSet.CompanyId, ReplenishmentNo.Value.ToString(), "AD0000006", orderType);
                            orderTrace = _orderHandle.AddOrderTrace(orderTrace, orderModel, "AD0000006", orderType, "", "", DateTime.Now);

                            listorderTrace.Add(orderTrace);
                            #endregion
                            _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,处理成功:单号:" + ReplenishmentNo.Value.ToString());
                        }
                    }
                    else
                    {
                        _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:" + getReplenishmentOrderBase.HEAD[0].StatusCode + ";" + getReplenishmentOrderBase.HEAD[0].MSG + ", content:" + response);
                    }
                    _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,客户代码" + customerCode + "处理完成");
                }
                else
                {
                    _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:返回内容为空, content:" + response);
                }
                #endregion
            }
            catch (Exception ex)
            {
                _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:" + customerCode + ex.ToString());
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
            int count = info.Count();
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
            strsql += column + val; ;
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

        private static void InsertConnextLog(string RequestData, string ResponseData)
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
                                     ('AD0000005' ,'" + RequestData + "'  ,'" + ResponseData + "'  ,getdate() ,getdate())";

                DBHelper.ExecuteNonQuery(sql);
            }
            catch { }
        }

        public void DelOrderBySignCode(SD_Adibus_Interface config, string SignCode)
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
            GetReplenishmentOrderMethod(config, "");
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
            GetReplenishmentOrderMethod(config, BillNo);
        }
        public void PostOrderBySignCode(SD_Adibus_Interface config, string CompanyId, string SignCode)
        {
            GetReplenishmentOrderMethod(config, "");
        }
        public string PostSummaryBillNo(SD_Adibus_Interface config, string CompanyId, string ShopID, string DateTime)
        {
            throw new NotImplementedException();
        }
    }
}
