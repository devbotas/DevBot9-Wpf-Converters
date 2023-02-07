namespace DevBot9.Wpf.Converters;

public class BooleanNegationConverter : IValueConverter {
    public static BooleanNegationConverter Default { get; } = new();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var booleanValue = ConverterHelper.GetNullableBooleanValue(value);
        if (booleanValue != null) {
            booleanValue = !booleanValue.Value;
        }

        if (targetType == typeof(bool)) {
            return booleanValue ?? true;
        }

        return booleanValue;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        return Convert(value, targetType, parameter, culture);
    }
}
