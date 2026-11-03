using HK_EDI.Core;
using HK_EDI.Core.Data;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Security;
using HK_EDI.Services.Tasks;
using HK_EDI.Web.Infrastructure;
using HK_EDI.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HK_EDI.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //initialize engine context
            EngineContext.Initialize(false);
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            WebApiConfig.Register();

            AreaRegistration.RegisterAllAreas();
            System.Web.Mvc.ViewEngines.Engines.Clear();
            System.Web.Mvc.ViewEngines.Engines.Add(new RazorViewEngine());

            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();

            AutoMapperStartup.Execute();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //ignore static resources
            //var webHelper = EngineContext.Current.Resolve<IWebHelper>();
        }

        protected void Application_End(object sender, EventArgs e)
        {
            var logService = EngineContext.Current.Resolve<ILogger>();
            try
            {
                string activeUrl = System.Configuration.ConfigurationManager.AppSettings["ActiveUrl"];
                if (activeUrl.IsNotEmpty())
                {
                    using (var wc = new WebClient())
                    {
                        wc.DownloadString(activeUrl);
                    }
                    logService.Information("重新激活网站");
                }
            }
            catch (Exception ex)
            {
                logService.Error("激活网站失败："+ex.Message);
            }
        }
    }
}
