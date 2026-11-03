using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public interface IJobOrderTraceDetailService
    {
        JobOrderTraceDetail GetJobOrderTraceDetail(int Id);

        IList<JobOrderTraceDetail> GetJobOrderTraceDetail(string companyId, string billNo);
        IList<JobOrderTraceDetail> GetJobOrderTraceDetailBySignCode(string companyId, string SignCode);
        IList<JobOrderTraceDetail> GetJobOrderTraceDetailByPId(int pid, string IsEffective = null);

        JobOrderTraceDetail GetJobOrderTraceDetailOutOfContent(JobOrderTrace jobmodel, int contentType);

        void AddJobOrderTraceDetail(JobOrderTraceDetail m);

        void UpdateJobOrderTraceDetail(JobOrderTraceDetail m);

        void DeleteJobOrderTraceDetail(JobOrderTraceDetail m);
    }
}
