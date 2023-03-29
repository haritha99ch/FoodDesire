using Windows.ApplicationModel.DataTransfer;

namespace FoodDesire.IMS.Views;

public sealed partial class SuppliesPage : Page {
    public SuppliesViewModel ViewModel { get; }

    public SuppliesPage() {
        InitializeComponent();
        ViewModel = App.GetService<SuppliesViewModel>();
    }

    private void PendingSupplies_DragItemsStarting(object sender, DragItemsStartingEventArgs e) {
        var item = e.Items.FirstOrDefault() as Supply;
        if (item != null) {
            e.Data.SetText(item.Id.ToString());
            e.Data.RequestedOperation = DataPackageOperation.Move;
        }
    }

    private async void UserSupplies_Drop(object sender, DragEventArgs e) {
        if (e.DataView.Contains(StandardDataFormats.Text)) {
            var deferral = e.GetDeferral();
            var id = await e.DataView.GetTextAsync();
            var supplyId = int.Parse(id);

            ViewModel.AcceptSupplyCommand.Execute(supplyId);

            deferral.Complete();
        }
    }

    private void UserSupplies_DragOver(object sender, DragEventArgs e) {
        e.AcceptedOperation = DataPackageOperation.Move;
    }

    private async void UserSupplies_ItemClick(object sender, ItemClickEventArgs e) {
        Supply selectedSupply = (Supply)e.ClickedItem;
        CompleteSupplyDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<CompleteSupplyDialog>(XamlRoot);
        dialog.ViewModel.Supply = selectedSupply;

        await dialog.ShowAsync();

        if (dialog.ViewModel.Result.Equals(SupplyResult.Failed)) return;
        ViewModel.UserSupplies.Remove(ViewModel.UserSupplies.FirstOrDefault(s => s.Id == selectedSupply.Id)!);
    }
}
