using System.Collections.Generic;
using System.Windows.Media.Media3D;
using RuntimeCompiler.FormulaCompiler;

namespace PNCreator.PNObjectsIerarchy
{
    public class Location : Shape3D
    {
        private List<long> incomeTransitionsID;
        private IDictionary<long, long> incomeArcsID;
        protected string formulaString = null;
        protected DoubleFormula doubleFormula;

        public Location(Point3D position, PNObjectTypes objectType)
            : base(position, objectType)
        {
            incomeTransitionsID = new List<long>();
            incomeArcsID = new Dictionary<long, long>();

            doubleFormula = new DoubleFormula();
            MaxCapacity = 9E8;
        }

        public double MinCapacity { get; set; }

        public double MaxCapacity { get; set; }

        //==================================================================================================
        /// <summary>
        /// Set or get income transitions id collection
        /// </summary>
        public List<long> IncomeTransitionsID
        {
            get
            {
                return incomeTransitionsID;
            }
            set
            {
                incomeTransitionsID = value;
            }
        }
        //==================================================================================================
        /// <summary>
        /// Set or get income arcs id collection
        /// </summary>
        public IDictionary<long, long> IncomeArcsID
        {
            get
            {
                return incomeArcsID;
            }
            set
            {
                incomeArcsID = value;
            }
        }

        public string Formula
        {
            get
            {
                return doubleFormula.Expression;
            }
            set
            {
                if (value.Equals(""))
                    doubleFormula.Expression = null;
                else
                    doubleFormula.Expression = value;
            }
        }

        public void CompileFormula(string expression)
        {
            doubleFormula.CompileFormula(expression);
        }
    }
}
