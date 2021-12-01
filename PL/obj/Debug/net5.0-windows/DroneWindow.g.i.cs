// Updated by XamlIntelliSenseFileGenerator 01/12/2021 12:58:33
#pragma checksum "..\..\..\DroneWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2FA07FF21A2DFF7B6268D294669D5E000C1FFA57"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PL;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PL
{


    /// <summary>
    /// DroneWindow
    /// </summary>
    public partial class DroneWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector
    {


#line 26 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;

#line default
#line hidden


#line 33 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ID;

#line default
#line hidden


#line 55 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddDrone;

#line default
#line hidden


#line 64 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox StationList;

#line default
#line hidden


#line 75 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Battery;

#line default
#line hidden


#line 88 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Status;

#line default
#line hidden


#line 101 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Parcel;

#line default
#line hidden


#line 113 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Latitude;

#line default
#line hidden


#line 126 "..\..\..\DroneWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Longitude;

#line default
#line hidden

        private bool _contentLoaded;

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        public void InitializeComponent()
        {
            if (_contentLoaded)
            {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/dronewindow.xaml", System.UriKind.Relative);

#line 1 "..\..\..\DroneWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);

#line default
#line hidden
        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.11.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Name = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 2:
                    this.ID = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 3:
                    this.Weight = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 4:
                    this.AddDrone = ((System.Windows.Controls.Button)(target));

#line 62 "..\..\..\DroneWindow.xaml"
                    this.AddDrone.Click += new System.Windows.RoutedEventHandler(this.AddDrone_Click);

#line default
#line hidden
                    return;
                case 5:
                    this.StationList = ((System.Windows.Controls.ListBox)(target));

#line 67 "..\..\..\DroneWindow.xaml"
                    this.StationList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.StationList_MouseDoubleClick);

#line default
#line hidden
                    return;
                case 6:
                    this.Battery = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 7:
                    this.Status = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 8:
                    this.Parcel = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 9:
                    this.Latitude = ((System.Windows.Controls.TextBox)(target));
                    return;
                case 10:
                    this.Longitude = ((System.Windows.Controls.TextBox)(target));
                    return;
            }
            this._contentLoaded = true;
        }

        internal System.Windows.Controls.ComboBox WeightSelector;
    }
}

