using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.PagedExend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public interface IPostSkuErrorRecordsService
    {
        List<PostSkuErrorRecords> GetEntityList(string SkuCode, int InterfaceType, string OSInterfaceID);
        void Updata(PostSkuErrorRecords Entity);
        void Add(PostSkuErrorRecords Entity);
        void Delete(string  SkuCode, int InterfaceType, string OSInterfaceID,bool IsSucess=false );
        IPagedList<PostSkuErrorRecords> GetAll(
           bool IsSuccess,
           DateTime? createdFrom = null,
           DateTime? createdTo = null,
           string osstockId = null,
           string SKUCode = null,         
           int pageIndex = 0, int pageSize = 2147483647);
    }
}
