using FoodDesire.IMS.Services;
using FoodDesire.IMS.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodDesire.IMS;
/// <summary>
/// The App.xaml.cs will uses Microsoft.Extensions.DependencyInjection to register the DbContext and other domain services.
/// </summary>
public partial class App : Application {
    public static WindowEx MainWindow = new MainWindow();
    public IHost Host { get; }

    public App() {
        InitializeComponent();
        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) => {
                string environmentName = context.HostingEnvironment.EnvironmentName;
                AppSettings.Configure.ConfigureEnvironment(config, environmentName);
                config.AddUserSecrets<App>();
            })
            .ConfigureServices((context, services) => {
                //Configure Domain services here
                string connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;
                DAL.Configure.ConfigureServices(services, connectionString);
                Core.Configure.ConfigureServices(services);

                //Configure IMS services here
                services.AddTransient<INavigationViewService, NavigationViewService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IPageService, PageService>();

                services.AddTransient<IHomeService, HomeService>();

                //Pages
                services.AddTransient<ShellPage>();

                //ViewModels
                services.AddTransient<ShellViewModel>();
            }).Build();
    }

    public static T GetService<T>() where T : class {
        if (!((Current as App)!.Host.Services.GetService(typeof(T)) is not T service)) return service;
        throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
        MainWindow.Content = GetService<ShellPage>();
        MainWindow.Activate();
        INavigationService navigationService = GetService<INavigationService>();
    }
}
