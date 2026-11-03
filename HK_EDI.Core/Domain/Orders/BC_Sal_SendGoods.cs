using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class BC_Sal_SendGoods : BaseEntity
    {

        public string CompanyID { get; set; }
        public string BillNo { get; set; }
        public string SkuCode { get; set; }
        public string LotNumber { get; set; }
        public string UniqueID { get; set; }
        public int? ItemCount { get; set; }
        public string Remark { get; set; }
        public DateTime? AddDTM { get; set; }
        public string MailNo { get; set; }
        public string MainWayBillNo { get; set; }
        public string LogisticProviderID { get; set; }
        public string WareHouseCode { get; set; }
        public string OwnerCode { get; set; }
        public string OrderType { get; set; }
        public string OrderCode { get; set; }

    }
}
