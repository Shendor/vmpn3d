using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool AllowToUpdateObjectsWithFormula
        {
            get;
            set;
        }

        public FormulaManager(IFormulaManager formulaManager)
        {
            this.formulaManager = formulaManager;
            AllowToUpdateObjectsWithFormula = true;
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

        public void UpdateObjectsWithFormula(List<ObjectWithFormula> objectsWithFormula)
        {
            if (AllowToUpdateObjectsWithFormula)
            {
                AllowToUpdateObjectsWithFormula = false;
                Task.Factory.StartNew(() =>
                {
                    if (objectsWithFormula == null)
                    {
                         GetObjectsWithFormula();
                    }
                    formulaManager.UpdateObjectsWithFormula(this.objectsWithFormula);

                    AllowToUpdateObjectsWithFormula = true;
                    IsNeedToCompile = false;
                });
            }
        }
        #endregion

    }
}
