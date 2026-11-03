using HK_EDI.Web.Mvc;
using System;


namespace HK_EDI.Web.Models.Logging
{
    public partial class ActivityLogModel : BaseNopEntityModel
    {
        
        public string ActivityLogTypeName { get; set; }
        
        public int CustomerId { get; set; }
        
        public string CustomerEmail { get; set; }
        
        public string Comment { get; set; }
        
        public DateTime CreatedOn { get; set; }
    }
}
