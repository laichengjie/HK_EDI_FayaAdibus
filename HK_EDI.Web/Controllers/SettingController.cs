using HK_EDI.Core;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Service.Orders;
using HK_EDI.Services.Security;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.ExtensionsCommon;
using HK_EDI.Web.Models.Setting;
using Nop.Services.Configuration;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;

namespace HK_EDI.Web.Controllers
{
    public class SettingController : BaseController
    {

        private readonly IBasCompanyService _iBasCompanyService;
        private readonly ISettingService _settingService;
        private readonly GolbalSetting _golbalSetting;
        private readonly IPermissionService _permissionService;
        private readonly FayaAdibusGetPackingListSet _fayaAdibusGetPackingListSet;
        private readonly FayaAdibusGetEstimatedShipmentSet _fayaAdibusGetEstimatedShipmentSet;
        private readonly fayaadibusReplenishmentOrderSet _fayaadibusReplenishmentOrderSet;

        public SettingController( 
            IBasCompanyService iBasCompanyService,
            ISettingService settingService,
            GolbalSetting golbalSetting,
            IPermissionService permissionService,
            FayaAdibusGetPackingListSet fayaAdibusGetPackingListSet, 
            FayaAdibusGetEstimatedShipmentSet fayaAdibusGetEstimatedShipmentSet,
           fayaadibusReplenishmentOrderSet fayaadibusReplenishmentOrderSet
            )
        {
            this._iBasCompanyService = iBasCompanyService;
            this._settingService = settingService;
            this._golbalSetting = golbalSetting;
            this._permissionService = permissionService;
            this._fayaAdibusGetPackingListSet = fayaAdibusGetPackingListSet;
            this._fayaAdibusGetEstimatedShipmentSet = fayaAdibusGetEstimatedShipmentSet;
            this._fayaadibusReplenishmentOrderSet = fayaadibusReplenishmentOrderSet;
        }

        public ActionResult GolbalSet()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();
            string str = ConfigurationManager.AppSettings["BillTypeIDS"];
            string[] strs = str.Split(";");
            List<BillTypeModel> listBillType = new List<BillTypeModel>();

            for (int i = 0; i < strs.Count(); i++)
            {
                if (!string.IsNullOrEmpty(strs[i]))
                {
                    string[] items = strs[i].Split(",");
                    listBillType.Add(new BillTypeModel()
                    {
                        BillTypeId = items[0],
                        BillTypeName = items[1],
                        IsSelect = false
                    });
                }
            }

            List<BasCompany> listCompany = new List<BasCompany>();
            listCompany = _iBasCompanyService.GetBasCompanyALL();
            List<CompanyModel> companyList = new List<CompanyModel>();
            listCompany.ForEach(t => companyList.Add(new CompanyModel() { CompanyId = t.CompanyID, CompanyName = t.CompanyName, IsSelect = false }));
            var model = new GolbalSettingModel();
            model.KeepWebUrl = _golbalSetting.KeepWebUrl;
            if (!string.IsNullOrEmpty(_golbalSetting.CompanyIds))
            {
                string[] CompanyArray = _golbalSetting.CompanyIds.Split(",");
                for (int i = 0; i < CompanyArray.Length; i++)
                {
                    companyList.Where(t => t.CompanyId.ToUpper() == CompanyArray[i].ToUpper()).ToList().ForEach(t => t.IsSelect = true);
                }
            }

