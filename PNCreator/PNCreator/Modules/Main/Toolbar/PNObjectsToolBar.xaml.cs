using System.Windows;
using System.Windows.Controls;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.Modules.Main.Toolbar
{
    public partial class PNObjectsToolBar
    {
        private readonly PNObjectManager objectManager;

        public PNObjectsToolBar()
        {
            InitializeComponent();

            objectManager = App.GetObject<PNObjectManager>();

            locationBtn.Tag = PNObjectTypes.DiscreteLocation;
            cLocationBtn.Tag = PNObjectTypes.ContinuousLocation;
            transitionBtn.Tag = PNObjectTypes.DiscreteTransition;
            cTransitionBtn.Tag = PNObjectTypes.ContinuousTransition;
            arcBtn.Tag = PNObjectTypes.DiscreteArc;
            inghibitorBtn.Tag = PNObjectTypes.DiscreteInhibitorArc;
            testBtn.Tag = PNObjectTypes.DiscreteTestArc;
            membraneBtn.Tag = PNObjectTypes.Membrane;
            structureMembraneBtn.Tag = PNObjectTypes.StructuralMembrane;
            cArcBtn.Tag = PNObjectTypes.ContinuousArc;
            cInghibitorBtn.Tag = PNObjectTypes.ContinuousInhibitorArc;
            cTestBtn.Tag = PNObjectTypes.ContinuousTestArc;
            flowArcBtn.Tag = PNObjectTypes.ContinuousFlowArc;
            selectBtn.Tag = PNObjectTypes.None;
        }


        private void RadioButtonsClick(object sender, RoutedEventArgs e)
        {
            var btn = (RadioButton)e.OriginalSource;
            objectManager.TypeOfAddObject = (PNObjectTypes)btn.Tag;
        }
    }
}
