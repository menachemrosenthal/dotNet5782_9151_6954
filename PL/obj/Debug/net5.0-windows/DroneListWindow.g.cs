﻿#pragma checksum "..\..\..\DroneListWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D7B24FFD5983A54F87754BF7F14D455DEB611FD"
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


namespace PL {
    
    
    /// <summary>
    /// DroneListWindow
    /// </summary>
    public partial class DroneListWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\..\DroneListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\DroneListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid UpGrid;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\DroneListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StatusSelector;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\DroneListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox WeightSelector;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\..\DroneListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView DroneListView;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\DroneListWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button allDronesButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PL;component/dronelistwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\DroneListWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.UpGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.StatusSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 27 "..\..\..\DroneListWindow.xaml"
            this.StatusSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.StatusSelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.WeightSelector = ((System.Windows.Controls.ComboBox)(target));
            
            #line 47 "..\..\..\DroneListWindow.xaml"
            this.WeightSelector.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.WeightSelector_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 57 "..\..\..\DroneListWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 65 "..\..\..\DroneListWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 67 "..\..\..\DroneListWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 8:
            this.DroneListView = ((System.Windows.Controls.ListView)(target));
            
            #line 75 "..\..\..\DroneListWindow.xaml"
            this.DroneListView.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DroneListView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.allDronesButton = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\DroneListWindow.xaml"
            this.allDronesButton.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

