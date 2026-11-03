
using HK_EDI.Core.Domain.Logging;
using HK_EDI.Services.Logging;
using Nop.Admin.Models.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HK_EDI.Web.ExtensionsCommon;
using HK_EDI.Web.Kendoui;
using HK_EDI.Web.ControllersBaseFolder;

namespace HK_EDI.Web.Controllers
{
    public class LogController : BaseController
    {
        private readonly ILogger _logger;
        
        public LogController(
            ILogger logger
            )
        {
            this._logger = logger;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new LogListModel();
            model.AvailableLogLevels = LogLevel.Debug.ToSelectList(false).ToList();
            model.AvailableLogLevels.Insert(0, new SelectListItem { Text = "全部", Value = "0" });
            return View(model);
        }

        [HttpPost]
        public ActionResult LogList(DataSourceRequest command, LogListModel model)
        {

            DateTime? createdOnFromValue = (model.CreatedOnFrom == null) ? null
                            : (DateTime?)model.CreatedOnFrom.Value;

            DateTime? createdToFromValue = (model.CreatedOnTo == null) ? null
                            : (DateTime?)model.CreatedOnTo.Value.AddDays(1);

            LogLevel? logLevel = model.LogLevelId > 0 ? (LogLevel?)(model.LogLevelId) : null;


            var logItems = _logger.GetAllLogs(createdOnFromValue, createdToFromValue, model.Message,
                logLevel, command.Page - 1, command.PageSize);
            var gridModel = new DataSourceResult
            {
                Data = logItems.Select(x => new LogModel
                {
                    Id = x.Id,
                    LogLevel = x.LogLevel.ToString(),
                    ShortMessage = x.ShortMessage,
                    FullMessage = "",
                    IpAddress = x.IpAddress,
                    CustomerId = x.CustomerId,
                    CustomerEmail = x.Customer != null ? x.Customer.Email : null,
                    PageUrl = x.PageUrl,
                    ReferrerUrl = x.ReferrerUrl,
                    CreatedOn = x.CreatedOnUtc
                }),
                Total = logItems.TotalCount
            };

            return Json(gridModel);
        }

        [HttpPost, ActionName("List")]
        [FormValueRequired("clearall")]
        public ActionResult ClearAll()
        {
            _logger.ClearLog();
            return RedirectToAction("List");
        }

        public ActionResult View(int id)
        {

            var log = _logger.GetLogById(id);
            if (log == null)
                return RedirectToAction("List");

            var model = new LogModel
            {
                Id = log.Id,
                LogLevel = log.LogLevel.ToString(),
                ShortMessage = log.ShortMessage,
                FullMessage = log.FullMessage,
                IpAddress = log.IpAddress,
                CustomerId = log.CustomerId,
                CustomerEmail = log.Customer != null ? log.Customer.Email : null,
                PageUrl = log.PageUrl,
                ReferrerUrl = log.ReferrerUrl,
                CreatedOn = log.CreatedOnUtc
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var log = _logger.GetLogById(id);
            if (log == null)
                return RedirectToAction("List");

            _logger.DeleteLog(log);
            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult DeleteSelected(ICollection<int> selectedIds)
        {
            if (selectedIds != null)
            {
                var logItems = _logger.GetLogByIds(selectedIds.ToArray());
                foreach (var logItem in logItems)
                    _logger.DeleteLog(logItem);
            }
            return Json(new { Result = true });
        }

        //public ActionResult Test()
        //{
        //    UnitTestProject1.UnitTest1 test=new UnitTestProject1.UnitTest1();
        //    test.TestMethod1();
        //    return View();
        //}
    }
}