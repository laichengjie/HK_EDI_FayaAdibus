using HK_EDI.Web.Mvc;

namespace HK_EDI.Web.Models.Home
{
    public partial class DashboardModel : BaseNopModel
    {
        public bool IsLoggedInAsVendor { get; set; }
    }
}