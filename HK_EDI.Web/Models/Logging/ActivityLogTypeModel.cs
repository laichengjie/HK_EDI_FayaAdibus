
using HK_EDI.Web.Mvc;

namespace Nop.Admin.Models.Logging
{
    public partial class ActivityLogTypeModel : BaseNopEntityModel
    {
        
        public string Name { get; set; }
        
        public bool Enabled { get; set; }
    }
}