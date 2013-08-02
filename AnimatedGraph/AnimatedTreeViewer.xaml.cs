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
using System.Windows.Threading;
using System.IO;

namespace AnimatedGraph
{
    /// <summary>
    /// Interaction logic for AnimatedTreeViewer.xaml
    /// </summary>
    public partial class AnimatedTreeViewer : Graph
    {
        public static string MESH_EXTENSION = "*.3ds*";
        public static string TEXTURE_EXTENSION = "*.jpg*";

        private MeshNode rootDirectory;
        private NodeCollection<MeshNode> nodes;
        public static readonly RoutedUICommand ChangeCenter;
        public static readonly RoutedUICommand OpenMesh;
        private MeshNode selectedNode;
        private string extension;
        private string defaultRelativePath;

        static AnimatedTreeViewer()
        {
            ChangeCenter = new RoutedUICommand("Change Center", "ChangeCenter", typeof(AnimatedTreeViewer));

            ExecutedRoutedEventHandler executeChangeCenter =
                delegate(object sender, ExecutedRoutedEventArgs e)
                {
                    ((AnimatedTreeViewer)sender).CenterObject = e.Parameter;

                };

            CanExecuteRoutedEventHandler canExecuteChangeCenter =
                delegate(object sender, CanExecuteRoutedEventArgs e)
                {
                    e.CanExecute = true;
                };

            CommandManager.RegisterClassCommandBinding(typeof(AnimatedTreeViewer),
                new CommandBinding(ChangeCenter, executeChangeCenter, canExecuteChangeCenter));


            OpenMesh = new RoutedUICommand("Open Mesh", "OpenMesh", typeof(AnimatedTreeViewer));

            ExecutedRoutedEventHandler executeOpenMesh =
                delegate(object sender, ExecutedRoutedEventArgs e)
                {
                    object node = e.Parameter;
                    ((AnimatedTreeViewer)sender).selectedNode = node as MeshNode;
                };

            CanExecuteRoutedEventHandler canExecuteOpenMesh =
                delegate(object sender, CanExecuteRoutedEventArgs e)
                {
                    e.CanExecute = true;
                };

            CommandManager.RegisterClassCommandBinding(typeof(AnimatedTreeViewer),
                new CommandBinding(OpenMesh, executeOpenMesh, canExecuteOpenMesh));
        }

        public AnimatedTreeViewer()
        {
            InitializeComponent();
            extension = MESH_EXTENSION;
           // InitializeTree();
            DefaultRelativePath = "\\Content";
            rootDirectory = new MeshNode(DefaultRelativePath);
            NodeTemplateSelector = new NodeTemplateSelector();
        }

        public static readonly DependencyProperty SelectedNodeProperty =
        DependencyProperty.Register("SelectedNode", typeof(MeshNode), typeof(AnimatedTreeViewer), new PropertyMetadata(new MeshNode(), OnSelectedNodeChanged));

        private static void OnSelectedNodeChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            AnimatedTreeViewer tree = (AnimatedTreeViewer)sender;
            tree.selectedNode = (MeshNode)args.NewValue;
            tree.OnNodeChanged((MeshNode)args.OldValue, (MeshNode)args.NewValue);
        }

        public string DefaultFileExtension
        {
            get { return extension; }
            set { extension = value; }
        }

        public string DefaultRelativePath
        {
            get { return defaultRelativePath; }
            set { defaultRelativePath = Environment.CurrentDirectory + value; }
        }

