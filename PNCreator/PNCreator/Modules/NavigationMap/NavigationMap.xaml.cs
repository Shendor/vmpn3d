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
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using PNCreator.UIStyles.GlassEffect;
using PNCreator.PNObjectsIerarchy;
using PNCreator.ManagerClasses;
using _3DTools;

namespace PNCreator.Modules.NavigationMap
{
    /// <summary>
    /// Логика взаимодействия для NavigationMap.xaml
    /// </summary>
    public partial class NavigationMap : Window
    {

        public NavigationMap(PNObjectManager objManager, TrackballDecorator trackball, MainWindow mainWindow, Viewport3D viewport)
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, Brush brush)
        {
   
        }

        private void DrawCircle(DrawingVisual visual, Point topLeftCorner, Brush brush)
        {
            
        }


        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            map.ClearAllVisuals();
            map = null;
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
    }
}
