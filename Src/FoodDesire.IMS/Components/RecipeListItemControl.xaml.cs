using DocumentFormat.OpenXml.EMMA;

namespace FoodDesire.IMS.Components;
public sealed partial class RecipeListItemControl : UserControl {
    public RecipeListItemControl() {
        InitializeComponent();
    }

    private void InfoButton_Click(object sender, RoutedEventArgs e) {
        Info.IsOpen = true;
    }
}
