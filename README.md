<h1 align="center">PlainToolkit.UI</h1>

<p align="center">
  <strong>WPF control library extracted from <a href="https://github.com/PCL-Community/PCL2-CE">PCL Craft Launcher 2 Community Edition</a></strong>
  <br />
  Clean, modern WPF styles and templates — ready to drop into any .NET WPF project.
</p>

<p align="center">
  <a href="#features">Features</a> •
  <a href="#installation">Installation</a> •
  <a href="#usage">Usage</a> •
  <a href="#controls">Controls</a> •
  <a href="#theming">Theming</a> •
  <a href="#contributing">Contributing</a> •
  <a href="#thanks">Thanks</a>
</p>

---

## Features

- **17+ styled controls** — Button, CheckBox, RadioButton, Slider, ComboBox, TextBox, PasswordBox, ScrollBar, ContextMenu, ToolTip, and more.
- **Color-theme aware** — All hover/focus/checked states use `DynamicResource` setters (no hardcoded `ColorAnimation`). Theme switches at runtime without flashes.
- **Dark mode built-in** — `ThemeHelper` detects Windows system dark mode (registry poll) and swaps color palettes live.
- **PCL-CE visual fidelity** — Templates match the original PCL2 layout exactly: border outlines, elastic-ease animations, scale-on-press feedback.
- **Zero custom controls** — Pure XAML styles + one utility class (`ThemeHelper.cs`). No custom WPF controls to maintain.

## Installation

```shell
dotnet add reference PlainToolkit.UI/PlainToolkit.UI.csproj
```

Or add a project reference in your `.csproj`:

```xml
<ItemGroup>
  <ProjectReference Include="..\PlainToolkit.UI\PlainToolkit.UI.csproj" />
</ItemGroup>
```

## Usage

### 1. Reference the theme

In your `App.xaml` (or any top-level `ResourceDictionary`):

```xml
<Application.Resources>
  <ResourceDictionary>
    <ResourceDictionary.MergedDictionaries>
      <ResourceDictionary Source="pack://application:,,,/PlainToolkit.UI;component/Themes/Generic.xaml" />
    </ResourceDictionary.MergedDictionaries>
  </ResourceDictionary>
</Application.Resources>
```

### 2. Enable dark mode (optional)

```csharp
// App.xaml.cs
using PlainToolkit.UI;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ThemeHelper.Initialize(Resources);
    }
}
```

### 3. Use styled controls

All standard WPF controls automatically pick up the implicit styles — no special markup needed:

```xml
<Button Content="Primary" Background="{StaticResource ColorBrush2}" BorderBrush="{StaticResource ColorBrush2}" />
<Button Content="Danger"  Background="{StaticResource ColorBrushTransparent}" BorderBrush="{StaticResource ColorBrushRedDark}" />
<CheckBox Content="Enable logging" />
<RadioButton Content="Option A" GroupName="group" />
<Slider Minimum="0" Maximum="100" Value="42" Width="200" />
```

## Controls

| Control       | File               | Highlights |
|---------------|---------------------|------------|
| Button        | `Button.xaml`      | Scale-on-press, variant color via `BorderBrush` |
| CheckBox      | `CheckBox.xaml`    | ElasticEase damping animation, indeterminate state |
| RadioButton   | `RadioButton.xaml` | ScaleTransform dot animation (0→1), left-aligned |
| Slider        | `Slider.xaml`      | WPF Track/Thumb, 14px thumb, thin 4px progress bar |
| TextBox       | `TextBox.xaml`     | CaretBrush/SelectionBrush for dark mode |
| PasswordBox   | `TextBox.xaml`     | Same template as TextBox |
| ComboBox      | `ComboBox.xaml`    | Dropdown chevron rotation animation |
| ScrollBar     | `ScrollBar.xaml`   | Thin track, theme-aware thumb |
| ContextMenu   | `ContextMenu.xaml` | Implicit style with Separator styling |
| MenuItem      | `MenuItem.xaml`    | Implicit style for menu children |
| ToolTip       | `ToolTip.xaml`     | Rounded border via ControlTemplate override |
| Label         | `Label.xaml`       | Foreground + font matching |
| TextBlock     | `TextBlock.xaml`   | Default font/foreground |
| ToggleButton  | `ToggleButton.xaml`| Checked state styling |
| Calendar      | `Calendar.xaml`    | Theme-consistent day cells |
| ScrollViewer  | `ScrollViewer.xaml`| Flush scroll styling |
| Window        | `Window.xaml`      | Default window chrome |

