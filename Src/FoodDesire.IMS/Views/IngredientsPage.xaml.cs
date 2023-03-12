using FoodDesire.IMS.Controls;
using Microsoft.UI.Xaml.Data;

namespace FoodDesire.IMS.Views;

public sealed partial class IngredientsPage : Page {
    public IngredientsViewModel ViewModel { get; }

    public IngredientsPage() {
        ViewModel = App.GetService<IngredientsViewModel>();
        DataContext = ViewModel;
        InitializeComponent();
    }

    private async void IngredientList_ItemClick(object sender, ItemClickEventArgs e) {
        IngredientDetails ingredient = (IngredientDetails)e.ClickedItem;
        ContentDialog dialog = new ContentDialog {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            Title = $"Request For Supply: {ingredient.Name}",
            PrimaryButtonText = "Request",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            Content = new ViewIngredientItemControl(ingredient),
        };
        dialog.DataContext = ((ViewIngredientItemControl)dialog.Content).ViewModel;
        Binding primaryButtonBinding = new Binding {
            Path = new PropertyPath(nameof(ViewIngredientItemViewModel.IsRequested)),
            Mode = BindingMode.OneWay,
            Source = dialog.DataContext,
        };
        dialog.SetBinding(ContentDialog.IsPrimaryButtonEnabledProperty, primaryButtonBinding);

        var result = await dialog.ShowAsync();
    }

    private async void NewIngredient_Click(object sender, RoutedEventArgs e) {
        ContentDialog dialog = new ContentDialog {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            Title = "New Ingredient",
            PrimaryButtonText = "Create",
            CloseButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            Content = new NewIngredientFormControl(),
        };
        dialog.DataContext = ((NewIngredientFormControl)dialog.Content).ViewModel;
        Binding primaryButtonBinding = new Binding {
            Path = new PropertyPath(nameof(NewIngredientFormViewModel.CanBeCreated)),
            Mode = BindingMode.OneWay,
            Source = dialog.DataContext,
        };
        dialog.SetBinding(ContentDialog.IsPrimaryButtonEnabledProperty, primaryButtonBinding);

        var result = await dialog.ShowAsync();
    }
}
