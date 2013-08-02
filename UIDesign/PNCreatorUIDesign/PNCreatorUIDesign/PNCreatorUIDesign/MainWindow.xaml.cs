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

namespace PNCreatorUIDesign
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Commands
        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void OnWindowMaximize(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized) this.WindowState = System.Windows.WindowState.Normal;
            else Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
        }

        private void OnWindowMinimize(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
        }
        #endregion

        private void Sliders_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void MenuItems_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
