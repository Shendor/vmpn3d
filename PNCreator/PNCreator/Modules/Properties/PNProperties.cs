using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml;
using System.IO;
using PNCreator.ManagerClasses;

namespace PNCreator.Modules.Properties
{
    class PNProperties
    {
        public static string DiscreteArcTexture { get; set; }
        public static string DiscreteIArcTexture { get; set; }
        public static string DiscreteTArcTexture { get; set; }
        public static string ContinuousArcTexture { get; set; }
        public static string ContinuousIArcTexture { get; set; }
        public static string ContinuousTArcTexture { get; set; }
        public static string ContinuousFArcTexture { get; set; }

        public static string DiscreteLocationsForm { get; set; }
        public static string ContinuousLocationsForm { get; set; }
        public static string DiscreteTransitionsForm { get; set; }
        public static string ContinuousTransitionsForm { get; set; }
        public static string MembranesForm { get; set; }

        public static Color SelectionColor { get; set; }
        public static Color DiscreteObjetcsColor { get; set; }
        public static Color ContinuousObjetcsColor { get; set; }
        public static Color MembranesColor { get; set; }
        public static string ArcsColor { get; set; }

        public static bool DiscreteObjectsNamesVisibility { get; set; }
        public static bool DiscreteObjectsValuesVisibility { get; set; }
        public static bool ContinuousObjectsNamesVisibility { get; set; }
        public static bool ContinuousObjectsValuesVisibility { get; set; }
        public static bool ArcsNamesVisibility { get; set; }
        public static bool ArcsValuesVisibility { get; set; }

        public static bool IsConfirmRemoving { get; set; }
        public static bool IsConfirmExit { get; set; }

        public static double OpacityLevel { get; set; }
        public static double ArcsThickness { get; set; }

        public static bool IsCanvasContentVisible
        {
            get
            {
                return (DiscreteObjectsNamesVisibility || 
                        DiscreteObjectsValuesVisibility ||
                        ContinuousObjectsNamesVisibility ||
                        ContinuousObjectsValuesVisibility ||
                        ArcsNamesVisibility ||
                        ArcsValuesVisibility);
            }
        }

        static PNProperties()
        {
            SelectionColor = Colors.White;
            DiscreteObjetcsColor = Colors.White;
        }

        public static PNProperties GetProperties()
        {
            return new PNProperties();
        }

