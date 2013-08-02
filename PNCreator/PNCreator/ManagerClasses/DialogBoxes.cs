using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace PNCreator.ManagerClasses
{
    
    class DialogBoxes
    {
        //==================================================================================================
        /// <summary>
        /// Print content
        /// </summary>
        public static void PrintContent(Visual content)
        {
            PrintDialog dialog = new PrintDialog();
            bool? result = dialog.ShowDialog();
            if (result != null && result.Value)
            {
                System.Windows.Xps.XpsDocumentWriter writer = System.Printing.PrintQueue.CreateXpsDocumentWriter(dialog.PrintQueue);
                writer.Write(content);
            }
        }
        //==================================================================================================
        /// <summary>
        /// Show save dialog
        /// </summary>
        /// <param name="defaultPath">Default path directory (may be null)</param>
        /// <param name="docType">Type of document which's gonna be saved</param>
        public static string SaveDialog(string defaultPath, DocumentFormat docType)
        {
            var saveDlg = new Microsoft.Win32.SaveFileDialog();

            switch (docType)
            {
                case DocumentFormat.Net: saveDlg.Filter = "MPN files (*.mpn)|*.mpn|JPEG files (*.jpg)|*.jpg|XML files (*.xml)|*.xml"; break;
                case DocumentFormat.Image: saveDlg.Filter = "JPEG files (*.jpg)|*.jpg"; break;
                case DocumentFormat.Txt: saveDlg.Filter = "Text files (*.txt)|*.txt"; break;
                case DocumentFormat.MSWord: saveDlg.Filter = "MS Word 2003 files (*.doc)|*.doc"; break;
                case DocumentFormat.MSExcel: saveDlg.Filter = "MS Excel 2003 files (*.xls)|*.xls"; break;
                case DocumentFormat.MSExcelML: saveDlg.Filter = "MS ExcelML files (*.xml)|*.xml"; break;
                case DocumentFormat.MSExcelCsv: saveDlg.Filter = "MS Excel Csv files (*.csv)|*.csv"; break;
                case DocumentFormat.XML: saveDlg.Filter = "XML files (*.xml)|*.xml"; break;
                case DocumentFormat.Chart: saveDlg.Filter = "PNG Image files (*.png)|*.png|BMP Image files (*.bmp)|*.bmp|MS Excel files (*.xls)|*.xls|XPS Document files (*.xps)|*.xps"; break;
            }
            if(defaultPath != null) saveDlg.InitialDirectory = defaultPath;
            saveDlg.ShowDialog();
            if (!saveDlg.FileName.Equals("")) return saveDlg.FileName;
            else return null;
        }
        //==================================================================================================
        /// <summary>
        /// Show open dialog
        /// </summary>
        public static string OpenDialog(string defaultPath, DocumentFormat docType)
        {
            Microsoft.Win32.OpenFileDialog openDlg = new Microsoft.Win32.OpenFileDialog();
            switch (docType)
            {
                case DocumentFormat.Net: openDlg.Filter = "MPN files (*.mpn)|*.mpn"; break;
                case DocumentFormat.Image: openDlg.Filter = "JPEG files (*.jpg)|*.jpg"; break;
                case DocumentFormat.Mesh: openDlg.Filter = "3Ds files (*.3ds)|*.3ds"; break;
                case DocumentFormat.Texture: openDlg.Filter = "JPEG files (*.jpg)|*.jpg"; break;
                case DocumentFormat.XML: openDlg.Filter = "XML files (*.xml)|*.xml"; break;
            }
            openDlg.InitialDirectory = defaultPath;
            openDlg.ShowDialog();
            if (!openDlg.FileName.Equals("")) return openDlg.FileName;
            else return null;
        }

        public static string OpenDialog(DocumentFormat docType)
        {
            return OpenDialog(PNDocument.ApplicationPath, docType);
        }
    }
}
