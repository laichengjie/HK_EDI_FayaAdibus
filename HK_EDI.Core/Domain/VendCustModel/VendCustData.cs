using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.VendCustModel
{
    public class VendCustData : BaseEntity
    {
        public string CompanyID { get; set; }
        public string VendCustID { get; set; }
        public string VendCustCode { get; set; }
        public string VendCustName { get; set; }
        public int StockType { get; set; }
    }
}
