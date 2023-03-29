using Microsoft.UI.Xaml.Media.Imaging;

namespace FoodDesire.IMS.Models;
public class RecipeDetail : Recipe {
    public BitmapImage FeatureImage => AllImages[0];
    public ObservableCollection<BitmapImage> AllImages { get; set; } = new();

    public RecipeDetail() {
        OnInit();
    }

    private async void OnInit() {
        foreach (FoodDesire.Models.Image image in Images) {
            AllImages.Add(await ByteArrayToImageSourceConverter.GetBitmap(image.Data));
        }
    }
}
