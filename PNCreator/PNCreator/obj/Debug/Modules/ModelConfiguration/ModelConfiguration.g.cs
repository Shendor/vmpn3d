﻿#pragma checksum "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7A2ACE1EAA8E4C3173C811BD44E3FCFF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.33440
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PNCreator.Modules.ModelConfiguration.Tables;
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


namespace PNCreator.Modules.ModelConfiguration {
    
    
    /// <summary>
    /// ModelConfiguration
    /// </summary>
    public partial class ModelConfiguration : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.PNObjectsPropertiesTable membranesTable;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.StructuralMembranesPropertiesTable structuralMembranesTable;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.DiscreteLocationsPropertiesTable discreteLocationsTable;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.ContinuousLocationsPropertiesTable continuousLocationsTable;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.DiscreteTransitionsPropertiesTable discreteTransitionsTable;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.ContinuousTransitionsPropertiesTable continuousTransitionsTable;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PNCreator.Modules.ModelConfiguration.Tables.ArcsPropertiesTable arcsTable;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border naBorder;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/PNCreator;component/modules/modelconfiguration/modelconfiguration.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
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
            
            #line 11 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
            ((System.Windows.Controls.Grid)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Grid_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.membranesTable = ((PNCreator.Modules.ModelConfiguration.Tables.PNObjectsPropertiesTable)(target));
            return;
            case 3:
            this.structuralMembranesTable = ((PNCreator.Modules.ModelConfiguration.Tables.StructuralMembranesPropertiesTable)(target));
            return;
            case 4:
            this.discreteLocationsTable = ((PNCreator.Modules.ModelConfiguration.Tables.DiscreteLocationsPropertiesTable)(target));
            return;
            case 5:
            this.continuousLocationsTable = ((PNCreator.Modules.ModelConfiguration.Tables.ContinuousLocationsPropertiesTable)(target));
            return;
            case 6:
            this.discreteTransitionsTable = ((PNCreator.Modules.ModelConfiguration.Tables.DiscreteTransitionsPropertiesTable)(target));
            return;
            case 7:
            this.continuousTransitionsTable = ((PNCreator.Modules.ModelConfiguration.Tables.ContinuousTransitionsPropertiesTable)(target));
            return;
            case 8:
            this.arcsTable = ((PNCreator.Modules.ModelConfiguration.Tables.ArcsPropertiesTable)(target));
            return;
            case 9:
            this.naBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.cancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\..\..\Modules\ModelConfiguration\ModelConfiguration.xaml"
            this.cancelBtn.Click += new System.Windows.RoutedEventHandler(this.AcceptButtonsClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

