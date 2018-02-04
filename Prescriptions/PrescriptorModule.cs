using Autofac;
using Prescriptions.Database;
using Prescriptions.Services;

namespace Prescriptions
{
    public class PrescriptorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<DatabaseAccess>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<DrugsImportService>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ImportToDBService>().AsImplementedInterfaces().SingleInstance();

        }
    }
}
