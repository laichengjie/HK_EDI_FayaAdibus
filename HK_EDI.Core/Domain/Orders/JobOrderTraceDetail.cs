using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class JobOrderTraceDetail : BaseEntity
    {

        public int PId { get; set; }

        public string CompanyID { get; set; }

        public string BillNo { get; set; }

        public string SignCode { get; set; }

        public string ContentNode { get; set; }

        public string ContentNodeRequest { get; set; }

        public int ContentType { get; set; }

        public bool IsEffective { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? ModifyTime { get; set; }

        public string Description { get; set; }

    }
}
