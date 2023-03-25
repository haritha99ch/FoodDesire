namespace FoodDesire.IMS.Components;
public sealed partial class NewEmployeeFormDialog : ContentDialog {
    public NewEmployeeViewModel ViewModel { get; set; }
    public NewEmployeeFormDialog(NewEmployeeViewModel viewModel) {
        InitializeComponent();
        ViewModel = viewModel;
    }
}
