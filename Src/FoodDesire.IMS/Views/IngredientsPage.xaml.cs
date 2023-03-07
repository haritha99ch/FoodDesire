namespace FoodDesire.IMS.Views;

public sealed partial class IngredientsPage : Page {
    public IngredientsViewModel ViewModel { get; }

    public IngredientsPage() {
        ViewModel = App.GetService<IngredientsViewModel>();
        InitializeComponent();
    }
}
