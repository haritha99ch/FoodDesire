namespace FoodDesire.IMS.Components;
public sealed partial class NewEmployeeDialog : ContentDialog {
    public NewEmployeeViewModel ViewModel { get; set; }
    public NewEmployeeDialog(NewEmployeeViewModel viewModel) {
        InitializeComponent();
        ViewModel = viewModel;
    }
}
