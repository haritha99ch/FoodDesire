using CommunityToolkit.WinUI.UI.Controls;

namespace FoodDesire.IMS.Views;
public sealed partial class EmployeesPage : Page {
    public EmployeesViewModel ViewModel {
        get;
    }

    public EmployeesPage() {
        ViewModel = App.GetService<EmployeesViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e) {
        if (e == ListDetailsViewState.Both) {
            ViewModel.EnsureItemSelected();
        }
    }
}
