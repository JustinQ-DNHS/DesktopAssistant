using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Input;
using DesktopAssistant.ViewModels;
using Avalonia;

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

        var screen = Screens.Primary;
        if (screen != null)
        {
            Position = new PixelPoint(
                screen.WorkingArea.X + 10,
                screen.WorkingArea.Y + 10
            );
        }
    }

    private bool _walking = false;

    private void PointerPressedHandler(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is MainWindowViewModel vm)
        {
            if (_walking)
                vm.SwitchToIdle();
            else
                vm.SwitchToWalk();

            _walking = !_walking;
        }
    }
}