using System;
using System.Drawing;
using System.IO;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using H.NotifyIcon.Core;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using NetworkManager.Contracts.Services;
using NetworkManager.Helpers;
using Windows.Devices.Usb;

namespace NetworkManager;

public sealed partial class MainWindow : WindowEx
{
    readonly IPwork IPdev = new("networks.xml");

    public MainWindow()
    {
        InitializeComponent();

        AppWindow.SetIcon(Path.Combine(AppContext.BaseDirectory, "Resources/Images/lan.ico"));
        Content = null;
        Title = "AppDisplayName".GetLocalized();

        
        trayIcon.ForceCreate();


        //trayIcon.DoubleClickCommand = ShowHideWindow;
        CreateTrayMenu();


        AppWindow.Closing += AppWindow_Closing;
    }

    private void AppWindow_Closing(Microsoft.UI.Windowing.AppWindow sender, Microsoft.UI.Windowing.AppWindowClosingEventArgs args)
    {
        AppWindow.Hide();
        args.Cancel = true;
    }

    private void CreateTrayMenu()
    {
        MenuFlyoutItem item;

        tryMenu.Items.Clear();

        // ----- Show / Hide -----
        item = new();
        item.Text = "Show/Hide Window";
        item.Command = ShowHideWindowCommand;
        tryMenu.Items.Add(item);

        // ----- Refresh profiles -----
        item = new();
        item.Text = "Refresh profiles";
        item.Command = RefreshProfilesCommand;
        tryMenu.Items.Add(item);

        // ----- Separator -----

        tryMenu.Items.Add(new MenuFlyoutSeparator());


        var profiles = IPdev.GetProfileNames();
        
        foreach (var profileNull in profiles)
        {
            var profile = profileNull ?? "";

            item = new();
            item.Text = profileNull ?? "Unknown";
            item.Command = ApplyProfileCommand;
            item.CommandParameter = profile;

            var splitProfile = profile.Split(new string[] { "/" }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (splitProfile.Length > 1)
            {
                var group = GetMenu(tryMenu, splitProfile[0]);
                if (group == null)
                {
                    group = new();
                    group.Text = splitProfile[0];
                    tryMenu.Items.Add(group);
                }

                item.Text = splitProfile[1];
                group.Items.Add(item);
            } 
            else
            {
                tryMenu.Items.Add(item);
            }
        }

        // ----- Separator -----
        tryMenu.Items.Add(new MenuFlyoutSeparator());

        // ----- Exit -----
        item = new();
        item.Text = "Exit";
        item.Command = ExitApplicationCommand;
        tryMenu.Items.Add(item);
    }

    private MenuFlyoutSubItem? GetMenu(MenuFlyout menu, string name)
    {
        foreach (var menuItem in menu.Items)
        {

            if (menuItem.GetType() == typeof(MenuFlyoutSubItem))
            {
                var item = (MenuFlyoutSubItem)menuItem;
                if (item.Text == name)
                    return item;
            }
        }

        return null;
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
    public void RefreshProfiles()
    {
        IPdev.LoadIPlist();
        CreateTrayMenu();
    }

    [RelayCommand]
    public void ApplyProfile(string name)
    {
        IpSetting item = IPdev.GetProfile(name);

        NetCommandClient client = new NetCommandClient();
        client.WriteCommand(item).Switch(
            message =>
            {
                App.GetService<IAppNotificationService>().ShowMessage("The setting change was successful", message);
            },
        error =>
        {
            App.GetService<IAppNotificationService>().ShowMessage("The setting change failed", error.Message);
        });
    }

    [RelayCommand]
    public void ExitApplication()
    {
        //App.HandleClosedEvents = false;
        //TrayIcon.Dispose();
        trayIcon.Dispose();
        App.MainWindow?.Close();
    }

}
