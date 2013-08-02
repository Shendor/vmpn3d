using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Documents;
using System.IO;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace PNCreator.ManagerClasses
{
    class ContentExporter
    {
        #region Image Exporter
        /// <summary>
        /// Export System.Windows.Media.Visual content to .jpg file
        /// </summary>
        /// <param name="filename">File name (Full path)</param>
        /// <param name="width">Image width</param>
        /// <param name="height">Image height</param>
        /// <param name="content">Visual content for exporting</param>
        public static void ExportVisualContent(string filename, int width, int height, Visual content)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Default);
            bmp.Render(content);

            WriteableBitmap bitmap = new WriteableBitmap(bmp);

            using (FileStream fileStream = new FileStream(filename, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                encoder.Save(fileStream);
            }
            bitmap = null;
            bmp = null;
        }
        #endregion

        public static void ExportChart(string filename, RadChart chart, string docExt)
        {
            switch (docExt)
            {
                case "png":
                    chart.Save(filename, 96d, 96d, new PngBitmapEncoder());
                    break;

                case "bmp":
                    chart.Save(filename, 96d, 96d, new BmpBitmapEncoder());
                    break;

                case "xls":
                    chart.ExportToExcelML(filename);
                    break;

                case "xps":
                    chart.ExportToXps(filename);
                    break;

                default:
                    chart.Save(filename, 96d, 96d, new PngBitmapEncoder());
                    break;
            }
        }

        public static void ExportTable(RadGridView gridView, bool isIncludeColumnHeaders, DocumentFormat docType)
        {
            var format = ExportFormat.Html;

            string path = DialogBoxes.SaveDialog(null, docType);
            if (docType == DocumentFormat.MSExcel)
            {
                format = ExportFormat.Html;
            }
            if (docType == DocumentFormat.MSExcelML)
            {
                format = ExportFormat.ExcelML;
            }
            if (docType == DocumentFormat.MSWord)
            {
                format = ExportFormat.Html;
            }
            if (docType == DocumentFormat.MSExcelCsv)
            {
                format = ExportFormat.Csv;
            }
            if (docType == DocumentFormat.Txt)
            {
                format = ExportFormat.Text;
            }
            
            if (path != null)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                if (format == ExportFormat.Text)
                {
                    using (TextWriter writer = new StreamWriter(File.Create(path)))
                    {
                        var table = (DataTable)gridView.ItemsSource; //FIXME: hard code 
                        foreach (DataRow row in table.Rows)
                        {
                            writer.Write(row["Value"] + ";");
                        }
                    }
                }
                else
                {
                    using (Stream stream = File.Create(path))
                    {
                        gridView.Export(stream,
                                        new GridViewExportOptions
                                            {
                                                Format = format,
                                                ShowColumnHeaders = isIncludeColumnHeaders,
                                                ShowColumnFooters = true,
                                                ShowGroupFooters = true,
                                            });
                    }
                }
            }
        }
    }
}
