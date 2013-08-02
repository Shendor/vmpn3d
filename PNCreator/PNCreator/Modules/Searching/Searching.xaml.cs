using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PNCreator.PNObjectsIerarchy;
using PNCreator.ManagerClasses;

namespace PNCreator.Modules.Searching
{
    public partial class Searching
    {
        public Searching()
        {
            InitializeComponent();
             Owner = Application.Current.MainWindow;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;
            switch (btn.Name)
            {
                case "searchBtn": FindObjectAndZoom(); break;
                case "closeBtn": Close() ; break;
            }
        }


        /// <summary>
        /// Find object by name entered at the TextBox. If object was found, camera will move to it
        /// </summary>
        private void FindObjectAndZoom()
        {
            MainWindow mainWnd = (MainWindow)this.Owner;
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject.Name.Contains(searchedNameTB.Text))
                {
                    mainWnd.pnObjectPicker.SelectedObject = pnObject;
                    mainWnd.ShowSelectedObjectProperties(pnObject);
                    mainWnd.pnViewport.SetCameraPosition(pnObject.Position);
                    break;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) FindObjectAndZoom();
        }
      
    }
}
