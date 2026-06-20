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

- **21 styled controls** — Button, CheckBox, RadioButton, Slider, ComboBox, TextBox, PasswordBox, ScrollBar, ContextMenu, ToolTip, ListBox, ListView, DataGrid, TabControl, Card, ToggleButton, and more.
- **Smooth state animations** — All hover/focus/checked color changes use fast opacity transitions (0.1s) instead of instant snapping.
- **Color-theme aware** — All state colors use `DynamicResource`. Theme switches at runtime without flashes. No `ColorAnimation` anywhere.
- **Dark mode built-in** — Call `ThemeHelper.SetTheme(false)` for dark, `SetTheme(true)` for light. PCL-CE SkyBlue OKLCH-based palette, consumer decides.
- **PCL-CE visual fidelity** — Templates match the original PCL2 layout: border outlines, elastic-ease animations, scale-on-press feedback, animated card shadows.
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
        // true = light, false = dark — consumer decides
        ThemeHelper.SetTheme(false);
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
| Button        | `Button.xaml`      | Scale-on-press, variant color via `BorderBrush`, smooth hover fade |
| CheckBox      | `CheckBox.xaml`    | ElasticEase damping animation, indeterminate state |
| RadioButton   | `RadioButton.xaml` | ScaleTransform dot animation (0→1), left-aligned |
| Slider        | `Slider.xaml`      | WPF Track/Thumb, 14px thumb, thin 4px progress bar |
| TextBox       | `TextBox.xaml`     | CaretBrush/SelectionBrush for dark mode, animated focus |
| PasswordBox   | `TextBox.xaml`     | Same template as TextBox |
| ComboBox      | `ComboBox.xaml`    | Dropdown chevron rotation, animated focus |
| ScrollBar     | `ScrollBar.xaml`   | Thin track, theme-aware thumb, hover fade-in |
| ContextMenu   | `ContextMenu.xaml` | Implicit style with Separator styling |
| MenuItem      | `MenuItem.xaml`    | Implicit style for menu children |
| ToolTip       | `ToolTip.xaml`     | Rounded border via ControlTemplate override |
| Label         | `Label.xaml`       | Foreground + font matching |
| TextBlock     | `TextBlock.xaml`   | Default font/foreground |
| ToggleButton  | `ToggleButton.xaml`| Visible border, centered chevron, rotate animation, checked state |
| ScrollViewer  | `ScrollViewer.xaml`| Flush scroll styling |
| Window        | `Window.xaml`      | Default window chrome |
| ListBox       | `ListBox.xaml`     | ListBox + ListBoxItem + accent selection bar |
| ListView      | `ListView.xaml`    | ListView + ListViewItem + GridViewColumnHeader |
| DataGrid      | `DataGrid.xaml`    | DataGrid + column header + row + cell |
| TabControl    | `TabControl.xaml`  | Underline indicator, separator between tabs |
| Card          | `Card.xaml`        | PCL-CE MaterialCard: frosted background, animated shadow + border on hover |

## Theming

### Color resources

All colors are defined as `SolidColorBrush` resources in `Colors.xaml` (light) and `ColorsDark.xaml` (dark):

| Resource                     | Light     | Dark (SkyBlue) |
|------------------------------|-----------|----------------|
| `ColorBrush1` (text)         | `#343d4a` | `#dce4f0`      |
| `ColorBrush2` (accent)       | `#0b5bcb` | `#6ba3f5`      |
| `ColorBrush3` (primary)      | `#1370f3` | `#3d82e8`      |
| `ColorBrushBg0` (accent bg)  | `#96c0f9` | `#3e5070`      |
| `ColorBrushBackground`       | `#fbfbfb` | `#202834`      |
| `ColorBrushWhite`            | `#ffffff` | `#252d3c`      |
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
├── ThemeHelper.cs                     # Palette reorder (light/dark)
├── Themes/
│   ├── Generic.xaml                   # MergedDictionaries hub
│   ├── Colors.xaml                    # Light palette
│   ├── ColorsDark.xaml                # Dark palette
│   └── Controls/
│       ├── Button.xaml                # Per-component styles
│       ├── Card.xaml
│       ├── CheckBox.xaml
│       └── ... (21 files)
└── PlainToolkit.UI.Gallery/
    ├── App.xaml / App.xaml.cs          # ThemeHelper init demo
    └── MainWindow.xaml / .cs           # Interactive control gallery
```

## Dark Mode

`ThemeHelper` reorders `Colors.xaml` and `ColorsDark.xaml` within `Generic.xaml`'s MergedDictionaries. Both palettes are always loaded; the last one wins per key. `DynamicResource` re-evaluates on reorder — no flash, no restart.

```csharp
ThemeHelper.Initialize(Resources);  // call once at startup
ThemeHelper.SetTheme(false);         // false = dark, true = light
```

The dark palette uses PCL-CE SkyBlue OKLCH approximation (hue≈235°, inverted lightness per `ToneProfileConfig.DefaultDark`).

## Contributing

1. Fork the repo.
2. Add or modify a `.xaml` file under `Themes/Controls/`.
3. Verify in the Gallery project (`F5`).
4. Open a pull request.

**Guidelines:**
- Use overlay `Border` + `Opacity` animation for state transitions (never `ColorAnimation`).
- Use `{DynamicResource}` for all brush references, never `{StaticResource}` across files.
- Add matching dark resource in `ColorsDark.xaml` for any new color key.
- Overlays must sit **behind** content in z-order (`IsHitTestVisible="False"`).

## Thanks

- **[PCL Community](https://github.com/PCL-Community)** and all contributors of [PCL2-CE](https://github.com/PCL-Community/PCL2-CE) — the UI design and visual language that inspired this library. Every template is adapted from PCL2's `MyButton`, `MyCheckBox`, `MyRadioBox`, `MySlider`, `MyComboBox`, `MyTextBox`, `MyScrollBar`, and their companion styles.

## License

[MIT](LICENSE) © 2026 PCL Community
