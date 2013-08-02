using System.Collections.Generic;
using System.Windows;
using Meshes3D;
using PNCreator.ManagerClasses.MeshTransfromation;

namespace PNCreator.Modules.Main.PNObjectProperties.TransformationProperties
{
    public partial class PositionPropertiesPanel
    {
        private readonly MeshTransformator meshTransformator;
        private List<Mesh3D> meshes;

        public PositionPropertiesPanel()
        {
            InitializeComponent();

            meshTransformator = new MeshTransformator();
        }

        public List<Mesh3D> Meshes
        {
            get { return meshes; }
            set
            {
                meshes = value;

                if (Meshes != null && Meshes.Count == 1)
                {
                    double x = Meshes[0].Position.X;
                    double y = Meshes[0].Position.Y;
                    double z = Meshes[0].Position.Z;

                    moveXNB.Value = x;
                    moveYNB.Value = y;
                    moveZNB.Value = z;
                }
            }
        }

        private void NumericBoxValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Meshes != null)
            {
                var nb = (NumericBox.NumericBox) e.OriginalSource;

                bool isManyObjects = Meshes.Count > 1;
                double valueX = moveXNB.Value;
                double valueY = moveYNB.Value;
                double valueZ = moveZNB.Value;

                if (isManyObjects)
                {
                    valueX = (nb.Equals(moveXNB)) ? moveXNB.Increment : 0;
                    valueY = (nb.Equals(moveYNB)) ? moveYNB.Increment : 0;
                    valueZ = (nb.Equals(moveZNB)) ? moveZNB.Increment : 0;
                }
                
                meshTransformator.Translate(Meshes, valueX, valueY, valueZ);
            }
        }
    }
}
