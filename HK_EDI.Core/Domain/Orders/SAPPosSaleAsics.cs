using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class AdibusPosSaleAsics : BaseEntity
    {
        //public int PId { get; set; }
        public string CompanyID { get; set; }
        public string ShopID { get; set; }
        public string SignCode { get; set; }
        public string BillNo { get; set; }
        public int? OpResultStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        
        
    }
}
