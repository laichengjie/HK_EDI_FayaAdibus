using HK_EDI.Core;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Data;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Service.FayaAdibus.BasSet;
using HK_EDI.Service.Orders;
using HK_EDI.Service.Utility;
using HK_EDI.Service.WMS;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Security;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Kendoui;
using HK_EDI.Web.Models.DiscountSet;
using HK_EDI.Web.Models.Setting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Controllers
{
    public class DiscountSettingsController : BaseController
    {

        #region Fields
        private readonly ILogger _logger;
        private readonly IPermissionService _permissionService;
        
        #endregion
        public DiscountSettingsController(
            IPermissionService permissionService,
            ILogger iLogger
         )
        {
            this._permissionService = permissionService;
            this._logger = iLogger;
        }
        // GET: ALiBasCardQuery
        public ActionResult Index()
        {
            //ViewBag.Appkey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
            //return View();
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var listModel = new DiscountSetModel();
            ////获取店铺
            //IList<HK_EDI.Core.Domain.ShopModel.ShopData> list = _iShopDataService.GetList("");
            //var select3 = new SelectList(list, "ShopID", "ShopName", null);
            //listModel.ShopLists = select3.ToList();
            //listModel.ShopLists.Insert(0, new SelectListItem { Text = "全部", Value = "", Selected = true });


            return View(listModel);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="command"></param>
        /// <param name="modes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueryList(DataSourceRequest command, DiscountSetModel modes)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();
            List<DiscountSetModel> modellist = new List<DiscountSetModel>();
            DiscountTypeDal abc = new DiscountTypeDal();
            DataTable dt = abc.QueryDiscountType(modes.DiscountType);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    DiscountSetModel model = new DiscountSetModel();
                    model.ID = item["ID"].ToString();
                    model.Xuhao = item["Xuhao"].ToString();
                    model.DiscountType = item["DiscountType"].ToString();
                    model.Discount = Convert.ToDecimal(item["Discount"]);
                    model.Checker = item["Checker"].ToString();
                    model.ModifyDTM = item["ModifyDTM"].ToString();
                    //if (!string.IsNullOrEmpty(model.Checker))
                    //{
                    //    DataTable dtlists = abc.GetUserName(_FayaAdibusReceiptSet.CompanyId, model.Checker);
                    //    if (dtlists.Rows.Count > 0)
                    //    {
                    //            model.CheckerName = dtlists.Rows[0]["CardTypeName"].ToString();
                    //    }
                    //}
                    model.Page = command.Page - 1;
                    model.PageSize = command.PageSize;
                    modellist.Add(model);
                }
            }
            var gridModel = new DataSourceResult
            {
                Data = modellist.Select(x => new DiscountSetModel
                {
                    ID = x.ID,
                    Xuhao = x.Xuhao,
                    DiscountType = x.DiscountType,
                    Discount = x.Discount,
                    Checker = x.Checker,
                    ModifyDTM = x.ModifyDTM,
                    Page = x.Page,
                    PageSize = x.PageSize
                }),
                Total = modellist.Count
            };
            return Json(gridModel);

        }

        [HttpPost]
        public ActionResult CheackDataAdd(string otherdate)
        {
            //if (string.IsNullOrEmpty(_FayaAdibusReceiptSet.CheckerID))
            //{

            //    return  Content("请先设置操作员");
            //}
            return Content("");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void UpdateList(DiscountSetModel modes)
        {
           
            //if (!string.IsNullOrEmpty(_FayaAdibusReceiptSet.CheckerID))
            //{
            //    DiscountTypeDal abc = new DiscountTypeDal();
            //    modes.Checker = _FayaAdibusReceiptSet.CheckerID;
            //    modes.ModifyDTM = DateTime.Now.ToString();
            //    abc.UpdateDiscountType(modes.ID, modes.DiscountType, modes.Discount, modes.Checker, modes.ModifyDTM);
            //}
           
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void DeleteList(DiscountSetModel modes)
        {
            DiscountTypeDal abc = new DiscountTypeDal();
            abc.DeleteDiscountType(modes.ID, modes.DiscountType);

        }


        //public ActionResult GetSelectList()
        //{
        //    //DiscountTypeDal abc = new DiscountTypeDal();
        //    //DataTable dtlist = abc.GetCardType("");
        //    //if (dtlist.Rows.Count > 0)
        //    //{
        //    //    List<CardTypeIDList> listshop = new List<CardTypeIDList>();
        //    //    foreach (DataRow dtlists in dtlist.Rows)
        //    //    {
        //    //        CardTypeIDList bsdata = new CardTypeIDList();
        //    //        bsdata.CardTypeID = dtlists["CardTypeID"].ToString();
        //    //        bsdata.CardTypeName = dtlists["CardTypeName"].ToString();
        //    //        listshop.Add(bsdata);
        //    //    }
        //    //    return Json(listshop.ToList(), JsonRequestBehavior.AllowGet);

        //    //}
        //    //return Json("", JsonRequestBehavior.AllowGet);
        //    return Json("", JsonRequestBehavior.AllowGet);
        //}
    }
}