            if (!string.IsNullOrEmpty(_golbalSetting.BillTypeIDs))
            {
                string[] BillTypeArray = _golbalSetting.BillTypeIDs.Split(",");
                for (int i = 0; i < BillTypeArray.Length; i++)
                {
                    listBillType.Where(t => t.BillTypeId == BillTypeArray[i]).ToList().ForEach(t => t.IsSelect = true);
                }
            }
            model.CompanyIds = companyList;
            model.BillTypeIds = listBillType;
            model.SkuCodeStr = _golbalSetting.SkuCodeStr;
            model.BrandStr = (_golbalSetting.BrandStr ?? "").Trim();
            model.TaskStopExecuteTime = (_golbalSetting.TaskStopExecuteTime ?? "").Trim();
            model.TurnOnShopncInvoice = _golbalSetting.TurnOnShopncInvoice;
            //  model.TurnOnAutoReceive = _golbalSetting.TurnOnAutoReceive;
            return View(model);
        }
        [HttpPost]
        public ActionResult GolbalSet(GolbalSettingModel model)
        {
            if (ModelState.IsValid)
            {
                _golbalSetting.KeepWebUrl = model.KeepWebUrl;
                string companyId = string.Empty;
                string billTypeId = string.Empty;
                if (model.CompanyIds != null && model.CompanyIds.Count > 0)
                {
                    companyId = string.Join(",", model.CompanyIds.Where(t => t.CompanyId != "").Select(t => t.CompanyId).ToArray());
                }
                if (model.BillTypeIds != null && model.BillTypeIds.Count > 0)
                {
                    billTypeId = string.Join(",", model.BillTypeIds.Where(t => t.BillTypeId != "").Select(t => t.BillTypeId).ToArray());
                }
                _golbalSetting.CompanyIds = companyId;
                _golbalSetting.BillTypeIDs = billTypeId;
                _golbalSetting.SkuCodeStr = model.SkuCodeStr;
                _golbalSetting.BrandStr = (model.BrandStr ?? "").Trim();
                _golbalSetting.TaskStopExecuteTime = (model.TaskStopExecuteTime ?? "").Trim();
                _golbalSetting.TurnOnShopncInvoice = model.TurnOnShopncInvoice;//这个是否生成发票的开关（在LogicPut项目中没有引用依赖注入和实力类的项目，所以在LoginPub中该值是直接用sql语句从表中读取的。）
                // _golbalSetting.TurnOnAutoReceive = model.TurnOnAutoReceive;//这个开关（在LogicPut项目中没有引用依赖注入和实力类的项目，所以在LoginPub中该值是直接用sql语句从表中读取的。）
                _settingService.SaveSetting(_golbalSetting);
            }
            return GolbalSet();
        }

        /// <summary>
        /// Get active store scope (for multi-store configuration mode)
        /// </summary>
        /// <param name="storeService">Store service</param>
        /// <param name="workContext">Work context</param>
        /// <returns>Store ID; 0 if we are in a shared mode</returns>
        public virtual int GetActiveStoreScopeConfiguration(IWorkContext workContext)
        {
            return 0;
        }
        /// <summary>
        /// 获取装箱单
        /// </summary>
        /// <returns></returns>
        public ActionResult FayaAdibusGetPackingListSet()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var model = _fayaAdibusGetPackingListSet.ToModel<FayaAdibusGetPackingListSettingModel>();
            return View(model);
        }
        [HttpPost]
        public ActionResult FayaAdibusGetPackingListSet(FayaAdibusGetPackingListSettingModel model)
        {
            if (ModelState.IsValid)
            {
                model.ToEntity(_fayaAdibusGetPackingListSet);

                _settingService.SaveSetting(_fayaAdibusGetPackingListSet);
            }
            return FayaAdibusGetPackingListSet();
        }

        /// <summary>
        /// 获取补货单
        /// </summary>
        /// <returns></returns>
        public ActionResult fayaadibusReplenishmentOrderSet() 
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var model = _fayaadibusReplenishmentOrderSet.ToModel<fayaadibusReplenishmentOrderSetSettingModel>();
            return View(model);
        }
        [HttpPost]
        public ActionResult fayaadibusReplenishmentOrderSet(fayaadibusReplenishmentOrderSetSettingModel model)
        {
            if (ModelState.IsValid)
            {
                model.ToEntity(_fayaadibusReplenishmentOrderSet);
                _settingService.SaveSetting(_fayaadibusReplenishmentOrderSet);
            }
            return fayaadibusReplenishmentOrderSet();
        }

        /// <summary>
        /// 获取采购到货
        /// </summary>
        /// <returns></returns>
        public ActionResult FayaAdibusGetEstimatedShipmentSet()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var model = _fayaAdibusGetEstimatedShipmentSet.ToModel<FayaAdibusGetEstimatedShipmentSettingModel>();
            return View(model);
        }
        [HttpPost]
        public ActionResult FayaAdibusGetEstimatedShipmentSet(FayaAdibusGetEstimatedShipmentSettingModel model)
        {
            if (ModelState.IsValid)
            {
                model.ToEntity(_fayaAdibusGetEstimatedShipmentSet);

                _settingService.SaveSetting(_fayaAdibusGetEstimatedShipmentSet);
            }
            return FayaAdibusGetEstimatedShipmentSet();
        }

    }
}