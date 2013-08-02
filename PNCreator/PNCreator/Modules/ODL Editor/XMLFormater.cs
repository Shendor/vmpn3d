using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace PNCreator.Modules.ODLEditor
{
    /// <summary>
    /// Returns formatted xml string (indent and newlines) from unformatted XML
    /// string for display in eg textboxes.
    /// </summary>
    /// <param name="sUnformattedXml">Unformatted xml string.</param>
    /// <returns>Formatted xml string and any exceptions that occur.</returns>
    static class XMLFormater
    {
        public static string FormatXML(string xmlDocument)
        {
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xmlDocument);
            StringBuilder sb = new StringBuilder();
          
            XmlTextWriter xtw = null;
            
            try
            {
                StringWriter sw = new StringWriter(sb);
                xtw = new XmlTextWriter(sw);
                xtw.Formatting = Formatting.Indented;
                xd.WriteTo(xtw);
            }
            finally
            {
                //clean up even if error

                if (xtw != null)
                    xtw.Close();
            }

            //return the formatted xml

            return sb.ToString();
        }
    }
}
