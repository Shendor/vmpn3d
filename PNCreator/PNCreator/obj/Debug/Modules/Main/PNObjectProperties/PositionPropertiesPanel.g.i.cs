﻿#pragma checksum "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "89B57BF3CB55BDD39EC4B4109CD79CCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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


namespace PNCreator.Modules.Main.PNObjectProperties {
    
    
    /// <summary>
    /// PositionPropertiesPanel
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class PositionPropertiesPanel : System.Windows.Controls.Grid, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox moveXNB;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox moveYNB;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NumericBox.NumericBox moveZNB;
        
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
            System.Uri resourceLocater = new System.Uri("/PNCreator;component/modules/main/pnobjectproperties/positionpropertiespanel.xaml" +
                    "", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
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
            this.moveXNB = ((NumericBox.NumericBox)(target));
            
            #line 19 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
            this.moveXNB.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.NumericBoxValueChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.moveYNB = ((NumericBox.NumericBox)(target));
            
            #line 20 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
            this.moveYNB.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.NumericBoxValueChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.moveZNB = ((NumericBox.NumericBox)(target));
            
            #line 21 "..\..\..\..\..\Modules\Main\PNObjectProperties\PositionPropertiesPanel.xaml"
            this.moveZNB.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.NumericBoxValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

