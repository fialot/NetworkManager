﻿#pragma checksum "C:\Projekty\Software\C#\#Private\NetworkManager2\NetworkManager\NetworkManager\Views\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "125F74D0D0CD8A353C23CC922137CA73897330D081CD371829A8A2B445D6FC83"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetworkManager.Views
{
    partial class MainPage : 
        global::Microsoft.UI.Xaml.Controls.Page, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MainPage.xaml line 12
                {
                    this.ContentArea = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Grid>(target);
                }
                break;
            case 3: // Views\MainPage.xaml line 198
                {
                    this.btnApply = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnApply).Click += this.btnApply_Click;
                }
                break;
            case 4: // Views\MainPage.xaml line 153
                {
                    this.chbSetDNS = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                }
                break;
            case 5: // Views\MainPage.xaml line 174
                {
                    this.lblDNS2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 6: // Views\MainPage.xaml line 175
                {
                    this.txtDNS2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.txtDNS2).LostFocus += this.CheckIP_LostFocus;
                }
                break;
            case 7: // Views\MainPage.xaml line 177
                {
                    this.flyDNS2 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 8: // Views\MainPage.xaml line 161
                {
                    this.lblDNS1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 9: // Views\MainPage.xaml line 162
                {
                    this.txtDNS1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.txtDNS1).LostFocus += this.CheckIP_LostFocus;
                }
                break;
            case 10: // Views\MainPage.xaml line 164
                {
                    this.flyDNS1 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 11: // Views\MainPage.xaml line 156
                {
                    this.rbDnsAuto = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.rbDnsAuto).Checked += this.rbDnsAuto_Checked;
                }
                break;
            case 12: // Views\MainPage.xaml line 157
                {
                    this.rbDnsStatic = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.rbDnsStatic).Checked += this.rbDnsStatic_Checked;
                }
                break;
            case 13: // Views\MainPage.xaml line 91
                {
                    this.chbSetIP = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.CheckBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.chbSetIP).Checked += this.chbSetIP_Checked;
                    ((global::Microsoft.UI.Xaml.Controls.CheckBox)this.chbSetIP).Unchecked += this.chbSetIP_Unchecked;
                }
                break;
            case 14: // Views\MainPage.xaml line 125
                {
                    this.lblGateway = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 15: // Views\MainPage.xaml line 126
                {
                    this.txtGateway = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.txtGateway).LostFocus += this.CheckIP_LostFocus;
                }
                break;
            case 16: // Views\MainPage.xaml line 128
                {
                    this.flyGateway = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 17: // Views\MainPage.xaml line 112
                {
                    this.lblMask = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 18: // Views\MainPage.xaml line 113
                {
                    this.txtMask = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.txtMask).LostFocus += this.CheckIP_LostFocus;
                }
                break;
            case 19: // Views\MainPage.xaml line 115
                {
                    this.flyMask = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 20: // Views\MainPage.xaml line 99
                {
                    this.lblIP = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 21: // Views\MainPage.xaml line 100
                {
                    this.txtIP = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.TextBox)this.txtIP).LostFocus += this.CheckIP_LostFocus;
                }
                break;
            case 22: // Views\MainPage.xaml line 102
                {
                    this.flyIp = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 23: // Views\MainPage.xaml line 94
                {
                    this.rbIpDHCP = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.rbIpDHCP).Checked += this.rbIpDHCP_Checked;
                }
                break;
            case 24: // Views\MainPage.xaml line 95
                {
                    this.rbIpStatic = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                    ((global::Microsoft.UI.Xaml.Controls.RadioButton)this.rbIpStatic).Checked += this.rbIpStatic_Checked;
                }
                break;
            case 25: // Views\MainPage.xaml line 36
                {
                    this.lblProfile = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 26: // Views\MainPage.xaml line 37
                {
                    this.cbProfile = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                    ((global::Microsoft.UI.Xaml.Controls.ComboBox)this.cbProfile).SelectionChanged += this.cbProfile_SelectionChanged;
                }
                break;
            case 27: // Views\MainPage.xaml line 61
                {
                    this.lblInterface = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            case 28: // Views\MainPage.xaml line 62
                {
                    this.cbInterface = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ComboBox>(target);
                }
                break;
            case 29: // Views\MainPage.xaml line 63
                {
                    this.btnInterfaceRefresh = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnInterfaceRefresh).Click += this.btnInterfaceRefresh_Click;
                }
                break;
            case 30: // Views\MainPage.xaml line 39
                {
                    this.btnSaveProfile = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnSaveProfile).Click += this.btnSaveProfile_Click;
                }
                break;
            case 31: // Views\MainPage.xaml line 48
                {
                    this.btnDeleteProfile = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.btnDeleteProfile).Click += this.btnDeleteProfile_Click;
                }
                break;
            case 32: // Views\MainPage.xaml line 50
                {
                    this.flyDelete = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 33: // Views\MainPage.xaml line 53
                {
                    global::Microsoft.UI.Xaml.Controls.Button element33 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)element33).Click += this.DeleteConfirmation_Click;
                }
                break;
            case 34: // Views\MainPage.xaml line 41
                {
                    this.flySave = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Flyout>(target);
                }
                break;
            case 35: // Views\MainPage.xaml line 43
                {
                    this.flySaveText = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

