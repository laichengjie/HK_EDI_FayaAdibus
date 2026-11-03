using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HK_EDI.Service.WMS;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Services.Security;
using HK_EDI.Core;

namespace HK_EDI.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IPermissionService _permissionService;
        private readonly IWebHelper _webHelper;
        private IWMSService _wmsService;

        public HomeController(IWMSService wmsService, IPermissionService permissionService, IWebHelper webHelper)
        {
            _wmsService = wmsService;
            _permissionService = permissionService;
            _webHelper = webHelper;
        }
        public HomeController()
        {
           
        }

        // GET: /Home/
        public ActionResult Index()
        {
            var model = new DashboardModel();
            model.IsLoggedInAsVendor = true;

            //var enums = _wmsService.GetOrderStatusEnums();



            return View(model);
        }

        public ActionResult GetOrderStatus(string signCode)
        {
            var scope = EngineContext.Current.ContainerManager.Scope();
            var edi_configService = EngineContext.Current.ContainerManager.Resolve<IEDI_ConfigService>("", scope);
            var config = edi_configService.GetConfig("00000000", "00000005");
            string str = _wmsService.GetOrderStatus(config, signCode);
            ViewData["str"] = str;
            return View();
        }

        public ActionResult RestartApplication()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            //restart application
            _webHelper.RestartAppDomain();
            return RedirectToAction("Index", "Home");
        }

    }
}