﻿#pragma checksum "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F7CED341476F5DA48482FE6095F4D385"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NumericBox;
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
    /// PositioniserSizePropertyPanel
    /// </summary>
    public partial class PositioniserSizePropertyPanel : System.Windows.Controls.Grid, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox lengthNB;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox widthNB;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox heightNB;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox xCellSizeNB;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox yCellSizeNB;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox zCellSizeNB;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox rowsNB;
        
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
            System.Uri resourceLocater = new System.Uri("/PNCreator;component/modules/positioniser/positionisersizepropertypanel.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Modules\Positioniser\PositioniserSizePropertyPanel.xaml"
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
            this.lengthNB = ((NumericBox.NumericBox)(target));
            return;
            case 2:
            this.widthNB = ((NumericBox.NumericBox)(target));
            return;
            case 3:
            this.heightNB = ((NumericBox.NumericBox)(target));
            return;
            case 4:
            this.xCellSizeNB = ((NumericBox.NumericBox)(target));
            return;
            case 5:
            this.yCellSizeNB = ((NumericBox.NumericBox)(target));
            return;
            case 6:
            this.zCellSizeNB = ((NumericBox.NumericBox)(target));
            return;
            case 7:
            this.rowsNB = ((NumericBox.NumericBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

