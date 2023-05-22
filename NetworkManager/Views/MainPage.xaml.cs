using System;
using System.Net;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using NetworkManager.Contracts.Services;
using NetworkManager.Helpers;
using NetworkManager.ViewModels;
using Windows.Devices.Usb;
using WinUIEx.Messaging;
namespace NetworkManager.Views;

public sealed partial class MainPage : Page
{
    readonly IPwork IPdev = new("networks.xml");

    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();

        this.Loaded += MainPage_Loaded;
        this.Unloaded += MainPage_Unloaded;
    }

   
    private void MainPage_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ResetComponents();
        RefreshProfiles();
        RefreshInterfaces();
    }

    private void MainPage_Unloaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        
    }

    private void btnDeleteProfile_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        if (cbProfile.SelectedIndex >= 0)
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }

    private async void btnSaveProfile_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        var profileName = (string)(cbProfile.SelectedItem ?? "");

        if (profileName == "")
        {
            flySaveText.Text = "Profile must have name!";
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            return;
        }

        // ----- Validate IP -----
        var validIp = true;
        if (chbSetIP.IsChecked ?? false)
        {
            if (!(rbIpDHCP.IsChecked ?? false))
            {
                if (!IPv4.ValidateIP(txtIP.Text) || IPv4.ValidateIP(txtMask.Text) || (txtGateway.Text != "" && IPv4.ValidateIP(txtGateway.Text)))
                {
                    validIp = false;
                }
            }
        }
        if (chbSetDNS.IsChecked ?? false)
        {
            if (!(rbDnsAuto.IsChecked ?? false))
            {
                if (!IPv4.ValidateIP(txtDNS1.Text) || (txtDNS2.Text != "" && IPv4.ValidateIP(txtDNS2.Text)))
                {
                    validIp = false;
                }
            }
        }

        if (!validIp)
        {
            flySaveText.Text = "Some ip address is not valid!";
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            return;
        }

        IpSetting item = new IpSetting(profileName, (string)(cbInterface.SelectedItem ?? ""),
           chbSetIP.IsChecked ?? false, rbIpDHCP.IsChecked ?? false, txtIP.Text, txtMask.Text, txtGateway.Text,
           chbSetDNS.IsChecked ?? false, rbDnsAuto.IsChecked ?? false, txtDNS1.Text, txtDNS2.Text);

        var profileIndex = IPdev.GetProfileIndex(profileName);
        if (profileIndex >= 0) 
        {
            ContentDialog dialog = new ContentDialog();

            // XamlRoot must be set in the case of a ContentDialog running in a Desktop app
            dialog.XamlRoot = this.XamlRoot;
            dialog.Style = Application.Current.Resources["DefaultContentDialogStyle"] as Style;
            dialog.Title = "Really edit profile?";
            dialog.PrimaryButtonText = "Save";
            //dialog.SecondaryButtonText = "Don't Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            //dialog.Content = new ContentDialogContent();

            var result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                IPdev.EditList(profileIndex, item);
                IPdev.Save();
            }
        } 
        else
        {
            IPdev.AddList(item);
            IPdev.Save();
            RefreshProfiles();
            cbProfile.SelectedIndex = cbProfile.Items.Count - 1;
        }
    }

    private void chbSetIP_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void chbSetIP_Unchecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }

    private void rbIpDHCP_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        txtIP.IsEnabled = false;
        txtMask.IsEnabled = false;
        txtGateway.IsEnabled = false;

        rbDnsAuto.IsEnabled = true;
    }

    private void rbIpStatic_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        txtIP.IsEnabled = true;
        txtMask.IsEnabled = true;
        txtGateway.IsEnabled = true;

        rbDnsAuto.IsEnabled = false;
        rbDnsStatic.IsChecked = true;
    }

    private void btnInterfaceRefresh_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        RefreshInterfaces();
    }

    private void RefreshProfiles()
    {
        var list = IPdev.GetProfileNames();

        var index = cbProfile.SelectedIndex;
        cbProfile.ItemsSource = list;
        if (cbProfile.Items.Count > 0)
        {
            if (index < 0) cbProfile.SelectedIndex = 0;
            else if (index < cbProfile.Items.Count) cbProfile.SelectedIndex = index;
            else cbProfile.SelectedIndex = 0;
        }

        if (cbProfile.SelectedIndex < 0) btnDeleteProfile.IsEnabled = false;
        else btnDeleteProfile.IsEnabled = true;

        LoadProfile(cbProfile.SelectedIndex);

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

    private void LoadProfile(int index)
    {
        var list = IPdev.GetList();
        if (index >= list.Count || index < 0)
        {
            //ResetComponents();
            return;
        }

        cbProfile.SelectedItem = list[index].Name;
        cbInterface.SelectedItem = list[index].Interface;
        // ----- If interface not exist -----
        if ((string)cbInterface.SelectedItem != list[index].Interface)
        {
            cbInterface.Items.Add(list[index].Interface);
            cbInterface.SelectedItem = list[index].Interface;
        }

        chbSetIP.IsChecked = list[index].SetIP;
        if (list[index].IsDHCP) rbIpDHCP.IsChecked = true;
        else rbIpStatic.IsChecked = true;
        txtIP.Text = list[index].IP;
        txtMask.Text = list[index].NetMask;
        txtGateway.Text = list[index].Gateway;

        chbSetDNS.IsChecked = list[index].SetDNS;
        if (list[index].IsAutoDNS) rbDnsAuto.IsChecked = true;
        else rbDnsStatic.IsChecked = true;
        txtDNS1.Text = list[index].DNS1;
        txtDNS2.Text = list[index].DNS2;
    }

    private void RefreshInterfaces()
    {
        var list = IPv4.GetInterfaces();

        var index = cbInterface.SelectedIndex;
        cbInterface.Items.Clear();
        foreach (var iface in list)
        {
            cbInterface.Items.Add(iface);
        }

        //cbInterface.ItemsSource = list;
        if (cbInterface.Items.Count > 0)
        {
            if (index < 0) cbInterface.SelectedIndex = 0;
            else if (index < cbInterface.Items.Count) cbInterface.SelectedIndex = index;
            else cbInterface.SelectedIndex = 0;
        }
    }

    private void ResetComponents()
    {
        cbProfile.SelectedIndex = -1;
        cbInterface.SelectedIndex = cbInterface.Items.Count -1;

        chbSetIP.IsChecked = false;
        rbIpDHCP.IsChecked = true;
        txtIP.Text = "";
        txtMask.Text = "";
        txtGateway.Text = "";

        chbSetDNS.IsChecked = false;
        rbDnsAuto.IsChecked = true;
        txtDNS1.Text = "";
        txtDNS2.Text = "";
    }

    private void cbProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        LoadProfile(cbProfile.SelectedIndex);
        if (cbProfile.SelectedIndex < 0) btnDeleteProfile.IsEnabled = false;
        else btnDeleteProfile.IsEnabled = true;
    }

    private void btnApply_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        IpSetting item = new IpSetting((string)(cbProfile.SelectedItem ?? ""), (string)(cbInterface.SelectedItem ?? ""),
           chbSetIP.IsChecked ?? false, rbIpDHCP.IsChecked ?? false, txtIP.Text, txtMask.Text, txtGateway.Text,
           chbSetDNS.IsChecked ?? false, rbDnsAuto.IsChecked ?? false, txtDNS1.Text, txtDNS2.Text);

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

        //string msg = "<toast>\r\n  <visual>\r\n    <binding template=\"ToastGeneric\">\r\n      <text>App Notification</text>\r\n      <text>ahoj</text>\r\n     </binding>\r\n  </visual>\r\n  </toast>";


        /*try
        {
            IPv4.Set(item);

            
            App.GetService<IAppNotificationService>().ShowMessage(msg);

            //bool answer = await DisplayAlert("Question?", "Would you like to play a game", "Yes", "No");
            //Dialogs.ShowInfo("IP updated!", "Info");
        }
        catch (Exception err)
        {
            App.GetService<IAppNotificationService>().Show(msg); //string.Format("Set Error: " + err.Message, AppContext.BaseDirectory)
            //await DisplayAlert("Error", err.Message, "Yes");
            //Dialogs.ShowErr(err.Message, "Error");
        }*/
        //RefreshIP();
    }

    private void DeleteConfirmation_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

        var list = IPdev.GetList();
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].Name == (string)(cbProfile.SelectedItem ?? ""))
            {
                IPdev.DelList(i);
                IPdev.Save();
                RefreshProfiles();
                break;
            }
        }

        flyDelete.Hide();
    }

    private void rbDnsAuto_Checked(object sender, RoutedEventArgs e)
    {
        txtDNS1.IsEnabled = false;
        txtDNS2.IsEnabled = false;
    }

    private void rbDnsStatic_Checked(object sender, RoutedEventArgs e)
    {
        txtDNS1.IsEnabled = true;
        txtDNS2.IsEnabled = true;
    }

    private void CheckIP_LostFocus(object sender, RoutedEventArgs e)
    {
        var item = (Microsoft.UI.Xaml.Controls.TextBox)sender;

        if (item.Text != "" && !IPv4.ValidateIP(item.Text))
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
    }
}
