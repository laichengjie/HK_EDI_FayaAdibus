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
using HK_EDI.Web.Models.ManageOrdersErrDetail;

namespace HK_EDI.Web.Controllers
{
    public class ManageOrdersErrDetailController : BaseController
    {
        #region Fields

        private readonly IJobOrderTraceService _jobOrderTraceService;
        private readonly IJobOrderTraceDetailService _jobOrderTraceDetailService;
        private readonly IPermissionService _permissionService;
        private readonly IEDI_ConfigService _edi_ConfgService;
        private IWMSService _wmsService;
        private readonly Service.ShopModel.IShopDataService _iShopDataService;

        #endregion

        #region Constructors

        public ManageOrdersErrDetailController(IJobOrderTraceService jobOrderTraceService,
            IJobOrderTraceDetailService _jobOrderTraceDetailService,
            IPermissionService permissionService,
            IEDI_ConfigService edi_ConfgService,
             Service.ShopModel.IShopDataService iShopDataService)
        {
            this._jobOrderTraceService = jobOrderTraceService;
            this._jobOrderTraceDetailService = _jobOrderTraceDetailService;
            this._permissionService = permissionService;
            this._edi_ConfgService = edi_ConfgService;
            this._iShopDataService = iShopDataService;
        }

        #endregion

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List(string OSInterfaceID)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();

            var listModel = new OrderTraceErrDetailListModel();
            var config = _edi_ConfgService.GetAll();
            if (config.Count > 0)
            {
                if (OSInterfaceID.IsNullOrEmpty())
                {
                    _wmsService = config.First(p => p.OSInterfaceID.Contains("AD")).GetWMSServiceInstance();
                }
                else
                {
                    _wmsService = config.First(m => m.OSInterfaceID == OSInterfaceID).GetWMSServiceInstance();
                }
                if (_wmsService != null)
                {
                    var orderStatusEnums = _wmsService.GetOrderStatusEnums().ToList();
                    var select = new SelectList(orderStatusEnums, "ID", "Name", null);
                    listModel.OrderStatus = select.ToList();
                    listModel.OrderStatus.Insert(0, new SelectListItem { Text = "全部", Value = "", Selected = true });
                }
                var wmsSys = config.Where(p => p.OSInterfaceID.Contains("AD")).Select(m => new SelectListItem { Text = m.OSInterfaceName, Value = m.OSInterfaceID }).ToList();
                if (OSInterfaceID.IsNullOrEmpty())
                    wmsSys.First().Selected = true;
                else
                    wmsSys.First(m => m.Value == OSInterfaceID).Selected = true;
                listModel.WMSSystems = wmsSys;

            }


            var orderTypeEnums = HK_EDI.Service.WMS.CommonModels.OrderTypeEnum.CREATERECEIPT.ToEnumSelectList().ToList();
            var select2 = new SelectList(orderTypeEnums, "ID", "Name", null);
            listModel.OrderTypes = select2.ToList();
            listModel.OrderTypes.Insert(0, new SelectListItem { Text = "全部", Value = "", Selected = true });

            //获取店铺
            IList<HK_EDI.Core.Domain.ShopModel.ShopData> list = _iShopDataService.GetList("");
            var select3 = new SelectList(list, "ShopID", "ShopName", null);
            listModel.ShopLists = select3.ToList();
            listModel.ShopLists.Insert(0, new SelectListItem { Text = "全部", Value = "", Selected = true });

            return View(listModel);
        }

