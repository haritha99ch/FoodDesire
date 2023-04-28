using CommunityToolkit.WinUI.UI.Controls;

namespace FoodDesire.IMS.Views;
public sealed partial class DeliveriesPage : Page {
    public DeliveriesViewModel ViewModel {
        get;
    }

    public DeliveriesPage() {
        ViewModel = App.GetService<DeliveriesViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e) {
        if (e == ListDetailsViewState.Both) {
            ViewModel.EnsureItemSelected();
        }
    }
}
