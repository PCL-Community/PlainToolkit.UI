using System.Windows;
using System.Windows.Threading;

namespace PlainToolkit.UI;

/// <summary>
/// Detects Windows dark mode and swaps theme ResourceDictionaries at runtime.
/// Not a custom control — utility only.
/// </summary>
public static class ThemeHelper
{
    private const string RegistryPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private const string RegistryValue = "AppsUseLightTheme";

    private static ResourceDictionary? _darkColors;
    private static ResourceDictionary? _targetRoot;
    private static DispatcherTimer? _timer;

    /// <summary>
    /// Initialize automatic dark mode detection.
    /// Call once at app startup, passing the Generic.xaml ResourceDictionary.
    /// </summary>
    public static void Initialize(ResourceDictionary genericRoot)
    {
        _targetRoot = genericRoot;

        // Locate ColorsDark.xaml in merged dictionaries, or create placeholder
        foreach (var dict in genericRoot.MergedDictionaries)
        {
            var src = dict.Source?.OriginalString ?? "";
            if (src.EndsWith("/ColorsDark.xaml") || src.EndsWith("/ColorsDark.xaml"))
            {
                _darkColors = dict;
                break;
            }
        }

        if (_darkColors == null)
        {
            var uri = new Uri("pack://application:,,,/PlainToolkit.UI;component/Themes/ColorsDark.xaml", UriKind.Absolute);
            _darkColors = new ResourceDictionary { Source = uri };
        }

        // Apply current OS theme
        ApplyThemeFromOS();

        // Poll registry every 5s for theme changes
        _timer = new DispatcherTimer(TimeSpan.FromSeconds(5), DispatcherPriority.Background,
            (_, _) => ApplyThemeFromOS(), Dispatcher.CurrentDispatcher);
        _timer.Start();
    }

    /// <summary>
    /// Manually set light (true) or dark (false) mode.
    /// </summary>
    public static void SetTheme(bool isLight)
    {
        if (_targetRoot == null || _darkColors == null) return;

        if (isLight)
        {
            // Remove dark override so Colors.xaml wins
            if (_targetRoot.MergedDictionaries.Contains(_darkColors))
                _targetRoot.MergedDictionaries.Remove(_darkColors);
        }
        else
        {
            // Insert dark override after the first few (Colors.xaml) so it overrides
            if (!_targetRoot.MergedDictionaries.Contains(_darkColors))
            {
                // Insert after position 0 (Colors.xaml) so dark wins
                _targetRoot.MergedDictionaries.Insert(1, _darkColors);
            }
        }
    }

    private static void ApplyThemeFromOS()
    {
        try
        {
            using var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryPath);
            var val = key?.GetValue(RegistryValue);
            var isLight = val is int i && i != 0;
            SetTheme(isLight);
        }
        catch
        {
            SetTheme(true);
        }
    }
}
