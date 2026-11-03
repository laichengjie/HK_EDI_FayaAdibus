using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Linq;
using System.Web.Http.Description;
using SDammann.WebApi.Versioning;
using Autofac;
using System.Reflection;
using Autofac.Integration.WebApi;
using HK_EDI.WebApi.Logger;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using HK_EDI.WebApi.Security;
using System.Net.Http.Formatting;
using HK_EDI.Core.Infrastructure;
using System.Text;

namespace HK_EDI.WebApi
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            var config = GlobalConfiguration.Configuration;
            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApiWithVersion",
            //    routeTemplate: "api/v{version}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApiWithVersionAndAction",
                routeTemplate: "api/v{version}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CheckToken",
                routeTemplate: "api/CheckToken",
                defaults: new { controller = "Default", action = "CheckToken", id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
               name: "UpdateOrderStatus",
               routeTemplate: "api/UpdateOrderStatus",
               defaults: new { controller = "WMS", action = "UpdateOrderStatus", id = RouteParameter.Optional }
           );

            config.Routes.MapHttpRoute(
               name: "ZSDUpdateOrderStatus",
               routeTemplate: "api/zsd/UpdateOrderStatus",
               defaults: new { controller = "WMSZSD", action = "UpdateOrderStatus", id = RouteParameter.Optional }
           );
            config.Routes.MapHttpRoute(
      name: "SavePurReject",
      routeTemplate: "api/SavePurReject",
      defaults: new { controller = "Adibus", action = "SavePurReject", id = RouteParameter.Optional }
  );
            config.Routes.MapHttpRoute(
                name: "DefaultApiForPlugin",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            



            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // We have to register services specifically for the API calls!
            //builder.RegisterType<CategoryService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            //Update existing, don't create a new container
            builder.Update(EngineContext.Current.ContainerManager.Container);

            //Feed the current container to the AutofacWebApiDependencyResolver
            var resolver = new AutofacWebApiDependencyResolver(EngineContext.Current.ContainerManager.Container);
            config.DependencyResolver = resolver;


            // enable API versioning
            config.Services.Replace(typeof(IApiExplorer), new VersionedApiExplorer(GlobalConfiguration.Configuration));
            config.Services.Replace(typeof(IHttpControllerSelector), new RouteVersionedControllerSelector(GlobalConfiguration.Configuration));

            //exception logger 
            config.Services.Add(typeof(IExceptionLogger), new SimpleExceptionLogger());
            //exception handler
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            //config.Filters.Add(new UnifyResultAttribute());
            config.Filters.Add(new UnifyErrorHandleAttribute());

            //we will get JSON by default, but it will still allow you to return XML if you pass text/xml as the request Accept header
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            var appJsonType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appJsonType);

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new HK_EDI.WebApi.App_Start.CustomNamespaceXmlFormatter { UseXmlSerializer = true });

            config.Formatters.XmlFormatter.AddQueryStringMapping("$format", "xml", "application/xml");
            config.Formatters.JsonFormatter.AddQueryStringMapping("$format", "json", "application/json");


            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