        public MeshNode SelectedNode
        {
            get { return (MeshNode)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }
        //===========================================================
        /// <summary>
        /// Set or get value
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(SelectedNodeProperty); }
            set { SetValue(SelectedNodeProperty, value); }
        }

        #region ValueEvent
        public static readonly RoutedEvent NodeChangedEvent =
            EventManager.RegisterRoutedEvent("NodeChanged", RoutingStrategy.Direct, typeof(RoutedPropertyChangedEventHandler<MeshNode>), typeof(AnimatedTreeViewer));

        public event RoutedPropertyChangedEventHandler<MeshNode> NodeChanged
        {
            add { AddHandler(NodeChangedEvent, value); }
            remove { RemoveHandler(NodeChangedEvent, value); }
        }

        private void OnNodeChanged(MeshNode oldValue, MeshNode newValue)
        {
            RoutedPropertyChangedEventArgs<MeshNode> args = new RoutedPropertyChangedEventArgs<MeshNode>(oldValue, newValue);
            args.RoutedEvent = AnimatedTreeViewer.NodeChangedEvent;
            args.Source = this;
            RaiseEvent(args);
        }
        #endregion

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = e.OriginalSource as MenuItem;
            switch (mi.Header.ToString())
            {
                case "Open mesh": ; break;
                case "Rename mesh": ; break;
                case "Remove mesh": RemoveMesh(SelectedNode); break;
            }
        }

        public void AddMesh(MeshNode path)
        {
            Node<MeshNode> node = this.CenterObject as Node<MeshNode>;
            if (node != null)
            {
                nodes.AddNode(node.Item, path);
            }
        }

        public void RenameMesh(string oldName, string newName)
        {
            throw new NotImplementedException();
        }


        public void RemoveMesh(MeshNode name)
        {
            Node<MeshNode> node = this.CenterObject as Node<MeshNode>;
            if (node != null)
            {
                nodes.RemoveNode(node.Item,name);
            }
        }

        private void TreeButtons_Click(object sender, RoutedEventArgs e)
        {
            GetNodeByControlTag(sender);
        }

        private void TreeButtons_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetNodeByControlTag(sender);
        }

        private void GetNodeByControlTag(Object control)
        {
            if (control.GetType().Equals(typeof(Button)))
            {
                Button btn = control as Button;
                if (btn.Tag.GetType().Equals(typeof(MeshNode)))
                {
                    SelectedNode = btn.Tag as MeshNode;
                }
            }
        }

        public NodeCollection<MeshNode> Items
        {
            get { return nodes; }
            set { nodes = value; }
        }


        public void InitializeTree()
        {
            nodes = new NodeCollection<MeshNode>(new MeshNode[] { rootDirectory });
            GetFiles(rootDirectory,extension);

            this.CenterObject = nodes[rootDirectory];
        }

        private void GetFiles(MeshNode root, string extension)
        {
            FileInfo[] files = null;
            DirectoryInfo[] subDirs = null;

            try
            {
                files = ((DirectoryInfo)root.Info).GetFiles(extension);
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            this.CenterObject = nodes[root];
            Node<MeshNode> node = this.CenterObject as Node<MeshNode>;

            foreach (FileInfo file in files)
            {
                MeshNode meshDirectory = new MeshNode(file);
                nodes.AddNode(node.Item, meshDirectory);
            }

            subDirs = ((DirectoryInfo)root.Info).GetDirectories();

            foreach (DirectoryInfo dirInfo in subDirs)
            {
                MeshNode meshDirectory = new MeshNode(dirInfo);
                nodes.AddNode(node.Item, meshDirectory);
                GetFiles(meshDirectory, extension);
            }
        }
    }

    public class MeshNode : IEquatable<MeshNode>
    {
        private FileSystemInfo info;

        public MeshNode(FileSystemInfo info) 
        {
            this.info = info;    
        }

        public MeshNode()
        {
              info = new DirectoryInfo(Environment.CurrentDirectory);     
        }

        public MeshNode(string path)
        {
            info = new DirectoryInfo(path);
            if (!info.Exists)
                info = new DirectoryInfo(Environment.CurrentDirectory);
        }

        public FileSystemInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public string FullPath
        {
            get { return info.FullName; }
        }

        public string Name
        {
            get { return info.Name; }
        }

        public bool IsFile
        {
            get {return info.GetType().Equals(typeof(FileInfo)); }           
        }

        public bool Equals(MeshNode other)
        {
            return this.FullPath.Equals(other.FullPath);
        }

        public override string ToString()
        {
            return Name;
        }
    }


    internal class NodeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            Node<MeshNode> node = item as Node<MeshNode>;
            if (node != null)
            {
                if (node.Item.IsFile)
                {
                    if (fileTemplate == null)
                    {
                        fileTemplate = (DataTemplate)((FrameworkElement)container).FindResource("fileTemplate");
                    }
                    return fileTemplate;
                }
                else
                {
                    if (folderTemplate == null)
                    {
                        folderTemplate = (DataTemplate)((FrameworkElement)container).FindResource("folderTemplate");
                    }
                    return folderTemplate;
                }
            }
            return null;
        }

        private DataTemplate folderTemplate, fileTemplate;

    }
}
