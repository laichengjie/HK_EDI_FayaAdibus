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
using HK_EDI.Web.Models.StockSet;
using HK_EDI.Service.FayaAdibus.BasSet;
using HK_EDI.Service.StockModel;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Data.Mapping.StockModel;

namespace HK_EDI.Web.Controllers
{
    public class StockSettingsController : BaseController
    {
        #region Fields
        private readonly IPermissionService _permissionService;
        private readonly IStockDataService _istockDataService;
        private readonly FayaAdibusGetEstimatedShipmentSet _FayaAdibusGetEstimatedShipmentSet;
        #endregion
        #region Constructors

        public StockSettingsController(
            IPermissionService permissionService,
            IStockDataService iStockDataService,
              FayaAdibusGetEstimatedShipmentSet fayaAdibusGetEstimatedShipmentSet)
        {
            this._permissionService = permissionService;
            this._istockDataService = iStockDataService;
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

            var listErpStock = new ErpStockSetModel();
            var listCorStock = new CorrespondingStockModel();
            ////获取仓库
            //IList<HK_EDI.Core.Domain.StockModel.StockData> list = _istockDataService.GetList("");
            //var select3 = new SelectList(list, "StockID", "StockName", null);
            //listErpStock.StockLists = select3.ToList();
            //listErpStock.StockLists.Insert(0, new SelectListItem { Text = "全部", Value = "", Selected = true });

            return View(Tuple.Create(listErpStock, listCorStock));
        }
        #region Erp仓    
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="command"></param>
        /// <param name="modes"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult QueryErpStockList(DataSourceRequest command, ErpStockSetModel modes)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();
            List<ErpStockSetModel> modellist = new List<ErpStockSetModel>();
            StockSetDal abc = new StockSetDal();
            DataTable dt = abc.QueryErpStock();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ErpStockSetModel model = new ErpStockSetModel();
                    model.ID = item["ID"].ToString();
                    model.Xuhao = item["Xuhao"].ToString();
                    model.ErpStockID = item["ErpStockID"].ToString();
                
                    if (!string.IsNullOrEmpty(model.ErpStockID))
                    {
                        DataTable dtlists = abc.GetStockName(_FayaAdibusGetEstimatedShipmentSet.CompanyId, model.ErpStockID);
                        if (dtlists.Rows.Count > 0)
                        {
                            model.ErpStockCode = dtlists.Rows[0]["StockCode"].ToString();
                            model.ErpStockName = dtlists.Rows[0]["StockName"].ToString();
                        }
                    }
                    model.IsDemorderForType = Convert.ToInt32(item["IsDemorderForType"]);
                    model.IsDemorderForTypeName = item["IsDemorderForTypeName"].ToString();
                    model.ModifyDTM = item["ModifyDTM"].ToString();
                    model.Page = command.Page - 1;
                    model.PageSize = command.PageSize;
                    modellist.Add(model);
                }
            }
            var gridModel = new DataSourceResult
            {
                Data = modellist.Select(x => new ErpStockSetModel
                {
                    ID = x.ID,
                    Xuhao = x.Xuhao,
                    ErpStockID = x.ErpStockID,
                    ErpStockCode = x.ErpStockCode,
                    ErpStockName = x.ErpStockName,
                    IsDemorderForType = x.IsDemorderForType,
                    IsDemorderForTypeName = x.IsDemorderForTypeName,
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
        public void UpdateErpStockList(ErpStockSetModel modes)
        {
            StockSetDal abc = new StockSetDal();
            modes.ModifyDTM = DateTime.Now.ToString();
            abc.UpdateErpStock(modes.ID, modes.ErpStockID, modes.IsDemorderForType, modes.ModifyDTM);
 
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void DeleteErpStockList(ErpStockSetModel modes)
        {
            StockSetDal abc = new StockSetDal();
            abc.DeleteErpStock(modes.ID, modes.ErpStockID);

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
        public ActionResult QueryCorStockList(string vErpStockID)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();
            List<CorrespondingStockModel> modellist = new List<CorrespondingStockModel>();
            StockSetDal abc = new StockSetDal();
            if (!string.IsNullOrEmpty(vErpStockID))
            {
                DataTable dt = abc.QueryCorStock(vErpStockID);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        CorrespondingStockModel model = new CorrespondingStockModel();
                        model.ID = item["ID"].ToString();
                        model.Xuhao = item["Xuhao"].ToString();
                        model.ErpStockID = item["ErpStockID"].ToString();
                        model.CorStockCode = item["CorStockCode"].ToString();
                        model.CorStockName = item["CorStockName"].ToString();
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
                Data = modellist.Select(x => new CorrespondingStockModel
                {
                    ID = x.ID,
                    Xuhao = x.Xuhao,
                    ErpStockID = x.ErpStockID,
                    CorStockCode = x.CorStockCode,
                    CorStockName = x.CorStockName,
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
        public void UpdateCorStockList(CorrespondingStockModel modes, string vErpStockID)
        {
            if (!string.IsNullOrEmpty(vErpStockID))
            {
                StockSetDal abc = new StockSetDal();
                modes.ModifyDTM = DateTime.Now.ToString();
                abc.UpdateCorStock(modes.ID, vErpStockID, modes.CorStockCode, modes.CorStockName, _FayaAdibusGetEstimatedShipmentSet.CheckerID, modes.ModifyDTM);
            }

        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="modes"></param>
        [HttpPost]
        public void DeleteCorStockList(CorrespondingStockModel modes)
        {
            StockSetDal abc = new StockSetDal();
            abc.DeleteCorStock(modes.ID, modes.CorStockCode);

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
            StockSetDal abc = new StockSetDal();
            DataTable dtlist = abc.GetStockName(_FayaAdibusGetEstimatedShipmentSet.CompanyId, "");
            if (dtlist.Rows.Count > 0)
            {
                List<StockDataMap> listStock = new List<StockDataMap>();
                foreach (DataRow dtlists in dtlist.Rows)
                {
                    StockDataMap bsdata = new StockDataMap();
                    bsdata.ErpStockID = dtlists["StockID"].ToString();
                    bsdata.ErpStockCode = dtlists["StockCode"].ToString();
                    bsdata.ErpStockName = dtlists["StockName"].ToString();
                    listStock.Add(bsdata);
                }
                return Json(listStock.ToList(), JsonRequestBehavior.AllowGet);

            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}