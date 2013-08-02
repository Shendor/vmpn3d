using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using System.Xml;

namespace PNCreator.Modules.Simulation.Service
{
    public class SimulationHistoryDao : ISimulationHistoryDao
    {
        private static readonly CultureInfo CURRENT_CULTURE = new CultureInfo("ru-RU");

        public void LoadSimulationData(string fileName, ICollection<PNObject> pnObjects)
        {
//            var eventPublisher = App.GetObject<EventPublisher>();
//            var progressEvent = new ProgressEventArgs(0);
            
            XmlDocument document = new XmlDocument();
            document.Load(fileName);
          
            XmlNodeList objectNodes = document.GetElementsByTagName("Object");
           
            foreach (XmlNode objectNode in objectNodes)
            {
                PNObject pnObject = GetPnObjectFromNode(pnObjects, objectNode);
                if (pnObject != null)
                {
                    XmlElement xmlElement = objectNode["Simulation"];
                    if (xmlElement != null)
                    {
                        string simulationHistoryName = xmlElement.Attributes["Name"].InnerText;

                        pnObject.AddNewHistoryTable(simulationHistoryName);
                        pnObject.AllowSaveHistory = true;
                        XmlNodeList stepNodes = xmlElement.ChildNodes;
                        foreach (XmlNode stepNode in stepNodes)
                        {
                            double time = Convert.ToDouble(stepNode.Attributes["Time"].InnerText, CURRENT_CULTURE);
                            double value = Convert.ToDouble(stepNode.Attributes["Value"].InnerText, CURRENT_CULTURE);

                            pnObject.AddNewRowOfHistory(simulationHistoryName, time, value);
                            /*Thread.Sleep(100);
                            progressEvent.MaximumProgress = stepNodes.Count * objectNodes.Count;
                            progressEvent.Progress++;
                            eventPublisher.ExecuteEvents(new ProgressEventArgs(progressEvent.Progress));*/
                        }
                    }
                }
            }
//            ExecuteFinishProgressEvent(progressEvent, eventPublisher);
        }

        public void SaveSimulationData(string fileName, ICollection<PNObject> pnObjects)
        {
           /* var eventPublisher = App.GetObject<EventPublisher>();
            var progressEvent = new ProgressEventArgs(0)
                {
                    MaximumProgress = pnObjects.Count
                };*/

            XmlTextWriter writer = new XmlTextWriter(fileName, Encoding.Unicode);
            writer.WriteStartDocument();
            writer.WriteStartElement("Net");

            foreach (PNObject pnObject in pnObjects)
            {
                Dictionary<string, DataTable> simulationHistories = pnObject.ObjectHistory;
                if (simulationHistories.Count > 0)
                {
                    writer.WriteStartElement("Object");
                    writer.WriteAttributeString("Name", pnObject.Name);

                    foreach (var simulationHistory in simulationHistories)
                    {
                        writer.WriteStartElement("Simulation");
                        writer.WriteAttributeString("Name", simulationHistory.Key);

                        foreach (DataRow simulationHistoryValue in simulationHistory.Value.Rows)
                        {
                            double time = (double)simulationHistoryValue[0];
                            double value = (double)simulationHistoryValue[1];

                            writer.WriteStartElement("Step");

                            writer.WriteAttributeString("Time", time.ToString(CURRENT_CULTURE));
                            writer.WriteAttributeString("Value", value.ToString(CURRENT_CULTURE));

                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                }
               /* Thread.Sleep(1000);
                progressEvent.Progress++;
                eventPublisher.ExecuteEvents(progressEvent);*/
            }

            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

//            ExecuteFinishProgressEvent(progressEvent, eventPublisher);
        }

        private PNObject GetPnObjectFromNode(IEnumerable<PNObject> pnObjects, XmlNode objectNode)
        {
            foreach (PNObject pnObject in pnObjects)
            {
                XmlAttribute xmlAttribute = objectNode.Attributes["Name"];
                if (xmlAttribute != null && pnObject.Name.Equals(xmlAttribute.InnerText))
                {
                    return pnObject;
                }
            }
            return null;
        }

        private static void ExecuteFinishProgressEvent(ProgressEventArgs progressEvent, EventPublisher eventPublisher)
        {
            progressEvent.Progress = progressEvent.MaximumProgress;
            eventPublisher.ExecuteEvents(progressEvent);
            eventPublisher.UnRegister(typeof(ProgressEventArgs));
        }
    }

}
