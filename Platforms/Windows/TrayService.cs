using Hardcodet.Wpf.TaskbarNotification.Interop;
using Microsoft.UI.Xaml;
using NetworkManager.Services;

namespace NetworkManager.Platforms.Windows;

public class TrayService : ITrayService
{
    WindowsTrayIcon tray;

    public Action LeftClickHandler { get; set; }
    public Action RightClickHandler { get; set; }
    public Action DoubleClickHandler { get; set; }

    public void Initialize()
    {
        tray = new WindowsTrayIcon("lan.ico");
        tray.LeftClick = () => {
            WindowExtensions.BringToFront();
            LeftClickHandler?.Invoke();
        };
        tray.RightClick = () => {
            WindowExtensions.BringToFront();
            RightClickHandler?.Invoke();
        };
        tray.DoubleClick = () => {
            WindowExtensions.BringToFront();
            DoubleClickHandler?.Invoke();
        };
    }
}
