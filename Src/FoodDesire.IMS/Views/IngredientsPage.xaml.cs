using AutoMapper;
using FoodDesire.IMS.Components;
using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.Foundation;

namespace FoodDesire.IMS.Views;
public sealed partial class IngredientsPage : Page {
    public IngredientsViewModel? ViewModel { get; }
    private IngredientDetails? _ingredientDetails { get; set; }

    public IngredientsPage() {
        InitializeComponent();
        ViewModel = App.GetService<IngredientsViewModel>();
        DataContext = ViewModel;
    }

    private void IngredientList_ItemClick(object sender, ItemClickEventArgs e) {
        _ingredientDetails = (IngredientDetails)e.ClickedItem;

        var listViewItem = IngredientList.ContainerFromItem(e.ClickedItem) as GridViewItem;
        var index = listViewItem!.TabIndex;
        var transform = listViewItem.TransformToVisual(null);
        var position = transform.TransformPoint(new Point(0, 0));

        var options = new FlyoutShowOptions {
            Position = position
        };

        CommandBarFlyout.ShowAt(null, options);
    }

    private async void NewIngredient_Click(object sender, RoutedEventArgs e) {
        NewIngredientFormDialog dialog = new NewIngredientFormDialog() {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
        };
        var result = await dialog.ShowAsync();
        ViewModel!.IsLoading = true;
        if (result == ContentDialogResult.Primary) {
            Ingredient? ingredient = await dialog.ViewModel.CreateIngredient();
            ViewModel.NewIngredient(ingredient);
        }
        ViewModel.IsLoading = false;
    }

    private async void RequestButton_Click(object sender, RoutedEventArgs e) {
        CommandBarFlyout.Hide();
        RequestIngredientDialog dialog = new RequestIngredientDialog(_ingredientDetails!.Id) {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
        };

        var result = await dialog.ShowAsync();
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e) {
        CommandBarFlyout.Hide();
        EditIngredientDialog dialog = new EditIngredientDialog(_ingredientDetails!.Id) {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
        };
        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;
        Ingredient ingredient = await dialog.ViewModel.EditIngredient();
        int index = ViewModel!.IngredientsDetail.IndexOf(_ingredientDetails);
        _ingredientDetails = App.GetService<IMapper>().Map<IngredientDetails>(ingredient);
        ViewModel.IngredientsDetail[index] = _ingredientDetails;

    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
        ContentDialog dialog = new() {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            Title = "Delete Ingredient",
            Content = $"Are you sure you want to delete this ingredient {_ingredientDetails!.Name}?"
        };
        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;
        ViewModel!.DeleteIngredient(_ingredientDetails.Id);
    }
}
