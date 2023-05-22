using System.Drawing;
using System.IO;
using H.NotifyIcon;
using NetworkManager.Helpers;

namespace NetworkManager;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Assets/WindowIcon.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();


        trayIcon.ForceCreate();
    }
}
