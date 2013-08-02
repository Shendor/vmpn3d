using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.FormulaManager
{
    public class FormulaManager : IFormulaManager
    {
        private List<ObjectWithFormula> objectsWithFormula;
        private IFormulaManager formulaManager;

        /// <summary>
        /// 
        /// </summary>
        public bool IsNeedToCompile
        {
            get
            {
                return PNProgramStorage.IsNeedToCompile;
            }
            set
            {
                PNProgramStorage.IsNeedToCompile = value;
            }
        }

        public FormulaManager(IFormulaManager formulaManager)
        {
            this.formulaManager = formulaManager;
        }

        #region IFormulaManager Members

        public List<ObjectWithFormula> GetObjectsWithFormula()
        {
            if (objectsWithFormula == null || IsNeedToCompile)
            {
                objectsWithFormula = formulaManager.GetObjectsWithFormula();
            }

            return objectsWithFormula;
        }

        #endregion
    }
}
