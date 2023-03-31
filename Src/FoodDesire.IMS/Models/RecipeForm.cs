using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace FoodDesire.IMS.Models;
public partial class RecipeForm : ObservableValidator {
    public int Id { get; set; }
    public int ChefId { get; set; }
    [ObservableProperty]
    [MinLength(5)]
    [NotifyPropertyChangedFor(nameof(NameErrors))]
    private string? _name;
    public string NameErrors => string.Join(Environment.NewLine, GetErrors(nameof(Name)));

    [ObservableProperty]
    private string? _description;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsRecipeCategoryEditable))]
    private RecipeCategory? _selectedRecipeCategory;
    public ObservableCollection<FoodDesire.Models.Image> Images { get; set; } = new();
    public ObservableCollection<BitmapImage> BitmapImages { get; set; } = new();
    public ObservableCollection<RecipeIngredient> RecipeIngredients { get; set; } = new();
    public ObservableCollection<RecipeInstruction> RecipeInstructions { get; set; } = new();
    [ObservableProperty]
    private bool _asIngredient = false;
    [ObservableProperty]
    private bool _isMenuItem = true;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAddRecipeCategoryButtonEnabled))]
    private RecipeCategory _newRecipeCategory = new();

    [ObservableProperty]
    private RecipeIngredient _recipeIngredient = new();
    private Ingredient? _selectedIngredient;
    //Recipe Ingredient ObservableProperties
    private double _amount;
    public double Amount {
        get => RecipeIngredient.Amount;
        set {
            RecipeIngredient.Amount = value;
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
        get => RecipeIngredient.RecommendedAmount;
        set {
            RecipeIngredient.RecommendedAmount = value;
            SetProperty(ref _recommendedAmount, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private bool _isRequired;
    public bool IsRequired {
        get => RecipeIngredient.IsRequired;
        set {
            RecipeIngredient.IsRequired = value;
            SetProperty(ref _isRequired, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private bool _canModify;
    public bool CanModify {
        get => RecipeIngredient.CanModify;
        set {
            if (!value) RecommendedAmount = 0; PricePerMultiplier = 0;
            RecipeIngredient.CanModify = value;
            SetProperty(ref _canModify, value);
            OnPropertyChanged(nameof(IsAddRecipeIngredientButtonEnabled));
        }
    }
    private double _pricePerMultiplier;
    public double PricePerMultiplier {
        get => (double)RecipeIngredient.PricePerMultiplier;
        set {
            RecipeIngredient.PricePerMultiplier = (decimal)value;
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

    public Ingredient? SelectedIngredient {
        get => _selectedIngredient;
        set {
            if (value == null) return;
            RecipeIngredient.Ingredient_Id = value.Id;
            RecipeIngredient.Recipe_Id = null;
            RecipeIngredient.Recipe_Name = null;
            RecipeIngredient.Ingredient_Name = value.Name;
            RecipeIngredient.Measurement = value.Measurement;
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
            RecipeIngredient.Ingredient_Id = null;
            RecipeIngredient.Ingredient_Name = null;
            RecipeIngredient.Recipe_Id = value.Id;
            RecipeIngredient.Recipe_Name = value.Name;
            RecipeIngredient.Measurement = Measurement.Each;
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
    public bool IsAddRecipeCategoryButtonEnabled => !(string.IsNullOrEmpty(NewRecipeCategory.Name) || string.IsNullOrEmpty(NewRecipeCategory.Description));
    public bool IsRecipeCategoryEditable => SelectedRecipeCategory != null;
    public int? RecipeCategoryId => SelectedRecipeCategory?.Id;


    public void UpdateIsAddRecipeCategoryButtonEnabled() => OnPropertyChanged(nameof(IsAddRecipeCategoryButtonEnabled));

    [RelayCommand]
    private async void AddNewImage() {
        FileOpenPicker picker = new FileOpenPicker() {
            CommitButtonText = "Open",
            SuggestedStartLocation = PickerLocationId.ComputerFolder,
            FileTypeFilter = { ".jpg", ".jpeg" }
        };

        IntPtr hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
        WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

        var images = await picker.PickMultipleFilesAsync();
        foreach (var image in images) {
            await NewImage(image);
        }
    }

    private async Task NewImage(StorageFile file) {
        using var stream = await file.OpenReadAsync();
        var buffer = new Windows.Storage.Streams.Buffer((uint)stream.Size);
        await stream.ReadAsync(buffer, (uint)stream.Size, Windows.Storage.Streams.InputStreamOptions.None);
        byte[] bytes = buffer.ToArray();
        Images.Add(new FoodDesire.Models.Image() { Data = bytes });
        BitmapImages.Add(await ByteArrayToImageSourceConverter.GetBitmap(bytes));
        stream.Dispose();
    }

    [RelayCommand]
    private void SetRecipeCategoryToEdit() {
        if (SelectedRecipeCategory == null) return;
        NewRecipeCategory = SelectedRecipeCategory;
    }

    [RelayCommand]
    private void EmptyRecipeCategoryDetailsToCreateANew() {
        NewRecipeCategory = new();
    }

    [RelayCommand]
    private void AddRecipeIngredient() {
        RecipeIngredients.Add(RecipeIngredient);
    }
}

