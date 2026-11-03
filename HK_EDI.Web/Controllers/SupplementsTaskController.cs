using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Services.Tasks;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Kendoui;
using HK_EDI.Web.Models.Tasks;
using HK_EDI.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HK_EDI.Service.ShopModel;
using HK_EDI.Core.Domain.ShopModel;
using HK_EDI.Web.Models.ShopModel;

namespace HK_EDI.Web.Controllers
{
    public class SupplementsTaskController : BaseController
    {
        private readonly IShopDataService _iShopDataService;
        public SupplementsTaskController(IShopDataService iShopDataService)
        {
            this._iShopDataService = iShopDataService;
        }

        public ActionResult SupplementsSummarySet()
        {
            ShopDataModel model = new ShopDataModel();
            model.CompanyId =System.Configuration.ConfigurationManager.AppSettings["CompanyId"];
            model.DateTime = DateTime.Now;
            IList<ShopData> list = _iShopDataService.GetList("");
            var select = new SelectList(list, "ShopID", "ShopName", null);

            model.ShopList = select.ToList();

            return View(model);
        }
    }
}