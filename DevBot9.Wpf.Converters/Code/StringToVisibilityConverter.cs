namespace DevBot9.Wpf.Converters;

public class StringToVisibilityConverter : IValueConverter {
    public static StringToVisibilityConverter Default { get; } = new();
    public static StringToVisibilityConverter Inverting { get; } = new() { Inverse = true };

    public bool HiddenInsteadOfCollapsed { get; set; }
    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var boolean = ConverterHelper.StringToBoolean(value, Inverse);
        return ConverterHelper.BooleanToVisibility(boolean, HiddenInsteadOfCollapsed);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
}
