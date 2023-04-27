using CommunityToolkit.WinUI.UI.Controls;
namespace FoodDesire.IMS.Views;

public sealed partial class FoodItemsPage : Page {
    public FoodItemsViewModel ViewModel {
        get;
    }

    public FoodItemsPage() {
        ViewModel = App.GetService<FoodItemsViewModel>();
        DataContext = ViewModel;
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e) {
        if (e == ListDetailsViewState.Both) {
            ViewModel.EnsureItemSelected();
        }
    }
}
