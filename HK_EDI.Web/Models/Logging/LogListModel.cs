using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using HK_EDI.Web.Mvc;

namespace Nop.Admin.Models.Logging
{
    public partial class LogListModel : BaseNopModel
    {
        public LogListModel()
        {
            AvailableLogLevels = new List<SelectListItem>();
        }

        
        [UIHint("DateNullable")]
        public DateTime? CreatedOnFrom { get; set; }

        
        [UIHint("DateNullable")]
        public DateTime? CreatedOnTo { get; set; }

        
        [AllowHtml]
        public string Message { get; set; }

        
        public int LogLevelId { get; set; }


        public IList<SelectListItem> AvailableLogLevels { get; set; }
    }
}