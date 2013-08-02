
using System.Windows;
using PNCreator.ManagerClasses;

namespace PNCreator.Commands
{
    public class SaveNetCommand
    {
        private readonly PNDocument pnDocument;

        public SaveNetCommand()
        {
            pnDocument = App.GetObject<PNDocument>();
        }

        public void SaveNet()
        {
            if (PNDocument.CurrentModelPath != null)
            {
                pnDocument.SaveNet(PNDocument.CurrentModelPath);
            }
            else
            {
                SaveNetAs();
            }
        }

        public void SaveNetAs()
        {
            string filename = DialogBoxes.SaveDialog(PNDocument.ApplicationPath, DocumentFormat.Net);
            if (filename != null)
            {
                if (filename[filename.Length - 1].Equals('g'))
                    ContentExporter.ExportVisualContent(filename, 
                        (int)Application.Current.MainWindow.Width,
                        (int)Application.Current.MainWindow.Height + 200, Application.Current.MainWindow);
                else
                    pnDocument.SaveNet(filename);
            } 
        }
    }
}
