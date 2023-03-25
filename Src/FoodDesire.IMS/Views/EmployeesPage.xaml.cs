using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.EntityFrameworkCore;

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

    private async void AddButton_Click(object sender, RoutedEventArgs e) {
        NewEmployeeFormDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<NewEmployeeFormDialog>(XamlRoot);
        ContentDialogResult result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;
        ViewModel.IsUserAdding = true;
        try {
            if (dialog.ViewModel.SelectedRole == Role.Chef)
                await ViewModel.AddNewUser<Chef>();

            if (dialog.ViewModel.SelectedRole == Role.Supplier)
                await ViewModel.AddNewUser<Supplier>();

            if (dialog.ViewModel.SelectedRole == Role.Deliverer)
                await ViewModel.AddNewUser<Deliverer>();
        } catch (DbUpdateException) {
            dialog.Content = "Account Already Exists";
            dialog.PrimaryButtonText = "Ok";
            await dialog.ShowAsync();
        } catch (HttpRequestException) {
            dialog.Content = "No Internet Connection";
            dialog.PrimaryButtonText = "Try Again";
            await dialog.ShowAsync();
        } catch (Exception ex) {
            dialog.Content = ex.Message;
            dialog.PrimaryButtonText = "Ok";
            await dialog.ShowAsync();
        }
        ViewModel.IsUserAdding = false;
    }
}
