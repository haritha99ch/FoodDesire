using CommunityToolkit.WinUI.UI.Animations;

namespace FoodDesire.IMS.Views;
public sealed partial class EditRecipePage : Page {
    public EditRecipeViewModel ViewModel { get; }

    public EditRecipePage() {
        ViewModel = App.GetService<EditRecipeViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e) {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", RecipeForm.ImagesContainer);
    }
}
