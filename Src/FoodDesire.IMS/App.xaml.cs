using Microsoft.UI.Xaml;

namespace FoodDesire.IMS;
/// <summary>
/// The App.xaml.cs will uses Microsoft.Extensions.DependencyInjection to register the DbContext and other domain services.
/// </summary>
public partial class App : Application {
    public App() {
        InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args) {
        m_window = new MainWindow();
        m_window.Activate();
    }

    private Window m_window;
}
