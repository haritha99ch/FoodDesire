using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace FoodDesire.IMS.Models;
public partial class RecipeForm : ObservableObject {
    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    private string? _description;
    [ObservableProperty]
    private ObservableCollection<RecipeCategory> _recipeCategories = new();
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsRecipeCategoryEditable))]
    private RecipeCategory? _recipeCategory;
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
    private RecipeCategory _newRecipeCategory = new();

    public bool IsAddRecipeCategoryButtonEnabled => !(string.IsNullOrEmpty(NewRecipeCategory.Name) || string.IsNullOrEmpty(NewRecipeCategory.Description));
    public bool IsRecipeCategoryEditable => RecipeCategory != null;
    public int? RecipeCategoryId => (RecipeCategories.SingleOrDefault(e => e.Id == RecipeCategory?.Id)?.Id);

    public void UpdateIsAddRecipeCategoryButtonEnabled() {
        OnPropertyChanged(nameof(IsAddRecipeCategoryButtonEnabled));
    }

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
        if (RecipeCategory == null) return;
        NewRecipeCategory = RecipeCategory;
    }

    [RelayCommand]
    private void EmptyRecipeCategoryDetailsToCreateANew() {
        NewRecipeCategory = new();
    }
}

