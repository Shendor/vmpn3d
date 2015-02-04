﻿#pragma checksum "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DF8BE3A8AA46F69BCE49F2F9DFD68684"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Expression.Controls;
using Microsoft.Expression.Media;
using Microsoft.Expression.Shapes;
using Microsoft.Windows.Shell;
using PNCreator.Controls.TextBox;
using PNCreator.Modules.Main;
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
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Charting;


namespace PNCreator.Modules.Simulation {
    
    
    /// <summary>
    /// SimulationChooserWindow
    /// </summary>
    public partial class SimulationChooserWindow : PNCreator.Modules.Main.BaseWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button animationBtn;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button noAnimationBtn;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.Popup sectorOptionPanel;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Controls.TextBox.TipTextBox simulationNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton simulationNameBtn;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button closeBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/PNCreator;component/modules/simulation/simulationchooserwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            
            #line 9 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.WindowMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 26 "..\..\..\..\Modules\Simulation\SimulationChooserWindow.xaml"
            ((System.Windows.Controls.WrapPanel)(target)).AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.ButtonsClick));
            
            #line default
            #line hidden
            return;
            case 3:
            this.animationBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.noAnimationBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.sectorOptionPanel = ((System.Windows.Controls.Primitives.Popup)(target));
            return;
            case 6:
            this.simulationNameTextBox = ((PNCreator.Controls.TextBox.TipTextBox)(target));
            return;
            case 7:
            this.simulationNameBtn = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 8:
            this.closeBtn = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

