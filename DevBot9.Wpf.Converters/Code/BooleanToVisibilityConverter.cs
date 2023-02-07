namespace DevBot9.Wpf.Converters;

public class BooleanToVisibilityConverter : IValueConverter {
    public static BooleanToVisibilityConverter Default { get; } = new();
    public static BooleanToVisibilityConverter Inverting { get; } = new() { Inverse = true };
    public bool HiddenInsteadOfCollapsed { get; set; }
    public bool Inverse { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var booleanValue = ConverterHelper.GetBooleanValue(value);
        return ConverterHelper.BooleanToVisibility(booleanValue ^ Inverse, HiddenInsteadOfCollapsed);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        var booleanValue = ((value is Visibility visibility) && (visibility == Visibility.Visible)) ^ Inverse;
        return booleanValue;
    }
}
