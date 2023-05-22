using System.Drawing;
using System.IO;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using H.NotifyIcon.Core;
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

    [RelayCommand]
    public void ShowHideWindow()
    {
        var window = App.MainWindow;
        if (window == null)
        {
            return;
        }

        if (window.Visible)
        {
            window.Hide();
        }
        else
        {
            window.Show();
        }
    }

    [RelayCommand]
    public void ExitApplication()
    {
        //App.HandleClosedEvents = false;
        //TrayIcon.Dispose();
       // App.MainWindow?.Close();
    }
}
