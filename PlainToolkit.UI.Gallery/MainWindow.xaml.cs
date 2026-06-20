using System.Collections.ObjectModel;

namespace PlainToolkit.UI.Gallery;

public class SampleItem
{
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public int Value { get; set; }
}

public partial class MainWindow
{
    public ObservableCollection<SampleItem> SampleData { get; } = new()
    {
        new() { Name = "Alpha", Category = "A", Value = 10 },
        new() { Name = "Beta", Category = "B", Value = 25 },
        new() { Name = "Gamma", Category = "A", Value = 15 },
        new() { Name = "Delta", Category = "C", Value = 30 },
    };

    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }
}
