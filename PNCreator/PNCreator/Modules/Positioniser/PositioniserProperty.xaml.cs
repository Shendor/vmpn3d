using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PNCreator.Controls.Positioniser;

namespace PNCreator.Modules.Positioniser
{
    /// <summary>
    /// Interaction logic for PositioniserProperty.xaml
    /// </summary>
    public partial class PositioniserProperty 
    {
        private static readonly RoutedEvent ParametersChangedEvent =
            EventManager.RegisterRoutedEvent("ParametersChanged", RoutingStrategy.Direct, 
                                            typeof(RoutedPropertyChangedEventHandler<Grid3DParameters>), typeof(PositioniserProperty));
        
        public PositioniserProperty()
        {
            InitializeComponent();

            Parameters = new Grid3DParameters();
            sizePropertyPanel.SetParameters(Parameters);
            translationPropertyPanel.SetParameters(Parameters);
            visualPropertyPanel.SetParameters(Parameters);
            Parameters.ParametersChangedMethod = OnParametersChanged;
        }

        public Grid3DParameters Parameters
        {
            get;
            set;
        }

        public event RoutedPropertyChangedEventHandler<Grid3DParameters> ParametersChanged
        {
            add
            {
                AddHandler(ParametersChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ParametersChangedEvent, value);
            }
        }


        protected void OnParametersChanged(Grid3DParameters newValue)
        {
            RoutedPropertyChangedEventArgs<Grid3DParameters> args =
                new RoutedPropertyChangedEventArgs<Grid3DParameters>(newValue, newValue);
            args.RoutedEvent = PositioniserProperty.ParametersChangedEvent;
            RaiseEvent(args);
        }

    }
}
