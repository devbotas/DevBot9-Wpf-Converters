namespace DevBot9.Wpf.Converters;

static class ConverterHelper {
    public static Visibility BooleanToVisibility(bool booleanValue, bool hiddenInsteadOfCollapsed) {
        return booleanValue ? Visibility.Visible : (hiddenInsteadOfCollapsed ? Visibility.Hidden : Visibility.Collapsed);
    }

    public static bool GetBooleanParameter(string[] parameters, string name) {
        foreach (var parameter in parameters) {
            if (string.Equals(parameter, name, StringComparison.OrdinalIgnoreCase)) {
                return true;
            }
        }
        return false;
    }

    public static bool GetBooleanValue(object value) {
        if (value is bool boolean) {
            return boolean;
        }

        if (value is bool?) {
            var nullable = (bool?)value;
            return nullable.HasValue ? nullable.Value : false;
        }
        return false;
    }

    public static bool? GetNullableBooleanValue(object value) {
        if (value is bool boolean) {
            return boolean;
        }

        if (value is bool?) {
            return (bool?)value;
        }

        return null;
    }

    public static string[] GetParameters(object parameter) {
        var param = parameter as string;
        if (string.IsNullOrEmpty(param)) {
            return Array.Empty<string>();
        }

        return param.Split(';');
    }
    public static bool NumericToBoolean(object value, bool inverse) {
        if (value == null) {
            return CorrectBoolean(false, inverse);
        }

        try {
            var d = (double)Convert.ChangeType(value, typeof(double), null);
            return CorrectBoolean(d != 0d, inverse);
        } catch (Exception) { }
        return CorrectBoolean(false, inverse);
    }
    public static bool StringToBoolean(object value, bool inverse) {
        if (value is not string) {
            return CorrectBoolean(false, inverse);
        }

        return CorrectBoolean(!string.IsNullOrEmpty((string)value), inverse);
    }
    static bool CorrectBoolean(bool value, bool inverse) {
        return value ^ inverse;
    }
}
