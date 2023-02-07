namespace DevBot9.Wpf.Converters;

public class BooleanToObjectConverter : IValueConverter {
    public static BooleanToObjectConverter Default { get; } = new();
    public object FalseValue { get; set; }
    public object NullValue { get; set; }
    public object TrueValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        if (value is bool?) {
            value = (bool?)value == true;
        }
        if (value == null) {
            return NullValue;
        }

        if (value is not bool) {
            return null;
        }

        var asBool = (bool)value;
        return asBool ? TrueValue : FalseValue;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
