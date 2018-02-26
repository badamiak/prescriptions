using Autofac;
using Prescriptions.GUI.ViewModels;
using Prescriptions.GUI.Views;

namespace Prescriptions.GUI
{
    public class GuiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainWindowContext>().AsSelf();
        }
    }
}