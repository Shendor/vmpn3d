using System.Threading;
using System.Windows;

namespace PNCreator.Controls.Progress
{
    public partial class ProgressWindow
    {
        public ProgressWindow()
        {
            InitializeComponent();
        }

        public double Minimum
        {
            get { return progressBar.Minimum; }
            set { progressBar.Minimum = value; }
        }

        public double Maximum
        {
            get
            {
                return progressBar.Maximum;
            }
            set
            {
                progressBar.Maximum = value;
            }
        }

        public double Progress
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                progressBar.Value = value;
                if (progressBar.Value >= Maximum - 1)
                {
                    progressBar.Value = Maximum;
                    Close();
                }
            }
        }

        public Thread Thread
        {
            get; set;
        }

        private void MenuItemClick(object sender, RoutedEventArgs e)
        {
             //Thread.Abort();
             Close();
        }
    }
}
