using FoodDesire.IMS.Components;

namespace FoodDesire.IMS.Views;

public sealed partial class IngredientsPage : Page {
    public IngredientsViewModel ViewModel { get; }

    public IngredientsPage() {
        InitializeComponent();
        ViewModel = App.GetService<IngredientsViewModel>();
        DataContext = ViewModel;
    }

    private async void IngredientList_ItemClick(object sender, ItemClickEventArgs e) {
        IngredientDetails ingredient = (IngredientDetails)e.ClickedItem;
        RequestIngredientDialog dialog = new RequestIngredientDialog(ingredient) {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
        };

        var result = await dialog.ShowAsync();
    }

    private async void NewIngredient_Click(object sender, RoutedEventArgs e) {
        NewIngredientFormDialog dialog = new NewIngredientFormDialog() {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
        };
        var result = await dialog.ShowAsync();
        ViewModel.IsLoading = true;
        if (result == ContentDialogResult.Primary) {
            Ingredient? ingredient = await dialog.ViewModel.CreateIngredient();
            ViewModel.NewIngredient(ingredient);
        }
        ViewModel.IsLoading = false;
    }
}
