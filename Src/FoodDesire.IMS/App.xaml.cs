using System.Windows.Forms;

namespace FoodDesire.IMS;
public partial class App : Microsoft.UI.Xaml.Application {
    public IHost Host { get; }
    public static WindowEx MainWindow { get; } = new MainWindow();
    public static IConfiguration Configuration { get; } = (Current as App)!.Host.Services.GetRequiredService<IConfiguration>();
    public static Account? CurrentUserAccount { get; set; }

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
        try {
            await EnsureAdmin();
            await GetCurrentUser();
        } catch (Exception ex) {
            MessageBox.Show($"Error while ensuring admin: {ex.Message}");
            Exit();
            return;
        }
        base.OnLaunched(args);
        await GetService<IActivationService>().ActivateAsync(args);
    }

    private async Task EnsureAdmin() {
        List<Admin> admins = await GetService<IUserService<Admin>>().GetAll();
        if (!admins.Any()) await GetService<IEmployeePageService>().NewUser<Admin>();
    }

    private async Task GetCurrentUser() {
        string? token = await GetService<ILocalSettingsService>().ReadSettingAsync<string>("CurrentUserToken");
        if (token == null) return;
        IAuthenticationService? authentication = GetService<IAuthenticationService>();
        CurrentUserAccount = await authentication.AcquireAccount(token);
        if (CurrentUserAccount == null) return;
        string? newToken = authentication.AcquireAccessToken();
        if (!string.IsNullOrEmpty(newToken) && !string.Equals(token, newToken)) {
            await GetService<ILocalSettingsService>().SaveSettingAsync<string>("CurrentUserToken", newToken);
        }
    }
}
