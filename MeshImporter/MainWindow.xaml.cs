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
using Meshes3D;
using System.Windows.Media.Media3D;
using AnimatedGraph;
using System.IO;
using WindowsControl;

namespace MeshImporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MaterialGroup material;
        private string source;

        static MainWindow()
        {
            material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(new SolidColorBrush(Color.FromArgb(0xFF,0x32,0x32,0x32))));
            material.Children.Add(new SpecularMaterial(Brushes.White, 50));
        }

        public MainWindow()
        {
            InitializeComponent();
            InitializeCamera();
            treeViewer.InitializeTree();
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            switch (btn.Name)
            {
                case "openBtn": OpenMesh(OpenMeshFile()); break;
                case "addBtn": treePopup.IsOpen = true; break; 
                case "closeBtn": Close() ; break;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.OriginalSource as MenuItem;
            switch (mi.Header.ToString())
            {
                case "Add file": newFilePopup.IsOpen = true; break;
                case "Add folder": newFolderPopup.IsOpen = true; break;
                case "Close": treePopup.IsOpen = false; break;
            }
        }

        private void treeViewer_NodeChanged(object sender, RoutedPropertyChangedEventArgs<MeshNode> e)
        {
            if (treeViewer.SelectedNode.IsFile)
            {
                OpenMesh(treeViewer.SelectedNode.FullPath);
                treePopup.IsOpen = false;
            }
        }

        private void TextBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (newFilePopup.IsOpen) AddFile();
                else if (newFolderPopup.IsOpen) AddFolder();
                newFolderPopup.IsOpen = false;
                newFilePopup.IsOpen = false;
            }
        }

        private void treePopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            treePopup.IsOpen = false;
        }

        private void AddFolder()
        {
            if (folderNameTB.Text.Equals("")) return;

            Node<MeshNode> node = treeViewer.CenterObject as Node<MeshNode>;
            string destination = node.Item.Info.FullName + "\\" + folderNameTB.Text;

            try
            {
                Directory.CreateDirectory(destination);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                DialogWindow.Error(dnfe.Message);
            }
            catch (Exception ex)
            {
                DialogWindow.Error(ex.Message);
            }

            treeViewer.AddMesh(new MeshNode(destination));
        }

        private void AddFile()
        {
            if (fileNameTB.Text.Equals("") || source == null || source.Equals("")) return;

            Node<MeshNode> node = treeViewer.CenterObject as Node<MeshNode>;
            string destination = node.Item.Info.FullName + "\\" + fileNameTB.Text;

            try
            {
                File.Copy(source, destination, true);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                DialogWindow.Error(dnfe.Message);
            }
            catch (Exception ex)
            {
                DialogWindow.Error(ex.Message);
            }

            treeViewer.AddMesh(new MeshNode(new FileInfo(destination)));
        }

        private string OpenMeshFile()
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();

            openDlg.Filter = "3DS files (*.3ds)|*.3ds";

            openDlg.ShowDialog();
            if (!openDlg.FileName.Equals("")) return openDlg.FileName;
            else return null;
        }

        private void OpenMesh(string filename)
        {
            source = filename;
            if (filename != null)
            {
                long points = 0;
                int meshes = 0;

                Model3DGroup models = null;

                viewport.Children.Clear();
                AddLight();

                try
                {
                    models = MeshReader.GetModel3DGroup(filename, material);
                }
                catch (Exception ex)
                {
                    DialogWindow.Error(ex.Message);
                }

                if (models == null) return;

                foreach(Model3D m in models.Children)
                {
                    ModelVisual3D model = new ModelVisual3D();
                    model.Content = m;
                   
                    if(m.GetType().Equals(typeof(GeometryModel3D)))
                    {
                        points += ((MeshGeometry3D)((GeometryModel3D)m).Geometry).Positions.Count;
                    }
                    meshes++;
                    viewport.Children.Add(model);
                }
                totalPoints.Text = points.ToString();
                totalMeshes.Text = meshes.ToString();
            }
        }

        private void AddLight()
        {
            AmbientLight aLight = new AmbientLight(Colors.Gray);
            DirectionalLight dLight = new DirectionalLight(Colors.White, new Vector3D(-0.6, -0.5, -0.6));
            
            ModelVisual3D lights = new ModelVisual3D();

            Model3DGroup groupLights = new Model3DGroup();
            groupLights.Children.Add(aLight);
            groupLights.Children.Add(dLight);

            lights.Content = groupLights;

            viewport.Children.Add(lights);
        }

        private void InitializeCamera()
        {
            trackball.SetCameraView(_3DTools.CameraView.Perspective, new Vector3D(0,0,-650));
            trackball.ZoomRatio = 100;
            trackball.Move.OffsetX = -10.65;
            trackball.Move.OffsetY = -127.64;
            trackball.Move.OffsetZ = 114;

            trackball.Scale.ScaleX = trackball.Scale.ScaleY = trackball.Scale.ScaleZ = 0.4;

            trackball.AxisRotation.Axis = new Vector3D(0.987,-0.10105,-0.1175);
            trackball.AxisRotation.Angle = 64;
        }


        #region Commands
        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
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
