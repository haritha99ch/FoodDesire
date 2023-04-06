using Microsoft.UI.Xaml.Media.Imaging;

namespace FoodDesire.IMS.Models;
public partial class RecipeListItemDetail : ObservableObject {
    [ObservableProperty]
    private int _id;
    private List<FoodDesire.Models.Image> _images = new();
    public List<FoodDesire.Models.Image> Images {
        get => _images;
        set {
            SetProperty(ref _images, value);
            UpdateImage();
        }
    }
    [ObservableProperty]
    private BitmapImage? _featuredImage;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(Stars))]
    private float _rating = 0;
    public float Stars => (Rating == 0) ? -1 : Rating * 5;
    [ObservableProperty]
    private string? _name;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMinimumAndFixedPriceEquals))]
    private double _minimumPrice;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsMinimumAndFixedPriceEquals))]
    private double _fixedPrice;
    [ObservableProperty]
    private bool _asIngredient;
    [ObservableProperty]
    private bool _isMenuItem;
    [ObservableProperty]
    private RecipeCategory? _recipeCategory;

    public bool IsMinimumAndFixedPriceEquals => MinimumPrice == FixedPrice;

    public RecipeListItemDetail() {
    }

    protected virtual async void UpdateImage() {
        if (!Images.Any()) return;
        FeaturedImage = await ByteArrayToImageSourceConverter.GetBitmap(Images.FirstOrDefault()!.Data);
    }
}
