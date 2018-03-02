using Autofac;
using Prescriptions.API.Services;
using Prescriptions.GUI.Views;
using System.Configuration;
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

            container.Resolve<IDatabaseAccess>().InitDbConnection(ConfigurationManager.AppSettings.Get("DbHost"));

            container.Resolve<MainWindow>().AddContainer(container).Show();
        }
    }
}
