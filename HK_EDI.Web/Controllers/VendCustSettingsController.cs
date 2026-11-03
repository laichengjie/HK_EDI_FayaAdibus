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
using HK_EDI.Web.Models.PaymentMethod;
using System.Data;
using HK_EDI.Service.ShopModel;
using HK_EDI.Core.PagedExend;
using HK_EDI.Service.FayaAdibus.BasSet;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Web.Models.VendCustSet;
using HK_EDI.Data.Mapping.VendCustModel;

namespace HK_EDI.Web.Controllers
{
    public class VendCustSettingsController : BaseController
    {
        #region Fields
        private readonly IPermissionService _permissionService;
        private readonly FayaAdibusGetEstimatedShipmentSet _FayaAdibusGetEstimatedShipmentSet;
        #endregion
        #region Constructors

        public VendCustSettingsController(
            IPermissionService permissionService,
              FayaAdibusGetEstimatedShipmentSet fayaAdibusGetEstimatedShipmentSet)
        {
            this._permissionService = permissionService;
            this._FayaAdibusGetEstimatedShipmentSet = fayaAdibusGetEstimatedShipmentSet;
        }

        #endregion

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var listErpVendCust = new ErpVendCustModel();
            var listCorVendCust = new CorVendCustModel();

            return View(Tuple.Create(listErpVendCust, listCorVendCust));
        }
        #region Erp仓    
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="command"></param>
        /// <param name="modes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueryErpVendCustList(DataSourceRequest command, ErpVendCustModel modes)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();
            List<ErpVendCustModel> modellist = new List<ErpVendCustModel>();
            VendCustSetDal abc = new VendCustSetDal();
            DataTable dt = abc.QueryErpVendCust();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ErpVendCustModel model = new ErpVendCustModel();
                    model.ID = item["ID"].ToString();
                    model.Xuhao = item["Xuhao"].ToString();
                    model.ErpVendCustID = item["VendCustID"].ToString();

                    if (!string.IsNullOrEmpty(model.ErpVendCustID))
                    {
                        DataTable dtlists = abc.GetVendCustName(_FayaAdibusGetEstimatedShipmentSet.CompanyId, model.ErpVendCustID);
                        if (dtlists.Rows.Count > 0)
                        {
                            model.ErpVendCustCode = dtlists.Rows[0]["VendCustCode"].ToString();
                            model.ErpVendCustName = dtlists.Rows[0]["VendCustName"].ToString();
                        }
                    }
                    model.OperationType = item["OperationType"].ToString();
                    model.OperationTypeName = item["OperationTypeName"].ToString();
                    model.BillState = item["BillState"].ToString();
                    model.BillStateName = item["BillStateName"].ToString();
                    
