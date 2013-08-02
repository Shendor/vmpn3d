using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;

namespace PNCreator.Modules.ODLEditor
{
    class XMLSchemaDataFactory
    {
        private bool isValid = false;
        private XmlSchema schema;

        public XMLSchemaDataFactory(string xmlFile)
        {
            schema = ReadAndCompileSchema(xmlFile);
        }

        public string GetElementByText(int line, int column)
        {
            return null;
        }

        public IEnumerable<CompletionData> GetElements()
        {
            foreach (XmlSchemaElement elem in schema.Elements.Values)
            {
                yield return new CompletionData(elem.Name, GetAnnotation(elem), null, CompletionDataType.Element);
            }
        }

        public IEnumerable<CompletionData> GetElementsByName(string name)
        {
            foreach (XmlSchemaElement elem in schema.Elements.Values)
            {
                if (elem.Name.Contains(name))
                {
                    yield return new CompletionData(elem.Name, GetAnnotation(elem), null, CompletionDataType.Element);
                }
            }
        }

        public IEnumerable<CompletionData> GetAttributesOfElement(string elementName)
        {
            foreach (XmlSchemaElement element in schema.Elements.Values)
            {
                if (element.Name.Equals(elementName))
                {
                    if (element.ElementSchemaType is XmlSchemaComplexType)
                    {
                        XmlSchemaComplexType complexType = element.ElementSchemaType as XmlSchemaComplexType;

                        foreach (DictionaryEntry obj in complexType.AttributeUses)
                        {
                            XmlSchemaAttribute attribute = obj.Value as XmlSchemaAttribute;
                            yield return new CompletionData(attribute.QualifiedName.Name, GetAnnotation(attribute),
                                                                attribute.DefaultValue, CompletionDataType.Attribute);
                        }

                    }
                    break;
                }
            }
        }

        private string GetAnnotation(XmlSchemaAnnotated item)
        {
            if (item.Annotation == null) return null;
            StringBuilder description = new StringBuilder();
            for (int i = 0; i < item.Annotation.Items.Count; i++)
            {
                if (item.Annotation.Items[i] is XmlSchemaDocumentation)
                    for (int j = 0; j < ((XmlSchemaDocumentation)item.Annotation.Items[i]).Markup.Length; j++)
                        description.Append(((XmlSchemaDocumentation)item.Annotation.Items[i]).Markup[j].InnerText);
                else if (item.Annotation.Items[i] is XmlSchemaAppInfo)
                    for (int j = 0; j < ((XmlSchemaDocumentation)item.Annotation.Items[i]).Markup.Length; j++)
                        description.Append(((XmlSchemaAppInfo)item.Annotation.Items[i]).Markup[j].InnerText);
            }
            return description.ToString();
        }

        public IEnumerable<string> GetElementsOfElement(string elementName)
        {
            return null;
        }

        public bool ValidateXML(string xmlContent)
        {
            XmlTextReader tr = new XmlTextReader(xmlContent);
            XmlValidatingReader v = new XmlValidatingReader(tr);
            v.ValidationType = ValidationType.Schema;
            v.ValidationEventHandler += new ValidationEventHandler(ValidationCallbackOne);
            v.Read();
            return isValid;
        }

        private XmlSchema ReadAndCompileSchema(string fileName)
        {
            XmlTextReader tr = new XmlTextReader(fileName, new NameTable());

            XmlSchema schema = XmlSchema.Read(tr, new ValidationEventHandler(ValidationCallbackOne));
            tr.Close();

            XmlSchemaSet xset = new XmlSchemaSet();
            xset.Add(schema);

            xset.ValidationEventHandler += new ValidationEventHandler(ValidationCallbackOne);

            xset.Compile();

            return schema;
        }

        private void ValidationCallbackOne(object sender, ValidationEventArgs args)
        {
            isValid = false;
            Console.WriteLine("Exception Severity: " + args.Severity);
            Console.WriteLine(args.Message);
        }

    }
    class XmlSchemaData
    {
        public string Item
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

    }
}
