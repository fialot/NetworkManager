﻿#pragma checksum "C:\Projekty\Software\C#\#Private\NetworkManager2\NetworkManager\NetworkManager\Views\ListDetailsDetailControl.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "60358232B50D4B3D58C62474849577785DB7B936BE14BAC83C08754E9CEC73B1"
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
    partial class ListDetailsDetailControl : 
        global::Microsoft.UI.Xaml.Controls.UserControl, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(global::Microsoft.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
            public static void Set_Microsoft_UI_Xaml_Controls_FontIcon_Glyph(global::Microsoft.UI.Xaml.Controls.FontIcon obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Glyph = value ?? global::System.String.Empty;
            }
            public static void Set_Microsoft_UI_Xaml_Automation_AutomationProperties_Name(global::Microsoft.UI.Xaml.DependencyObject obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                global::Microsoft.UI.Xaml.Automation.AutomationProperties.SetName(obj, value);
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class ListDetailsDetailControl_obj1_Bindings :
            global::Microsoft.UI.Xaml.Markup.IDataTemplateComponent,
            global::Microsoft.UI.Xaml.Markup.IXamlBindScopeDiagnostics,
            global::Microsoft.UI.Xaml.Markup.IComponentConnector,
            IListDetailsDetailControl_Bindings
        {
            private global::NetworkManager.Views.ListDetailsDetailControl dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj3;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj4;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj5;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj6;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj7;
            private global::Microsoft.UI.Xaml.Controls.FontIcon obj8;
            private global::Microsoft.UI.Xaml.Controls.TextBlock obj9;

            // Static fields for each binding's enabled/disabled state
            private static bool isobj3TextDisabled = false;
            private static bool isobj4TextDisabled = false;
            private static bool isobj5TextDisabled = false;
            private static bool isobj6TextDisabled = false;
            private static bool isobj7TextDisabled = false;
            private static bool isobj8GlyphDisabled = false;
            private static bool isobj8NameDisabled = false;
            private static bool isobj9TextDisabled = false;

            private ListDetailsDetailControl_obj1_BindingsTracking bindingsTracking;

            public ListDetailsDetailControl_obj1_Bindings()
            {
                this.bindingsTracking = new ListDetailsDetailControl_obj1_BindingsTracking(this);
            }

            public void Disable(int lineNumber, int columnNumber)
            {
                if (lineNumber == 30 && columnNumber == 28)
                {
                    isobj3TextDisabled = true;
                }
                else if (lineNumber == 36 && columnNumber == 28)
                {
                    isobj4TextDisabled = true;
                }
                else if (lineNumber == 41 && columnNumber == 28)
                {
                    isobj5TextDisabled = true;
                }
                else if (lineNumber == 47 && columnNumber == 28)
                {
                    isobj6TextDisabled = true;
                }
                else if (lineNumber == 52 && columnNumber == 28)
                {
                    isobj7TextDisabled = true;
                }
                else if (lineNumber == 19 && columnNumber == 21)
                {
                    isobj8GlyphDisabled = true;
                }
                else if (lineNumber == 20 && columnNumber == 21)
                {
                    isobj8NameDisabled = true;
                }
                else if (lineNumber == 24 && columnNumber == 21)
                {
                    isobj9TextDisabled = true;
                }
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 3: // Views\ListDetailsDetailControl.xaml line 30
                        this.obj3 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 4: // Views\ListDetailsDetailControl.xaml line 36
                        this.obj4 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 5: // Views\ListDetailsDetailControl.xaml line 41
                        this.obj5 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 6: // Views\ListDetailsDetailControl.xaml line 47
                        this.obj6 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 7: // Views\ListDetailsDetailControl.xaml line 52
                        this.obj7 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    case 8: // Views\ListDetailsDetailControl.xaml line 15
                        this.obj8 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.FontIcon>(target);
                        break;
                    case 9: // Views\ListDetailsDetailControl.xaml line 21
                        this.obj9 = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.TextBlock>(target);
                        break;
                    default:
                        break;
                }
            }
                        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
                        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
                        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target) 
                        {
                            return null;
                        }

            // IDataTemplateComponent

            public void ProcessBindings(global::System.Object item, int itemIndex, int phase, out int nextPhase)
            {
                nextPhase = -1;
            }

            public void Recycle()
            {
                return;
            }

            // IListDetailsDetailControl_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
                this.bindingsTracking.ReleaseAllListeners();
                this.initialized = false;
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                this.bindingsTracking.ReleaseAllListeners();
                if (newDataRoot != null)
                {
                    this.dataRoot = global::WinRT.CastExtensions.As<global::NetworkManager.Views.ListDetailsDetailControl>(newDataRoot);
                    return true;
                }
                return false;
            }

            public void Activated(object obj, global::Microsoft.UI.Xaml.WindowActivatedEventArgs data)
            {
                this.Initialize();
            }

            public void Loading(global::Microsoft.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::NetworkManager.Views.ListDetailsDetailControl obj, int phase)
            {
                this.bindingsTracking.UpdateChildListeners_(obj);
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ListDetailsMenuItem(obj.ListDetailsMenuItem, phase);
                    }
                }
            }
            private void Update_ListDetailsMenuItem(global::NetworkManager.Core.Models.SampleOrder obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | DATA_CHANGED | (1 << 0))) != 0)
                    {
                        this.Update_ListDetailsMenuItem_Status(obj.Status, phase);
                        this.Update_ListDetailsMenuItem_OrderDate(obj.OrderDate, phase);
                        this.Update_ListDetailsMenuItem_Company(obj.Company, phase);
                        this.Update_ListDetailsMenuItem_ShipTo(obj.ShipTo, phase);
                        this.Update_ListDetailsMenuItem_OrderTotal(obj.OrderTotal, phase);
                        this.Update_ListDetailsMenuItem_Symbol(obj.Symbol, phase);
                        this.Update_ListDetailsMenuItem_SymbolName(obj.SymbolName, phase);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_Status(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 30
                    if (!isobj3TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj3, obj, null);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_OrderDate(global::System.DateTime obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 36
                    if (!isobj4TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj4, obj.ToString(), null);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_Company(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 41
                    if (!isobj5TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj5, obj, null);
                    }
                    // Views\ListDetailsDetailControl.xaml line 21
                    if (!isobj9TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj9, obj, null);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_ShipTo(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 47
                    if (!isobj6TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj6, obj, null);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_OrderTotal(global::System.Double obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 52
                    if (!isobj7TextDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_TextBlock_Text(this.obj7, obj.ToString(), null);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_Symbol(global::System.Char obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 15
                    if (!isobj8GlyphDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Controls_FontIcon_Glyph(this.obj8, obj.ToString(), null);
                    }
                }
            }
            private void Update_ListDetailsMenuItem_SymbolName(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED | DATA_CHANGED)) != 0)
                {
                    // Views\ListDetailsDetailControl.xaml line 15
                    if (!isobj8NameDisabled)
                    {
                        XamlBindingSetters.Set_Microsoft_UI_Xaml_Automation_AutomationProperties_Name(this.obj8, obj, null);
                    }
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private class ListDetailsDetailControl_obj1_BindingsTracking
            {
                private global::System.WeakReference<ListDetailsDetailControl_obj1_Bindings> weakRefToBindingObj; 

                public ListDetailsDetailControl_obj1_BindingsTracking(ListDetailsDetailControl_obj1_Bindings obj)
                {
                    weakRefToBindingObj = new global::System.WeakReference<ListDetailsDetailControl_obj1_Bindings>(obj);
                }

                public ListDetailsDetailControl_obj1_Bindings TryGetBindingObject()
                {
                    ListDetailsDetailControl_obj1_Bindings bindingObject = null;
                    if (weakRefToBindingObj != null)
                    {
                        weakRefToBindingObj.TryGetTarget(out bindingObject);
                        if (bindingObject == null)
                        {
                            weakRefToBindingObj = null;
                            ReleaseAllListeners();
                        }
                    }
                    return bindingObject;
                }

                public void ReleaseAllListeners()
                {
                    UpdateChildListeners_(null);
                }

                public void DependencyPropertyChanged_ListDetailsMenuItem(global::Microsoft.UI.Xaml.DependencyObject sender, global::Microsoft.UI.Xaml.DependencyProperty prop)
                {
                    ListDetailsDetailControl_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        global::NetworkManager.Views.ListDetailsDetailControl obj = sender as global::NetworkManager.Views.ListDetailsDetailControl;
                        if (obj != null)
                        {
                            bindings.Update_ListDetailsMenuItem(obj.ListDetailsMenuItem, DATA_CHANGED);
                        }
                    }
                }
                private long tokenDPC_ListDetailsMenuItem = 0;
                public void UpdateChildListeners_(global::NetworkManager.Views.ListDetailsDetailControl obj)
                {
                    ListDetailsDetailControl_obj1_Bindings bindings = TryGetBindingObject();
                    if (bindings != null)
                    {
                        if (bindings.dataRoot != null)
                        {
                            bindings.dataRoot.UnregisterPropertyChangedCallback(global::NetworkManager.Views.ListDetailsDetailControl.ListDetailsMenuItemProperty, tokenDPC_ListDetailsMenuItem);
                        }
                        if (obj != null)
                        {
                            bindings.dataRoot = obj;
                            tokenDPC_ListDetailsMenuItem = obj.RegisterPropertyChangedCallback(global::NetworkManager.Views.ListDetailsDetailControl.ListDetailsMenuItemProperty, DependencyPropertyChanged_ListDetailsMenuItem);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\ListDetailsDetailControl.xaml line 8
                {
                    this.ForegroundElement = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.ScrollViewer>(target);
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
            switch(connectionId)
            {
            case 1: // Views\ListDetailsDetailControl.xaml line 1
                {                    
                    global::Microsoft.UI.Xaml.Controls.UserControl element1 = (global::Microsoft.UI.Xaml.Controls.UserControl)target;
                    ListDetailsDetailControl_obj1_Bindings bindings = new ListDetailsDetailControl_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                    global::Microsoft.UI.Xaml.Markup.XamlBindingHelper.SetDataTemplateComponent(element1, bindings);
                }
                break;
            }
            return returnValue;
        }
    }
}

