using System;
using System.Web.Mvc;

using HK_EDI.Web.Mvc;

namespace Nop.Admin.Models.Logging
{
    public partial class LogModel : BaseNopEntityModel
    {
        
        public string LogLevel { get; set; }

        
        [AllowHtml]
        public string ShortMessage { get; set; }

        
        [AllowHtml]
        public string FullMessage { get; set; }

        
        [AllowHtml]
        public string IpAddress { get; set; }

        
        public int? CustomerId { get; set; }
        
        public string CustomerEmail { get; set; }

        
        [AllowHtml]
        public string PageUrl { get; set; }

        
        [AllowHtml]
        public string ReferrerUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}