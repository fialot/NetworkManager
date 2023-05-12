
//using CoreFoundation;
//using CoreGraphics;
using Microsoft.Extensions.DependencyInjection;
using NetworkManager.Services;
using System;

namespace NetworkManager;

public partial class MainPage : ContentPage
{

    IPwork IPdev = new IPwork("networks.xml");

    public MainPage()
	{
		InitializeComponent();
        this.Loaded += MainPage_Loaded;
        this.Unloaded += MainPage_Unloaded;
    }


    private void MainPage_Loaded(object sender, EventArgs e)
    {
        RefreshProfiles();
        RefreshInterfaces();
        SetupTrayIcon();
    }
    private void MainPage_Unloaded(object sender, EventArgs e)
    {
        IPdev.Save();
    }


    private void CreateTrayMenu()
    {
        var trayService = ServiceProvider.GetService<ITrayService>();

        if (trayService != null)
        {

        }


        MenuFlyout menu = new MenuFlyout();
        MenuFlyoutItem item = new MenuFlyoutItem();
        item.Text = "test";
        menu.Add(item);

        //FlyoutBase.SetContextFlyout(trayService, menu);
    }

    private void HideWindow()
    {
#if WINDOWS
        var mauiWindow = App.Current.Windows.First();
        var nativeWindow = mauiWindow.Handler.PlatformView;
        IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);

        PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_HIDE);
