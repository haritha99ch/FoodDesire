using CommunityToolkit.Mvvm.Input;
using Microsoft.IdentityModel.Tokens;

namespace FoodDesire.IMS.Core.Models;
public abstract partial class IngredientForm : ObservableObject {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private string? _ingredientName;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private string? _ingredientDescription;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Categories))]
    private ObservableCollection<IngredientCategory> _ingredientCategories = new();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated), nameof(IngredientCategoryCanBeEdited))]
    private string? _category;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private double? _ingredientMaximumQuantity;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    [NotifyPropertyChangedFor(nameof(DisplayMeasurement))]
    private Measurement _measurement;
    private IList<Measurement> _measurements = Enum.GetValues(typeof(Measurement)).Cast<Measurement>().ToList();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CategoryCanBeCreated))]
    private string? _newIngredientCategoryName;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CategoryCanBeCreated))]
    private string? _newIngredientCategoryDescription;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Categories), nameof(Categories))]
    private IngredientCategory? _newIngredientCategory;
    [ObservableProperty]
    private bool _isLoading = true;
    [ObservableProperty]
    private bool _isCategoriesLoaded = true;
    public bool IngredientCategoryCanBeEdited => (Category != null) ? IngredientCategories.SingleOrDefault(e => e.Name.Equals(Category))!.Ingredients.IsNullOrEmpty() : false;

    protected int? _ingredientCategoryId => IngredientCategories.SingleOrDefault(e => e.Name.Equals(Category))?.Id;
    public List<string> Categories => IngredientCategories.Select(e => e.Name).ToList();
    public IList<Measurement> Measurements => _measurements;
    public bool CanBeCreated {
        get {
            if (string.IsNullOrEmpty(IngredientName)) return false;
            if (string.IsNullOrEmpty(IngredientDescription)) return false;
            if (string.IsNullOrEmpty(Category)) return false;
            if (IngredientMaximumQuantity == null) return false;
            if (IngredientMaximumQuantity == 0) return false;
            return true;
        }
    }
    public bool CategoryCanBeCreated {
        get {
            if (string.IsNullOrEmpty(NewIngredientCategoryName)) return false;
            if (string.IsNullOrEmpty(NewIngredientCategoryDescription)) return false;
            return true;
        }
    }
    public string DisplayMeasurement => Measurement switch {
        Measurement.Grams => "g",
        Measurement.Liquid => "ml",
        Measurement.Each => "each",
        _ => "",
    };

    [RelayCommand]
    public abstract void AddNewIngredientCategory();
    [RelayCommand]
    public abstract void EditIngredientCategory();
    [RelayCommand]
    public abstract void DeleteIngredientCategory();

    public void SetIngredientCategoryToEdit() {
        IngredientCategory category = IngredientCategories.SingleOrDefault(e => e.Name.Equals(Category))!;
        NewIngredientCategoryName = category.Name;
        NewIngredientCategoryDescription = category.Description;
    }
}
