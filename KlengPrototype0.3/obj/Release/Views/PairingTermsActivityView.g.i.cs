﻿#pragma checksum "..\..\..\Views\PairingTermsActivityView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C621CACC7E7B1E0112C1C30EBBEBA4FC"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Kleng.Views;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace Kleng.Views {
    
    
    /// <summary>
    /// PairingTermsActivityView
    /// </summary>
    public partial class PairingTermsActivityView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image background_png;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock pairing_name;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListRight;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListLeft;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button left_pair;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button right_pair;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\Views\PairingTermsActivityView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Help_Button;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        
        #line 28 "..\..\..\Views\PairingTermsActivityView.xaml"
            
        private void WindowMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    
        #line default
        #line hidden
        
        
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
            System.Uri resourceLocater = new System.Uri("/KlengPrototype0.3;component/views/pairingtermsactivityview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\PairingTermsActivityView.xaml"
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
            
            #line 14 "..\..\..\Views\PairingTermsActivityView.xaml"
            ((Kleng.Views.PairingTermsActivityView)(target)).Deactivated += new System.EventHandler(this.Window_Deactivated);
            
            #line default
            #line hidden
            
            #line 16 "..\..\..\Views\PairingTermsActivityView.xaml"
            ((Kleng.Views.PairingTermsActivityView)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.WindowMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.background_png = ((System.Windows.Controls.Image)(target));
            return;
            case 3:
            this.pairing_name = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ListRight = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.ListLeft = ((System.Windows.Controls.ListBox)(target));
            return;
            case 6:
            this.left_pair = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\..\Views\PairingTermsActivityView.xaml"
            this.left_pair.Click += new System.Windows.RoutedEventHandler(this.pair_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.right_pair = ((System.Windows.Controls.Button)(target));
            
            #line 66 "..\..\..\Views\PairingTermsActivityView.xaml"
            this.right_pair.Click += new System.Windows.RoutedEventHandler(this.pair_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Help_Button = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\..\Views\PairingTermsActivityView.xaml"
            this.Help_Button.Click += new System.Windows.RoutedEventHandler(this.Help);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

