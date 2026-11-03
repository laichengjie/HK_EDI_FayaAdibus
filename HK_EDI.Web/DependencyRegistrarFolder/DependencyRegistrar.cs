using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using HK_EDI.Core;
using HK_EDI.Core.Caching;
using HK_EDI.Core.Configuration;
using HK_EDI.Core.Data;
using HK_EDI.Core.Fakes;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Core.Infrastructure.DependencyManagement;
using HK_EDI.Data;
using HK_EDI.Service.EDI_Config;
using HK_EDI.Service.FayaAdibus.CreateCGOrder;
using HK_EDI.Service.FayaAdibus.CreateReceipt;
using HK_EDI.Service.FayaAdibus.GetEstimatedShipment;
using HK_EDI.Service.FayaAdibus.GetPackingList;
using HK_EDI.Service.FayaAdibus.GetReplenishmentOrder;
using HK_EDI.Service.FayaAdibus.Pur_ReceiptData;
using HK_EDI.Service.Orders;
using HK_EDI.Service.ShopModel;
using HK_EDI.Service.StockModel;
using HK_EDI.Service.Tasks;
using HK_EDI.Services.Authentication;
using HK_EDI.Services.Customers;
using HK_EDI.Services.Logging;
using HK_EDI.Services.Security;
using HK_EDI.Services.Tasks;
using HK_EDI.Web.Filter;
using HK_EDI.Web.WebWorkContextFolder;
using Nop.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace HK_EDI.Web
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                HttpContext.Current != null ?
                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
                (new FakeHttpContext("~/") as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();


            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //cache manager
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_static").SingleInstance();
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nop_cache_per_request").InstancePerLifetimeScope();

            //pass MemoryCacheManager as cacheManager (cache settings between requests)
            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();

            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            string connectionString = string.Empty;
            string connectionStringRead = string.Empty;
            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();
                connectionString = dataProviderSettings.DataConnectionString;
                builder.Register<IDbContext>(c => new HK_EDI_ObjectContext(connectionString)).InstancePerLifetimeScope();
            }
            else
            {
                connectionString = dataSettingsManager.LoadSettings().DataConnectionString;
                builder.Register<IDbContext>(c => new HK_EDI_ObjectContext(connectionString)).InstancePerLifetimeScope();
            }

            var dataProviderSettings_Read = dataSettingsManager.LoadSettings_Read();
            if (dataProviderSettings_Read != null && dataProviderSettings_Read.IsValid())
            {
                connectionStringRead = dataProviderSettings_Read.DataConnectionString;
            }
            DBHelper.ConnectionString = connectionString;
            DBHelper.ConnectionStringRead = connectionStringRead;

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();

            //零售汇总补传
            builder.RegisterType<ShopDataService>().As<IShopDataService>().InstancePerLifetimeScope();
            builder.RegisterType<StockDataService>().As<IStockDataService>().InstancePerLifetimeScope();

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<TaskRunTimeService>().As<ITaskRunTimeService>().InstancePerLifetimeScope();
            builder.RegisterType<EDI_ConfigService>().As<IEDI_ConfigService>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<StandardPermissionProvider>().As<IPermissionProvider>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerRegistrationService>().As<ICustomerRegistrationService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();
            builder.RegisterType<JobOrderTraceService>().As<IJobOrderTraceService>().InstancePerLifetimeScope();
            builder.RegisterType<JobOrderTraceDetailService>().As<IJobOrderTraceDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<BasCompanyService>().As<IBasCompanyService>().WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterType<PostSkuErrorRecordsService>().As<IPostSkuErrorRecordsService>().InstancePerLifetimeScope();
            builder.RegisterType<PostSkuRecordsDetailService>().As<IPostSkuRecordsDetailService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderHandle>().As<IOrderHandle>().InstancePerLifetimeScope();

            builder.RegisterType<CheckAuthorizedTimeFilter>().SingleInstance();
            builder.RegisterType<AdibusPosSaleAsicsService>().As<IAdibusPosSaleAsicsService>().InstancePerLifetimeScope();

            //builder.RegisterType<ReceiptDataApi>().As<IReceiptDataApi>().InstancePerLifetimeScope();
            builder.RegisterType<CreateReceiptService>().As<ICreateReceiptService>().InstancePerLifetimeScope();
            builder.RegisterType<GetEstimatedShipmentService>().As<IGetEstimatedShipmentService>().InstancePerLifetimeScope();
            builder.RegisterType<GetPackingListService>().As<IGetPackingListService>().InstancePerLifetimeScope();
            builder.RegisterType<GetReplenishmentOrderService>().As<IGetReplenishmentOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<CreateCGOrderService>().As<ICreateCGOrderService>().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Autofac.Core.Service service,
                Func<Autofac.Core.Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    //var currentStoreId = c.Resolve<IStoreContext>().CurrentStore.Id;
                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
                    //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>(0);
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }
}