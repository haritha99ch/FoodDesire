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
        ContentDialog dialog = new ContentDialog();
        dialog.XamlRoot = XamlRoot;
        dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
        dialog.Title = $"Request For Supply: {ingredient.Name}";
        dialog.PrimaryButtonText = "Request";
        dialog.CloseButtonText = "Cancel";
        dialog.DefaultButton = ContentDialogButton.Primary;

        dialog.Content = new ViewIngredientItemControl(ingredient);
        dialog.DataContext = ((ViewIngredientItemControl)dialog.Content).ViewModel;
        Binding primaryButtonBinding = new Binding();
        primaryButtonBinding.Path = new PropertyPath(nameof(ViewIngredientItemViewModel.IsRequested));
        primaryButtonBinding.Mode = BindingMode.OneWay;
        primaryButtonBinding.Source = dialog.DataContext;
        dialog.SetBinding(ContentDialog.IsPrimaryButtonEnabledProperty, primaryButtonBinding);

        var result = await dialog.ShowAsync();

    }
}
