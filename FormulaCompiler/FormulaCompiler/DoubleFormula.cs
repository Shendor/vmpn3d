using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuntimeCompiler.FormulaCompiler
{
    public class DoubleFormula : IFormulaCompiler
    {
        private CompilerMethods doubleMethod;
        private string expression;

        public DoubleFormula()
        {
            doubleMethod = null;
        }

        public double ExecuteFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)
        {
            if (doubleMethod != null)
            {
                return doubleMethod.ExecuteFormula(doubleValues, boolValues);
            }
            return 0.0;
        }

        public void CompileFormula(string expression)
        {
            if(expression != null)
                doubleMethod = FormulaCompiler.CompileFormula(expression);
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
