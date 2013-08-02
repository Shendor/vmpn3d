
using System.Collections.Generic;
using Meshes3D;

namespace PNCreator.Modules.Main.PNObjectProperties.Panels
{

    public partial class MultipleObjectPositionPanel
    {
        public MultipleObjectPositionPanel()
        {
            InitializeComponent();
        }

        public List<Mesh3D> Meshes
        {
            get { return positionPanel.Meshes; }
            set { positionPanel.Meshes = value; }
        }
    }
}
