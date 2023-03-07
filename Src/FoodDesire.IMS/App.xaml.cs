using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FoodDesire.IMS;
public partial class App : Application {
    public IHost Host { get; }
    public static WindowEx MainWindow { get; } = new MainWindow();
    public static IConfiguration Configuration { get; } = (Current as App)!.Host.Services.GetRequiredService<IConfiguration>();

    public App() {
        InitializeComponent();
        Host = Microsoft.Extensions.Hosting.Host
            .CreateDefaultBuilder()
            .UseContentRoot(AppContext.BaseDirectory)
            .ConfigureAppConfiguration(AppConfigurator.Configure)
            .ConfigureServices(AppConfigurator.Configure)
            .Build();
    }

    public static T GetService<T>() where T : class {
        if ((Current as App)!.Host.Services.GetService(typeof(T)) is not T service) {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within AppConfigurator.cs.");
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
