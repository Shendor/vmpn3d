using System.Windows.Input;

namespace PNCreator.Modules.About
{
    public partial class About
    {
        public About()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
