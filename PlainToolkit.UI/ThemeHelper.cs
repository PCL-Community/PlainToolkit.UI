using System.Windows;

namespace PlainToolkit.UI;

/// <summary>
/// Reorders palette ResourceDictionaries at runtime.
/// Both Colors.xaml and ColorsDark.xaml are always loaded in Generic.xaml.
/// Light mode = ColorsDark.xaml before Colors.xaml (Colors.xaml wins).
/// Dark  mode = ColorsDark.xaml after  Colors.xaml (ColorsDark.xaml wins).
/// </summary>
public static class ThemeHelper
{
    private static ResourceDictionary? _genericDict;
    private static ResourceDictionary? _colorsLight;
    private static ResourceDictionary? _colorsDark;

    /// <summary>
    /// Initialize theme support. Must be called once at app startup.
    /// </summary>
    public static void Initialize(ResourceDictionary appResources)
    {
        // Find Generic.xaml inside Application.Resources.MergedDictionaries
        foreach (var dict in appResources.MergedDictionaries)
        {
            var src = dict.Source?.OriginalString ?? "";
            if (src.Contains("/Generic.xaml"))
            {
                _genericDict = dict;
                break;
            }
        }

        // Find Colors.xaml and ColorsDark.xaml inside Generic.xaml's MergedDictionaries
        foreach (var dict in _genericDict?.MergedDictionaries ?? [])
        {
            var src = dict.Source?.OriginalString ?? "";
            if (src.Contains("/Colors.xaml") && !src.Contains("Dark"))
                _colorsLight = dict;
            if (src.Contains("/ColorsDark.xaml"))
                _colorsDark = dict;
        }
    }

    /// <summary>
    /// Set light (true) or dark (false) mode.
    /// </summary>
    public static void SetTheme(bool isLight)
    {
        if (_genericDict == null || _colorsLight == null || _colorsDark == null) return;

        var dicts = _genericDict.MergedDictionaries;

        if (isLight)
        {
            // ColorsDark.xaml before Colors.xaml → Colors.xaml wins
            if (dicts.IndexOf(_colorsDark) > dicts.IndexOf(_colorsLight))
            {
                dicts.Remove(_colorsDark);
                var lightIdx = dicts.IndexOf(_colorsLight);
                dicts.Insert(lightIdx, _colorsDark);
            }
        }
        else
        {
            // ColorsDark.xaml after Colors.xaml → ColorsDark.xaml wins
            if (dicts.IndexOf(_colorsDark) < dicts.IndexOf(_colorsLight))
            {
                dicts.Remove(_colorsDark);
                var lightIdx = dicts.IndexOf(_colorsLight);
                dicts.Insert(lightIdx + 1, _colorsDark);
            }
        }
    }
}
