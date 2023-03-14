using Microsoft.UI.Xaml.Controls.Primitives;

namespace FoodDesire.IMS.Components;
[INotifyPropertyChanged]
public sealed partial class IngredientListItemControl : UserControl {
    private IngredientDetails _ingredient => (IngredientDetails)DataContext;
    [ObservableProperty]
    private bool _isLoading = false;
    public IngredientListItemControl() {
        InitializeComponent();
    }

    private void ContentGrid_ContextRequested(UIElement sender, Microsoft.UI.Xaml.Input.ContextRequestedEventArgs args) {
        FlyoutShowOptions myOption = new FlyoutShowOptions();
        CommandBarFlyout.ShowAt(ContentGrid, myOption);
    }

    private async void EditButton_Click(object sender, RoutedEventArgs e) {
        IsLoading = true;
        CommandBarFlyout.Hide();
        EditIngredientDialog dialog = new EditIngredientDialog(_ingredient.Id) {
            XamlRoot = XamlRoot,
            Style = (Style)Application.Current.Resources["DefaultContentDialogStyle"],
            DefaultButton = ContentDialogButton.Primary,
        };
        var result = await dialog.ShowAsync();
        IsLoading = false;
    }
}