        [HttpPost]
        public ActionResult OrderTraceList(DataSourceRequest command, OrderTraceErrDetailListModel model)
        {

            if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
                return AccessDeniedView();
            var config = _edi_ConfgService.GetConfig("00000000", model.SearchOSInterfaceID);
            _wmsService = config.GetWMSServiceInstance();
            if (_wmsService != null)
            {
                var list = this._jobOrderTraceService.GetErrDetailAll(
                    model.CreateDateFrom,
                    model.CreateDateTo,
                    model.ReadDateFrom,
                    model.ReadDateTo,
                    model.CancelDateFrom,
                    model.CancelDateTo,
                    model.BillDate,
                    model.ShopID,
                    model.SearchOSInterfaceID,
                    model.SearchBillTypeID, model.SearchCompanyId,
                    model.SearchNoticeBillNo, model.SearchSignCode,
                    model.SearchOrderType, model.SearchOpResultStatus,
                    pageIndex: command.Page - 1,
                    pageSize: command.PageSize);

                var orderStatusEnums = _wmsService.GetOrderStatusEnums().ToList();
                var orderTypeEnums = HK_EDI.Service.WMS.CommonModels.OrderTypeEnum.CREATERECEIPT.ToEnumSelectList().ToList();
                //获取店铺
                var shopList = _iShopDataService.GetList("");
                var gridModel = new DataSourceResult
                {
                    Data = list.Select(m => PrepareModelForList(m, orderStatusEnums, orderTypeEnums, shopList)),
                    Total = list.TotalCount
                };

                return Json(gridModel);
            }
            return Json(new DataSourceResult { Data = null, Total = 0 });
        }


        //[HttpPost]
        //public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        //{
        //    var fileContents = Convert.FromBase64String(base64);

        //    return File(fileContents, contentType, fileName);
        //}
        public ActionResult Detail(int PId)
        {
            var job = _jobOrderTraceService.JobOrderTraceById(Convert.ToInt32(PId));

            ViewData["SignCode"] = job.SignCode;

            var detail = _jobOrderTraceDetailService.GetJobOrderTraceDetailByPId(PId);
            var model = detail.Select(m => new OrderTraceDetailModel
            {
                Id = m.Id.ToString(),
                ContentNote = m.ContentNode,
                ContentNoteRequest = m.ContentNodeRequest,
                ContentType = m.ContentType.ToString(),
                CreateTime = m.CreateTime,
                ModifyTime = m.ModifyTime,
                SignCode = m.SignCode,
                Description = m.Description
            }).ToList();

            return View(model);
        }

        [NonAction]
        protected virtual OrderTraceErrDetailModel PrepareModelForList(JobOrderTraceErrDetail model,
            IList<BaseItem> orderStatusEnums, IList<BaseItem> orderTypeEnums, IList<HK_EDI.Core.Domain.ShopModel.ShopData> shopData)
        {
            string ShopName = string.Empty;
            if (!string.IsNullOrEmpty(model.ShopID))
                ShopName = shopData.First(m => m.ShopID == model.ShopID).ShopName;
            return new OrderTraceErrDetailModel
            {
                Id = model.Id,
                BillTypeID = model.BillTypeID,
                SignCode = model.SignCode,
                CompanyId = model.CompanyID,
                CreateDate = model.CreateDate,
                CustomerCode = model.CustomerCode,
                NoticeBillNo = model.NoticeBillNo,
                OpResultStatus = orderStatusEnums.First(m => m.ID == model.OpResultStatus.ToString()).Name,
                OrderTypeName = orderTypeEnums.First(m => m.ID == model.OrderType.ToString()).Name,
                IsCancel = _wmsService.IsCancelStatus(model.OpResultStatus.Value),
                ReadDate = model.ReadDate,
                CancelDate = model.CancelDate,
                ShopID = ShopName,
                BillDate = model.BillDate,
                OrderTraceDetail = model.OrderTraceDetail,
                RequestData = model.RequestData.Trim()
            };
        }

