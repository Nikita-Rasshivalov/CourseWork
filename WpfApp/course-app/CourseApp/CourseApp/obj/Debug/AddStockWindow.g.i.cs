﻿#pragma checksum "..\..\AddStockWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AC806C1B2DF89242D2C9AEB0D81842271AF885DE0CB5B617CDA31A0C0AE2FDA2"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseApp;
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


namespace CourseApp {
    
    
    /// <summary>
    /// AddStockWindow
    /// </summary>
    public partial class AddStockWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\AddStockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid StocksGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\AddStockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\AddStockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DescrBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\AddStockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MarkupBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\AddStockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox StokerComboBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\AddStockWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button UpdStockBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseApp;component/addstockwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AddStockWindow.xaml"
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
            this.StocksGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 10 "..\..\AddStockWindow.xaml"
            this.StocksGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.StocksGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.NameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.DescrBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.MarkupBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\AddStockWindow.xaml"
            this.MarkupBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.MarkupBox_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.StokerComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            
            #line 27 "..\..\AddStockWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.RemoveBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.UpdStockBtn = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\AddStockWindow.xaml"
            this.UpdStockBtn.Click += new System.Windows.RoutedEventHandler(this.UpdStockBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 29 "..\..\AddStockWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Back_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