#endif
    }

    private void RestoreWindow()
    {
#if WINDOWS
        var mauiWindow = App.Current.Windows.First();
        var nativeWindow = mauiWindow.Handler.PlatformView;
        IntPtr windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(nativeWindow);

        PInvoke.User32.ShowWindow(windowHandle, PInvoke.User32.WindowShowStyle.SW_RESTORE);
#endif


    }


    private void SetupTrayIcon()
    {
        var trayService = ServiceProvider.GetService<ITrayService>();

        if (trayService != null)
        {
            trayService.Initialize();
            /*trayService.LeftClickHandler = () =>
                ServiceProvider.GetService<INotificationService>()
                    ?.ShowNotification("Test", "FDF");*/
            trayService.RightClickHandler = () => CreateTrayMenu();
            trayService.DoubleClickHandler = () => RestoreWindow();
        }
    }

    private void Profile_Changed(object sender, EventArgs e)
    {
        LoadProfile(txtProfile.SelectedIndex);
    }

    private string RefreshIP()
    {
        /*string ip = IPdev.getIPstring(cbNetwork.Text);
        if (ip.Length > 63) ip = ip.Substring(0, 63);
        notifyIcon1.Text = ip;
        return ip;*/
        return "";
    }

    private void RefreshProfiles()
    {
        var list = IPdev.GetProfileNames();
        list.Add("New profile ...");

        var index = txtProfile.SelectedIndex;
        txtProfile.ItemsSource = list;
        if (txtProfile.Items.Count > 0)
        {
            if (index < 0) txtProfile.SelectedIndex = 0;
            else if (index < txtProfile.Items.Count) txtProfile.SelectedIndex = index;
            else txtProfile.SelectedIndex = 0;
        }

        LoadProfile(txtProfile.SelectedIndex);

        /*mnuProfiles.DropDownItems.Clear();

        for (int i = 0; i < list.Count; i++)
        {
            cbProfile.Items.Add(list[i].name);

            ToolStripMenuItem item = new ToolStripMenuItem();
            item.Name = "mnuProfile" + list[i].name;
            item.Text = list[i].name;
            item.Tag = i;
            item.Click += new EventHandler(mnuProfilesItem_Click);
            mnuProfiles.DropDownItems.Add(item);
        }*/
    }

    private void RefreshInterfaces()
    {
        var list = IPv4.GetInterfaces();

        var index = txtInterface.SelectedIndex;
        txtInterface.ItemsSource = list;
        if (txtInterface.Items.Count > 0)
        {
            if (index < 0) txtInterface.SelectedIndex = 1;
            else if (index < txtInterface.Items.Count) txtInterface.SelectedIndex = index;
            else txtInterface.SelectedIndex = 1;
        }
    }

    private void ResetComponents()
    {
        txtProfile.SelectedIndex = -1;
        txtInterface.SelectedIndex = -1;

        chbSetIP.IsChecked = false;
        rbIP.IsChecked = true;
        txtIP.Text = "";
        txtMask.Text = "";
        txtGateway.Text = "";
       
        chbSetDNS.IsChecked = false;
        rbDNS.IsChecked = true;
        txtDNS1.Text = "";
        txtDNS2.Text = "";
    }

    private void LoadProfile(int index)
    {
        var list = IPdev.GetList();
        if (index >= list.Count || index < 0)
        {
            ResetComponents();
            return;
        }

        txtProfile.SelectedItem = list[index].Name;
        txtInterface.SelectedItem = list[index].Interface;

        chbSetIP.IsChecked = list[index].SetIP;
        if (list[index].IsDHCP) rbIP.IsChecked = true;
        else rbIPStatic.IsChecked = true;
        txtIP.Text = list[index].IP;
        txtMask.Text = list[index].NetMask;
        txtGateway.Text = list[index].Gateway;

        chbSetDNS.IsChecked = list[index].SetDNS;
        if(list[index].IsAutoDNS) rbDNS.IsChecked = true;
        else rbDNSStatic.IsChecked = false;
        txtDNS1.Text = list[index].DNS1;
        txtDNS2.Text = list[index].DNS2;
    }

    private void OnLoadPage(object sender, EventArgs e)
    {
        /*string ver = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
        this.Text += " - v" + ver.Substring(0, ver.Length - 2);
        netRefresh();
        IPdev = new IPwork("networks.xml");
        loadProfile();
        loadList(0);

        // ----- minimalize to tray -----
        notifyIcon1.Visible = true;
        //notifyIcon1.ShowBalloonTip(500);
        this.Hide();

        this.WindowState = FormWindowState.Minimized;
        this.ShowInTaskbar = true;

        string notText = IPdev.getIPstring(cbNetwork.Text);
        if (notText.Length > 63) notText = notText.Substring(0, 63);
        notifyIcon1.Text = notText;
        newIPcheck = false;*/
    }


    private async void OnSaveProfileClicked(object sender, EventArgs e)
	{


        IpSetting item = new IpSetting((string)(txtProfile.SelectedItem ?? "") , (string)(txtInterface.SelectedItem ?? ""),
           chbSetIP.IsChecked, rbIP.IsChecked, txtIP.Text, txtMask.Text, txtGateway.Text,
           chbSetDNS.IsChecked, rbDNS.IsChecked, txtDNS1.Text, txtDNS2.Text);

       
        var list = IPdev.GetList();
        bool edited = false;
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name == (string)(txtProfile.SelectedItem ?? ""))
            {
                IPdev.EditList(i, item);
                edited = true;
                break;
            }
        }
        if (!edited)
        {
            var res = await DisplayPromptAsync("New profile", "Profile name:");
            if (res != null)
            {
                item.Name = res;
                IPdev.AddList(item);
                RefreshProfiles();
                txtProfile.SelectedIndex = txtProfile.Items.Count - 1;
            }
        }
    }

    private void OnDeleteProfileClicked(object sender, EventArgs e)
    {
       var list = IPdev.GetList();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name == (string)(txtProfile.SelectedItem ?? ""))
            {
                IPdev.DelList(i);
                RefreshProfiles();
                break;
            }
        }
       
    }

    private void OnRefreshInterfaceClicked(object sender, EventArgs e)
    {
        RefreshInterfaces();
    }


    private async void OnApplyClicked(object sender, EventArgs e)
    {
        IpSetting item = new IpSetting((string)txtProfile.SelectedItem, (string)txtInterface.SelectedItem, 
            chbSetIP.IsChecked, rbIP.IsChecked, txtIP.Text, txtMask.Text, txtGateway.Text,
            chbSetDNS.IsChecked, rbDNS.IsChecked, txtDNS1.Text, txtDNS2.Text);
      
        try
        {
            IPv4.Set(item);

            bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            //Dialogs.ShowInfo("IP updated!", "Info");
        }
        catch (Exception err)
        {
            await DisplayAlert("Error", err.Message, "Yes");
            //Dialogs.ShowErr(err.Message, "Error");
        }
        RefreshIP();
    }

}

