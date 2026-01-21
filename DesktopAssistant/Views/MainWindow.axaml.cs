using Avalonia.Controls;
using Avalonia.Media;

namespace DesktopAssistant.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        // Set window to be transparent
        TransparencyLevelHint = new[] { WindowTransparencyLevel.Transparent };

        // Set the actual background brush to transparent
        Background = Brushes.Transparent;
    }
}