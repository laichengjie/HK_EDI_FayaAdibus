using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
   public  class PostSkuRecordsDetail : BaseEntity
    {
       public int  PId { get; set; }
       public string Contents { get; set; }
       public DateTime CreateTime { get; set; }
       public string ContentNode { get; set; }
    }
}
