using MyCompanyName.MyProjectName.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace MyCompanyName.MyProjectName.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(MyProjectNameEntityFrameworkCoreDbMigrationsModule),
        typeof(MyProjectNameApplicationContractsModule)
        )]
    public class MyProjectNameDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
