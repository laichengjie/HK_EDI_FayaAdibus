using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HK_EDI.Web.Models.Customers;
using HK_EDI.Core;
using HK_EDI.Core.Domain.Customers;
using HK_EDI.Services.Customers;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Security;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Kendoui;
using HK_EDI.Web.ExtensionsCommon;
using Nop.Web.Framework.Controllers;

namespace HK_EDI.Web.Controllers
{
    public partial class CustomerRoleController : BaseController
	{
		#region Fields

		private readonly ICustomerService _customerService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContext;

		#endregion

		#region Constructors

        public CustomerRoleController(ICustomerService customerService,
            IPermissionService permissionService,
            IWorkContext workContext)
		{
            this._customerService = customerService;
            this._permissionService = permissionService;
            this._workContext = workContext;
		}

		#endregion 

        #region Utilities

        [NonAction]
        protected CustomerRoleModel PrepareCustomerRoleModel(CustomerRole customerRole)
        {
            var model = customerRole.ToModel();
            return model;
        }

        #endregion

        #region Customer roles

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

		public ActionResult List()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
			return View();
		}

		[HttpPost]
		public ActionResult List(DataSourceRequest command)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
            var customerRoles = _customerService.GetAllCustomerRoles(true);
            var gridModel = new DataSourceResult
			{
                Data = customerRoles.Select(PrepareCustomerRoleModel),
                Total = customerRoles.Count()
			};
			return new JsonResult
			{
				Data = gridModel
			};
		}

        public ActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
            var model = new CustomerRoleModel();
            //default values
            model.Active = true;
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(CustomerRoleModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
            if (ModelState.IsValid)
            {
                var customerRole = model.ToEntity();
                _customerService.InsertCustomerRole(customerRole);


                SuccessNotification("添加成功");
                return continueEditing ? RedirectToAction("Edit", new { id = customerRole.Id }) : RedirectToAction("List");
            }

            //If we got this far, something failed, redisplay form
            return View(model);
        }

		public ActionResult Edit(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
            var customerRole = _customerService.GetCustomerRoleById(id);
            if (customerRole == null)
                //No customer role found with the specified id
                return RedirectToAction("List");
		    
            var model = PrepareCustomerRoleModel(customerRole);
            return View(model);
		}

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(CustomerRoleModel model, bool continueEditing)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
            var customerRole = _customerService.GetCustomerRoleById(model.Id);
            if (customerRole == null)
                //No customer role found with the specified id
                return RedirectToAction("List");

            try
            {
                if (ModelState.IsValid)
                {
                    if (customerRole.IsSystemRole && !model.Active)
                        throw new Exception("不能删除系统角色");

                    if (customerRole.IsSystemRole && !customerRole.SystemName.Equals(model.SystemName, StringComparison.InvariantCultureIgnoreCase))
                        throw new Exception("不能编辑系统角色的系统名称");
                    
                    customerRole = model.ToEntity(customerRole);
                    _customerService.UpdateCustomerRole(customerRole);

                    SuccessNotification("修改成功");
                    return continueEditing ? RedirectToAction("Edit", customerRole.Id) : RedirectToAction("List");
                }

                //If we got this far, something failed, redisplay form
                return View(model);
            }
            catch (Exception exc)
            {
                ErrorNotification(exc);
                return RedirectToAction("Edit", new { id = customerRole.Id });
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageCustomers))
                return AccessDeniedView();
            
            var customerRole = _customerService.GetCustomerRoleById(id);
            if (customerRole == null)
                //No customer role found with the specified id
                return RedirectToAction("List");

            try
            {
                _customerService.DeleteCustomerRole(customerRole);

                SuccessNotification("删除成功");
                return RedirectToAction("List");
            }
            catch (Exception exc)
            {
                ErrorNotification(exc.Message);
                return RedirectToAction("Edit", new { id = customerRole.Id });
            }

		}


		#endregion
    }
}
