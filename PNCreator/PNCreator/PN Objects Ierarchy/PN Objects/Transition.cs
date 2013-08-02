using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using RuntimeCompiler.FormulaCompiler;
using PNCreator.ManagerClasses;

namespace PNCreator.PNObjectsIerarchy
{
    public abstract class Transition : Shape3D
    {
        protected double priority;
        protected bool isActive;
        protected bool guard;

        protected List<long> incomeLocationsID;
        protected IDictionary<long, long> incomeArcsID;
        protected List<long> saLocations;

        protected int outcomeLocationsAmount;

        protected string formulaString = null;
        protected string guardFormulaString = null;

        protected BooleanFormula booleanFormula;
        protected DoubleFormula doubleFormula;

        public Transition(Point3D position, PNObjectTypes objectType)
            : base(position, objectType)
        {
            isActive = false;
            guard = true;
            priority = 0;
            outcomeLocationsAmount = 0;
            incomeLocationsID = new List<long>();
            incomeArcsID = new Dictionary<long, long>();
            saLocations = new List<long>();

            doubleFormula = new DoubleFormula();
            booleanFormula = new BooleanFormula();
        }
        //==================================================================================================
        /// <summary>
        /// Set or get transition activity
        /// </summary>
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                isActive = value;
            }
        }
        //==================================================================================================
        /// <summary>
        /// Set or get guard
        /// </summary>
        public bool Guard
        {
            get
            {
                return guard;
            }
            set
            {
                guard = value;
                PNObjectRepository.PNObjects.SetBooleanValue(ID, guard);
            }
        }
        //==================================================================================================
        /// <summary>
        /// Set or get income locations id collection
        /// </summary>
        public List<long> IncomeLocationsID
        {
            get
            {
                return incomeLocationsID;
            }
            set
            {
                incomeLocationsID = value;
            }
        }
        //==================================================================================================
        /// <summary>
        /// Set or get income locations ID's with special ability (SA) which has inghibitor arc or test arc and this arc connects with this transition
        /// </summary>
        public List<long> SALocations
        {
            get
            {
                return saLocations;
            }
            set
            {
                saLocations = value;
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
        //==================================================================================================
        /// <summary>
        /// Set or get outcome locations amount
        /// </summary>
        public int OutLocationAmount
        {
            get
            {
                return outcomeLocationsAmount;
            }
            set
            {
                outcomeLocationsAmount = value;
            }
        }
        //==================================================================================================
        /// <summary>
        /// Get or set priority of working out
        /// </summary>
        public double Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
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
        //==================================================================================================
        /// <summary>
        /// Get or set Guard formula
        /// </summary>
        public string TransitionGuardFormula
        {
            get
            {
                return booleanFormula.Expression;
            }
            set
            {
                if (value.Equals(""))
                    booleanFormula.Expression = null;
                else
                    booleanFormula.Expression = value;

            }
        }
        //==================================================================================================
        /// <summary>
        /// Compile formula
        /// </summary>
        public void CompileFormula(string expression)
        {
            doubleFormula.CompileFormula(expression);
        }
        //==================================================================================================
        /// <summary>
        /// Compile boolean formula
        /// </summary>
        public void CompileBooleanFormula(string expression)
        {

            booleanFormula.CompileFormula(expression);
        }
        //===================================================================================
        /// <summary>
        /// Launch formula which was built by Formula Builder. If formula is not correct, delay time of this transition
        /// will be set by default ( 1 )
        /// </summary>
        public bool ExecuteGuardFormula()
        {
            try
            {
                Guard = booleanFormula.ExecuteBooleanFormula(PNObjectRepository.PNObjects.DoubleValues, PNObjectRepository.PNObjects.BooleanValues);
            }
            catch (Exception)
            {
                Guard = true;
            }
            return Guard;
        }


    }
}
