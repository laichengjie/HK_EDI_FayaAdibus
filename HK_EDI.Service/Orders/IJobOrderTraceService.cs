using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.PagedExend;
using HK_EDI.Service.WMS.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public interface IJobOrderTraceService
    {
        IPagedList<JobOrderTrace> GetAll(
            DateTime? createdFrom = null,
            DateTime? createdTo = null,
            DateTime? ReadDateFrom = null,
            DateTime? ReadDateTo = null,
            DateTime? cancelDateFrom = null,
            DateTime? cancelDateTo = null,
            DateTime? BillDate = null,
            string ShopID = null,
            string osstockId = null,
            string billType = null, string companyID = null, string billNo = null, string CgbillNo = null,
            string signCode = null, int? orderType = null,
            int? status = null, int pageIndex = 0, int pageSize = 2147483647);
        IPagedList<JobOrderTraceErrDetail> GetErrDetailAll(
        DateTime? createdFrom = null,
        DateTime? createdTo = null,
        DateTime? ReadDateFrom = null,
        DateTime? ReadDateTo = null,
        DateTime? cancelDateFrom = null,
        DateTime? cancelDateTo = null,
        DateTime? BillDate = null,
        string ShopID = null,
        string osstockId = null,
        string billType = null, string companyID = null, string billNo = null,
        string signCode = null, int? orderType = null,
        int? status = null, int pageIndex = 0, int pageSize = 2147483647);

        List<JobOrderTrace> GetJobOrderTraces(string NoticeBillNo);

        JobOrderTrace JobOrderTraceById(int Id);

        JobOrderTrace GetJobOrderTrace(string SingCode);

        JobOrderTrace GetJobOrderTraceByNoticeBillNo(string noticeBillNo, int orderType);

        //JobOrderTrace GetJobOrderTraceTransBillNo(string TransBillNo);

        JobOrderTrace GetJobOrderTrace(string CompanyID, string NoticeBillNo, string OSInterfaceID, int? OrderType = null);
        JobOrderTrace GetJobOrderTrace(string CompanyID, string NoticeBillNo,string SignCode, string OSInterfaceID, int? OrderType = null);
        void AddJobOrderTrace(JobOrderTrace m);

        void DelateOrderTrace(JobOrderTrace m);

        void UpdateOrderTrace(JobOrderTrace m);

        IList<CompleteOrderGetModel> GetCompleteOrderGetModel(string barCodes);
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <param name="stockCode"></param>
        /// <param name="barCodes"></param>
        /// <returns></returns>
        IList<StockQueryModel> GetStockQueryModel(string stockCode, string barCodes);

        IList<CompleteOrderGetModel> GetCompleteOrderGetModelBySKUCode(string skuCode);
        List<productModel> GetOrderDetails(JobOrderTrace jobtrace, List<productModel> productmodel, int opType);

        /// <summary>
        /// 获得符合自动撤销条件的单据
        /// </summary>
        IList<JobOrderTrace> GetJobOrderTraceByValidAutoCancel();

        /// <summary>
        /// 自动取消订单 最后一步
        /// </summary>
        /// <param name="companyID">公司id</param>
        /// <param name="userID">erp用户sys_user</param>
        /// <param name="billNo">单号</param>
        /// <param name="msg">日志内容</param>
        /// <param name="billType">0网店零售订单，1是拣货出库单,2是交易号</param>
        void CompleteAutoCancel(string companyID, string userID, string billNo, string msg, int billType);
    }
}
