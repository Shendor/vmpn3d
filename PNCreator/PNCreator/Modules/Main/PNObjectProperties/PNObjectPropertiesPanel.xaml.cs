using System.Collections.Generic;
using Meshes3D;
using PNCreator.Modules.Main.PNObjectProperties.Panels;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.PNObjectProperties
{
    public partial class PNObjectPropertiesPanel
    {
        private readonly LocationPropertiesSetPanel locationPanel;
        private readonly TransitionPropertiesSetPanel transitionPanel;
        private readonly MembranePropertiesSetPanel membranePanel;
        private readonly ArcPropertiesSetPanel arcPanel;
        private readonly MultipleObjectPositionPanel positionPanel;

        public PNObjectPropertiesPanel()
        {
            InitializeComponent();

            locationPanel = new LocationPropertiesSetPanel();
            transitionPanel = new TransitionPropertiesSetPanel();
            membranePanel = new MembranePropertiesSetPanel();
            arcPanel = new ArcPropertiesSetPanel();
            positionPanel = new MultipleObjectPositionPanel();
        }

        public void ShowPropertiesPanelForManyPNObjects(List<Mesh3D> pnObjects)
        {
            content.Children.Clear();
            positionPanel.Meshes = pnObjects;
            content.Children.Add(positionPanel);
        }

        public void ShowPropertiesPanelForPNObject(PNObject pnObject)
        {
            content.Children.Clear();
            if (pnObject is Location)
            {
                locationPanel.SetPNObject(pnObject);
                content.Children.Add(locationPanel);
            } 
            else if (pnObject is Transition)
            {
                transitionPanel.SetPNObject(pnObject);
                content.Children.Add(transitionPanel); 
            } 
            else if (pnObject is Membrane)
            {
                membranePanel.SetPNObject(pnObject);
                content.Children.Add(membranePanel); 
            }
            else if(pnObject is Arc3D)
            {
                arcPanel.SetPNObject(pnObject);
                content.Children.Add(arcPanel);
            }
        }
    }
}
