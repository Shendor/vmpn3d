using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Properties;
using PNCreator.ManagerClasses.Exception;

namespace PNCreator.Modules.FormulaBuilder
{
    /// <summary>
    /// Convert formula string into comprehensible view for user
    /// </summary>
    public class FormulaConverter
    {

        static FormulaConverter()
        {
            ArrayOfObjects = new List<PNObject>();
        }

        public static List<PNObject> ArrayOfObjects
        {
            get;
            set;
        }
        // Interact with StringBuilder !!!
        public static string ToObjectNames(string formula)
        {
            var pnObjects = PNObjectRepository.PNObjects;

            if (formula == null || formula.Equals(""))
                return "";
            string convertedFormula = "";
            string[] lines;
            string[] separator = new string[] { "doubleValues" };

            lines = formula.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 2; i++)
            {
                convertedFormula = "";
                foreach (string line in lines)
                {
                    if (!line[0].Equals('['))
                        convertedFormula += line;
                    else
                    {
                        string key = "";
                        int j = 1;
                        while (!line[j].Equals(']'))
                        {
                            key += line[j];
                            ++j;
                        }
                        if (i == 0)
                            convertedFormula += pnObjects[Convert.ToInt64(key)].ToString() + line.Substring(j + 1);
                        if (i == 1)
                            convertedFormula += pnObjects[Convert.ToInt64(key)].ToString() + line.Substring(j + 1);
                        if (i == 2)
                            convertedFormula += "#" + pnObjects[Convert.ToInt64(key)].ToString() + line.Substring(j + 1);
                    }
                }
                if (i == 1)
                    separator[0] = "doubleValues";
                else if (i == 2)
                    separator[0] = "booleanValues";
                lines = convertedFormula.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            }
            return convertedFormula;
        }

        public static string ToArrayOfValues(string formula)
        {
            var pnObjects = PNObjectRepository.PNObjects;

            if (formula == null || formula.Equals(""))
                return null;
            int i = 0;
            formula.Trim();
            string formulaConverted = "";

            while (i < formula.Length)
            {
                if (!(formula[i].Equals('0') || formula[i].Equals('1') || formula[i].Equals('2') ||
                      formula[i].Equals('3') || formula[i].Equals('4') || formula[i].Equals('5') ||
                      formula[i].Equals('6') || formula[i].Equals('7') || formula[i].Equals('8') || formula[i].Equals('9')))
                {
                    int stopIndex = 0;
                    string buffer = "";
                    while (i < formula.Length && !(formula[i].Equals('+') || formula[i].Equals('-') || formula[i].Equals('*') ||
                             formula[i].Equals('/') || formula[i].Equals('(') || formula[i].Equals(')') || formula[i].Equals(',') ||
                             formula[i].Equals('.') || formula[i].Equals('>') || formula[i].Equals('<') || formula[i].Equals('=') ||
                             formula[i].Equals('!') || formula[i].Equals('?') || formula[i].Equals(':') || formula[i].Equals('&') ||
                             formula[i].Equals('|')))
                    {
                        buffer += formula[i];
                        ++i;
                        ++stopIndex;
                    }
                    if (stopIndex != 0)
                    {
                        bool isGuard = (buffer[0].Equals('#')) ? true : false;
                        if (isGuard)
                            buffer = buffer.Remove(0, 1);
                        if (!formula[(i == formula.Length) ? formula.Length - 1 : i].Equals('(') && !buffer.Equals("true") && !buffer.Equals("false"))
                        {
                            foreach(long key in pnObjects.Keys)    // check if buffer contains the name of object from Models
                            {
                                if (pnObjects[key].ToString().Equals(buffer))     // if it does, we add value of this object to our formulaConverted
                                {
                                    if (!isGuard)
                                        formulaConverted += "doubleValues[" + key.ToString() + "]";
                                    else if (isGuard && !(pnObjects[key] is Transition))
                                    {
                                        throw new IllegalPNObjectException(Messages.Default.NameWasNotFound + buffer);
                                    }
                                    else
                                        formulaConverted += "boolValues[" + key.ToString() + "]";
                                }
                        }
  
                        }
                        else
                            formulaConverted += buffer;
                        --i;
                    }
                    else
                        formulaConverted += formula[i];
                }
                else
                    formulaConverted += formula[i];
                ++i;
            }
            return formulaConverted;
        }

        public static List<List<PNObject>> ToArrayOfObjects(string formula)
        {
            var pnObjects = PNObjectRepository.PNObjects;

            if (formula == null || formula.Equals(""))
                return null;
            int i = 0;
            formula.Trim();

            List<PNObject> arrayOfObjects = new List<PNObject>();        // objects represented by apropriate value
            List<PNObject> arrayOfBoolObjects = new List<PNObject>();   // transitions represented by Guard

            while (i < formula.Length)
            {
                if (!(formula[i].Equals('0') || formula[i].Equals('1') || formula[i].Equals('2') ||
                      formula[i].Equals('3') || formula[i].Equals('4') || formula[i].Equals('5') ||
                      formula[i].Equals('6') || formula[i].Equals('7') || formula[i].Equals('8') || formula[i].Equals('9')))
                {
                    int stopIndex = 0;
                    string buffer = "";
                    while (i < formula.Length && !(formula[i].Equals('+') || formula[i].Equals('-') || formula[i].Equals('*') ||
                             formula[i].Equals('/') || formula[i].Equals('(') || formula[i].Equals(')') || formula[i].Equals(',') ||
                             formula[i].Equals('.') || formula[i].Equals('>') || formula[i].Equals('<') || formula[i].Equals('=') ||
                             formula[i].Equals('!') || formula[i].Equals('?') || formula[i].Equals(':') || formula[i].Equals('&') ||
                             formula[i].Equals('|')))
                    {
                        buffer += formula[i];
                        ++i;
                        ++stopIndex;
                    }
                    if (stopIndex != 0)
                    {
                        if (!formula[(i == formula.Length) ? formula.Length - 1 : i].Equals('(') && !buffer.Equals("true") && !buffer.Equals("false"))
                        {
                            bool isGuard = (buffer[0].Equals('#')) ? true : false;
                            if (isGuard)
                                buffer = buffer.Remove(0, 1);
                            foreach (PNObject pnObject in pnObjects.Values)     // check if buffer contains the name of object from Models
                            {
                                // if it does, we add value of this object to our formulaConverted
                                if (pnObject.ToString().Equals(buffer))
                                {
                                    if (!isGuard)
                                        arrayOfObjects.Add(pnObject);
                                    else if (isGuard && !(pnObject is Transition))
                                    {
                                        throw new IllegalPNObjectException(Messages.Default.NameWasNotFound + buffer);
                                    }
                                    else
                                        arrayOfBoolObjects.Add(pnObject);
                                }
                            }
                        }
                        --i;
                    }
                }
                ++i;
            }
            return new List<List<PNObject>> { arrayOfObjects, arrayOfBoolObjects };
        }

    }
}
