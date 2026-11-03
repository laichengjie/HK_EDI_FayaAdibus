using AutoMapper;
using HK_EDI.Core.Domain.Customers;
using HK_EDI.Core.Domain.Logging;
using HK_EDI.Core.Domain.Orders;
using HK_EDI.Core.Domain.SettingModel;
using HK_EDI.Service.WMS;
using HK_EDI.Service.WMS.CommonModels;
using HK_EDI.Service.WMS.SKU360.Models;
using HK_EDI.Web.Models.Customers;
using HK_EDI.Web.Models.Logging;
using HK_EDI.Web.Models.Setting;
using Nop.Admin.Models.Logging;


namespace HK_EDI.Web.Infrastructure
{
    public class AutoMapperStartup
    {
        public static void Execute()
        {
            //TODO remove 'CreatedOnUtc' ignore mappings because now presentation layer models have 'CreatedOn' property and core entities have 'CreatedOnUtc' property (distinct names)
            

            //logs
            Mapper.CreateMap<Log, LogModel>()
                .ForMember(dest => dest.CustomerEmail, mo => mo.Ignore())
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<LogModel, Log>()
                .ForMember(dest => dest.CreatedOnUtc, mo => mo.Ignore())
                .ForMember(dest => dest.LogLevelId, mo => mo.Ignore())
                .ForMember(dest => dest.Customer, mo => mo.Ignore());
            //ActivityLogType
            Mapper.CreateMap<ActivityLogTypeModel, ActivityLogType>()
                .ForMember(dest => dest.SystemKeyword, mo => mo.Ignore());
            Mapper.CreateMap<ActivityLogType, ActivityLogTypeModel>()
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<ActivityLog, ActivityLogModel>()
                .ForMember(dest => dest.ActivityLogTypeName, mo => mo.MapFrom(src => src.ActivityLogType.Name))
                .ForMember(dest => dest.CustomerEmail, mo => mo.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.CreatedOn, mo => mo.Ignore())
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            
            //customer roles
            Mapper.CreateMap<CustomerRole, CustomerRoleModel>()
                .ForMember(dest => dest.CustomProperties, mo => mo.Ignore());
            Mapper.CreateMap<CustomerRoleModel, CustomerRole>()
                .ForMember(dest => dest.PermissionRecords, mo => mo.Ignore());

            AutoMapper.Mapper.CreateMap<StockQueryModel,HK_EDI.Service.WMS.HK.Item>();

           //SKU360
            AutoMapper.Mapper.CreateMap<BaseDataModel, Product>();
            AutoMapper.Mapper.CreateMap<NormalOrderModel, B2BOrders>();
            AutoMapper.Mapper.CreateMap<NormalOrderDetailModel, B2BOrdersDetail>();
            AutoMapper.Mapper.CreateMap<NormalOrderModel, Purchases>();
            AutoMapper.Mapper.CreateMap<NormalOrderDetailModel, PurchaseDetail>();
            AutoMapper.Mapper.CreateMap<JobOrderTrace, CancelOrder>();
            AutoMapper.Mapper.CreateMap<JobOrderTrace, CancelPurchaseOrder>();
            AutoMapper.Mapper.CreateMap<NormalOrderModel, BackOrder>();
            AutoMapper.Mapper.CreateMap<NormalOrderDetailModel, BackOrderDetail>();
            AutoMapper.Mapper.CreateMap<Product, SuccessDTO>();
            AutoMapper.Mapper.CreateMap<Product, FailDTO>();

            AutoMapper.Mapper.CreateMap<NormalOrderModel, HK_EDI.Service.WMS.SKU360.Models.B2COrders>();
            AutoMapper.Mapper.CreateMap<NormalOrderDetailModel, HK_EDI.Service.WMS.SKU360.Models.B2COrdersDetail>();

            AutoMapper.Mapper.CreateMap<NormalOrderModel, HK_EDI.Service.WMS.SKU360.Models.JITB2BOrders>();
            AutoMapper.Mapper.CreateMap<NormalOrderDetailModel, HK_EDI.Service.WMS.SKU360.Models.JITB2BOrdersDetail>();

            AutoMapper.Mapper.CreateMap<NormalOrderModel, HK_EDI.Service.WMS.SKU360.Models.BackNetOrder>();
            
            //获取装箱单
            AutoMapper.Mapper.CreateMap<FayaAdibusGetPackingListSet, FayaAdibusGetPackingListSettingModel>();
            AutoMapper.Mapper.CreateMap<FayaAdibusGetPackingListSettingModel, FayaAdibusGetPackingListSet>();
            //获取采购发货
            AutoMapper.Mapper.CreateMap<FayaAdibusGetEstimatedShipmentSet, FayaAdibusGetEstimatedShipmentSettingModel>();
            AutoMapper.Mapper.CreateMap<FayaAdibusGetEstimatedShipmentSettingModel, FayaAdibusGetEstimatedShipmentSet>();
            //获取补货单
            AutoMapper.Mapper.CreateMap<fayaadibusReplenishmentOrderSet, fayaadibusReplenishmentOrderSetSettingModel>();
            AutoMapper.Mapper.CreateMap<fayaadibusReplenishmentOrderSetSettingModel, fayaadibusReplenishmentOrderSet>();

        }
        
        public int Order
        {
            get { return 0; }
        }
    }
}
