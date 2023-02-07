namespace DevBot9.Wpf.Converters;

public class StringToBooleanConverter : IValueConverter {
    public static StringToBooleanConverter Default { get; } = new();
    public static StringToBooleanConverter Inverting { get; } = new() { Inverse = true };
    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return ConverterHelper.StringToBoolean(value, Inverse);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
}
