using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.FormulaManager
{
    public interface IFormulaManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<ObjectWithFormula> GetObjectsWithFormula();

        //FIXME: define better way to inherit it
        /// <summary>
        /// Defines if compilation is required
        /// </summary>
        bool IsNeedToCompile
        {
            get;
            set;
        }
    }
}
