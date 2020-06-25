using System;
using Volo.Abp.ObjectExtending.Modularity;

namespace Volo.Abp.TenantManagement.ObjectExtending
{
    public class TenantManagementModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public TenantManagementModuleExtensionConfiguration ConfigureTenant(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                TenantManagementModuleExtensionConsts.EntityNames.Tenant,
                configureAction
            );
        }
        
        public TenantManagementModuleExtensionConfiguration ConfigureTenantConnectionString(
            Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                TenantManagementModuleExtensionConsts.EntityNames.TenantConnectionString,
                configureAction
            );
        }
    }
}