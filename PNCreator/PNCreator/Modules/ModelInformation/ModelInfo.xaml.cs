using System.Windows;
using System.Windows.Input;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;

namespace PNCreator.Modules.ModelInformation
{
    public partial class ModelInfo
    {
        public ModelInfo()
        {
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {
            int dLocsCounter = 0;
            int cLocsCounter = 0;
            int dTransCounter = 0;
            int cTransCounter = 0;
            int dArcsCounter = 0;
            int dIArcsCounter = 0;
            int dTArcsCounter = 0;
            int cFArcsCounter = 0;
            int cArcsCounter = 0;
            int cIArcsCounter = 0;
            int cTArcsCounter = 0;
            int membranesCounter = 0;
            int sMembranesCounter = 0;
            long totalPointsCounter = 0;
            
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                switch (pnObject.Type)
                {
                    case PNObjectTypes.DiscreteLocation:
                        ++dLocsCounter;
                        break;
                    case PNObjectTypes.ContinuousLocation:
                        ++cLocsCounter;
                        break;
                    case PNObjectTypes.DiscreteTransition:
                        ++dTransCounter;
                        break;
                    case PNObjectTypes.ContinuousTransition:
                        ++cTransCounter;
                        break;
                    case PNObjectTypes.Membrane:
                        ++membranesCounter;
                        break;
                    case PNObjectTypes.StructuralMembrane:
                        ++sMembranesCounter;
                        break;
                    case PNObjectTypes.DiscreteArc:
                        ++dArcsCounter;
                        break;
                    case PNObjectTypes.DiscreteInhibitorArc:
                        ++dIArcsCounter;
                        break;
                    case PNObjectTypes.DiscreteTestArc:
                        ++dTArcsCounter;
                        break;
                    case PNObjectTypes.ContinuousFlowArc:
                        ++cFArcsCounter;
                        break;
                    case PNObjectTypes.ContinuousArc:
                        ++cArcsCounter;
                        break;
                    case PNObjectTypes.ContinuousInhibitorArc:
                        ++cIArcsCounter;
                        break;
                    case PNObjectTypes.ContinuousTestArc:
                        ++cTArcsCounter;
                        break;
                }
                totalPointsCounter += ((MeshGeometry3D)pnObject.Geometry.Geometry).Positions.Count;
            }

            dLocs.Text = dLocsCounter.ToString();
            cLocs.Text = cLocsCounter.ToString();
            dTrans.Text = dTransCounter.ToString();
            cTrans.Text = cTransCounter.ToString();
            dArcs.Text = dArcsCounter.ToString();
            dIArcs.Text = dIArcsCounter.ToString();
            dTArcs.Text = dTArcsCounter.ToString();
            cFArcs.Text = cFArcsCounter.ToString();
            cArcs.Text = cArcsCounter.ToString();
            cIArcs.Text = cIArcsCounter.ToString();
            cTArcs.Text = cTArcsCounter.ToString();
            membranes.Text = membranesCounter.ToString();
            sMembranes.Text = sMembranesCounter.ToString();
            totalPoints.Text = totalPointsCounter.ToString();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
