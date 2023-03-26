﻿using Microsoft.UI.Xaml.Controls.Primitives;
using Windows.Foundation;

namespace FoodDesire.IMS.Views;
public sealed partial class IngredientsPage : Page {
    public IngredientsViewModel ViewModel { get; }
    private IngredientDetails? _selectedIngredient { get; set; }

    public IngredientsPage() {
        InitializeComponent();
        ViewModel = App.GetService<IngredientsViewModel>();
        DataContext = ViewModel;
    }

    private void IngredientList_ItemClick(object sender, ItemClickEventArgs e) {
        _selectedIngredient = (IngredientDetails)e.ClickedItem;

        var listViewItem = IngredientList.ContainerFromItem(e.ClickedItem) as GridViewItem;
        var transform = listViewItem!.TransformToVisual(null);
        var position = transform.TransformPoint(new Point(0, 0));

        var options = new FlyoutShowOptions {
            Position = position
        };

        CommandBarFlyout.ShowAt(null, options);
    }

    private async void NewIngredient_Click(object sender, RoutedEventArgs e) {
        NewIngredientFormDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<NewIngredientFormDialog>(XamlRoot);
        var result = await dialog.ShowAsync();
        ViewModel.IsLoading = true;
        if (result == ContentDialogResult.Primary) {
            Ingredient? ingredient = await dialog.ViewModel.CreateIngredient();
            ViewModel.NewIngredient(ingredient);
        }
        ViewModel.IsLoading = false;
    }

    private async void RequestButton_Click(object sender, RoutedEventArgs e) {
        CommandBarFlyout.Hide();
        RequestIngredientDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<RequestIngredientDialog>(XamlRoot);
        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;
        ViewModel.IsLoading = true;
        double amount = dialog.ViewModel.RequestingAmount;
        await ViewModel.RequestIngredient(_selectedIngredient!.Id, amount);
        ViewModel.IsLoading = false;
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e) {
        CommandBarFlyout.Hide();
        EditIngredientDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<EditIngredientDialog>(XamlRoot);
        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;
        ViewModel.IsLoading = true;
        Ingredient ingredient = await dialog.ViewModel.EditIngredient();
        int index = ViewModel.IngredientsDetail.IndexOf(_selectedIngredient!);
        _selectedIngredient = App.GetService<IMapper>().Map<IngredientDetails>(ingredient);
        ViewModel.IngredientsDetail[index] = _selectedIngredient;
        ViewModel.IsLoading = false;
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
        ContentDialog dialog = new() {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            Title = "Delete Ingredient",
            Content = $"Are you sure you want to delete this ingredient {_selectedIngredient!.Name}?"
        };
        var result = await dialog.ShowAsync();

        if (result != ContentDialogResult.Primary) return;
        ViewModel.IsLoading = true;
        await ViewModel.DeleteIngredient(_selectedIngredient.Id);
        ViewModel.IsLoading = false;
    }
}