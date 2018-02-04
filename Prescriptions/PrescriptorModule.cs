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

            builder.RegisterType<DatabaseAccess>().AsSelf().SingleInstance();
            builder.RegisterType<DrugsImportService>().AsSelf().SingleInstance();
            builder.RegisterType<ImportToDBService>().AsSelf().SingleInstance();

        }
    }
}
