using HK_EDI.Core.Domain.EDI_Config;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Data;
using HK_EDI.Service.Orders;
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

namespace HK_EDI.Service.FayaAdibus.Pur_ReceiptData
{
    public class ReceiptDataApi : IReceiptDataApi
    {
        private readonly IJobOrderTraceService _iJobOrderTraceService;
        private readonly IJobOrderTraceDetailService _iJobOrderTraceDetailService;
        private readonly IOrderHandle _orderHandle;
        private readonly IDbContext _dbContext;
        private readonly ILogger _logger;

        public ReceiptDataApi(ILogger logger, IJobOrderTraceDetailService iJobOrderTraceDetailService, IJobOrderTraceService iJobOrderTraceService,  IOrderHandle orderHandle, IDbContext dbContext)
        {
            this._logger = logger;
            this._iJobOrderTraceDetailService = iJobOrderTraceDetailService;
            this._iJobOrderTraceService = iJobOrderTraceService;
            this._orderHandle = orderHandle;
            this._dbContext = dbContext;
        }
        public void GetReceiptDataSync(string response, ref Response Rs)
        {
            try
            {
                //ReceiptDataBase ReceiptDataBaselist = new ReceiptDataBase();

                //ReceiptDataBaselist = JsonConvert.DeserializeObject<ReceiptDataBase>(response);

                //#region 保存数据
                ////DBHelper.CreateConnection().BeginTransaction();
                //DataTable dtDetail = DBHelper.ExecuteDataTable(@"  SELECT top 1 1  FROM    HK_Faya_NKReceiptMaster where packlist_number = '" + ReceiptDataBaselist.packlist_number + "' ");
                //if (dtDetail != null && dtDetail.Rows.Count == 0)
                //{
                //    ReceiptDataBaselist.RequestData = response;
                  
                //    #region 订单状态
                //    List<JobOrderTrace> listorderTrace = new List<JobOrderTrace>();
                //    int orderType = 0;
                //    JobOrderTrace orderTrace = new JobOrderTrace();

                //    var orderModel = new NormalOrderModel() { CompanyID = _FayaAdibusReceiptSet.CompanyId, BillNo = ReceiptDataBaselist.packlist_number, BillTypeID = "CreateReceipt" };
                //    orderType = _orderHandle.GetOrderType("CreateReceipt");
                //    orderTrace = _iJobOrderTraceService.GetJobOrderTrace(_FayaAdibusReceiptSet.CompanyId, ReceiptDataBaselist.packlist_number, "NK0000001", orderType);
                //    orderTrace = _orderHandle.AddOrderTrace(orderTrace, orderModel, "NK0000001", orderType, "", "", Convert.ToDateTime(ReceiptDataBaselist.send_datetime));

                //    listorderTrace.Add(orderTrace);

                //    #endregion

                //    foreach (ReceiptDataBaseDetailItem vDetail in ReceiptDataBaselist.detail)
                //    {
                //        AddDataMethod(vDetail, "HK_Faya_NKReceiptDetail");
                //    }
                //    AddDataMethod(ReceiptDataBaselist, "HK_Faya_NKReceiptMaster");
                //    //_logger.Error("ReceiptDataApi - err:获取采购到货单数据,处理成功:" + ReceiptDataBaselist.packlist_number);
                //}
                //else
                //{
                //    Rs.error_code = "901";
                //    Rs.error_message = "已存在采购到货单数据:" + ReceiptDataBaselist.packlist_number;
                //    _logger.Error("ReceiptDataApi - err:已存在采购到货单数据:" + ReceiptDataBaselist.packlist_number);
                //}
                
                //#endregion
            }
            catch (Exception ex)
            {
            
                //DBHelper.CreateConnection().BeginTransaction().Rollback();
                Rs.error_code = "900";
                Rs.error_message = "处理出错" + ex.Message;
                _logger.Error("ReceiptDataApi - err:获取采购到货单数据,处理失败:" + Rs.error_message + ", content:" + response);

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
            if (TabelName == "HK_Faya_NKReceiptMaster")
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
    }
    public class Response
    {
        [JsonProperty("error_code")]
        public string error_code { get; set; }
        [JsonProperty("error_message")]
        public string error_message { get; set; }
    }
}