## Theming

### Color resources

All colors are defined as `SolidColorBrush` resources in `Colors.xaml` (light) and `ColorsDark.xaml` (dark):

| Resource                     | Light     | Dark (approx) |
|------------------------------|-----------|---------------|
| `ColorBrush1` (text)         | `#343d4a` | `#EAEAEA`     |
| `ColorBrush2` (accent)       | `#0b5bcb` | `#7BA8F0`     |
| `ColorBrushBg0` (accent bg)  | `#96c0f9` | `#4A6A9E`     |
| `ColorBrushBackground`       | `#fbfbfb` | `#404040`     |
| `ColorBrushWhite`            | `#ffffff` | `#323232`     |
| `ColorBrushGray1`–`Gray8`    | Light grays | Dark grays (inverted) |

### State-specific resources

| Resource                        | Usage                          |
|---------------------------------|--------------------------------|
| `ColorBrushButtonHoverBg`       | Button hover background        |
| `ColorBrushInputFocusBorder`    | TextBox/ComboBox focus border  |
| `ColorBrushInputFocusBg`        | TextBox/ComboBox focus bg      |
| `ColorBrushScrollBarThumb`      | ScrollBar thumb fill           |
| `ColorBrushRedDark`             | Danger/error border            |

### Custom theme

Override any resource in your app-level dictionary _after_ merging Generic.xaml:

```xml
<SolidColorBrush x:Key="ColorBrush2" Color="#FF6B35" />
```

## Project Structure

```
PlainToolkit.UI/
├── PlainToolkit.UI.csproj
├── ThemeHelper.cs                     # Dark mode detection + palette swap
├── Themes/
│   ├── Generic.xaml                   # MergedDictionaries hub
│   ├── Colors.xaml                    # Light palette
│   ├── ColorsDark.xaml                # Dark palette
│   ├── Button.xaml                    # Per-component styles
│   ├── CheckBox.xaml
│   ├── RadioButton.xaml
│   ├── Slider.xaml
│   └── ... (17 files)
└── PlainToolkit.UI.Gallery/
    ├── App.xaml / App.xaml.cs          # ThemeHelper init demo
    └── MainWindow.xaml / .cs           # Interactive control gallery
```

## Dark Mode

`ThemeHelper` reads the Windows registry key `HKCU\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize\AppsUseLightTheme` and polls every 5 seconds. On change it swaps `ColorsDark.xaml` into the merged dictionaries.

To disable auto-detection:

```csharp
ThemeHelper.Initialize(Resources, enableAutoDetection: false);
```

## Contributing

1. Fork the repo.
2. Add or modify a `.xaml` file under `Themes/`.
3. Verify in the Gallery project (`F5`).
4. Open a pull request.

**Guidelines:**
- Keep animations color-independent (scale/rotate only — use `DoubleAnimation`, never `ColorAnimation`).
- Use `{DynamicResource}` for all brush references, never `{StaticResource}` across files.
- Add matching dark resource in `ColorsDark.xaml` for any new color key.

## Thanks

- **[PCL Community](https://github.com/PCL-Community)** and all contributors of [PCL2-CE](https://github.com/PCL-Community/PCL2-CE) — the UI design and visual language that inspired this library. Every template is adapted from PCL2's `MyButton`, `MyCheckBox`, `MyRadioBox`, `MySlider`, `MyComboBox`, `MyTextBox`, `MyScrollBar`, and their companion styles.

## License

[MIT](LICENSE) © 2026 PCL Community
