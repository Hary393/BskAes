﻿#pragma checksum "..\..\Register.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "417CA8CDE2701EE8AE339B58BEA1644E5C62B6C0"
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
using SzyfratorAES;


namespace SzyfratorAES {
    
    
    /// <summary>
    /// Register
    /// </summary>
    public partial class Register : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox LoginNameTxtBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox LoginPasswdTxtBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox LoginPasswdTxtBox2;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Exitbutton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Registerbutton;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\Register.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Infolabel;
        
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
            System.Uri resourceLocater = new System.Uri("/SzyfratorAES;component/register.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Register.xaml"
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
            
            #line 10 "..\..\Register.xaml"
            ((System.Windows.Input.CommandBinding)(target)).CanExecute += new System.Windows.Input.CanExecuteRoutedEventHandler(this.SendCommand_CanExecute);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Register.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.SendCommand_Executed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LoginNameTxtBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.LoginPasswdTxtBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.LoginPasswdTxtBox2 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.Exitbutton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\Register.xaml"
            this.Exitbutton.Click += new System.Windows.RoutedEventHandler(this.Exitbutton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Registerbutton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\Register.xaml"
            this.Registerbutton.Click += new System.Windows.RoutedEventHandler(this.Registerbutton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Infolabel = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

