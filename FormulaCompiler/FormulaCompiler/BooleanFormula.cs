using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuntimeCompiler.FormulaCompiler
{
    public class BooleanFormula: IFormulaCompiler
    {
        private CompilerMethods boolMethod;
        private string expression;

        public BooleanFormula()
        {
            boolMethod = null;
        }

        public bool ExecuteBooleanFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)
        {
            if (boolMethod != null)
            {
                return boolMethod.ExecuteBooleanFormula(doubleValues, boolValues);
            }
            return false;
        }
        public void CompileFormula(string expression)
        {
            if (expression != null)
                boolMethod = FormulaCompiler.CompileBoolFormula(expression);
        }

        public string Expression
        {
            get { return expression; }
            set
            {
                if (value != null && value != "")
                {
                    expression = value;
                }
                else expression = null;
            }
        }
    }
}
