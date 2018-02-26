using Autofac;
using Prescriptions.GUI.Views;
using System.Windows;

namespace Prescriptions.GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<PrescriptorModule>();
            builder.RegisterModule<GuiModule>();

            var container = builder.Build();

            container.Resolve<MainWindow>().Show();
        }
    }
}