                    model.ModifyDTM = item["ModifyDTM"].ToString();
                    model.Page = command.Page - 1;
                    model.PageSize = command.PageSize;
                    modellist.Add(model);
                }
            }
            var gridModel = new DataSourceResult
            {
                Data = modellist.Select(x => new ErpVendCustModel
                {
                    ID = x.ID,
                    Xuhao = x.Xuhao,
                    ErpVendCustID = x.ErpVendCustID,
                    ErpVendCustCode = x.ErpVendCustCode,
                    ErpVendCustName = x.ErpVendCustName,
                    OperationType = x.OperationType,
                    OperationTypeName = x.OperationTypeName,
                    BillState = x.BillState,
                    BillStateName = x.BillStateName,
                    
                    ModifyDTM = x.ModifyDTM,
                    Page = x.Page,
                    PageSize = x.PageSize
                }),
                Total = modellist.Count
            };
            return Json(gridModel);

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void UpdateErpVendCustList(ErpVendCustModel modes)
        {
            VendCustSetDal abc = new VendCustSetDal();
            modes.ModifyDTM = DateTime.Now.ToString();
            abc.UpdateErpVendCust(modes.ID, modes.ErpVendCustID, modes.OperationType,modes.BillState, modes.ModifyDTM);
 
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void DeleteErpVendCustList(ErpVendCustModel modes)
        {
            VendCustSetDal abc = new VendCustSetDal();
            abc.DeleteErpVendCust(modes.ID, modes.ErpVendCustID);

        }

   
        #endregion

        #region 对应仓    
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="command"></param>
        /// <param name="modes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueryCorVendCustList(string vErpVendCustID)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();
            List<CorVendCustModel> modellist = new List<CorVendCustModel>();
            VendCustSetDal abc = new VendCustSetDal();
            if (!string.IsNullOrEmpty(vErpVendCustID))
            {
                DataTable dt = abc.QueryCorVendCust(vErpVendCustID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CorVendCustModel model = new CorVendCustModel();
                        model.ID = item["ID"].ToString();
                        model.Xuhao = item["Xuhao"].ToString();
                        model.ErpVendCustID = item["VendCustID"].ToString();
                        model.CorVendCustCode = item["CorVendCustCode"].ToString();
                        model.CorVendCustName = item["CorVendCustName"].ToString();
                        model.Checker = item["Checker"].ToString();
                        model.ModifyDTM = item["ModifyDTM"].ToString();
                        if (!string.IsNullOrEmpty(model.Checker))
                        {
                            DataTable dtlists = abc.GetUserName(_FayaAdibusGetEstimatedShipmentSet.CompanyId, model.Checker);
                            if (dtlists.Rows.Count > 0)
                            {
                                model.CheckerName = dtlists.Rows[0]["CardTypeName"].ToString();
                            }
                        }
                        model.Page = 1 - 1;
                        model.PageSize = 10;
                        modellist.Add(model);
                    }
                }
            }
            var gridModel = new DataSourceResult
            {
                Data = modellist.Select(x => new CorVendCustModel
                {
                    ID = x.ID,
                    Xuhao = x.Xuhao,
                    ErpVendCustID = x.ErpVendCustID,
                    CorVendCustCode = x.CorVendCustCode,
                    CorVendCustName = x.CorVendCustName,
                    Checker = x.Checker,
                    ModifyDTM = x.ModifyDTM,
                    Page = x.Page,
                    PageSize = x.PageSize
                }),
                Total = modellist.Count
            };
            return Json(gridModel);

        }
      
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void UpdateCorVendCustList(CorVendCustModel modes, string vErpVendCustID)
        {
            if (!string.IsNullOrEmpty(vErpVendCustID))
            {
                VendCustSetDal abc = new VendCustSetDal();
                modes.ModifyDTM = DateTime.Now.ToString();
                abc.UpdateCorVendCust(modes.ID, vErpVendCustID, modes.CorVendCustCode, modes.CorVendCustName, _FayaAdibusGetEstimatedShipmentSet.CheckerID, modes.ModifyDTM);
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void DeleteCorVendCustList(CorVendCustModel modes)
        {
            VendCustSetDal abc = new VendCustSetDal();
            abc.DeleteCorVendCust(modes.ID, modes.CorVendCustCode);

        }
        #endregion
        public DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)//PageIndex表示第几页，PageSize表示每页的记录数
        {
            if (PageIndex == 0)
                return dt;//0页代表每页数据，直接返回

            DataTable newdt = dt.Copy();
            newdt.Clear();//copy dt的框架

            int rowbegin = (PageIndex - 1) * PageSize;
            int rowend = PageIndex * PageSize;

            if (rowbegin >= dt.Rows.Count)
                return newdt;//源数据记录数小于等于要显示的记录，直接返回dt

            if (rowend > dt.Rows.Count)
                rowend = dt.Rows.Count;
            for (int i = rowbegin; i <= rowend - 1; i++)
            {
                DataRow newdr = newdt.NewRow();
                DataRow dr = dt.Rows[i];
                foreach (DataColumn column in dt.Columns)
                {
                    newdr[column.ColumnName] = dr[column.ColumnName];
                }
                newdt.Rows.Add(newdr);
            }
            return newdt;
        }
        public ActionResult GetSelectList()
        {
            VendCustSetDal abc = new VendCustSetDal();
            DataTable dtlist = abc.GetVendCustName(_FayaAdibusGetEstimatedShipmentSet.CompanyId, "");
            if (dtlist.Rows.Count > 0)
            {
                List<VendCustDataMap> listVendCust = new List<VendCustDataMap>();
                foreach (DataRow dtlists in dtlist.Rows)
                {
                    VendCustDataMap bsdata = new VendCustDataMap();
                    bsdata.ErpVendCustID = dtlists["VendCustID"].ToString();
                    bsdata.ErpVendCustCode = dtlists["VendCustCode"].ToString();
                    bsdata.ErpVendCustName = dtlists["VendCustName"].ToString();
                    listVendCust.Add(bsdata);
                }
                return Json(listVendCust.ToList(), JsonRequestBehavior.AllowGet);

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}