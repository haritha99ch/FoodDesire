using CommunityToolkit.WinUI.UI.Animations;

namespace FoodDesire.IMS.Views;
public sealed partial class RecipesDetailPage : Page {
    public RecipesDetailViewModel ViewModel {
        get;
    }

    public RecipesDetailPage() {
        ViewModel = App.GetService<RecipesDetailViewModel>();
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e) {
        base.OnNavigatedTo(e);
        this.RegisterElementForConnectedAnimation("animationKeyContentGrid", itemHero);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e) {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back) {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Recipe != null) {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Recipe);
            }
        }
    }
}
