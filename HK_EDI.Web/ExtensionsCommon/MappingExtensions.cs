using System;
using AutoMapper;
using Nop.Admin.Models.Logging;
using HK_EDI.Core.Domain.Logging;
using HK_EDI.Web.Models.Logging;
using HK_EDI.Web.Models.Customers;
using HK_EDI.Core.Domain.Customers;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Web.Models.Setting;
using HK_EDI.Core.Configuration;

namespace HK_EDI.Web.ExtensionsCommon
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return Mapper.Map(source, destination);
        }

        #region Customer roles

        //customer roles
        public static CustomerRoleModel ToModel(this CustomerRole entity)
        {
            return entity.MapTo<CustomerRole, CustomerRoleModel>();
        }

        public static CustomerRole ToEntity(this CustomerRoleModel model)
        {
            return model.MapTo<CustomerRoleModel, CustomerRole>();
        }

        public static CustomerRole ToEntity(this CustomerRoleModel model, CustomerRole destination)
        {
            return model.MapTo(destination);
        }

        #endregion

        #region Log

        public static LogModel ToModel(this Log entity)
        {
            return entity.MapTo<Log, LogModel>();
        }

        public static Log ToEntity(this LogModel model)
        {
            return model.MapTo<LogModel, Log>();
        }

        public static Log ToEntity(this LogModel model, Log destination)
        {
            return model.MapTo(destination);
        }

        public static ActivityLogTypeModel ToModel(this ActivityLogType entity)
        {
            return entity.MapTo<ActivityLogType, ActivityLogTypeModel>();
        }

        public static ActivityLogModel ToModel(this ActivityLog entity)
        {
            return entity.MapTo<ActivityLog, ActivityLogModel>();
        }

        #endregion
        

        #region Settings

        public static TModel ToModel<TModel>(this ISettings entity)
        {
            return entity.MapTo<ISettings, TModel>();
        }
        public static TSettings ToEntity<TSettings>(this BaseModel model, TSettings destination)
        {
            return model.MapTo(destination);
        }

        #endregion



    }
}