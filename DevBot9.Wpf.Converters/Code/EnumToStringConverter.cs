namespace DevBot9.Wpf.Converters;

public class EnumToStringConverter : IValueConverter {
    public static EnumToStringConverter Default { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value == null) {
            return null;
        }

        return value.ToString();
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value == null || !targetType.IsEnum) {
            return value;
        }

        return Enum.Parse(targetType, value.ToString());
    }
}
