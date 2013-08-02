using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.IO;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Controls.CarcassControl;
using PNCreator.Controls.VectorControl;
using System.Globalization;
using PNCreator.Modules.Properties;
using PNCreator.Controls.SectorControl;
using PNCreator.Controls;


namespace PNCreator.ManagerClasses
{
    /// <summary>
    /// Contains methods to save and to open net
    /// </summary>
    public class PNDocument : IDisposable
    {
        private static byte KEY = 12;
        private static string TEMP_MODEL_FILE = "tmpModel.mpn";
        public static readonly CultureInfo CURRENT_CULTURE = new CultureInfo("ru-RU");

        private EventPublisher eventPublisher;
        private static string applicationPath =
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        private string[] recentOpenedModels;
        private byte pathsCounter;

        public PNDocument()
        {
            eventPublisher = App.GetObject<EventPublisher>();
            recentOpenedModels = new string[5];
            pathsCounter = 0;
        }

        #region Object properties

        /// <summary>
        /// Load object properties
        /// </summary>
        public string[] LoadObjectProperties()
        {
            string[] lines = new string[11];
            using (StreamReader fileReader = new StreamReader(applicationPath + "/Properties.ini", Encoding.UTF8))
            {
                pathsCounter = 0;
                while (!fileReader.EndOfStream)
                {
                    lines[pathsCounter] = fileReader.ReadLine();
                    int startIndex = 0;
                    while (startIndex < lines[pathsCounter].Length && !lines[pathsCounter][startIndex].Equals('='))
                        ++startIndex;
                    lines[pathsCounter] = lines[pathsCounter].Substring(startIndex, lines[pathsCounter].Length);
                    ++pathsCounter;
                }
                fileReader.Close();
            }
            return lines;
        }
        #endregion

        #region Recent opened models
        //==========================================================================================
        /// <summary>
        /// Load list of recent opened models
        /// </summary>
        public void LoadListOfRecentOpenedModels()
        {
            using (StreamReader fileReader = new StreamReader(applicationPath + "/RecenOpenedModels.txt", Encoding.UTF8))
            {
                int i = 0;
                pathsCounter = 0;
                while (!fileReader.EndOfStream)
                {
                    if (i.Equals(0))
                    {
                        try
                        {
                            pathsCounter = Convert.ToByte(fileReader.ReadLine());
                        }
                        catch (FormatException)
                        {
                            pathsCounter = 0;
                        }
                        catch (OverflowException)
                        {
                            pathsCounter = 0;
                        }
                    }
                    recentOpenedModels[i] = fileReader.ReadLine();
                    ++i;
                }
            }
        }

        /// <summary>
        /// Add path of recent opened models
        /// </summary>
        public void AddRecentOpenedModel(string path)
        {
            if (pathsCounter == 5)
                pathsCounter = 0;
            recentOpenedModels[pathsCounter++] = path;

            using (StreamWriter fileWriter = new StreamWriter(applicationPath + "/RecenOpenedModels.txt", false, Encoding.UTF8))
            {
                fileWriter.WriteLine(pathsCounter.ToString());
                for (int i = 0; i < recentOpenedModels.Length; i++)
                    fileWriter.WriteLine(recentOpenedModels[i]);
            }
        }
        #endregion

        #region Save and open model
        /// <summary>
        /// Save current net in file
        /// </summary>
        /// <param name="fileName">Name of output file</param>
        public void SaveNet(string fileName)
        {
            XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.Unicode);
            writer.WriteStartDocument();
            writer.WriteStartElement("Net");

            #region Save models

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                writer.WriteStartElement("obj");

                writer.WriteAttributeString("ID", pnObject.ID.ToString());
                writer.WriteAttributeString("Name", pnObject.Name);
                writer.WriteAttributeString("Group", pnObject.Group.ToString());


                #region Write Arc

