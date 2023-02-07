namespace DevBot9.Wpf.Converters;

public class ChoiceToVisibilityConverter : IValueConverter {
    public static ChoiceToVisibilityConverter Default { get; } = new();
    public static ChoiceToVisibilityConverter Inverting { get; } = new() { Inverse = true };

    public bool HiddenInsteadOfCollapsed { get; set; }
    public bool Inverse { get; set; }
    public string VisibleValue { get; set; }
    public string CollapsedValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var boolean = ConverterHelper.StringToBoolean(value, Inverse);
        return ConverterHelper.BooleanToVisibility(boolean, HiddenInsteadOfCollapsed);
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { return null; }
}
