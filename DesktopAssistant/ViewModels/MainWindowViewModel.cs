using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Threading;

namespace DesktopAssistant.ViewModels;
public partial class MainWindowViewModel : ViewModelBase
{
    public Bitmap _bitmap { get; }
    public List<CroppedBitmap> idleAni = new List<CroppedBitmap>();
    public List<CroppedBitmap> walkAni = new List<CroppedBitmap>();
    public List<CroppedBitmap> frames = new List<CroppedBitmap>();
    private int currentFrame = 0;
    public CroppedBitmap _Frame
    {
        get => frames[currentFrame];
    }
    private readonly DispatcherTimer _timer;
    public MainWindowViewModel()
    {
        // Loads asset since it is saved as a URI
        var uri = new Uri("avares://DesktopAssistant/Assets/Spritesheet1.png");
        // Uses AssetLoader in order to open asset attached to stream, this deletes it quickly
        using var stream = AssetLoader.Open(uri);
        // Creates bitmap of opened asset
        _bitmap = new Bitmap(stream);
        
        // Load the individual frames
        for (int i = 0; i < 4; i++)
        {
            PixelRect rect = new PixelRect(i*56,1,56,72);
            CroppedBitmap frame = new CroppedBitmap(_bitmap,rect);
            idleAni.Add(frame);
        }
        for (int i = 0; i < 9; i++)
        {
            PixelRect rect = new PixelRect(i*56,97,56,72);
            CroppedBitmap frame = new CroppedBitmap(_bitmap,rect);
            walkAni.Add(frame);
        }

        frames = idleAni;
        
        _timer = new DispatcherTimer
        
        {
            Interval = TimeSpan.FromMilliseconds(200) // adjust speed
        };
        _timer.Tick += (_, _) => AdvanceFrame();
        _timer.Start();
    }

    private void AdvanceFrame()
    {
        currentFrame = (currentFrame + 1) % frames.Count;
        OnPropertyChanged(nameof(_Frame)); // Notify the view to redraw
    }

    public void SwitchToIdle()
    {
        frames = idleAni;
        currentFrame = 0;
        OnPropertyChanged(nameof(_Frame));
    }

    public void SwitchToWalk()
    {
        frames = walkAni;
        currentFrame = 0;
        OnPropertyChanged(nameof(_Frame));
    }
}
