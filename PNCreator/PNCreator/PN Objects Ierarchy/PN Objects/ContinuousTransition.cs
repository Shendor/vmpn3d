using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows;
using PNCreator.ManagerClasses;

namespace PNCreator.PNObjectsIerarchy
{
    public class ContinuousTransition : Transition, IExtendedFormula
    {
        private double expectance;
        private double acceptedLevel;

        public ContinuousTransition(Point3D position = new Point3D())
            : base(position, PNObjectTypes.ContinuousTransition)
        {
            acceptedLevel = 1;
            expectance = 1;
            ValueInCanvas.Text = expectance.ToString();

            materialColor = PNCreator.ManagerClasses.PNColors.ContinuousObjects;
            SetMaterial(MaterialColor);

            SetMesh(PNCreator.Modules.Properties.PNProperties.ContinuousTransitionsForm);
        }

        public double Expectance
        {
            get { return expectance; }
            set 
            { 
                expectance = value;
                PNObjectRepository.PNObjects.SetDoubleValue(ID, expectance);
            }
        }

        public double AcceptedLevel
        {
            get { return acceptedLevel; }
            set { acceptedLevel = value; }
        }
        /// <summary>
        /// Launch formula which was built by Formula Builder. If formula is not correct, expectance of this transition
        /// will be set by default ( 1 )
        /// <param name="arcsValues">array of all weights of arcs</param>
        /// <param name="objectsValues">array of all delay time of transitions and tokens amount of locations</param>
        /// </summary>
        public double ExecuteFormula()
        {
            try
            {
                //Expectance = formula(objectsValues, arcsValues);
                Expectance = doubleFormula.ExecuteFormula(PNObjectRepository.PNObjects.DoubleValues, PNObjectRepository.PNObjects.BooleanValues);
            }
            catch (Exception)
            {
                Expectance = 1;
            }
            return Expectance;
        }
    }
}
