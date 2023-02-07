using System.Windows.Media;

namespace DevBot9.Wpf.Converters;

public class BrushToColorConverter : IValueConverter {
    public static BrushToColorConverter Default { get; } = new();

    public static Color Convert(object value) {
        return ColorToBrushConverter.ConvertBack(value);
    }
    public static SolidColorBrush ConvertBack(object value) {
        return ColorToBrushConverter.Convert(value);
    }
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is not SolidColorBrush) {
            return Colors.Black;
        }

        return Convert(value);
    }
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return ConvertBack(value);
    }
}
