using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class PostSkuErrorRecords : BaseEntity
    {
        public string SKUCode { get; set; }
        public int InterfaceType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LastQueryTime { get; set; }
        public bool IsSuccess { get; set; }
        public int Qty { get; set; }
        public string OSInterfaceID { get; set; }
        
    }
}
