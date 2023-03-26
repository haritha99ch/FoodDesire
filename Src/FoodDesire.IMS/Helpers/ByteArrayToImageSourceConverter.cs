using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;

namespace FoodDesire.IMS.Helpers;
public class ByteArrayToImageSourceConverter {
    public static async Task<BitmapImage> GetBitmap(byte[] data) {
        var bitmapImage = new BitmapImage();

        using (var stream = new InMemoryRandomAccessStream()) {
            using (var writer = new DataWriter(stream)) {
                writer.WriteBytes(data);
                await writer.StoreAsync();
                await writer.FlushAsync();
                writer.DetachStream();
            }

            stream.Seek(0);
            await bitmapImage.SetSourceAsync(stream);
        }

        return bitmapImage;
    }
}
