using HK_EDI.Core.Infrastructure;
using HK_EDI.Web.Filter;
using System.Web.Mvc;

namespace HK_EDI.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(EngineContext.Current.Resolve<CheckAuthorizedTimeFilter>());
        }
    }
}