using System.Windows;

namespace PlainToolkit.UI.Gallery;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Theme must be set before window creation to avoid flash
        ThemeHelper.Initialize(Resources);
        // true = light, false = dark
        ThemeHelper.SetTheme(true);
        base.OnStartup(e);
    }
}
