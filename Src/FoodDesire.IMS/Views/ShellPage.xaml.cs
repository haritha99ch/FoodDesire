namespace FoodDesire.IMS.Views;
/// <summary>
/// The page where Navigation view components are implemented.
/// </summary>
public sealed partial class ShellPage : Page {
    public ShellViewModel ShellViewModel { get; set; }
    public ShellPage(ShellViewModel shellViewModel) {
        ShellViewModel = shellViewModel;
        InitializeComponent();
    }
}
