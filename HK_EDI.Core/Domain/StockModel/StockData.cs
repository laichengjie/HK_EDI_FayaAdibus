using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.StockModel
{
    public class StockData : BaseEntity
    {
        public string CompanyID { get; set; }
        public string StockID { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public int StockType { get; set; }
    }
}