                if (pnObject is Arc3D)
                {
                    Arc3D arc = (Arc3D)pnObject;

                    if (pnObject.Type == PNObjectTypes.DiscreteArc)
                        writer.WriteAttributeString("Type", "dA");
                    if (pnObject.Type == PNObjectTypes.DiscreteInhibitorArc)
                        writer.WriteAttributeString("Type", "dIA");
                    if (pnObject.Type == PNObjectTypes.DiscreteTestArc)
                        writer.WriteAttributeString("Type", "dTA");
                    if (pnObject.Type == PNObjectTypes.ContinuousArc)
                        writer.WriteAttributeString("Type", "cA");
                    if (pnObject.Type == PNObjectTypes.ContinuousInhibitorArc)
                        writer.WriteAttributeString("Type", "cIA");
                    if (pnObject.Type == PNObjectTypes.ContinuousTestArc)
                        writer.WriteAttributeString("Type", "cTA");
                    if (pnObject.Type == PNObjectTypes.ContinuousFlowArc)
                        writer.WriteAttributeString("Type", "fA");

                    writer.WriteAttributeString("Weight", arc.Weight.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Texture", arc.TextureName);

                    writer.WriteStartElement("SP");
                    writer.WriteAttributeString("X", arc.StartPoint.X.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Y", arc.StartPoint.Y.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Z", arc.StartPoint.Z.ToString(CURRENT_CULTURE));
                    writer.WriteEndElement();

                    writer.WriteStartElement("MP");
                    writer.WriteAttributeString("X", arc.MiddlePoint.X.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Y", arc.MiddlePoint.Y.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Z", arc.MiddlePoint.Z.ToString(CURRENT_CULTURE));
                    writer.WriteEndElement();

                    writer.WriteStartElement("EP");
                    writer.WriteAttributeString("X", arc.EndPoint.X.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Y", arc.EndPoint.Y.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Z", arc.EndPoint.Z.ToString(CURRENT_CULTURE));
                    writer.WriteEndElement();

                    writer.WriteStartElement("Con");
                    writer.WriteAttributeString("SID", arc.StartID.ToString());
                    writer.WriteAttributeString("EID", arc.EndID.ToString());
                    writer.WriteEndElement();

                }
                #endregion

                #region Write Location
                else
                {
                    if (pnObject.Type == PNObjectTypes.DiscreteLocation || pnObject.Type == PNObjectTypes.ContinuousLocation)
                    {
                        Location location = (Location)pnObject;
     
                        if (pnObject.Type == PNObjectTypes.DiscreteLocation)
                        {
                            writer.WriteAttributeString("Type", "DL");
                            writer.WriteAttributeString("Tokens", ((DiscreteLocation)location).Tokens.ToString());
                        }
                        if (pnObject.Type == PNObjectTypes.ContinuousLocation)
                        {
                            writer.WriteAttributeString("Type", "CL");
                            writer.WriteAttributeString("Level", ((ContinuousLocation)location).Level.ToString(CURRENT_CULTURE));
                        }

                        writer.WriteAttributeString("MinCapacity", location.MinCapacity.ToString(CURRENT_CULTURE));
                        writer.WriteAttributeString("MaxCapacity", location.MaxCapacity.ToString(CURRENT_CULTURE));

                        writer.WriteStartElement("InT");
                        for (int j = 0; j < location.IncomeTransitionsID.Count; j++)
                        {
                            writer.WriteAttributeString("id" + j.ToString(), location.IncomeTransitionsID[j].ToString());
                        }
                        writer.WriteEndElement();
                    }
                #endregion

                    #region Write Transitions
                    else if (pnObject.Type == PNObjectTypes.DiscreteTransition || pnObject.Type == PNObjectTypes.ContinuousTransition)
                    {
                        Transition transition = (Transition)pnObject;

                        if (pnObject.Type == PNObjectTypes.DiscreteTransition)
                        {
                            writer.WriteAttributeString("Type", "DT");
                            writer.WriteAttributeString("Delay", ((DiscreteTransition)transition).Delay.ToString(CURRENT_CULTURE));
                        }
                        if (pnObject.Type == PNObjectTypes.ContinuousTransition)
                        {
                            writer.WriteAttributeString("Type", "CT");
                            writer.WriteAttributeString("Expectance", ((ContinuousTransition)transition).Expectance.ToString(CURRENT_CULTURE));
                        }
                        writer.WriteAttributeString("Guard", ((Transition)pnObject).Guard.ToString());
                        writer.WriteAttributeString("OutL", transition.OutLocationAmount.ToString());

                        writer.WriteStartElement("InL");
                        for (int j = 0; j < transition.IncomeLocationsID.Count; j++)
                        {
                            writer.WriteAttributeString("id" + j.ToString(), transition.IncomeLocationsID[j].ToString());
                        }
                        writer.WriteEndElement();
                        writer.WriteStartElement("InSAL");
                        for (int j = 0; j < transition.SALocations.Count; j++)
                        {
                            writer.WriteAttributeString("id" + j.ToString(), transition.SALocations[j].ToString());
                        }
                        writer.WriteEndElement();
                    }
                    #endregion

                    #region Write Membrane
                    else if (pnObject.Type == PNObjectTypes.Membrane)
                    {
                        writer.WriteAttributeString("Type", "M");
                    }
                    #endregion

                    #region Write Structural Membrane
                    else if (pnObject.Type == PNObjectTypes.StructuralMembrane)
                    {
                        writer.WriteAttributeString("Type", "SM");
                        writer.WriteAttributeString("Speed", ((StructuralMembrane)pnObject).Size.ToString(CURRENT_CULTURE));


                    }
                    #endregion

                    writer.WriteStartElement("Mesh");
                    writer.WriteAttributeString("MName", ((Shape3D)pnObject).MeshName);
                    writer.WriteEndElement();

                    writer.WriteStartElement("Color");
                    writer.WriteAttributeString("R", pnObject.MaterialColor.R.ToString());
                    writer.WriteAttributeString("G", pnObject.MaterialColor.G.ToString());
                    writer.WriteAttributeString("B", pnObject.MaterialColor.B.ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Pos");
                    writer.WriteAttributeString("X", pnObject.Position.X.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Y", pnObject.Position.Y.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Z", pnObject.Position.Z.ToString(CURRENT_CULTURE));
                    writer.WriteEndElement();

                    writer.WriteStartElement("Rot");
                    writer.WriteAttributeString("X", pnObject.RotateX.Angle.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Y", pnObject.RotateY.Angle.ToString(CURRENT_CULTURE));
                    writer.WriteAttributeString("Z", pnObject.RotateZ.Angle.ToString(CURRENT_CULTURE));
                    writer.WriteEndElement();

                    writer.WriteStartElement("Scale");
                    writer.WriteAttributeString("Value", pnObject.Size.ToString(CURRENT_CULTURE));
                    writer.WriteEndElement();
                }

                if (pnObject is IFormula)
                {
                    writer.WriteStartElement("F");
                    writer.WriteAttributeString("Str", ((IFormula)pnObject).Formula);
                    writer.WriteEndElement();
                }
                if (pnObject is IExtendedFormula)
                {
                    writer.WriteStartElement("BF");
                    writer.WriteAttributeString("Str", ((IExtendedFormula)pnObject).TransitionGuardFormula);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }

            #endregion

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            CurrentModelPath = fileName;
        }


        /// <summary>
        /// Open net from file
        /// </summary>
        /// <param name="fileName">Name of opened file</param>
        public PNObjectDictionary<long, PNObject> OpenNet(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);

            XmlNodeList objectNodes = doc.GetElementsByTagName("obj");
            foreach (XmlNode objectNode in objectNodes)
            {

                PNObject pnObject = GetPNObjectFromNode(objectNode);
                PNObjectRepository.PNObjects.Add(pnObject.ID, pnObject);
//                viewport.AddPNObject(pnObject);
            }

            //PNObjectNameUtil.TotalShapes = objManager.Shapes.Count;
            //PNObjectNameUtil.TotalArcs = objManager.Arcs.Count;

            IdGenerator.UpdateID(PNObjectRepository.PNObjects);
            var formulaManager = App.GetObject<FormulaManager.FormulaManager>();
            formulaManager.IsNeedToCompile = true;
            CurrentModelPath = fileName;

            return PNObjectRepository.PNObjects;
        }



        private PNObject GetPNObjectFromNode(XmlNode objectNode)
        {
            IdGenerator.ID = -1;
            PNObject pnObject = null;
            double value = 0;
            string objectType = objectNode.Attributes["Type"].InnerText;
            string doubleFormula = (objectNode["F"] != null) ? objectNode["F"].Attributes["Str"].InnerText : "";
            
            if (objectType.Equals("dA") || objectType.Equals("dIA") || objectType.Equals("dTA") ||
                          objectType.Equals("cA") || objectType.Equals("cIA") || objectType.Equals("cTA") || objectType.Equals("fA"))
            {
                pnObject = new Arc3D();
                value = Convert.ToDouble(objectNode.Attributes["Weight"].InnerText, CURRENT_CULTURE);
                ((Arc3D)pnObject).Weight = value;
                ReadArcProperties((Arc3D)pnObject, objectNode);
            }
            else
            {
                #region Read Locations
                if (objectType.Equals("DL") || objectType.Equals("CL"))
                {
                    if (objectType.Equals("DL"))
                    {
                        pnObject = new DiscreteLocation();
                        value = Convert.ToInt32(objectNode.Attributes["Tokens"].InnerText);
                        ((DiscreteLocation)pnObject).Tokens = (int)value;

                    }
                    else if (objectType.Equals("CL"))
                    {
                        pnObject = new ContinuousLocation();
                        value = Convert.ToDouble(objectNode.Attributes["Level"].InnerText, CURRENT_CULTURE);
                        ((ContinuousLocation)pnObject).Level = value;
                    }
                    Location location = (Location)pnObject;

                    if (objectNode.Attributes["MinCapacity"] != null)
                    {
                        location.MinCapacity = Convert.ToDouble(objectNode.Attributes["MinCapacity"].InnerText, CURRENT_CULTURE);
                    }
                    if (objectNode.Attributes["MaxCapacity"] != null)
                    {
                        location.MaxCapacity = Convert.ToDouble(objectNode.Attributes["MaxCapacity"].InnerText, CURRENT_CULTURE);
                    }

                    foreach (XmlAttribute inT in objectNode["InT"].Attributes)
                    {
                        location.IncomeTransitionsID.Add(Convert.ToInt64(inT.InnerText));
                    }

                }

                #endregion

                #region Read Transitions
                else if (objectType.Equals("DT") || objectType.Equals("CT"))
                {

                    if (objectType.Equals("DT"))
                    {
                        pnObject = new DiscreteTransition();
                        value = Convert.ToDouble(objectNode.Attributes["Delay"].InnerText, CURRENT_CULTURE);
                        ((DiscreteTransition)pnObject).Delay = value;
                        ((DiscreteTransition)pnObject).DelayCounter = value;
                    }
                    else if (objectType.Equals("CT"))
                    {
                        pnObject = new ContinuousTransition();
                        value = Convert.ToDouble(objectNode.Attributes["Expectance"].InnerText, CURRENT_CULTURE);
                        ((ContinuousTransition)pnObject).Expectance = value;
                    }
                    Transition transition = (Transition)pnObject;
                    transition.Guard = Convert.ToBoolean(objectNode.Attributes["Guard"].InnerText);
                    transition.OutLocationAmount = Convert.ToInt32(objectNode.Attributes["OutL"].InnerText);
                    transition.TransitionGuardFormula = objectNode["BF"].Attributes["Str"].InnerText;

                    foreach (XmlAttribute inL in objectNode["InL"].Attributes)
                    {
                        transition.IncomeLocationsID.Add(Convert.ToInt64(inL.InnerText));
                    }
                    foreach (XmlAttribute inSAL in objectNode["InSAL"].Attributes)
                    {
                        transition.SALocations.Add(Convert.ToInt64(inSAL.InnerText));
                    }
                }
                #endregion

                #region Read Membrane
                else if (objectType.Equals("M"))
                {
                    pnObject = new Membrane();
                }
                #endregion

                #region Read Structural Membrane
                else if (objectType.Equals("SM"))
                {
                    pnObject = new StructuralMembrane();
                    ((StructuralMembrane)pnObject).Speed = Convert.ToDouble(objectNode.Attributes["Speed"].InnerText, CURRENT_CULTURE);
                }
                #endregion

                ReadShapeProperties(pnObject as Shape3D, objectNode);
            }
            if (pnObject is IFormula)
            {
                ((IFormula)pnObject).Formula = doubleFormula;
            }

            pnObject.ID = Convert.ToInt64(objectNode.Attributes["ID"].InnerText);
            pnObject.Name = objectNode.Attributes["Name"].InnerText;
            pnObject.Group = Convert.ToInt64(objectNode.Attributes["Group"].InnerText);
            pnObject.Type = GetObjectType(objectType);
            pnObject.ValueInCanvas.Text = value.ToString();

            return pnObject;
        }

        private PNObjectTypes GetObjectType(string objectType)
        {
            if (objectType.Equals("dA"))
            {
                return PNObjectTypes.DiscreteArc;
            }
            else if (objectType.Equals("dIA"))
            {
                return PNObjectTypes.DiscreteInhibitorArc;
            }
            else if (objectType.Equals("dTA"))
            {
                return PNObjectTypes.DiscreteTestArc;
            }
            else if (objectType.Equals("cA"))
            {
                return PNObjectTypes.ContinuousArc;
            }
            else if (objectType.Equals("cIA"))
            {
                return PNObjectTypes.ContinuousInhibitorArc;
            }
            else if (objectType.Equals("cTA"))
            {
                return PNObjectTypes.ContinuousTestArc;
            }
            else if (objectType.Equals("fA"))
            {
                return PNObjectTypes.ContinuousFlowArc;
            }
            else if (objectType.Equals("DL"))
            {
                return PNObjectTypes.DiscreteLocation;
            }
            else if (objectType.Equals("DT"))
            {
                return PNObjectTypes.DiscreteTransition;
            }
            else if (objectType.Equals("CL"))
            {
                return PNObjectTypes.ContinuousLocation;
            }
            else if (objectType.Equals("CT"))
            {
                return PNObjectTypes.ContinuousTransition;
            }
            else if (objectType.Equals("M"))
            {
                return PNObjectTypes.Membrane;
            }
            else if (objectType.Equals("SM"))
            {
                return PNObjectTypes.StructuralMembrane;
            }
            return PNObjectTypes.None;
        }

        private void ReadArcProperties(Arc3D arc, XmlNode objectNode)
        {
            arc.Thickness = PNProperties.ArcsThickness;
            arc.SetTexture(objectNode.Attributes["Texture"].InnerText);
            arc.StartID = Convert.ToInt64(objectNode["Con"].Attributes["SID"].InnerText);
            arc.EndID = Convert.ToInt64(objectNode["Con"].Attributes["EID"].InnerText);

            XmlNode spNode = objectNode["SP"];
            XmlNode mpNode = objectNode["MP"];
            XmlNode epNode = objectNode["EP"];

            arc.Points.Add(new Point3D(Convert.ToDouble(spNode.Attributes["X"].InnerText, CURRENT_CULTURE),
                                       Convert.ToDouble(spNode.Attributes["Y"].InnerText, CURRENT_CULTURE),
                                       Convert.ToDouble(spNode.Attributes["Z"].InnerText, CURRENT_CULTURE)));

            arc.Points.Add(new Point3D(Convert.ToDouble(mpNode.Attributes["X"].InnerText, CURRENT_CULTURE),
                                       Convert.ToDouble(mpNode.Attributes["Y"].InnerText, CURRENT_CULTURE),
                                       Convert.ToDouble(mpNode.Attributes["Z"].InnerText, CURRENT_CULTURE)));

            arc.Points.Add(arc.Points[arc.Points.Count - 1]);

            arc.Points.Add(new Point3D(Convert.ToDouble(epNode.Attributes["X"].InnerText, CURRENT_CULTURE),
                                      Convert.ToDouble(epNode.Attributes["Y"].InnerText, CURRENT_CULTURE),
                                      Convert.ToDouble(epNode.Attributes["Z"].InnerText, CURRENT_CULTURE)));
        }

        private void ReadShapeProperties(Shape3D shape, XmlNode objectNode)
        {
            shape.SetMesh(objectNode["Mesh"].Attributes["MName"].InnerText);

            shape.MaterialColor= Color.FromRgb(Byte.Parse(objectNode["Color"].Attributes["R"].InnerText),
                                                Byte.Parse(objectNode["Color"].Attributes["G"].InnerText),
                                                Byte.Parse(objectNode["Color"].Attributes["B"].InnerText));

            shape.Position = new Point3D(Convert.ToDouble(objectNode["Pos"].Attributes["X"].InnerText, CURRENT_CULTURE),
                                        Convert.ToDouble(objectNode["Pos"].Attributes["Y"].InnerText, CURRENT_CULTURE),
                                        Convert.ToDouble(objectNode["Pos"].Attributes["Z"].InnerText, CURRENT_CULTURE));

            shape.RotateX.Angle = Convert.ToDouble(objectNode["Rot"].Attributes["X"].InnerText, CURRENT_CULTURE);
            shape.RotateY.Angle = Convert.ToDouble(objectNode["Rot"].Attributes["Y"].InnerText, CURRENT_CULTURE);
            shape.RotateZ.Angle = Convert.ToDouble(objectNode["Rot"].Attributes["Z"].InnerText, CURRENT_CULTURE);

            shape.Size = Convert.ToDouble(objectNode["Scale"].Attributes["Value"].InnerText, CURRENT_CULTURE);

            //if (reader.Name.Equals("Mesh"))
            //{
            //    shape.SetMesh(reader.GetAttribute(0));
            //}
            //else if (reader.Name.Equals("Color"))
            //{
            //    shape.SetMaterial(Color.FromRgb(Byte.Parse(reader.GetAttribute(0)),
            //                                        Byte.Parse(reader.GetAttribute(1)),
            //                                        Byte.Parse(reader.GetAttribute(2))));
            //}
            //else if (reader.Name.Equals("Pos"))
            //{
            //    shape.Position = new Point3D(Convert.ToDouble(reader.GetAttribute(0), CURRENT_CULTURE),
            //                                Convert.ToDouble(reader.GetAttribute(1), CURRENT_CULTURE),
            //                                Convert.ToDouble(reader.GetAttribute(2), CURRENT_CULTURE));
            //}
            //else if (reader.Name.Equals("Rot"))
            //{
            //    shape.RotateX.Angle = Convert.ToDouble(reader.GetAttribute(0), CURRENT_CULTURE);
            //    shape.RotateY.Angle = Convert.ToDouble(reader.GetAttribute(1), CURRENT_CULTURE);
            //    shape.RotateZ.Angle = Convert.ToDouble(reader.GetAttribute(2), CURRENT_CULTURE);
            //}
            //else if (reader.Name.Equals("Scale"))
            //{
            //    shape.Size = Convert.ToDouble(reader.GetAttribute(0), CURRENT_CULTURE);
            //}
        }

        /// <summary>
        /// Clear all data and vieport to begin new net
        /// </summary>
        /// <param name="objManager">Contains information about current net</param>
        /// <param name="viewport">Viewport 3D which shows petri nets objects at the screen</param>
        /// <param name="canvasViewport">Canvas which shows objects info at the screen</param>
        public void NewNet(PNViewport viewport)
        {
            /*try
            {
                CreateDescriptionDocument();
            }
            catch (DirectoryNotFoundException)
            {
            }*/
            viewport.ClearAll();
            PNObjectRepository.PNObjects = new PNObjectDictionary<long, PNObject>();

            CurrentModelPath = null;
        }

        public void NewNet()
        {
            PNObjectRepository.PNObjects.Clear();
            CurrentModelPath = null;
            eventPublisher.ExecuteEvents(new NewNetEventArgs());
        }

        #endregion

        #region Save and open vectors

        //==========================================================================================
        /// <summary>
        /// Save current net in file
        /// </summary>
        /// <param name="fileName">Name of output file</param>
        /// <param name="objManager">Contains information about current net</param>
        public static void SaveGeometryFormula(VectorProjectionCarcass[] vectorCarcasses,
                                            SectoredCircle[] sectors, string fileName)
        {
            if (fileName == null || fileName.Equals(""))
                return;
            XmlTextWriter writer = new XmlTextWriter(fileName, System.Text.Encoding.Unicode);
            writer.WriteStartDocument();
            writer.WriteStartElement("GeometryFormula");

            #region Vectors
            foreach (VectorProjectionCarcass vectorCarcass in vectorCarcasses)
            {
                writer.WriteStartElement("VectorProjection");
                writer.WriteAttributeString("Type", vectorCarcass.Projection.ToString());
                writer.WriteStartElement("Vectors");
                foreach (CustomVector vector in vectorCarcass.Vectors[VectorType.Vector])
                {
                    writer.WriteStartElement("Vector");
                    WriteVectorData(writer, vector);
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();

                writer.WriteStartElement("Axises");
                foreach (CustomVector vector in vectorCarcass.Vectors[VectorType.Axis])
                {
                    writer.WriteStartElement("Axis");
                    WriteVectorData(writer, vector);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();

                #region Sectors

                foreach (SectoredCircle sector in sectors)
                {
                    if (!vectorCarcass.Projection.ToString().Equals(sector.Name))
                        continue;
                    writer.WriteStartElement("Sectors");
                    writer.WriteAttributeString("Name", sector.Name);

                    // TODO How to identify sectors ?
                    for (int i = 0; i < sector.SectorCount; i++)
                    {
                        writer.WriteStartElement("Sector");
                        writer.WriteAttributeString("Angle", sector.GetAngle(i).ToString(CURRENT_CULTURE));
                        WriteColor(writer, ((Sector)sector.Children[i]).MaterialColor);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }

                #endregion

                writer.WriteEndElement();
            }
            #endregion

            #region Meshes
            //writer.WriteStartElement("Meshes");

            //writer.WriteEndDocument();
            #endregion

            writer.WriteEndDocument();
            writer.Close();

        }

        private static void WriteVectorData(XmlTextWriter writer, CustomVector vector)
        {
            writer.WriteAttributeString("Name", vector.Name.Text);

            WriteColor(writer, vector.Color);
            WritePoint("StartPoint", writer, vector.StartPoint);
            WritePoint("EndPoint", writer, vector.EndPoint);
        }

        private static void WritePoint(string elementName, XmlTextWriter writer, Point3D point)
        {
            writer.WriteStartElement(elementName);
            writer.WriteAttributeString("X", point.X.ToString(CURRENT_CULTURE));
            writer.WriteAttributeString("Y", point.Y.ToString(CURRENT_CULTURE));
            writer.WriteAttributeString("Z", point.Z.ToString(CURRENT_CULTURE));
            writer.WriteEndElement();
        }

        private static void WriteColor(XmlTextWriter writer, Color color)
        {
            writer.WriteStartElement("Color");
            writer.WriteAttributeString("R", color.R.ToString());
            writer.WriteAttributeString("G", color.G.ToString());
            writer.WriteAttributeString("B", color.B.ToString());
            writer.WriteEndElement();
        }

        public static void OpenGeometryFormula(VectorProjectionCarcass[] vectorCarcasses,
                                            SectoredCircle[] sectors, string fileName)
        {
            if (fileName == null || fileName.Equals(""))
                return;

            foreach (VectorProjectionCarcass carcass in vectorCarcasses)
            {
                carcass.RemoveAllVectors();
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            XmlNodeList projectionNodes = doc.GetElementsByTagName("VectorProjection");


            #region Projection nodes
            foreach (XmlNode node in projectionNodes)
            {
                string viewportType = node.Attributes["Type"].InnerText;
                VectorProjectionCarcass currentCarcass = null;
                foreach (VectorProjectionCarcass carcass in vectorCarcasses)
                {
                    if (carcass.Projection.ToString().Equals(viewportType))
                    {
                        currentCarcass = carcass;
                        break;
                    }
                }
                if (currentCarcass == null)
                    continue;

                currentCarcass.RemoveAllVectors();

                #region Vector nodes
                foreach (CustomVector vector in GetVectors(node["Vectors"].GetElementsByTagName("Vector")))
                {
                    currentCarcass.AddVector(VectorType.Vector, vector);
                }

                foreach (CustomVector vector in GetVectors(node["Axises"].GetElementsByTagName("Axis")))
                {
                    currentCarcass.AddVector(VectorType.Axis, vector);
                }
                #endregion

                #region Sector nodes
                XmlElement sectorsNode = (XmlElement)node["Sectors"];
                if (sectorsNode == null)
                    continue;

                XmlNodeList sectorNodes = sectorsNode.GetElementsByTagName("Sector");

                string sectorName = sectorsNode.Attributes["Name"].InnerText;
                SectoredCircle currentSectoredCircle = null;
                foreach (SectoredCircle sectorCircle in sectors)
                {
                    if (sectorCircle.Name.Equals(sectorName))
                    {
                        currentSectoredCircle = sectorCircle;
                        break;
                    }
                }
                if (currentSectoredCircle == null)
                    continue;

                currentSectoredCircle.SectorCount = sectorNodes.Count;
                int index = 0;
                foreach (XmlNode sector in sectorNodes)
                {
                    currentSectoredCircle.SetAngle(index, Convert.ToDouble(sector.Attributes["Angle"].InnerText, CURRENT_CULTURE));
                    ((Sector)currentSectoredCircle.Children[index]).MaterialColor = GetColor(sector["Color"]);
                    index++;
                }
                #endregion

            }
            #endregion
        }

        private static List<CustomVector> GetVectors(XmlNodeList vectorNodes)
        {
            List<CustomVector> vectors = new List<CustomVector>();
            foreach (XmlNode vectorNode in vectorNodes)
            {
                CustomVector vector = new CustomVector();
                vector.Name.Text = vectorNode.Attributes["Name"].InnerText;

                vector.SetMaterial(GetColor((XmlElement)vectorNode["Color"]));
                vector.StartPoint = GetPoint3D((XmlElement)vectorNode["StartPoint"]);
                vector.EndPoint = GetPoint3D((XmlElement)vectorNode["EndPoint"]);
                vector.ChangeVectorPosition(vector.StartPoint, vector.EndPoint);

                vectors.Add(vector);
            }

            return vectors;
        }

        private static Color GetColor(XmlElement colorElement)
        {
            return Color.FromRgb(Byte.Parse(colorElement.Attributes["R"].InnerText),
                                  Byte.Parse(colorElement.Attributes["G"].InnerText),
                                    Byte.Parse(colorElement.Attributes["B"].InnerText));
        }

        private static Point3D GetPoint3D(XmlElement pointElement)
        {
            return new Point3D(Convert.ToDouble(pointElement.Attributes["X"].InnerText, CURRENT_CULTURE),
                                  Convert.ToDouble(pointElement.Attributes["Y"].InnerText, CURRENT_CULTURE),
                                    Convert.ToDouble(pointElement.Attributes["Z"].InnerText, CURRENT_CULTURE));
        }

        public static void NewGeometryFormula(List<VectorProjectionCarcass> vectorsViewports)
        {
            foreach (VectorProjectionCarcass vectorViewport in vectorsViewports)
            {
                vectorViewport.RemoveAll();
            }
        }
        #endregion

        #region Object description

        //==================================================================================================
        /// <summary>
        /// Delete description for selected object
        /// </summary>
        /// <param name="id">ID of selected object</param>
        public void DeleteObjectDescription(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(applicationPath + "/TempFiles/tempObjDescr.xml");
            XmlNode node = doc.SelectSingleNode("/Net/PNObject[@ID='" + id.ToString() + "']");
            if (node != null)
            {
                node.ParentNode.RemoveChild(node);
            }
            doc.Save(applicationPath + "/TempFiles/tempObjDescr.xml");
        }

        //==================================================================================================
        /// <summary>
        /// Save description of selected object to file
        /// </summary>
        /// <param name="id">ID of selected object</param>
        /// <param name="text">Text for saving</param>
        public void SaveObjectDescription(int id, string text)
        {
            DeleteObjectDescription(id);
            XmlDocument doc = new XmlDocument();
            doc.Load(applicationPath + "/TempFiles/tempObjDescr.xml");
            XmlElement objElement = doc.CreateElement("PNObject");
            objElement.SetAttribute("ID", id.ToString());
            objElement.SetAttribute("Description", text);

            doc.DocumentElement.AppendChild(objElement);

            XmlTextWriter xmlWriter = new XmlTextWriter(applicationPath + "/TempFiles/tempObjDescr.xml", Encoding.Default);
            xmlWriter.Formatting = Formatting.Indented;
            doc.WriteContentTo(xmlWriter);
            xmlWriter.Close();
        }

        //==================================================================================================
        /// <summary>
        /// Open description of selected object
        /// </summary>
        /// <param name="id">ID of seled object</param>
        /// <returns>Returns loaded description</returns>
        public string OpenObjectDescription(int id)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(applicationPath + "/TempFiles/tempObjDescr.xml");
            XmlNode node = doc.SelectSingleNode("/Net/PNObject[@ID='" + id.ToString() + "']");
            if (node != null)
            {
                return node.Attributes[1].InnerXml;
            }
            else
                return "";
        }

        //==================================================================================================
        /// <summary>
        /// Create new temporary file which contains descriptions of all objects
        /// </summary>
        public void CreateDescriptionDocument()
        {
            XmlTextWriter writer = new XmlTextWriter(applicationPath + "/TempFiles/tempObjDescr.xml", Encoding.Default);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("Net");
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();
        }
        #endregion

        #region Properties
        //==================================================================================================
        /// <summary>
        /// Get path of current opened model
        /// </summary>
        public static string CurrentModelPath
        {
            get;
            set;
        }

        //==================================================================================================
        /// <summary>
        /// Get current path of the application
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                return applicationPath;
            }
        }

        //==================================================================================================
        /// <summary>
        /// Get list of paths of recent opened models
        /// </summary>
        public string[] RecentOpenedModels
        {
            get
            {
                return recentOpenedModels;
            }
        }

        #endregion

        #region Члены IDisposable

        public void Dispose()
        {
        }

        #endregion
    }
}
