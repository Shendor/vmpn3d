﻿#pragma checksum "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2E1B06286981267D1CFBAEF0DA4C2932"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.1
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Expression.Controls;
using Microsoft.Expression.Media;
using Microsoft.Expression.Shapes;
using Microsoft.Windows.Shell;
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
using Telerik.Windows.Controls.Carousel;
using Telerik.Windows.Controls.Charting;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;


namespace PNCreator.Modules.Analizing {
    
    
    /// <summary>
    /// Diagram
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Diagram : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Input.CommandBinding closeCommand;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Input.CommandBinding maximizeCommand;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Input.CommandBinding minimizeCommand;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition briefHistoryTableRow;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button exportBtn;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button printBtn;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadGridView briefHistoryTable;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox tableNames;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Telerik.Windows.Controls.RadGridView historyTable;
        
        #line default
        #line hidden
        
        
        #line 108 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid chartPanel;
        
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
            System.Uri resourceLocater = new System.Uri("/PNCreator;component/modules/analizing/diagram/diagram.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
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
            
            #line 8 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            ((PNCreator.Modules.Analizing.Diagram)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            ((PNCreator.Modules.Analizing.Diagram)(target)).Unloaded += new System.Windows.RoutedEventHandler(this.Window_Unloaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.closeCommand = ((System.Windows.Input.CommandBinding)(target));
            
            #line 10 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.closeCommand.Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.OnWindowClose);
            
            #line default
            #line hidden
            return;
            case 3:
            this.maximizeCommand = ((System.Windows.Input.CommandBinding)(target));
            
            #line 11 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.maximizeCommand.Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.OnWindowMaximize);
            
            #line default
            #line hidden
            return;
            case 4:
            this.minimizeCommand = ((System.Windows.Input.CommandBinding)(target));
            
            #line 12 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.minimizeCommand.Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.OnWindowMinimize);
            
            #line default
            #line hidden
            return;
            case 5:
            this.briefHistoryTableRow = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 6:
            this.exportBtn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.exportBtn.Click += new System.Windows.RoutedEventHandler(this.Buttons_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.printBtn = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.printBtn.Click += new System.Windows.RoutedEventHandler(this.Buttons_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.briefHistoryTable = ((Telerik.Windows.Controls.RadGridView)(target));
            
            #line 57 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.briefHistoryTable.SelectionChanged += new System.EventHandler<Telerik.Windows.Controls.SelectionChangeEventArgs>(this.briefHistoryTable_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 60 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.tableNames = ((System.Windows.Controls.ComboBox)(target));
            
            #line 80 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            this.tableNames.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.tableNames_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.historyTable = ((Telerik.Windows.Controls.RadGridView)(target));
            return;
            case 12:
            
            #line 85 "..\..\..\..\..\Modules\Analizing\Diagram\Diagram.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.chartPanel = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
