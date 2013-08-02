
using System.Collections.Generic;
using Meshes3D;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties.Panels
{
    public partial class ShapePropertiesSetPanel : IPNObjectProperties
    {
        public ShapePropertiesSetPanel()
        {
            InitializeComponent();
        }

        public virtual void SetPNObject(PNObject pnObject)
        {
            DataContext = pnObject;
            positionPanel.Meshes = new List<Mesh3D> {pnObject};
        }

    }
}
