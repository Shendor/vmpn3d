﻿#pragma checksum "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "12598B5D4A33CB07289473C202B9216A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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


namespace PNCreator.Modules.Positioniser {
    
    
    /// <summary>
    /// PositioniserPropertiesWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class PositioniserPropertiesWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 34 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border naBorder;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button searchBtn;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeBtn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PNCreator;component/modules/positioniser/positioniserpropertieswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.naBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.searchBtn = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
            this.searchBtn.Click += new System.Windows.RoutedEventHandler(this.OtherButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.closeBtn = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\..\Modules\Positioniser\PositioniserPropertiesWindow.xaml"
            this.closeBtn.Click += new System.Windows.RoutedEventHandler(this.OtherButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

