using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core.Data;

namespace HK_EDI.Service.Orders
{
    public class JobOrderTraceDetailService : IJobOrderTraceDetailService
    {
        public readonly IRepository<JobOrderTraceDetail> _jobOrderTraceDetailRepository;

        public JobOrderTraceDetailService(
            IRepository<JobOrderTraceDetail> jobOrderTraceDetailRepository
            )
        {
            this._jobOrderTraceDetailRepository = jobOrderTraceDetailRepository;
        }

        public JobOrderTraceDetail GetJobOrderTraceDetail(int Id)
        {
            return this._jobOrderTraceDetailRepository.Table.FirstOrDefault(t => t.Id == Id);
        }

        public void AddJobOrderTraceDetail(JobOrderTraceDetail m)
        {
            this._jobOrderTraceDetailRepository.Insert(m);
        }

        public void UpdateJobOrderTraceDetail(JobOrderTraceDetail m)
        {
            this._jobOrderTraceDetailRepository.Update(m);
        }

        public void DeleteJobOrderTraceDetail(JobOrderTraceDetail m)
        {
            this._jobOrderTraceDetailRepository.Delete(m);
        }


        public IList<JobOrderTraceDetail> GetJobOrderTraceDetail(string companyId, string billNo)
        {
            return _jobOrderTraceDetailRepository.TableNoTracking
                .Where(m => m.CompanyID == companyId && m.BillNo == billNo)
                .OrderByDescending(m => m.CreateTime).ToList();
        }
        public IList<JobOrderTraceDetail> GetJobOrderTraceDetailBySignCode(string companyId, string SignCode)
        {
            return _jobOrderTraceDetailRepository.TableNoTracking
                .Where(m => m.CompanyID == companyId && m.SignCode == SignCode)
                .OrderByDescending(m => m.CreateTime).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="IsEffective">是否有效（用于取回执）</param>
        /// <returns></returns>
        public IList<JobOrderTraceDetail> GetJobOrderTraceDetailByPId(int pid, string  IsEffective=null )
        {
            if (IsEffective == null)
            {
                return _jobOrderTraceDetailRepository.TableNoTracking
                    .Where(m => m.PId == pid)
                    .OrderByDescending(m => m.CreateTime).ToList();
            }
            else
            {
                return _jobOrderTraceDetailRepository.TableNoTracking
                    .Where(m => m.PId == pid && m.IsEffective==true )
                    .OrderByDescending(m => m.CreateTime).ToList();
            }
        }

        /// <summary>
        /// 取的发送的出库单对象明细
        /// </summary>
        /// <param name="jobmodel"></param>
        /// <param name="contentType">接口类型(科捷,中邮)</param>
        /// <returns></returns>
        public JobOrderTraceDetail GetJobOrderTraceDetailOutOfContent(JobOrderTrace jobmodel,int contentType)
        {
            return _jobOrderTraceDetailRepository.TableNoTracking
                .Where(t => t.PId == jobmodel.Id 
                && t.IsEffective == false 
                && t.ContentType == contentType 
              //  && t.Description == "发送出库请求成功."
                )
                .OrderByDescending(m=>m.CreateTime).FirstOrDefault();
        }

    }
}
