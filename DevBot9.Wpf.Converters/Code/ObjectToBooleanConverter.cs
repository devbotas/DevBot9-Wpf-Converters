namespace DevBot9.Wpf.Converters;

public class ObjectToBooleanConverter : IValueConverter {
    public static ObjectToBooleanConverter Default { get; } = new();
    public static ObjectToBooleanConverter Inverting { get; } = new() { Inverse = true };

    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return value != null ^ Inverse;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
}
