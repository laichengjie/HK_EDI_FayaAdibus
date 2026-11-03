using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.PagedExend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.Orders
{
    public class PostSkuErrorRecordsService : IPostSkuErrorRecordsService
    {
        public readonly IRepository<PostSkuErrorRecords> _irepositoryPostSkuErrorRecords;
        public PostSkuErrorRecordsService(
            IRepository<PostSkuErrorRecords> irepositoryPostSkuErrorRecords
            )
        {
            this._irepositoryPostSkuErrorRecords = irepositoryPostSkuErrorRecords;
        }

        public List<PostSkuErrorRecords> GetEntityList(string SkuCode, int InterfaceType, string OSInterfaceID)
        {
           return this._irepositoryPostSkuErrorRecords.Table.Where(t => t.SKUCode == SkuCode && t.OSInterfaceID == OSInterfaceID && t.InterfaceType == InterfaceType ).ToList();
        }

        public void Updata(PostSkuErrorRecords Entity)
        {
            this._irepositoryPostSkuErrorRecords.Update(Entity);
        }

        public void Add(PostSkuErrorRecords Entity)
        {
            this._irepositoryPostSkuErrorRecords.Insert(Entity);
        }
        public void Delete(string SkuCode, int InterfaceType, string OSInterfaceID,bool IsSucess=false )
        {
            var EntityList = this._irepositoryPostSkuErrorRecords.Table.Where(t => t.SKUCode.ToUpper() == SkuCode.ToUpper() && t.InterfaceType == InterfaceType && t.OSInterfaceID == (OSInterfaceID ?? "") && t.IsSuccess == IsSucess).ToList();
            this._irepositoryPostSkuErrorRecords.Delete(EntityList);
        }

        public IPagedList<PostSkuErrorRecords> GetAll(
           bool IsSuccess ,
           DateTime? createdFrom = null,
           DateTime? createdTo = null,
           string osstockId = null,
           string SKUCode = null,          
           int pageIndex = 0, int pageSize = 2147483647)
        {
            var query = _irepositoryPostSkuErrorRecords.TableNoTracking;
            query = query.Where(m => m.OSInterfaceID == osstockId);
            query = query.Where(m => m.IsSuccess == IsSuccess);
            if (createdFrom.HasValue)
                query = query.Where(m => m.CreateTime > createdFrom);
            if (createdTo.HasValue)
                query = query.Where(m => m.CreateTime < createdTo);

            if (!string.IsNullOrEmpty(SKUCode))
            {
                query = query.Where(m => m.SKUCode.Contains(SKUCode.Trim()));
            }

            query = query.OrderByDescending(m => m.CreateTime);

            var list = new PagedList<PostSkuErrorRecords>(query, pageIndex, pageSize);
            return list;
        }
    }
}
