namespace FoodDesire.IMS.Components;
[INotifyPropertyChanged]
public sealed partial class IngredientListItemControl : UserControl {
    [ObservableProperty]
    private bool _isLoading = false;
    public IngredientListItemControl() {
        InitializeComponent();
    }
}

