namespace DevBot9.Wpf.Converters;

public class NumericToBooleanConverter : IValueConverter {
    public static NumericToBooleanConverter Default { get; } = new();
    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        return ConverterHelper.NumericToBoolean(value, Inverse);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
}