        public static void SavePropeties()
        {
            XmlTextWriter writer = new XmlTextWriter(PNCreator.ManagerClasses.PNDocument.ApplicationPath + "/Properties.xml", System.Text.Encoding.Unicode);
            writer.WriteStartDocument();
            writer.WriteStartElement("Properties");

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dArcTexture");
            writer.WriteAttributeString("value", DiscreteArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dIArcTexture");
            writer.WriteAttributeString("value", DiscreteIArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dTArcTexture");
            writer.WriteAttributeString("value", DiscreteTArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cArcTexture");
            writer.WriteAttributeString("value", ContinuousArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cIArcTexture");
            writer.WriteAttributeString("value", ContinuousIArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cTArcTexture");
            writer.WriteAttributeString("value", ContinuousTArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cFArcTexture");
            writer.WriteAttributeString("value", ContinuousFArcTexture);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dLocForm");
            writer.WriteAttributeString("value", DiscreteLocationsForm);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dTransForm");
            writer.WriteAttributeString("value", DiscreteTransitionsForm);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cLocForm");
            writer.WriteAttributeString("value", ContinuousLocationsForm);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cTransForm");
            writer.WriteAttributeString("value", ContinuousTransitionsForm);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "mForm");
            writer.WriteAttributeString("value", MembranesForm);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dColor");
            writer.WriteAttributeString("R", DiscreteObjetcsColor.R.ToString());
            writer.WriteAttributeString("G", DiscreteObjetcsColor.G.ToString());
            writer.WriteAttributeString("B", DiscreteObjetcsColor.B.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cColor");
            writer.WriteAttributeString("R", ContinuousObjetcsColor.R.ToString());
            writer.WriteAttributeString("G", ContinuousObjetcsColor.G.ToString());
            writer.WriteAttributeString("B", ContinuousObjetcsColor.B.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "mColor");
            writer.WriteAttributeString("R", MembranesColor.R.ToString());
            writer.WriteAttributeString("G", MembranesColor.G.ToString());
            writer.WriteAttributeString("B", MembranesColor.B.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "arcColor");
            writer.WriteAttributeString("value", ArcsColor);
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dNameVisibility");
            writer.WriteAttributeString("value", DiscreteObjectsNamesVisibility.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "dValueVisibility");
            writer.WriteAttributeString("value", DiscreteObjectsValuesVisibility.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cNameVisibility");
            writer.WriteAttributeString("value", ContinuousObjectsNamesVisibility.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "cValueVisibility");
            writer.WriteAttributeString("value", ContinuousObjectsValuesVisibility.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "arcNameVisibility");
            writer.WriteAttributeString("value", ArcsNamesVisibility.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "arcValueVisibility");
            writer.WriteAttributeString("value", ArcsValuesVisibility.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "arcThickness");
            writer.WriteAttributeString("value", ArcsThickness.ToString(PNDocument.CURRENT_CULTURE));
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "opacityLevel");
            writer.WriteAttributeString("value", OpacityLevel.ToString(PNDocument.CURRENT_CULTURE));
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "isConfirmRemoving");
            writer.WriteAttributeString("value", IsConfirmRemoving.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Property");
            writer.WriteAttributeString("name", "isConfirmExit");
            writer.WriteAttributeString("value", IsConfirmExit.ToString());
            writer.WriteEndElement();


            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public static PNProperties LoadProperties()
        {
            XmlNode node = null;
            XmlDocument doc = new XmlDocument();
            doc.Load(PNCreator.ManagerClasses.PNDocument.ApplicationPath + "/Properties.xml");

            DiscreteArcTexture = doc.SelectSingleNode("/Properties/Property[@name='dArcTexture']").Attributes[1].InnerText;
            DiscreteIArcTexture = doc.SelectSingleNode("/Properties/Property[@name='dIArcTexture']").Attributes[1].InnerText;
            DiscreteTArcTexture = doc.SelectSingleNode("/Properties/Property[@name='dTArcTexture']").Attributes[1].InnerText;
            ContinuousArcTexture = doc.SelectSingleNode("/Properties/Property[@name='cArcTexture']").Attributes[1].InnerText;
            ContinuousIArcTexture = doc.SelectSingleNode("/Properties/Property[@name='cIArcTexture']").Attributes[1].InnerText;
            ContinuousTArcTexture = doc.SelectSingleNode("/Properties/Property[@name='cTArcTexture']").Attributes[1].InnerText;
            ContinuousFArcTexture = doc.SelectSingleNode("/Properties/Property[@name='cFArcTexture']").Attributes[1].InnerText;

            DiscreteLocationsForm =  doc.SelectSingleNode("/Properties/Property[@name='dLocForm']").Attributes[1].InnerText;
            DiscreteTransitionsForm = doc.SelectSingleNode("/Properties/Property[@name='dTransForm']").Attributes[1].InnerText;
            ContinuousLocationsForm = doc.SelectSingleNode("/Properties/Property[@name='cLocForm']").Attributes[1].InnerText;
            ContinuousTransitionsForm = doc.SelectSingleNode("/Properties/Property[@name='cTransForm']").Attributes[1].InnerText;
            MembranesForm = doc.SelectSingleNode("/Properties/Property[@name='mForm']").Attributes[1].InnerText;

            node = doc.SelectSingleNode("/Properties/Property[@name='dColor']");
            DiscreteObjetcsColor = Color.FromRgb(Convert.ToByte(node.Attributes[1].InnerText),
                                                 Convert.ToByte(node.Attributes[2].InnerText),
                                                 Convert.ToByte(node.Attributes[3].InnerText));

            node = doc.SelectSingleNode("/Properties/Property[@name='cColor']");
            ContinuousObjetcsColor = Color.FromRgb(Convert.ToByte(node.Attributes[1].InnerText),
                                                 Convert.ToByte(node.Attributes[2].InnerText),
                                                 Convert.ToByte(node.Attributes[3].InnerText));

            node = doc.SelectSingleNode("/Properties/Property[@name='mColor']");
            MembranesColor = Color.FromRgb(Convert.ToByte(node.Attributes[1].InnerText),
                                            Convert.ToByte(node.Attributes[2].InnerText),
                                            Convert.ToByte(node.Attributes[3].InnerText));

            

            DiscreteObjectsNamesVisibility = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='dNameVisibility']").Attributes[1].InnerText);
            DiscreteObjectsValuesVisibility = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='dValueVisibility']").Attributes[1].InnerText);
            ContinuousObjectsNamesVisibility = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='cNameVisibility']").Attributes[1].InnerText);
            ContinuousObjectsValuesVisibility = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='cValueVisibility']").Attributes[1].InnerText);
            ArcsNamesVisibility = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='arcNameVisibility']").Attributes[1].InnerText);
            ArcsValuesVisibility = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='arcValueVisibility']").Attributes[1].InnerText);

            IsConfirmRemoving = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='isConfirmRemoving']").Attributes[1].InnerText);
            IsConfirmExit = Convert.ToBoolean(doc.SelectSingleNode("/Properties/Property[@name='isConfirmExit']").Attributes[1].InnerText);

            ArcsThickness = Convert.ToDouble(doc.SelectSingleNode("/Properties/Property[@name='arcThickness']").Attributes[1].InnerText, PNDocument.CURRENT_CULTURE);
            OpacityLevel = Convert.ToDouble(doc.SelectSingleNode("/Properties/Property[@name='opacityLevel']").Attributes[1].InnerText, PNDocument.CURRENT_CULTURE);

            return GetProperties();
        }

        public static string GetTextureByArcType(PNCreator.PNObjectsIerarchy.PNObjectTypes type)
        {
            switch (type)
            {
                case PNObjectsIerarchy.PNObjectTypes.DiscreteArc: return DiscreteArcTexture;
                case PNObjectsIerarchy.PNObjectTypes.DiscreteInhibitorArc: return DiscreteIArcTexture;
                case PNObjectsIerarchy.PNObjectTypes.DiscreteTestArc: return DiscreteTArcTexture;
                case PNObjectsIerarchy.PNObjectTypes.ContinuousArc: return ContinuousArcTexture;
                case PNObjectsIerarchy.PNObjectTypes.ContinuousInhibitorArc: return ContinuousIArcTexture;
                case PNObjectsIerarchy.PNObjectTypes.ContinuousTestArc: return ContinuousTArcTexture;
                case PNObjectsIerarchy.PNObjectTypes.ContinuousFlowArc: return ContinuousFArcTexture;
                default: return "";
            }
        }
    }
}
