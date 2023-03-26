using Microsoft.UI.Xaml.Media.Imaging;
using System.ComponentModel;

namespace FoodDesire.IMS.Models;
public partial class UserDetail : ObservableObject {
    [ObservableProperty]
    private string? _id;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _firstName;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(FullName))]
    private string? _lastName;
    [ObservableProperty]
    private string? _phoneNumber;
    [ObservableProperty]
    private Account? _account;
    [ObservableProperty]
    private BitmapImage? _profilePicture;
    public string? FullName => $"{FirstName} {LastName}";

    public async Task LoadProfilePictureAsync() {
        if (Account!.ProfilePicture == null) return;
        ProfilePicture = await ByteArrayToImageSourceConverter.GetBitmap(Account!.ProfilePicture);
    }

    protected override async void OnPropertyChanged(PropertyChangedEventArgs args) {
        base.OnPropertyChanged(args);

        if (args.PropertyName != nameof(Account)) return;
        await LoadProfilePictureAsync();
    }
}
