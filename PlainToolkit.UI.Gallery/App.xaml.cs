using System.Windows;

namespace PlainToolkit.UI.Gallery;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Theme must be set before window creation to avoid flash
        ThemeHelper.Initialize(Resources);
        // Default to light mode; call ThemeHelper.SetTheme(false) for dark
        base.OnStartup(e);
    }
}
