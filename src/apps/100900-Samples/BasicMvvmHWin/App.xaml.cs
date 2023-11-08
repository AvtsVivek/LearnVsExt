using BasicMvvmHWin.ViewModels;
using BasicMvvmHWin.Views;
using BasicMvvmHWin.Infra;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace BasicMvvmHWin
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                // Configure your services here as shown below.
                services.AddSingleton<MainWindow>();
                //services.AddTransient<IDataModel, DataModel>();
            }).Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();

            // We are setting the data context directly in the xaml itself. 
            // Something like the following
            /*
            <Window.DataContext>
                <localVms:MainWindowViewModel/>
            </Window.DataContext>
            */
            // So we dont need the following.
            // startupForm!.DataContext = new MainWindowViewModel(new DataModel { Data = "Placeholder" });
            // startupForm!.DataContext = new MainWindowViewModel();
            startupForm!.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }


        public static IHost? AppHost { get; private set; }
    }
}
