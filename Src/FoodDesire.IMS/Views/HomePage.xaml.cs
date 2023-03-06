namespace FoodDesire.IMS.Views;
public sealed partial class HomePage : Page {
    public HomeViewModel ViewModel { get; set; }

    public HomePage() {
        ViewModel = App.GetScoped<HomeViewModel>();
        InitializeComponent();
    }
}
