using System.Collections.Generic;
using HK_EDI.Core.Domain.Customers;
using HK_EDI.Core.Domain.Security;


namespace HK_EDI.Services.Security
{
    /// <summary>
    /// Standard permission provider
    /// </summary>
    public partial class StandardPermissionProvider : IPermissionProvider
    {
        //admin area permissions
        public static readonly PermissionRecord AccessAdminPanel = new PermissionRecord { Name = "访问后台", SystemName = "AccessAdminPanel", Category = "Standard" };
        public static readonly PermissionRecord ManageCustomers = new PermissionRecord { Name = "管理用户", SystemName = "ManageCustomers", Category = "Customers" };
        public static readonly PermissionRecord ManageOrders = new PermissionRecord { Name = "管理订单", SystemName = "ManageOrders", Category = "Orders" };
        public static readonly PermissionRecord ManageSettings = new PermissionRecord { Name = "管理配置", SystemName = "ManageSettings", Category = "Configuration" };
        public static readonly PermissionRecord ManageSystemLog = new PermissionRecord { Name = "查看系统日志", SystemName = "ManageSystemLog", Category = "Configuration" };
        public static readonly PermissionRecord ManageScheduleTasks = new PermissionRecord { Name = "管理任务计划", SystemName = "ManageScheduleTasks", Category = "Configuration" };
        public static readonly PermissionRecord ManageAcl = new PermissionRecord { Name = "管理用户访问权限", SystemName = "ManageACL", Category = "Configuration" };
        public static readonly PermissionRecord AccessWebApiService = new PermissionRecord { Name = "访问Web API", SystemName = "AccessWebApiService", Category = "Plugin" };
        public static readonly PermissionRecord CancelOrders = new PermissionRecord { Name = "撤销订单", SystemName = "CancelOrders", Category = "Configuration" };

        public virtual IEnumerable<PermissionRecord> GetPermissions()
        {
            return new[] 
            {
                AccessAdminPanel,
                ManageCustomers,
                ManageOrders,
                ManageSettings,
                ManageSystemLog,
                ManageScheduleTasks,
                ManageAcl,
                AccessWebApiService,
                CancelOrders
            };
        }

        public virtual IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[] 
            {
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Administrators,
                    PermissionRecords = new[] 
                    {
                        AccessAdminPanel,
                        ManageCustomers,
                        ManageOrders,
                        ManageSettings,
                        ManageSystemLog,
                        ManageScheduleTasks,
                        ManageAcl
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Guests,
                    PermissionRecords = new[] 
                    {
                        AccessAdminPanel,
                        ManageOrders,
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Registered,
                    PermissionRecords = new[] 
                    {
                        AccessAdminPanel,
                        ManageOrders,
                        ManageScheduleTasks
                    }
                },
                new DefaultPermissionRecord 
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.WebAPI,
                    PermissionRecords = new[] 
                    {
                        AccessWebApiService
                    }
                }
            };
        }
    }
}