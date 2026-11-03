using Autofac;
using Autofac.Builder;
using Autofac.Core;
using HK_EDI.Core.Caching;
using HK_EDI.Core.Data;
using HK_EDI.Core.Infrastructure;
using HK_EDI.Core.Infrastructure.DependencyManagement;
using HK_EDI.Data;
using HK_EDI.Services.Customers;
using System;
namespace HK_EDI.WebApi
{
    public class DependancyRegistrar : IDependencyRegistrar
    {
        public int Order
        {
            get
            {
                return 1;
            }
        }

        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            

        }
    }
}
