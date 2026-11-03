using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core;
using HK_EDI.Core.PagedExend;
using HK_EDI.Data;
using HK_EDI.Service.WMS.CommonModels;
using System.Data.SqlClient;

namespace HK_EDI.Service.Orders
{
    public class JobOrderTraceService : IJobOrderTraceService
    {
        public readonly IRepository<JobOrderTrace> _jobOrderTraceRepository;
        public readonly IRepository<JobOrderTraceErrDetail> _jobOrderErrDetailTrace;
        private readonly IDbContext _dbContext;
        public JobOrderTraceService(
            IRepository<JobOrderTrace> jobOrderTraceRepository,
             IRepository<JobOrderTraceErrDetail> jobOrderErrDetailTrace,
            IDbContext dbContext
            )
        {
            this._jobOrderTraceRepository = jobOrderTraceRepository;
            this._jobOrderErrDetailTrace = jobOrderErrDetailTrace;
            this._dbContext = dbContext;
        }

        public List<JobOrderTrace> GetJobOrderTraces(string NoticeBillNo)
        {
            NoticeBillNo = NoticeBillNo ?? "";
            return this._jobOrderTraceRepository.Table.Where(t => t.CompanyID + t.NoticeBillNo == NoticeBillNo).ToList();
        }

        public JobOrderTrace JobOrderTraceById(int Id)
        {
            return this._jobOrderTraceRepository.GetById(Id);
        }

        public JobOrderTrace GetJobOrderTrace(string SingCode)
        {
            return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.SignCode == SingCode);
        }

