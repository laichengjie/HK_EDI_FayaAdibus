using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.OrderTrace
{
    public class OrderTraceErrDetailModel : BaseNopModel
    {
        public long Id { get; set; }

        public string CompanyId { get; set; }
        public string CustomerCode { get; set; }
        
        public string NoticeBillNo { get; set; }

        public string SignCode { get; set; }

        public string OSInterfaceID { get; set; }

        public string BillTypeID { get; set; }

        public string OrderTypeName { get; set; }

        public string OpResultStatus { get; set; }

        public bool IsCancel { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ReadDate { get; set; }

        public DateTime? CancelDate { get; set; }

        public string ShopID { get; set; }
        public DateTime? BillDate { get; set; }
        public string OrderTraceDetail { get; set; }
        public string RequestData { get; set; }
    }
}