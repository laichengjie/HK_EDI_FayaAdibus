using HK_EDI.Core.Common;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Service.Orders;
using HK_EDI.Service.WMS;
using HK_EDI.Services.Security;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Kendoui;
using HK_EDI.Core.Common;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Service.Orders;
using HK_EDI.Service.WMS;
using HK_EDI.Services.Security;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Kendoui;
using HK_EDI.Web.Models.OrderTrace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HK_EDI.Core;
using HK_EDI.Web.ExtensionsCommon;
using HK_EDI.Web.Models.GoodPostRecord;


namespace HK_EDI.Web.Controllers
{
    public class GoodPostRecordController : BaseController
    {
        private readonly IPostSkuErrorRecordsService _ipostSkuErrorRecordsService;
        private readonly IPostSkuRecordsDetailService _iPostSkuRecordsDetailService;
        private readonly IPermissionService _permissionService;
        private readonly IEDI_ConfigService _edi_ConfgService;
        private IWMSService _wmsService;
        public GoodPostRecordController(
            IPostSkuErrorRecordsService ipostSkuErrorRecordsService,
            IPostSkuRecordsDetailService iPostSkuRecordsDetailService,
            IPermissionService permissionService,
            IEDI_ConfigService edi_ConfgService
            )
        {
            this._ipostSkuErrorRecordsService = ipostSkuErrorRecordsService;
            this._iPostSkuRecordsDetailService = iPostSkuRecordsDetailService;
            this._permissionService = permissionService;
            this._edi_ConfgService = edi_ConfgService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string osstockId)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();
            var listModel = new GoodPostRecordModel();
            var config = _edi_ConfgService.GetAll();
            if (config.Count > 0)
            {
                var wmsSys = config.Select(m => new SelectListItem { Text = m.OSInterfaceName, Value = m.OSInterfaceID }).ToList();
                if (osstockId.IsNullOrEmpty())
                    wmsSys.First().Selected = true;
                else
                    wmsSys.First(m => m.Value == osstockId).Selected = true;
                listModel.WMSSystems = wmsSys;
            }
            return View(listModel);
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, GoodPostRecordModel model)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();
                var list = this._ipostSkuErrorRecordsService.GetAll(
                    model.IsSuccess,
                    model.CreateDateFrom, model.CreateDateTo,
                    model.SearchOSStockID,
                    model.SkuCode,                   
                    pageIndex: command.Page - 1,
                    pageSize: command.PageSize);
                var gridModel = new DataSourceResult
                {
                    Data = list,
                    Total = list.TotalCount
                };
                return Json(gridModel);
        }

        public ActionResult Detail(int PId)
        {
            var detail = _iPostSkuRecordsDetailService.GetJobOrderTraceDetailByPId(PId);
            var model = detail.Select(m => new GoodPostRecordDetailModel
            {
                Id = m.Id.ToString(),
                Content = m.Contents,
                CreateTime = m.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
            }).ToList();
            return View(model);
        }
    }
}