        public JobOrderTrace GetJobOrderTraceByNoticeBillNo(string noticeBillNo, int orderType)
        {
            return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.NoticeBillNo == noticeBillNo && t.OrderType == orderType);
        }

        //public JobOrderTrace GetJobOrderTraceTransBillNo(string TransBillNo)
        //{
        //    return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.TransBillNo == TransBillNo);
        //}

        public JobOrderTrace GetJobOrderTrace(string CompanyID, string NoticeBillNo, string OSInterfaceID, int? OrderType = null)
        {
            CompanyID = CompanyID ?? "";
            NoticeBillNo = NoticeBillNo ?? "";
            if (OrderType == null)
            {
                return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.CompanyID == CompanyID && t.NoticeBillNo == NoticeBillNo && t.OSInterfaceID == OSInterfaceID);
            }
            else
            {
                return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.CompanyID == CompanyID && t.NoticeBillNo == NoticeBillNo && t.OSInterfaceID == OSInterfaceID && t.OrderType == OrderType);
            }
        }
        /// <summary>
        /// 根据订单号获取数据
        /// </summary>
        /// <param name="CompanyID"></param>
        /// <param name="NoticeBillNo"> 默认为空</param>
        /// <param name="SignCode"></param>
        /// <param name="OSInterfaceID"></param>
        /// <param name="OrderType"></param>
        /// <returns></returns>
        public JobOrderTrace GetJobOrderTrace(string CompanyID, string NoticeBillNo, string SignCode, string OSInterfaceID, int? OrderType = null)
        {
            CompanyID = CompanyID ?? "";
            NoticeBillNo = NoticeBillNo ?? "";
            SignCode = SignCode ?? "";
            if (OrderType == null)
            {
                return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.CompanyID == CompanyID && t.SignCode == SignCode && t.OSInterfaceID == OSInterfaceID);
            }
            else
            {
                return this._jobOrderTraceRepository.Table.FirstOrDefault(t => t.CompanyID == CompanyID && t.SignCode == SignCode && t.OSInterfaceID == OSInterfaceID && t.OrderType == OrderType);
            }
        }

        public void AddJobOrderTrace(JobOrderTrace m)
        {
            this._jobOrderTraceRepository.Insert(m);
        }

        public void DelateOrderTrace(JobOrderTrace m)
        {
            this._jobOrderTraceRepository.Delete(m);
        }

        public void UpdateOrderTrace(JobOrderTrace m)
        {
            this._jobOrderTraceRepository.Update(m);
        }

        public Core.PagedExend.IPagedList<JobOrderTrace> GetAll(
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
            string signCode = null, int? orderType = null, int? status = null,

            int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _jobOrderTraceRepository.TableNoTracking;
            query = query.Where(m => m.OSInterfaceID == osstockId);
            if (createdFrom.HasValue)
                query = query.Where(m => m.CreateDate > createdFrom);
            if (createdTo.HasValue)
                query = query.Where(m => m.CreateDate < createdTo);
            if (ReadDateFrom.HasValue)
                query = query.Where(m => m.ReadDate > ReadDateFrom);
            if (ReadDateTo.HasValue)
                query = query.Where(m => m.ReadDate < ReadDateTo);
            if (billType.IsNotEmpty())
                query = query.Where(m => m.BillTypeID == billType);
            if (billNo.IsNotEmpty())
                query = query.Where(m => m.NoticeBillNo.Contains(billNo));
            if (CgbillNo.IsNotEmpty())
                query = query.Where(m => m.CgbillNo.Contains(CgbillNo));
            if (signCode.IsNotEmpty())
                query = query.Where(m => m.SignCode.Contains(signCode));
            if (companyID.IsNotEmpty())
                query = query.Where(m => m.CompanyID == companyID);
            if (status.HasValue)
                query = query.Where(m => m.OpResultStatus == status);
            if (orderType.HasValue)
                query = query.Where(m => m.OrderType == orderType);
            if (cancelDateFrom.HasValue)
            {
                query = query.Where(m => m.CancelDate > cancelDateFrom);
            }
            if (cancelDateTo.HasValue)
            {
                query = query.Where(m => m.CancelDate < cancelDateTo);
            }
            if (ShopID.IsNotEmpty()) //店铺ID
                query = query.Where(m => m.ShopID == ShopID);
            if (BillDate.HasValue) //销售日期
            {
                DateTime begin = Convert.ToDateTime(Convert.ToDateTime(BillDate).ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime end = Convert.ToDateTime(Convert.ToDateTime(BillDate).ToString("yyyy-MM-dd") + " 23:59:59");
                query = query.Where(m => m.BillDate >= begin);
                query = query.Where(m => m.BillDate <= end);
            }
            query = query.OrderByDescending(m => m.ReadDate);

            var list = new PagedList<JobOrderTrace>(query, pageIndex, pageSize);
            return list;
        }
        public Core.PagedExend.IPagedList<JobOrderTraceErrDetail> GetErrDetailAll(
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
        string signCode = null, int? orderType = null, int? status = null,
       
        int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _jobOrderErrDetailTrace.TableNoTracking;
            query = query.Where(m => m.OSInterfaceID == osstockId);
            if (createdFrom.HasValue)
                query = query.Where(m => m.CreateDate > createdFrom);
            if (createdTo.HasValue)
                query = query.Where(m => m.CreateDate < createdTo);
            if (ReadDateFrom.HasValue)
                query = query.Where(m => m.ReadDate > ReadDateFrom);
            if (ReadDateTo.HasValue)
                query = query.Where(m => m.ReadDate < ReadDateTo);
            if (billType.IsNotEmpty())
                query = query.Where(m => m.BillTypeID == billType);
            if (billNo.IsNotEmpty())
                query = query.Where(m => m.NoticeBillNo.Contains(billNo));
            if (signCode.IsNotEmpty())
                query = query.Where(m => m.SignCode.Contains(signCode));
            if (companyID.IsNotEmpty())
                query = query.Where(m => m.CompanyID == companyID);
            if (status.HasValue)
                query = query.Where(m => m.OpResultStatus == status);
            if (orderType.HasValue)
                query = query.Where(m => m.OrderType == orderType);
            if (cancelDateFrom.HasValue)
            {
                query = query.Where(m => m.CancelDate > cancelDateFrom);
            }
            if (cancelDateTo.HasValue)
            {
                query = query.Where(m => m.CancelDate < cancelDateTo);
            }
            if (ShopID.IsNotEmpty()) //店铺ID
                query = query.Where(m => m.ShopID == ShopID);
            if (BillDate.HasValue) //销售日期
            {
                DateTime begin = Convert.ToDateTime(Convert.ToDateTime(BillDate).ToString("yyyy-MM-dd") + " 00:00:00");
                DateTime end = Convert.ToDateTime(Convert.ToDateTime(BillDate).ToString("yyyy-MM-dd") + " 23:59:59");
                query = query.Where(m => m.BillDate >= begin);
                query = query.Where(m => m.BillDate <= end);
            }
            query = query.OrderByDescending(m => m.ReadDate);

            var list = new PagedList<JobOrderTraceErrDetail>(query, pageIndex, pageSize);
            return list;
        }

        public IList<CompleteOrderGetModel> GetCompleteOrderGetModel(string barCodes)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"  SELECT a.BarCode, a.MaterialID + IsNull(c.SizeName,'') SKUCode
                                             FROM SD_Mat_MaterialSize a
                                             LEFT JOIN dbo.SD_Mat_Size c ON c.SizeID = a.SizeID
                                             where a.BarCode in ('{0}')", barCodes);

            var companybarcodeMode = _dbContext.ExecuteStoreCommandList<HK_EDI.Service.WMS.CommonModels.CompleteOrderGetModel>(
                sql.ToString()
                );
            return companybarcodeMode;
        }
        public IList<StockQueryModel> GetStockQueryModel(string stockCode, string barCodes)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"  SELECT b.BarCode BarCode,a.ActualQty Qty FROM dbo.SD_Inv_MaterialDetailSize a
                                 INNER JOIN dbo.SD_Mat_MaterialSize b ON 
                                    b.MaterialID = a.MaterialID  AND  b.SizeID = a.SizeID
                                 INNER JOIN dbo.Bas_Stock c ON c.StockID = a.StockID
                                 where c.StockCode='{0}' and  b.BarCode in ('{1}')", stockCode, barCodes);

            var companybarcodeMode = _dbContext.ExecuteStoreCommandList<HK_EDI.Service.WMS.CommonModels.StockQueryModel>(
                sql.ToString()
                );
            return companybarcodeMode;
        }
        public IList<CompleteOrderGetModel> GetCompleteOrderGetModelBySKUCode(string skuCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendFormat(@"  SELECT a.BarCode, a.MaterialID + IsNull(c.SizeName,'') SKUCode
                                             FROM SD_Mat_MaterialSize a
                                             LEFT JOIN dbo.SD_Mat_Size c ON c.SizeID = a.SizeID
                                             where a.MaterialID + IsNull(c.SizeName,'') in ('{0}')", skuCode);

            var companybarcodeMode = _dbContext.ExecuteStoreCommandList<HK_EDI.Service.WMS.CommonModels.CompleteOrderGetModel>(
                sql.ToString()
                );
            return companybarcodeMode;
        }
        public List<productModel> GetOrderDetails(JobOrderTrace jobtrace, List<productModel> productmodel, int opType)
        {
            if (productmodel.Count <= 0)
            {
                var orderDetail = this._dbContext.ExecuteStoredProcedureListN<NormalOrderDetailModel>
                            ("sp_HK_EDI_GetOrderDetailDataPro",
                            new SqlParameter("BillNo", jobtrace.NoticeBillNo),
                            new SqlParameter("CompanyID", jobtrace.CompanyID),
                            new SqlParameter("billTypeId", jobtrace.BillTypeID),
                            new SqlParameter("OutInFlag", opType)
                            );
                orderDetail.ToList().ForEach(t =>
                productmodel.Add(new productModel()
                {
                    SKUCode = t.BarCode,
                    Qty = Math.Abs(t.Qty.ToInt()).ToString()
                })
                );
            }
            return productmodel;
        }

        /// <summary>
        /// 获得符合自动撤销条件的单据
        /// </summary>
        public IList<JobOrderTrace> GetJobOrderTraceByValidAutoCancel()
        {
            var entityList = _dbContext.ExecuteStoredProcedureList<JobOrderTrace>("sp_HK_EDI_GetAutoCancelOrder", null);
            return entityList;
        }


        /// <summary>
        /// 自动取消订单 最后一步
        /// </summary>
        public void CompleteAutoCancel(string companyID, string userID, string billNo, string msg, int billType)
        {
            SqlParameter[] pars = new SqlParameter[5]
            {
                new SqlParameter("CompanyID", companyID),
                new SqlParameter("UserID ", userID),
                new SqlParameter("BillNo", billNo),
                new SqlParameter("msg", msg),
                new SqlParameter("Billtype", billType)
            };
            _dbContext.ExecuteStoredProcedure("spBC_WriteOrderLog", pars);
        }
    }
}
