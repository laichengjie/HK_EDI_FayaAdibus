using HK_EDI.Core.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HK_EDI.Core.Data;

namespace HK_EDI.Service.Orders
{
   public  class PostSkuRecordsDetailService : IPostSkuRecordsDetailService
    {
       private readonly IRepository<PostSkuRecordsDetail> _repositoryPostSkuErrorRecords;
       public PostSkuRecordsDetailService(
           IRepository<PostSkuRecordsDetail> repositoryPostSkuErrorRecords
           )
       {
           _repositoryPostSkuErrorRecords = repositoryPostSkuErrorRecords;
       }

       public List<PostSkuRecordsDetail> GetJobOrderTraceDetailByPId(int pid)
       {
           return _repositoryPostSkuErrorRecords.TableNoTracking
               .Where(m => m.PId == pid)
               .OrderByDescending(m => m.CreateTime).ToList();
       }

       public void Add(PostSkuRecordsDetail Entity)
       {
           this._repositoryPostSkuErrorRecords.Insert(Entity);
       }
    }
}
