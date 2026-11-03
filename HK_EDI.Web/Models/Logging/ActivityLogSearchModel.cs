using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using HK_EDI.Web.Mvc;

namespace Nop.Admin.Models.Logging
{
    public partial class ActivityLogSearchModel : BaseNopModel
    {
        public ActivityLogSearchModel()
        {
            ActivityLogType = new List<SelectListItem>();
        }
        
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        
        public int ActivityLogTypeId { get; set; }

        
        public IList<SelectListItem> ActivityLogType { get; set; }
    }
}