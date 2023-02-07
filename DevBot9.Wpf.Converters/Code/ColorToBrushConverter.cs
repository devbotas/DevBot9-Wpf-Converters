using System.Windows.Media;

namespace DevBot9.Wpf.Converters;

public class ColorToBrushConverter : IValueConverter {
    public static ColorToBrushConverter Default { get; } = new();

    public byte? CustomA { get; set; }
    public static SolidColorBrush Convert(object value, byte? customA = null) {
        if (value == null) {
            return null;
        }

        Color color;
        if (value is System.Drawing.Color mColor) {
            color = Color.FromArgb(mColor.A, mColor.R, mColor.G, mColor.B);
        } else {
            color = (Color)value;
        }

        if (customA != null) {
            color.A = customA.Value;
        }

        return BrushesCache.GetBrush(color);
    }
    public static Color ConvertBack(object value) {
        return value != null ? ((SolidColorBrush)value).Color : default;
    }
    object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return Convert(value, CustomA);
    }
    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return ConvertBack(value);
    }
}
