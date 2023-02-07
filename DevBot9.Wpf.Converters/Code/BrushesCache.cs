using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Media;

namespace DevBot9.Wpf.Converters;
public static class BrushesCache {
    readonly static Dictionary<Color, WeakReference> _cache;

    static BrushesCache() {
        _cache = new Dictionary<Color, WeakReference>();
        AddDefaultBrushes();
    }

    public static SolidColorBrush GetBrush(Color color) {
        _cache.TryGetValue(color, out var r);
        if (r != null && r.IsAlive) {
            return (SolidColorBrush)r.Target;
        }

        var res = new SolidColorBrush(color);
        res.Freeze();
        lock (_cache) {
            _cache[color] = new WeakReference(res);
        }

        return res;
    }
    static void AddDefaultBrushes() {
        var props = typeof(Brushes)
            .GetProperties(BindingFlags.Static | BindingFlags.Public)
            .AsEnumerable();
        _ = props.Where(x => x.PropertyType == typeof(SolidColorBrush));

        foreach (var prop in props) {
            var brush = (SolidColorBrush)prop.GetValue(null, null);
            var color = brush.Color;
            if (_cache.ContainsKey(color)) {
                continue;
            }

            _cache.Add(color, new WeakReference(brush));
        }
    }
}
