namespace FoodDesire.IMS.Components;
public sealed partial class CompleteSupplyDialog : ContentDialog {
    public CompleteSupplyViewModel ViewModel { get; set; }

    public CompleteSupplyDialog(CompleteSupplyViewModel viewModel) {
        InitializeComponent();
        ViewModel = viewModel;
    }

    private async void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args) {
        if (!args.Result.Equals(ContentDialogResult.Primary)) return;
        args.Cancel = true;
    }
}

