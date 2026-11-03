using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HK_EDI.Web.Models.OrderTrace
{
    public class OrderTraceDetailModel : BaseNopModel
    {
        public string Id { get; set; }

        public string SignCode { get; set; }

        public string ContentNote { get; set; }

        public string ContentNoteRequest { get; set; }

        public string ContentType { get; set; }

        public string Description { get; set; }

        public DateTime? CreateTime { get; set; }

        public DateTime? ModifyTime { get; set; }


    }
}