using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS.SKU360.Models
{
   public class SearchResponse
    {
        public string count { get; set; }

        List<stocks> stocks = new List<stocks>();
    }
    class stocks
    {
        public string product_code { get; set; }
        public string able_num { get; set; }
        public string unable_num { get; set; }
    }
}
