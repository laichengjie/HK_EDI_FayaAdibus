using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK_EDI.Core.Domain.Orders
{
    public class JobOrderTraceErrDetail : BaseEntity
    {

        public string CompanyID { get; set; }
        public string SignCode { get; set; }
        public string NoticeBillNo { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CustomerCode { get; set; }
        public string OSInterfaceID { get; set; }
        public string BillTypeID { get; set; }
        public int? OpResultStatus { get; set; }
        public int? OrderType { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ReadDate { get; set; }

        public DateTime? CancelDate { get; set; }
        public string ShopID { get; set; }
        public string StockID { get; set; }
        public DateTime? BillDate { get; set; }


        public DateTime? InsertedDate { get; set; }
        public string OrderTraceDetail { get; set; }

        public string RequestData { get; set; }

    }
}
