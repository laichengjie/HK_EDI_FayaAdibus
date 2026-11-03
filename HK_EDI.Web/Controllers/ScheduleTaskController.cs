using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Services.Security;
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

namespace HK_EDI.Web.Controllers
{
    public class ScheduleTaskController : BaseController
    {
        private readonly IScheduleTaskService _scheduleTaskService;
        private readonly IEDI_ConfigService _iEDI_ConfigService;
        private readonly IPermissionService _permissionService;
        public ScheduleTaskController(
            IPermissionService permissionService,
            IScheduleTaskService scheduleTaskService,
            IEDI_ConfigService  iEDI_ConfigService
            )
        {
            this._permissionService = permissionService;
            this._scheduleTaskService = scheduleTaskService;
            this._iEDI_ConfigService = iEDI_ConfigService;
        }

        public ActionResult ConfigIndex()
        {
            return RedirectToAction("ConfigIndexList");
        }
        public ActionResult ConfigIndexList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConfigIndexList(DataSourceRequest command)
        {
            var models = _iEDI_ConfigService.GetAll()                  
                    .ToList();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [ValidateInput(false )]
        public ActionResult List(string OSInterfaceID, string CompanyID)
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageScheduleTasks))
                return AccessDeniedView();
            ViewData["OSInterfaceID"] = OSInterfaceID??"";
            ViewData["CompanyID"] = CompanyID??"";
            if (!string.IsNullOrEmpty(OSInterfaceID))
            {
                ViewData["Isadmin"] = "1";
            }
            else
            {
                ViewData["Isadmin"] = "0";
            }
            return View();
        }

        [HttpPost]
        public ActionResult List(DataSourceRequest command, string OSInterfaceID, string CompanyID)
        {
            if (string.IsNullOrEmpty(OSInterfaceID))
            {
                var models = _scheduleTaskService.GetAllTasks(true)
                              .Select(PrepareScheduleTaskModel)
                              .ToList();
                var selmodels = models.Where(p => p.Name.Contains("Adibus")).ToList();
                var gridModel = new DataSourceResult
                {
                    Data = selmodels,
                    Total = selmodels.Count
                };

                return Json(gridModel);
            }
            else
            {
                var models = _scheduleTaskService.GetAllTasks(true).Where(t => t.OSInterfaceID == OSInterfaceID && t.CompanyID == CompanyID)
                              .Select(PrepareScheduleTaskModel)
                              .ToList();
                var gridModel = new DataSourceResult
                {
                    Data = models,
                    Total = models.Count
                };
                return Json(gridModel);
            }
          
        }

        [HttpPost]
        public ActionResult Insert(ScheduleTaskModel model, string OSInterfaceID, string CompanyID)
        {
            var entity = new ScheduleTask
            {
              Name=model.Name,
              Seconds=model.Seconds,
              Enabled=model.Enabled,
              StopOnError=model.StopOnError,
              OSInterfaceID=OSInterfaceID,
              CompanyID=CompanyID,
              Type=model.Type??""
            };
            _scheduleTaskService.InsertTask(entity);
            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult TaskUpdate(ScheduleTaskModel model, string OSInterfaceID, string CompanyID)
        {
            if (!ModelState.IsValid)
            {
                return Json(new DataSourceResult { Errors = ModelState.SerializeErrors() });
            }

            var scheduleTask = _scheduleTaskService.GetTaskById(model.Id);
            if (scheduleTask == null)
                return Content("Schedule task cannot be loaded");

            scheduleTask.Name = model.Name;
            scheduleTask.Seconds = model.Seconds;
            scheduleTask.Enabled = model.Enabled;
            scheduleTask.StopOnError = model.StopOnError;
            if (!string.IsNullOrEmpty(OSInterfaceID))
            {
                scheduleTask.Type = model.Type ?? "";
            }
            _scheduleTaskService.UpdateTask(scheduleTask);

            return new NullJsonResult();
        }


        [HttpPost]
        public ActionResult Delete(int id)
        {
            var scheduleTask = _scheduleTaskService.GetTaskById(id);
            if (scheduleTask == null)
                return Content("Schedule task cannot be loaded");
            _scheduleTaskService.DeleteTask(scheduleTask);
            return new NullJsonResult();
        }

        public ActionResult RunNow(int id, string OSInterfaceID, string CompanyID)
        {
            try
            {
                var scheduleTask = _scheduleTaskService.GetTaskById(id);
                if (scheduleTask == null)
                    throw new Exception("Schedule task cannot be loaded");

                var task = new Task(scheduleTask);
                //ensure that the task is enabled
                task.Enabled = true;
                //do not dispose. otherwise, we can get exception that DbContext is disposed
                task.Execute(true, false);
            }
            catch (Exception exc)
            {
              
            }
            if (string.IsNullOrEmpty(OSInterfaceID))
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("List", new { OSInterfaceID = OSInterfaceID, CompanyID = CompanyID });
        }

        [NonAction]
        protected virtual ScheduleTaskModel PrepareScheduleTaskModel(ScheduleTask task)
        {
            var model = new ScheduleTaskModel
            {
                Id = task.Id,
                Name = task.Name,
                Seconds = task.Seconds,
                Enabled = task.Enabled,
                Type=task.Type,
                StopOnError = task.StopOnError,
                LastStartUtc = task.LastStartUtc.HasValue ? task.LastStartUtc.Value.ToString("G") : "",
                LastEndUtc = task.LastEndUtc.HasValue ? task.LastEndUtc.Value.ToString("G") : "",
                LastSuccessUtc = task.LastSuccessUtc.HasValue ? task.LastSuccessUtc.Value.ToString("G") : "",
            };
            return model;
        }
    }
}