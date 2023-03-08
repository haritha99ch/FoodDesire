using Microsoft.UI.Xaml.Data;

namespace FoodDesire.IMS.Helpers;
public class StringConcatConverter : IValueConverter {
    public object Convert(object value, Type targetType, object parameter, string language) {
        if (value == null) return null!;

        string? formatString = parameter as string;
        return string.Format(formatString!, value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language) {
        throw new NotSupportedException();
    }
}
