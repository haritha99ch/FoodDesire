using Microsoft.UI.Xaml.Media.Imaging;

namespace FoodDesire.IMS.ViewModels;
public partial class ShellViewModel : ObservableRecipient {
    private bool _isBackEnabled;
    private object? _selected;

    public INavigationService NavigationService { get; }
    private readonly IAuthenticationService _authenticationService;
    private readonly ILocalSettingsService _localSettingsService;
    public INavigationViewService NavigationViewService { get; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private User? _user;

    public string FullName => $"{User?.FirstName} {User?.LastName}";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private BitmapImage? _profilePicture;

    public bool IsBackEnabled {
        get => _isBackEnabled;
        set => SetProperty(ref _isBackEnabled, value);
    }

    public object? Selected {
        get => _selected;
        set => SetProperty(ref _selected, value);
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAdmin))]
    private Admin? _admin;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsChef))]
    private Chef? _chef;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSupplier))]
    private Supplier? _supplier;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsDeliverer))]
    private Deliverer? _deliverer;

    public bool IsAdmin => Admin != null;
    public bool IsChef => Chef != null;
    public bool IsSupplier => Supplier != null;
    public bool IsDeliverer => Deliverer != null;

    public ShellViewModel(
        INavigationService navigationService,
        INavigationViewService navigationViewService,
        IAuthenticationService authenticationService,
        ILocalSettingsService localSettingsService
        ) {
        NavigationService = navigationService;
        NavigationService.Navigated += OnNavigated;
        NavigationViewService = navigationViewService;
        _authenticationService = authenticationService;
        _localSettingsService = localSettingsService;

        _ = GetUser();
    }

    private void OnNavigated(object sender, NavigationEventArgs e) {
        IsBackEnabled = NavigationService.CanGoBack;

        if (e.SourcePageType == typeof(SettingsPage)) {
            Selected = NavigationViewService.SettingsItem;
            return;
        }

        var selectedItem = NavigationViewService.GetSelectedItem(e.SourcePageType);
        if (selectedItem != null) {
            Selected = selectedItem;
        }
    }

    public async Task AuthenticateUser(string clientId) {
        App.CurrentUserAccount = await _authenticationService.AcquireAccount();
        if (App.CurrentUserAccount == null) return;
        await _localSettingsService.SaveSettingAsync<string>("CurrentUserToken", _authenticationService.AcquireAccessToken());
        await GetUser();
    }


    public async Task GetUser() {
        if (App.CurrentUserAccount == null) return;
        switch (App.CurrentUserAccount.Role) {
            case Role.Admin:
                Admin = await App.GetService<IUserService<Admin>>().GetByEmail(App.CurrentUserAccount.Email!);
                User = Admin.User;
                break;
            case Role.Chef:
                Chef = await App.GetService<IUserService<Chef>>().GetByEmail(App.CurrentUserAccount.Email!);
                User = Chef.User;
                break;
            case Role.Deliverer:
                Deliverer = await App.GetService<IUserService<Deliverer>>().GetByEmail(App.CurrentUserAccount.Email!);
                User = Deliverer.User;
                break;
            case Role.Supplier:
                Supplier = await App.GetService<IUserService<Supplier>>().GetByEmail(App.CurrentUserAccount.Email!);
                User = Supplier.User;
                break;
        }
        App.CurrentUserAccount = User!.Account;

        if (User.Account!.ProfilePicture == null) return;
        ProfilePicture = await ByteArrayToImageSourceConverter.GetBitmap(User.Account.ProfilePicture);
    }
}
