namespace FoodDesire.IMS.Helpers;
internal static class AppConfigurator {
    internal static void Configure(HostBuilderContext context, IConfigurationBuilder config) {
        string environmentName = context.HostingEnvironment.EnvironmentName;
        AppSettings.Configure.ConfigureEnvironment(config, environmentName);
    }

    internal static void Configure(HostBuilderContext context, IServiceCollection services) {
        // Default Activation Handler
        services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

        // Other Activation Handlers

        // Core Services
        string connectionString = context.Configuration.GetConnectionString("DefaultConnection")!;
        Core.Configure.ConfigureServices(services, connectionString);

        // Services
        services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
        services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
        services.AddTransient<INavigationViewService, NavigationViewService>();

        services.AddSingleton<IActivationService, ActivationService>();
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();

        services.AddTransient<IContentDialogFactory, ContentDialogFactory>();

        // AutoMapper
        MapperConfiguration? configuration = new(DtoConfigurator.Configure);
        IMapper? mapper = configuration.CreateMapper();
        services.AddSingleton(mapper);

        // Views and ViewModels
        services.AddTransient<SettingsViewModel>();
        services.AddTransient<SettingsPage>();
        services.AddTransient<HomeViewModel>();
        services.AddTransient<HomePage>();
        services.AddTransient<IngredientsViewModel>();
        services.AddTransient<IngredientsPage>();
        services.AddTransient<ShellViewModel>();
        services.AddTransient<ShellPage>();
        services.AddTransient<EmployeesViewModel>();
        services.AddTransient<EmployeesPage>();
        services.AddTransient<SuppliesViewModel>();
        services.AddTransient<SuppliesPage>();
        services.AddTransient<RecipesViewModel>();
        services.AddTransient<RecipesPage>();
        services.AddTransient<RecipesDetailViewModel>();
        services.AddTransient<RecipesDetailPage>();
        services.AddTransient<NewRecipeViewModel>();
        services.AddTransient<NewRecipePage>();
        services.AddTransient<EditRecipeViewModel>();
        services.AddTransient<EditRecipePage>();

        //Components ViewModels
        services.AddTransient<NewEmployeeViewModel>();
        services.AddTransient<NewEmployeeFormDialog>();
        services.AddTransient<NewIngredientFormDialog>();
        services.AddTransient<NewIngredientFormViewModel>();
        services.AddTransient<CompleteSupplyViewModel>();
        services.AddTransient<CompleteSupplyDialog>();

        services.AddTransient<ShowErrorsDialog>();

        services.AddTransient<RecipeFormViewModel>();
        services.AddTransient<RecipeFormControl>();

        services.AddTransient<RecipeIngredientFormViewModel>();
        services.AddTransient<NewRecipeIngredientDialog>();
        services.AddTransient<EditRecipeIngredientDialog>();

        services.AddTransient<FoodItemsViewModel>();
        services.AddTransient<FoodItemsPage>();

        services.AddTransient<DeliveriesViewModel>();
        services.AddTransient<DeliveriesPage>();

        // Configuration
        services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));
    }


}
