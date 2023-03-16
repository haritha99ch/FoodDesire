namespace FoodDesire.IMS.Components;
[INotifyPropertyChanged]
public sealed partial class IngredientListItemControl : UserControl {
    public IngredientDetails? ViewModel { get; set; }
    private IngredientDetails _ingredient {
        get => (IngredientDetails)DataContext;
        set => DataContext = value;
    }
    [ObservableProperty]
    private bool _isLoading = false;
    public IngredientListItemControl() {
        InitializeComponent();
    }
}

