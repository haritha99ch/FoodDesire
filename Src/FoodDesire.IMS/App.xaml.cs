using FoodDesire.IMS.Activation;
using FoodDesire.IMS.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodDesire.IMS;
public partial class App : Application {
    public IHost Host { get; }
    public static WindowEx MainWindow { get; } = new MainWindow();

    public App() {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureAppConfiguration((context, config) => {
                string environmentName = context.HostingEnvironment.EnvironmentName;
                AppSettings.Configure.ConfigureEnvironment(config, environmentName);
            })
            .ConfigureServices((context, services) => {
                // Default Activation Handler
                services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

                // Other Activation Handlers
                // Core Services
                string connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;
                Core.Configure.ConfigureServices(services, connectionString);

                // Services
                services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
                services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
                services.AddTransient<INavigationViewService, NavigationViewService>();

                services.AddSingleton<IActivationService, ActivationService>();
                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<INavigationService, NavigationService>();

                // Views and ViewModels
                services.AddTransient<SettingsViewModel>();
                services.AddTransient<SettingsPage>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<HomePage>();
                services.AddTransient<IngredientsViewModel>();
                services.AddTransient<IngredientsPage>();
                services.AddTransient<ShellViewModel>();
                services.AddTransient<ShellPage>();

                // Configuration
                services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
            }).Build();
    }

    public static T GetService<T>()
    where T : class {
        if ((Current as App)!.Host.Services.GetService(typeof(T)) is not T service) {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }
        return service;
    }

    public static T GetScoped<T>() where T : class {
        using (IServiceScope? scope = (Current as App)!.Host.Services.CreateScope()) {
            T? service = scope.ServiceProvider.GetService<T>()
                ?? throw new ArgumentException($"Service {typeof(T)} needs to be registered.");
            return service;
        }
    }

    protected async override void OnLaunched(LaunchActivatedEventArgs args) {
        base.OnLaunched(args);
        await GetService<IActivationService>().ActivateAsync(args);
    }
}
