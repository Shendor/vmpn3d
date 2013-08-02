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
using System.Windows.Controls.Primitives;
using AnimatedGraph;
using System.Windows.Threading;
using System.Windows.Media.Media3D;
using System.IO;
using MeshImporter.Properties;
using Meshes3D;

namespace MeshImporter
{
    /// <summary>
    /// Interaction logic for AddMeshWindow.xaml
    /// </summary>
    public partial class AddMeshWindow : Window
    {
        private MeshGeometry3D mesh;
        public AddMeshWindow(MeshGeometry3D mesh)
        {
            InitializeComponent();
            this.mesh = mesh;
            treeViewer.DefaultRelativePath = Settings.Default.DefaultMeshPath;
            treeViewer.InitializeTree();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.OriginalSource as MenuItem;
            switch (mi.Header.ToString())
            {
                case "Add mesh": newNodePopup.IsOpen = true; break;
                case "Close": Close() ; break;
            }
        }

        private void TextBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Node<MeshNode> node = treeViewer.CenterObject as Node<MeshNode>;
                treeViewer.AddMesh(new MeshNode(node.Item.Info.FullName + "\\" + meshNameTB.Text));
                newNodePopup.IsOpen = false;
            }
        }


        private void treeViewer_NodeChanged(object sender, RoutedPropertyChangedEventArgs<MeshNode> e)
        {
           // MessageBox.Show(treeViewer.SelectedNode.Name);
            if (treeViewer.SelectedNode.IsFile)
            {
                mesh = MeshReader.GetMesh(treeViewer.SelectedNode.FullPath);
                Close();
            }
        }
        
    }
}
