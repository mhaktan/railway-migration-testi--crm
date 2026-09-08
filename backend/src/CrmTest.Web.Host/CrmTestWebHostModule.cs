using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CrmTest.EntityFrameworkCore;

namespace CrmTest.Web.Host
{
    [DependsOn(typeof(CrmTestApplicationModule), typeof(CrmTestEntityFrameworkCoreModule), typeof(AbpAspNetCoreModule))]
    public class CrmTestWebHostModule : AbpModule
    {
        public override void PreInitialize()
        {
            // Expose all AppServices as dynamic API controllers
            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(CrmTestApplicationModule).GetAssembly(),
                    moduleName: "app",
                    useConventionalHttpVerbs: true
                );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CrmTestWebHostModule).GetAssembly());
        }
    }
}
