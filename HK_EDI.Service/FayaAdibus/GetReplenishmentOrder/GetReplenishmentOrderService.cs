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
    /// 获取FRS补货单（MW FRS Replenishment order）
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
                    string servicecode = _FayaAdibusReplenishmentOrderSet.SERVICECODE;

                    if (!string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(consumer_syscode))
                    {
                        // 接口只返回某一天生成的补货单，这里按配置的日期范围逐天获取
                        if (_FayaAdibusReplenishmentOrderSet.GetDate >= 0)//获取几天数据
                        {
                            for (int i = _FayaAdibusReplenishmentOrderSet.GetDate; i >= 0; i--)
                            {
                                string DocDate = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");
                                GetReplenishmentOrder(DocDate, password, consumer_syscode, servicecode);
                            }
                        }
                        else
                        {
                            string DocDate = _FayaAdibusReplenishmentOrderSet.BeginDate.ToString("yyyy-MM-dd 00:00:00").Replace("/", "-");
                            GetReplenishmentOrder(DocDate, password, consumer_syscode, servicecode);
                        }
                    }
                    else
                    {
                        _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,处理失败:WebConfing配置数据为空");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// 按指定日期（DocDate）获取补货单，内部按 CurrentPage/TotalPage 分页循环获取
        /// </summary>
        public void GetReplenishmentOrder(string docDate, string password, string consumer, string servicecode)
        {
            try
            {
                MD5Set mD5Set = new MD5Set();

                int currentPage = 1;
                int totalPage = 1;

                do
                {
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
                    postHEAD.DocType = "REPL";
                    postHEAD.DeparturePOSCode = _FayaAdibusReplenishmentOrderSet.DeparturePOSCode ?? "";
                    postHEAD.DestinationPOSCode = _FayaAdibusReplenishmentOrderSet.DestinationPOSCode ?? "";
                    postHEAD.DocDate = docDate;
                    postHEAD.GetDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("/", "-");
                    postHEAD.CurrentPage = currentPage.ToString();
                    postDATA.HEAD = postHEAD;

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

                    if (getReplenishmentOrderBase != null && getReplenishmentOrderBase.HEAD != null && getReplenishmentOrderBase.HEAD.Count > 0)
                    {
                        if (getReplenishmentOrderBase.HEAD[0].StatusCode == "200")
                        {
                            List<GetReplenishmentOrderBaseBODY> getlistBODY = getReplenishmentOrderBase.BODY ?? new List<GetReplenishmentOrderBaseBODY>();

                            // 依据返回内容更新总页码（未返回则按1处理）
                            if (getlistBODY.Count > 0)
                            {
                                int tmpTotal = 0;
                                if (int.TryParse(getlistBODY[0].TotalPage, out tmpTotal) && tmpTotal > 0)
                                {
                                    totalPage = tmpTotal;
                                }
                            }

                            // 按单据号去重
                            var list_distinct = getlistBODY.GroupBy(c => c.DocNo).Select(c => c.First()).ToList();
                            foreach (var docNo in list_distinct)
                            {
                                if (string.IsNullOrEmpty(docNo.DocNo))
                                    continue;

                                DataTable dtDetail = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_AdibusGetReplenishmentOrder where DocNo = '" + docNo.DocNo + "' ");
                                if (dtDetail != null && dtDetail.Rows.Count > 0)
                                {
                                    continue;
                                }
                                List<GetReplenishmentOrderBaseBODY> vReplenishmentOrder = getlistBODY.Where(p => p.DocNo == docNo.DocNo).ToList();
                                string _pnno = "";
                                foreach (GetReplenishmentOrderBaseBODY vReplenishment in vReplenishmentOrder)
                                {
                                    AddDataMethod(vReplenishment, "HK_Faya_AdibusGetReplenishmentOrder");
                                    _pnno += vReplenishment.Article + ",";
                                }

                                #region 订单状态
                                List<JobOrderTrace> listorderTrace = new List<JobOrderTrace>();
                                int orderType = 0;
                                JobOrderTrace orderTrace = new JobOrderTrace();

                                var orderModel = new NormalOrderModel() { CompanyID = _FayaAdibusReplenishmentOrderSet.CompanyId, BillNo = docNo.DocNo, BillTypeID = "GETREPLENISHMENTORDER", SourcebillNo = _pnno, CustomerCode = vReplenishmentOrder[0].Customer };
                                orderType = _orderHandle.GetOrderType("GETREPLENISHMENTORDER");
                                orderTrace = _iJobOrderTraceService.GetJobOrderTrace(_FayaAdibusReplenishmentOrderSet.CompanyId, docNo.DocNo, "AD0000006", orderType);
                                orderTrace = _orderHandle.AddOrderTrace(orderTrace, orderModel, "AD0000006", orderType, "", "", DateTime.Now);

                                listorderTrace.Add(orderTrace);
                                #endregion
                                _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,处理成功:单号:" + docNo.DocNo);
                            }

                            if (currentPage >= totalPage)
                            {
                                break;
                            }
                        }
                        else
                        {
                            _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:" + getReplenishmentOrderBase.HEAD[0].StatusCode + ";" + getReplenishmentOrderBase.HEAD[0].StatusDesc + ", content:" + response);
                            break;
                        }
                    }
                    else
                    {
                        _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:返回内容为空, content:" + response);
                        break;
                    }

                    currentPage++;
                } while (true);

                _logger.Information("GetReplenishmentOrderService - Information:获取补货单数据,日期" + docDate + "处理完成");
            }
            catch (Exception ex)
            {
                _logger.Error("GetReplenishmentOrderService - err:获取补货单数据,处理失败:" + docDate + ex.ToString());
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
            //获取Type对象所有公共属性
            PropertyInfo[] info = type.GetProperties();
            List<SqlParameter> p = new List<SqlParameter>();
            foreach (PropertyInfo item in info)
            {
                string name = item.Name.ToLower();
                // 明细导航属性与分页字段不入库
                if (name == "detail" || name == "currentpage" || name == "totalpage")
                {
                    continue;
                }

                column += link + item.Name;//定义需要添加的字段
                val += link + " @" + item.Name;//定义字段变量
                //给字段变量赋值
                if ((item.GetValue(nodel) != null ? item.GetValue(nodel).ToString() : "") == "0000-00-00 00:00:00")
                {
                    p.Add(new SqlParameter("@" + item.Name, null));
                }
                else
                {
                    p.Add(new SqlParameter("@" + item.Name, item.GetValue(nodel) != null ? item.GetValue(nodel) : DBNull.Value));
                }

                link = ",";
            }
            column += ")";
            val += ")";
            strsql += column + val; ;
            return DBHelper.ExecuteNonTranQuery(strsql, p.ToArray());
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
