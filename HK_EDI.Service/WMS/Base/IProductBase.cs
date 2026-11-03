using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Service.WMS
{
   public interface IProductBase
    {
          string SkuCode { get; set; }
          string Qty { get; set; }

    }
}
