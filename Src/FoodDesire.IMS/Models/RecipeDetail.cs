using Microsoft.UI.Xaml.Media.Imaging;

namespace FoodDesire.IMS.Models;
public partial class RecipeDetail : RecipeListItemDetail {
    [ObservableProperty]
    private int _chefId;
    [ObservableProperty]
    private string? _categoryName;
    [ObservableProperty]
    private int _recipeCategoryId;
    public ObservableCollection<RecipeInstruction> Instructions { get; set; } = new();
    public ObservableCollection<BitmapImage> RecipeImages { get; set; } = new();
    public ObservableCollection<RecipeIngredient> RecipeIngredients { get; set; } = new();
    [ObservableProperty]
    private string? _description;

    protected override async void UpdateImage() {
        RecipeImages.Clear();
        foreach (var image in Images) {
            RecipeImages.Add(await ByteArrayToImageSourceConverter.GetBitmap(image.Data));
        }
    }
}
