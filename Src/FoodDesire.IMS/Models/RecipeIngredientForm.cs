namespace FoodDesire.IMS.Models;
public partial class RecipeIngredientForm : ObservableObject {
    public int? Ingredient_Id { get; set; } = null;
    [ObservableProperty]
    private string? _ingredient_Name;
    public int? Recipe_Id { get; set; } = null;
    [ObservableProperty]
    private string? _recipe_Name;
    [ObservableProperty]
    private Measurement _measurement;
    private double _amount;
    public double Amount {
        get => _amount;
        set {
            SetProperty(ref _amount, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
            if (!CanModify) return;
            try {
                if (SelectedIngredient != null) {
                    double price = value * (double)SelectedIngredient.CurrentPricePerUnit;
                    PricePerMultiplier = (PricePerMultiplier < price) ? price : PricePerMultiplier;
                } else if (SelectedRecipeAsIngredient != null) {
                    double price = value * (double)SelectedRecipeAsIngredient.FixedPrice;
                    PricePerMultiplier = (PricePerMultiplier < price) ? price : PricePerMultiplier;
                }
            } catch (Exception) {
                return;
            }
        }
    }
    private double _recommendedAmount;
    public double RecommendedAmount {
        get => _recommendedAmount;
        set {
            SetProperty(ref _recommendedAmount, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private bool _isRequired;
    public bool IsRequired {
        get => _isRequired;
        set {
            SetProperty(ref _isRequired, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private bool _canModify;
    public bool CanModify {
        get => _canModify;
        set {
            if (!value) RecommendedAmount = 0; PricePerMultiplier = 0;
            SetProperty(ref _canModify, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private double _pricePerMultiplier;
    public double PricePerMultiplier {
        get => _pricePerMultiplier;
        set {
            SetProperty(ref _pricePerMultiplier, value);
        }
    }
    public bool IsAddRecipeIngredientButtonEnabled {
        get {
            if (SelectedIngredient == null && SelectedRecipeAsIngredient == null) return false;
            if (Amount == 0) return false;
            if (!CanModify) return true;
            if (CanModify && RecommendedAmount <= Amount) return false;
            if (!UpdateRecipeIngredientPricePerMultiplier) return false;
            return true;
        }
    }
    private Ingredient? _selectedIngredient;
    public Ingredient? SelectedIngredient {
        get => _selectedIngredient;
        set {
            if (value == null) return;
            Ingredient_Id = value.Id;
            Recipe_Id = null;
            Recipe_Name = null;
            Ingredient_Name = value.Name;
            Measurement = value.Measurement;
            SelectedRecipeAsIngredient = null;
            SetProperty(ref _selectedIngredient, value);
            OnPropertyChanged(nameof(SelectedIngredientIsRaw));
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private Recipe? _selectedRecipeAsIngredient;
    public Recipe? SelectedRecipeAsIngredient {
        get => _selectedRecipeAsIngredient;
        set {
            if (value == null) return;
            Ingredient_Id = null;
            Ingredient_Name = null;
            Recipe_Id = value.Id;
            Recipe_Name = value.Name;
            Measurement = Measurement.Each;
            SelectedIngredient = null;
            SetProperty(ref _selectedRecipeAsIngredient, value);
            OnPropertyChanged(nameof(SelectedIngredientIsRaw));
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    public bool SelectedIngredientIsRaw => SelectedIngredient != null;

    public bool UpdateRecipeIngredientPricePerMultiplier {
        get {
            try {
                if (SelectedIngredient != null) {
                    double price = Amount * (double)SelectedIngredient.CurrentPricePerUnit;
                    PricePerMultiplier = (PricePerMultiplier < price) ? price : PricePerMultiplier;
                    return true;
                }
                if (SelectedRecipeAsIngredient != null) {
                    double price = Amount * (double)SelectedRecipeAsIngredient.FixedPrice;
                    PricePerMultiplier = (PricePerMultiplier < price) ? price : PricePerMultiplier;
                    return true;
                }
            } catch (Exception) {
                return false;
            }
            return true;
        }
    }
}
