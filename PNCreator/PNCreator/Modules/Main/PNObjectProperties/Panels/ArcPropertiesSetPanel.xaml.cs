using System.Collections.Generic;
using Meshes3D;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties.Panels
{
    public partial class ArcPropertiesSetPanel : IPNObjectProperties
    {
        public ArcPropertiesSetPanel()
        {
            InitializeComponent();
        }

        public void SetPNObject(PNObject pnObject)
        {
            DataContext = pnObject;
            positionPanel.Meshes = new List<Mesh3D> { pnObject };
        }
    }
}
