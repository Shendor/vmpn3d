using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.PNObjectsIerarchy
{
    public interface IExtendedFormula : IFormula
    {
        string TransitionGuardFormula
        {
            get;
            set;
        }
        void CompileBooleanFormula(string expression);
        bool ExecuteGuardFormula();
    }
}
