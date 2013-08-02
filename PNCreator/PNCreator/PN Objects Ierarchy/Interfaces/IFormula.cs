using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.PNObjectsIerarchy
{
    public interface IFormula
    {
        /// <summary>
        /// Launch formula which was built by Formula Builder. If formula is not correct, expectance of this transition
        /// will be set by default ( 1 )
        /// <param name="arcsValues">array of all weights of arcs</param>
        /// <param name="objectsValues">array of all delay time of transitions and tokens amount of locations</param>
        /// </summary>
        double ExecuteFormula();

        /// <summary>
        /// Get or set double formula
        /// </summary>
        string Formula { get; set; }

        /// <summary>
        /// Compile formula
        /// </summary>
        void CompileFormula(string expression);
    }
}
