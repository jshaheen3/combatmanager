﻿#pragma checksum "..\..\HPChangeDialog.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "97B3D4EB418F3DBE02C6922885791013"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace CombatManager {
    
    
    /// <summary>
    /// HPChangeDialog
    /// </summary>
    public partial class HPChangeDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox CharacterList;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DamageButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button HealButton;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock1;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SelectAllButton;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UnselectAllButton;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\HPChangeDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HPChangeBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CombatManager;component/hpchangedialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HPChangeDialog.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 24 "..\..\HPChangeDialog.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CharacterList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.DamageButton = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\HPChangeDialog.xaml"
            this.DamageButton.Click += new System.Windows.RoutedEventHandler(this.DamageButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.HealButton = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\HPChangeDialog.xaml"
            this.HealButton.Click += new System.Windows.RoutedEventHandler(this.HealButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.SelectAllButton = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\HPChangeDialog.xaml"
            this.SelectAllButton.Click += new System.Windows.RoutedEventHandler(this.SelectAllButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.UnselectAllButton = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\HPChangeDialog.xaml"
            this.UnselectAllButton.Click += new System.Windows.RoutedEventHandler(this.UnselectAllButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.HPChangeBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

