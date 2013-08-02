using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.ManagerClasses;

namespace PNCreator.Modules.FormulaBuilder
{
    /// <summary>
    /// Convert formula string into comprehensible view for user
    /// </summary>
    public class FormulaConverter
    {
        public static string ToObjectNames(string formula, PNObjectManager objManager)
        {
            if (formula == null || formula.Equals("")) return "";
            string convertedFormula = "";
            string[] lines;
            string[] separator = new string[1];
            separator[0] = "shapesValues";
            lines = formula.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < 2; i++)
            {
                convertedFormula = "";
                foreach (string line in lines)
                {
                    if (line[0] != '[') convertedFormula += line;
                    else
                    {
                        string index = "";
                        int j = 1;
                        while (line[j] != ']')
                        {
                            index += line[j];
                            ++j;
                        }
                        if (i == 0) convertedFormula += objManager.Shapes[Convert.ToInt32(index)].Name + line.Substring(j + 1);
                        if (i == 1) convertedFormula += objManager.Arcs[Convert.ToInt32(index)].Name + line.Substring(j + 1);
                    }
                }
                separator[0] = "arcsValues";
                lines = convertedFormula.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            }
            return convertedFormula;
        }

        public static string ToArrayOfValues(string formula, PNObjectManager objManager)
        {
            if (formula == null || formula.Equals("")) return null;
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
                        if (!formula[(i == formula.Length) ? formula.Length - 1 : i].Equals('(') && !buffer.Equals("true") && !buffer.Equals("false"))
                        {
                            bool isNotFound = true;
                            for(int j = 0; j < objManager.Shapes.Count; j++)    // check if buffer contains the name of object from Models
                                if (objManager.Shapes[j].Name.Equals(buffer))     // if it does, we add value of this object to our formulaConverted
                                {
                                    formulaConverted += "shapesValues[" + j.ToString() + "]";
                                    j = objManager.Shapes.Count + 1;
                                    isNotFound = false;
                                }
                            if(isNotFound == true)
                                for (int j = 0; j < objManager.Arcs.Count; j++)    // check if buffer contains the name of object from Arcs
                                    if (objManager.Arcs[j].Name.Equals(buffer))     // if it does, we add value of this object to our formulaConverted
                                    {
                                        formulaConverted += "arcsValues[" + j.ToString() + "]";
                                        j = objManager.Arcs.Count + 1;
                                        isNotFound = false;
                                    }
                            if (isNotFound == true)     // if this name (from buffer) was not found - show Message and return from the parser
                            {
                                WindowsControl.DialogWindow.Error(MessagesList.GetMessage(1) + buffer);
                                return null;
                            }
                        }
                        else formulaConverted += buffer;
                        --i;
                    }
                    else formulaConverted += formula[i];
                }
                else formulaConverted += formula[i];
                ++i;
            }
            return formulaConverted;
        }

    }
}
