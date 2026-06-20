using System.Windows;

namespace PlainToolkit.UI.Gallery;

public partial class App
{
    protected override void OnStartup(StartupEventArgs e)
    {
        // Auto-detect Windows dark/light mode
        ThemeHelper.Initialize(Resources);
    }
}
