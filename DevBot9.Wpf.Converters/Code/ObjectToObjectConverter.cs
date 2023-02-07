using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Markup;
using System.Windows.Media;

namespace DevBot9.Wpf.Converters;

public class MapItem {
    public MapItem() { }
    public MapItem(object source, object target) {
        Source = source;
        Target = target;
    }
    public object Source { get; set; }
    public object Target { get; set; }
}

[ContentProperty("Map")]
public class ObjectToObjectConverter : IValueConverter {
    public ObjectToObjectConverter() {
        Map = new ObservableCollection<MapItem>();
    }

    public object DefaultSource { get; set; }
    public object DefaultTarget { get; set; }
    public ObservableCollection<MapItem> Map { get; set; }
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static object Coerce(object value, Type targetType, bool ignoreImplicitXamlConversions = false, bool convertIntToEnum = false) {
        if (value == null || targetType == typeof(object) || value.GetType() == targetType) {
            return value;
        }

        if (targetType.IsAssignableFrom(value.GetType())) {
            return value;
        }

        var nullableType = Nullable.GetUnderlyingType(targetType);
        var coerced = CoerceNonNullable(value, nullableType ?? targetType, ignoreImplicitXamlConversions, convertIntToEnum);
        if (nullableType != null) {
            return Activator.CreateInstance(targetType, coerced);
        }
        return coerced;
    }
    public static bool SafeCompare(object left, object right) {
        if (left == null) {
            if (right == null) {
                return true;
            }

            return right.Equals(left);
        }
        return left.Equals(right);
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
        var entry = Map.FirstOrDefault(MakeMapPredicate(item => item.Source, value));
        return Coerce(entry == null ? DefaultTarget : entry.Target, targetType);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
        var entry = Map.FirstOrDefault(MakeMapPredicate(item => item.Target, value));
        return Coerce(entry == null ? DefaultSource : entry.Source, targetType);
    }

    internal static object CoerceNonNullable(object value, Type targetType, bool ignoreImplicitXamlConversions, bool convertIntToEnum) {
        if (!ignoreImplicitXamlConversions && IsImplicitXamlConvertion(value.GetType(), targetType)) {
            return value;
        }

        if (targetType == typeof(string)) {
            return value.ToString();
        }
        if (targetType.IsEnum) {
            if (value is string stringValue) {
                return Enum.Parse(targetType, stringValue, false);
            }

            if (convertIntToEnum) {
                try {
                    var res = Enum.ToObject(targetType, value);
                    if (Enum.IsDefined(targetType, res)) {
                        return res;
                    }
                } catch { }
            }
        }
        if (targetType == typeof(Color)) {
            var c = new ColorConverter();
            if (c.IsValid(value)) {
                return c.ConvertFrom(value);
            }

            return value;
        }
        if (targetType == typeof(Brush) || targetType == typeof(SolidColorBrush)) {
            var c = new BrushConverter();
            if (c.IsValid(value)) {
                return c.ConvertFrom(value);
            }

            if (value is Color color) {
                return BrushesCache.GetBrush(color);
            }

            return value;
        }
        var cc = TypeDescriptor.GetConverter(targetType);
        try {
            if (cc != null && cc.IsValid(value)) {
                return cc.ConvertFrom(null, CultureInfo.InvariantCulture, value);
            }

            return System.Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        } catch {
            return value;
        }
    }

    internal static bool IsImplicitXamlConvertion(Type valueType, Type targetType) {
        if (targetType == typeof(Thickness)) {
            return true;
        }

        if (targetType == typeof(GridLength)) {
            return true;
        }

        if (targetType == typeof(ImageSource) && (valueType == typeof(string) || valueType == typeof(Uri))) {
            return true;
        }

        return false;
    }

    static Func<MapItem, bool> MakeMapPredicate(Func<MapItem, object> selector, object value) {
        return mapItem => {
            var source = Coerce(selector(mapItem), (value ?? string.Empty).GetType());
            return SafeCompare(source, value);
        };
    }
}
