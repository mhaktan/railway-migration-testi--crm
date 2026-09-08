using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace CrmTest
{
    [DependsOn(typeof(CrmTestCoreModule), typeof(AbpAutoMapperModule))]
    public class CrmTestApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAutoMapper().Configurators.Add(cfg =>
            {
                cfg.AddMaps(typeof(CrmTestApplicationModule).GetAssembly());
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CrmTestApplicationModule).GetAssembly());
        }
    }
}
