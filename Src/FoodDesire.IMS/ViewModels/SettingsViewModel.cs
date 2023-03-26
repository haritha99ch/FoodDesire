using CommunityToolkit.Mvvm.Input;
using System.Reflection;
using System.Windows.Input;
using Windows.ApplicationModel;

namespace FoodDesire.IMS.ViewModels;
public partial class SettingsViewModel : ObservableRecipient, INavigationAware {
    private readonly IThemeSelectorService _themeSelectorService;
    private readonly ISettingsPageService _settingsPageService;
    private readonly IMapper _mapper;

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
    [ObservableProperty]
    private UserDetail? _user;

    private ElementTheme _elementTheme;
    private string _versionDescription;

    public ElementTheme ElementTheme {
        get => _elementTheme;
        set => SetProperty(ref _elementTheme, value);
    }

    public string VersionDescription {
        get => _versionDescription;
        set => SetProperty(ref _versionDescription, value);
    }
    public Account CurrentUserAccount => App.CurrentUserAccount!;
    public bool IsAdmin => Admin != null;
    public bool IsChef => Chef != null;
    public bool IsSupplier => Supplier != null;
    public bool IsDeliverer => Deliverer != null;
    public ICommand SwitchThemeCommand { get; }

    public SettingsViewModel(IThemeSelectorService themeSelectorService, ISettingsPageService settingsPageService, IMapper mapper) {
        _themeSelectorService = themeSelectorService;
        _settingsPageService = settingsPageService;
        _elementTheme = _themeSelectorService.Theme;
        _versionDescription = GetVersionDescription();
        _mapper = mapper;

        SwitchThemeCommand = new RelayCommand<ElementTheme>(
            async (param) => {
                if (ElementTheme != param) {
                    ElementTheme = param;
                    await _themeSelectorService.SetThemeAsync(param);
                }
            });
    }

    public async void OnNavigatedTo(object parameter) {
        switch (CurrentUserAccount.Role) {
            case Role.Admin:
                Admin = await _settingsPageService.GetEmployeeByEmail<Admin>(CurrentUserAccount.Email!);
                SetUser(Admin.User!);
                break;
            case Role.Chef:
                Chef = await _settingsPageService.GetEmployeeByEmail<Chef>(CurrentUserAccount.Email!);
                SetUser(Chef.User!);
                break;
            case Role.Supplier:
                Supplier = await _settingsPageService.GetEmployeeByEmail<Supplier>(CurrentUserAccount.Email!);
                SetUser(Supplier.User!);
                break;
            case Role.Deliverer:
                Deliverer = await _settingsPageService.GetEmployeeByEmail<Deliverer>(CurrentUserAccount.Email!);
                SetUser(Deliverer.User!);
                break;
            case Role.Customer:
                break;
            default:
                break;
        }
    }

    [RelayCommand]
    public async void SignUserOut() {
        await App.GetService<ILocalSettingsService>().SaveSettingAsync<string>("CurrentUserToken", null!);
        App.CurrentUserAccount = null;
        App.MainWindow.Content = App.GetService<ShellPage>();
        (App.MainWindow.Content as ShellPage)!.ViewModel.NavigationService.NavigateTo(typeof(HomeViewModel).FullName!);
    }

    private void SetUser(User user) => User = _mapper.Map<UserDetail>(user)!;

    public void OnNavigatedFrom() { }

    private static string GetVersionDescription() {
        Version version;

        if (RuntimeHelper.IsMSIX) {
            var packageVersion = Package.Current.Id.Version;

            version = new(packageVersion.Major, packageVersion.Minor, packageVersion.Build, packageVersion.Revision);
        } else {
            version = Assembly.GetExecutingAssembly().GetName().Version!;
        }

        return $"{"AppDisplayName".GetLocalized()} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
    }
}
