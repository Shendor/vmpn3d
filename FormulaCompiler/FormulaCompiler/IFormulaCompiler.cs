using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuntimeCompiler.FormulaCompiler
{
    interface IFormulaCompiler
    {
        void CompileFormula(string expression);
    }
}
