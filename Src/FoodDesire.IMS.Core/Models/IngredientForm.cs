using CommunityToolkit.Mvvm.Input;

namespace FoodDesire.IMS.Core.Models;
public abstract partial class IngredientForm : ObservableObject {
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private string? _ingredientName;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private string? _ingredientDescription;
    [ObservableProperty]
    private ObservableCollection<IngredientCategory> _ingredientCategories = new();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated), nameof(IngredientCategoryCanBeEdited))]
    private IngredientCategory? _category;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private double? _ingredientMaximumQuantity;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CanBeCreated))]
    private Measurement _measurement;
    public ObservableCollection<Measurement> Measurements = new ObservableCollection<Measurement>(Enum.GetValues(typeof(Measurement)).Cast<Measurement>());
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CategoryCanBeCreated), nameof(IngredientCategoryId))]
    private string? _newIngredientCategoryName;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(CategoryCanBeCreated))]
    private string? _newIngredientCategoryDescription;
    [ObservableProperty]
    private IngredientCategory? _newIngredientCategory;
    [ObservableProperty]
    private bool _isLoading = true;
    [ObservableProperty]
    private bool _isCategoriesLoaded = true;
    public bool IngredientCategoryCanBeEdited => (Category != null);

    private int _ingredientCategoryId;
    protected int IngredientCategoryId {
        get => Category!.Id;
        set => _ingredientCategoryId = value;
    }
    public bool EditMode => !string.IsNullOrEmpty(IngredientName);
    public bool CanBeCreated {
        get {
            if (string.IsNullOrEmpty(IngredientName)) return false;
            if (string.IsNullOrEmpty(IngredientDescription)) return false;
            if (Category == null) return false;
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
