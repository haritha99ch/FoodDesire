using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace FoodDesire.IMS.Models;
public partial class RecipeForm : ObservableValidator {
    public XamlRoot? XamlRoot { get; set; }
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

    [RelayCommand]
    private async Task OpenAddRecipeIngredientDialog() {
        NewRecipeIngredientDialog dialog = App.GetService<IContentDialogFactory>()
            .ConfigureDialog<NewRecipeIngredientDialog>(XamlRoot!);
        ContentDialogResult result = await dialog.ShowAsync();

        if (!result.Equals(ContentDialogResult.Primary)) return;
        RecipeIngredients.Add(App.GetService<IMapper>().Map<RecipeIngredient>(dialog.RecipeIngredient));
    }
}