        [HttpPost]
        public ActionResult CancelOrder(string OSInterfaceID, string signCode)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CancelOrders))
            {
                return Content("无权操作");
            }
            var config = _edi_ConfgService.GetConfig("00000000", OSInterfaceID);
            _wmsService = config.GetWMSServiceInstance();
            var result = _wmsService.CancelOrder(config, signCode);
            return Content("撤销结果:" + result.MessageStr);
        }

        [HttpPost]
        public ActionResult CancelOrderList(string otherdate)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.CancelOrders))
            {
                return Json(new List<CancelCommon>() { new CancelCommon() { Message = "无权操作" } });
            }
            if (string.IsNullOrEmpty(otherdate))
            {
                return Content("");
            }
            string[] date = otherdate.Split(';');

            List<CancelCommon> list = new List<Controllers.ManageOrdersErrDetailController.CancelCommon>();
            foreach (string itemdate in date)
            {
                string[] item = itemdate.Split(',');
                if (item.Count() != 2)
                {
                    continue;
                }

                var config = _edi_ConfgService.GetConfig("00000000", item[0]);
                if (config == null)
                {
                    continue;
                }
                _wmsService = config.GetWMSServiceInstance();
                try
                {
                    var result = _wmsService.CancelOrder(config, item[1]);
                    list.Add(new CancelCommon() { Code = item[1], Message = result.MessageStr });
                }
                catch (Exception ex)
                {
                    list.Add(new CancelCommon() { Code = item[1], Message = ex.Message });
                }

            }
            var s = Json(list);
            return s;
        }




        [HttpPost]
        public ActionResult ForceOrder(string OSInterfaceID, string signCode)
        {
            var config = _edi_ConfgService.GetConfig("00000000", OSInterfaceID);
            _wmsService = config.GetWMSServiceInstance();
            var result = _wmsService.ForceProcess(config, signCode);
            return Content("结果:" + result.MessageStr);
        }

        [HttpPost]
        public ActionResult GetOrderStatus(string OSInterfaceID, string signCode)
        {
            var config = _edi_ConfgService.GetConfig("00000000", OSInterfaceID);
            _wmsService = config.GetWMSServiceInstance();
            var result = _wmsService.GetOrderStatus(config, signCode);
            return Content("当前订单状态为：" + result);
        }

        [HttpPost]
        public ActionResult Repost(string OSInterfaceID, string signCode)
        {
            var config = _edi_ConfgService.GetConfig("00000000", OSInterfaceID);
            _wmsService = config.GetWMSServiceInstance();
            var job = _jobOrderTraceService.GetJobOrderTrace(signCode);
            if (job != null)
            {
                if (!_wmsService.IsCancelStatus(job.OpResultStatus.Value))
                {
                    return Content("回传失败，此订单不可回传！");
                }
                var signcodeSplit = job.SignCode.Split('-');
                if (signcodeSplit.Length == 1)
                {
                    job.SignCode = job.SignCode + "-1";
                }
                else
                {
                    job.SignCode = signcodeSplit[0] + "-" + (signcodeSplit[1].ToInt() + 1);
                }
                job.OpResultStatus = -100;
                _jobOrderTraceService.UpdateOrderTrace(job);
                return Content("回传成功，下次同步时会以新的单号重新发送给WMS");
            }
            else
            {
                return Content("回传失败，订单不存在");
            }
        }

        /// <summary>
        /// 补传，零售日结As0000003和门店库存快照服务As0000006 无单号或单号为临时生成不可使用单号查询，只能发送上次发送的数据
        /// </summary>
        /// <param name="OSInterfaceID"></param>
        /// <param name="NoticeBillNo"></param>
        /// <param name="CompanyId"></param>
        /// <param name="SignCode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RepostOrder(string OSInterfaceID, string NoticeBillNo, string CompanyId, string SignCode)
        {
            var config = _edi_ConfgService.GetConfig("00000000", OSInterfaceID);
            _wmsService = config.GetWMSServiceInstance();
            if (OSInterfaceID != "As0000003" && OSInterfaceID != "As0000006")
                _wmsService.PostOrderByBillNo(config, NoticeBillNo);
            else
                _wmsService.PostOrderBySignCode(config, CompanyId, SignCode);
            return Content("已发送");
        }
        

        public class CancelCommon
        {
            public string Code { get; set; }

            public string Message { get; set; }
        }
    }
}