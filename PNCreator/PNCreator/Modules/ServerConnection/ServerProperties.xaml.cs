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
using System.Windows.Shapes;
using Tamir.SharpSsh;
using WindowsControl;
using System.Net.Sockets;

// user - AProfir
// password - profir
// host - hpc.usm.md
namespace PNCreator.Modules.ServerConnection
{
    /// <summary>
    /// Логика взаимодействия для ServerProperties.xaml
    /// </summary>
    public partial class ServerProperties : Window
    {
        public ServerProperties()
        {
            InitializeComponent();
//            userTB.Text = "AProfir";
//            passwordTB.Password = "profir";
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {

            Button btn = e.OriginalSource as Button;

            switch (btn.Name)
            {
                case "okBtn": CheckConnectionToCluster(); break;
                case "cancelBtn": Close(); break;
            }
            
        }

        private bool CheckConnectionToCluster()
        {
            try
            {
//                SshStream stream = new SshStream(hostTB.Text, userTB.Text, passwordTB.Password);
                return true;
            }
            catch (SocketException ex)
            {
                DialogWindow.Error(ex.Message);
            }
            catch (Exception ex)
            {
                DialogWindow.Error(ex.Message);
            }
            
            return false;
        }
    }
}
