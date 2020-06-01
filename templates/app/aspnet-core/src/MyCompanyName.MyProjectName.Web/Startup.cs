using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace MyCompanyName.MyProjectName.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ModuleLoader.Log("Before AddApplication");
            services.AddApplication<MyProjectNameWebModule>();
            ModuleLoader.Log("After AddApplication");
        }

        public void Configure(IApplicationBuilder app)
        {
            ModuleLoader.Log("Before InitializeApplication");
            app.InitializeApplication();
            ModuleLoader.Log("After InitializeApplication");
        }
    }
}
