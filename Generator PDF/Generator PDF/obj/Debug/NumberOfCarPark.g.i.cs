﻿#pragma checksum "..\..\NumberOfCarPark.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "75843A0076EC3B307CD78CFD4C89605A7DC80FF9AF85D694BE152CA111C04218"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Generator_PDF;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
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


namespace Generator_PDF {
    
    
    /// <summary>
    /// NumberOfCarPark
    /// </summary>
    public partial class NumberOfCarPark : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 43 "..\..\NumberOfCarPark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox selectList;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\NumberOfCarPark.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox mainList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        
        #line 11 "..\..\NumberOfCarPark.xaml"

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
            System.Uri resourceLocater = new System.Uri("/Generator PDF;component/numberofcarpark.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\NumberOfCarPark.xaml"
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
            
            #line 10 "..\..\NumberOfCarPark.xaml"
            ((Generator_PDF.NumberOfCarPark)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.WindowMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.selectList = ((System.Windows.Controls.ListBox)(target));
            
            #line 46 "..\..\NumberOfCarPark.xaml"
            this.selectList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstBox_SelectionChanget);
            
            #line default
            #line hidden
            return;
            case 3:
            this.mainList = ((System.Windows.Controls.ListBox)(target));
            
            #line 63 "..\..\NumberOfCarPark.xaml"
            this.mainList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstBox_SelectionChangetRemove);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

