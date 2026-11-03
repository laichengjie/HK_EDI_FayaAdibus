using HK_EDI.Core.Data;
using HK_EDI.Core.Domain.Tasks;
using HK_EDI.Service.Tasks;
using HK_EDI.Web.ControllersBaseFolder;
using HK_EDI.Web.Kendoui;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Controllers
{
    public class TaskRunTimeController : BaseController
    {
        private ITaskRunTimeService _taskRunTimeService;

        public TaskRunTimeController(ITaskRunTimeService taskRunTimeService)
        {
            _taskRunTimeService = taskRunTimeService;
        }


        public ActionResult Index()
        {
            return View("List");
        }

        [HttpPost]
        public ActionResult List()
        {
            var models = _taskRunTimeService.GetAll();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult TaskUpdate(TaskRunTime data)
        {
            DateTime datetime;
            if (data != null&&FormatDate(Request.Form["LastTime"],false,out datetime))
            {
                //data.LastTime = datetime;
                var entity = _taskRunTimeService.GetByKey(data.Key);
                entity.LastTime = datetime;
                _taskRunTimeService.Update(entity);
            }
            var models = _taskRunTimeService.GetAll();
            var gridModel = new DataSourceResult
            {
                Data = models,
                Total = models.Count
            };
            return Json(gridModel);
        }

        /// <summary>
        /// 只针对Wed Sep 30 2015 14:32:43 GMT+0800 (China Standard Time)这类型的字符串进行日期解析
        /// </summary>
        /// <param name="dateTime">待解析字符串</param>
        /// <param name="isRetainTimezone">是否解析时区，true会加上时区的小时数</param>
        private bool FormatDate(string dateTime, bool isRetainTimezone, out DateTime result)
        {
            result = DateTime.MinValue;
            if (dateTime == null ||string.IsNullOrWhiteSpace(dateTime))
            {
                return false;
            }
            string format = null;
            if (isRetainTimezone)
            {
                dateTime = dateTime.Substring(0, dateTime.LastIndexOf('('));
                format = "ddd MMM dd yyyy HH:mm:ss GMT+0800";
            }
            else
            {
                dateTime = dateTime.Substring(0, dateTime.LastIndexOf("G", StringComparison.OrdinalIgnoreCase));
                format = "ddd MMM dd yyyy HH:mm:ss";
            }
            return DateTime.TryParseExact(dateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.AllowTrailingWhite, out result);
        }
    }
}