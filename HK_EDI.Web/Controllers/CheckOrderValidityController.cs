using HK_EDI.Service.Orders;
using HK_EDI.Web.ControllersBaseFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HK_EDI.Web.Controllers
{
    public class CheckOrderValidityController : BaseController
    {
        private readonly IOrderHandle _orderHandle;

        public CheckOrderValidityController(IOrderHandle orderHandle)
        {
            _orderHandle = orderHandle;
        }

        public ActionResult Index()
        {
            return List();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Quest(string billNo, string ossStockID)
        {
            if (string.IsNullOrWhiteSpace(billNo))
            {
                return Json(new { state = false, msg = "BillNo不能为空字符串" });
            }
            if (string.IsNullOrWhiteSpace(ossStockID))
            {
                return Json(new { state = false, msg = "OSInterfaceID不能为空字符串" });
            }
            try
            {
                var resultList = _orderHandle.CheckOrderValidity(billNo, ossStockID);
                return Json(new { state = true, data = resultList });
            }
            catch
            {
                return Json(new { state = false, msg = "服务器错误" });
            }
        }
    }
}