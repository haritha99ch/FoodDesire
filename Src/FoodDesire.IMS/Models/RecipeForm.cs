using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;

namespace FoodDesire.IMS.Models;
public partial class RecipeForm : ObservableObject {
    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private string? _description;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Categories))]
    private ObservableCollection<RecipeCategory> _recipeCategories = new();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsRecipeCategoryEditable))]
    [NotifyPropertyChangedFor(nameof(RecipeCategoryId))]
    private string? _selectedRecipeCategory;
    [ObservableProperty]
    private ObservableCollection<FoodDesire.Models.Image> _images = new();
    [ObservableProperty]
    private ObservableCollection<BitmapImage> _bitmapImages = new();
    [ObservableProperty]
    private ObservableCollection<RecipeIngredient> _ingredients = new();
    [ObservableProperty]
    private ObservableCollection<RecipeInstruction> _instructions = new();


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAddRecipeCategoryButtonEnabled))]
    private string? _newRecipeCategory;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAddRecipeCategoryButtonEnabled))]
    private string? _newRecipeCategoryDescription;

    public bool IsAddRecipeCategoryButtonEnabled => !(string.IsNullOrEmpty(NewRecipeCategory) || string.IsNullOrEmpty(NewRecipeCategoryDescription));
    public bool IsRecipeCategoryEditable => SelectedRecipeCategory != null;

    public List<string> Categories => RecipeCategories.Select(c => c.Name).ToList();
    public int? RecipeCategoryId => (RecipeCategories.SingleOrDefault(e => e.Name.Equals(SelectedRecipeCategory))?.Id);

    public async void NewImage(StorageFile file) {
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
        NewRecipeCategory = RecipeCategories.SingleOrDefault(e => e.Id.Equals(RecipeCategoryId))?.Name;
        NewRecipeCategoryDescription = RecipeCategories.SingleOrDefault(e => e.Name.Equals(SelectedRecipeCategory))?.Description;
    }

    [RelayCommand]
    private void EmptyRecipeCategoryDetailsToCreateANew() {
        NewRecipeCategory = null;
        NewRecipeCategoryDescription = null;
    }
}

