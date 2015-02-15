using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses;

namespace PNCreator.PNObjectsIerarchy
{
    public class DiscreteTransition : Transition, IExtendedFormula
    {
        private double delay;
        private double delayCounter;
        private int acceptedTokens;
        private bool isValid;

        public DiscreteTransition(Point3D position = new Point3D())
            : base(position,PNObjectTypes.DiscreteTransition)
        {
            delay = 1;
            delayCounter = 0;
            acceptedTokens = 0;
            ValueInCanvas.Text = delay.ToString();
            materialColor = PNCreator.ManagerClasses.PNColors.DiscreteObjects;
            SetMaterial(MaterialColor);
            SetMesh(PNCreator.Modules.Properties.PNProperties.DiscreteTransitionsForm);
        }

        /// <summary>
        /// Indicate if transition can obtain marker from location but cannot be active due to its delay time
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        /// <summary>
        /// Set or get delay time (seconds)
        /// </summary>
        public double Delay
        {
            get { return delay; }
            set
            {
                if (value <= 0) delay = 1;
                else delay = value;

                PNObjectRepository.PNObjects.SetDoubleValue(ID, delay);
            }
        }
        /// <summary>
        /// Set or get delay counter. Increases on delay value each pass when net's in work
        /// </summary>
        public double DelayCounter
        {
            get { return delayCounter; }
            set { delayCounter = value; }
        }
        /// <summary>
        /// Set or get accepted tokens from locations
        /// </summary>
        public int AcceptedTokens
        {
            get { return acceptedTokens; }
            set { acceptedTokens = value; }
        }
        /// <summary>
        /// Launch formula which was built by Formula Builder. If formula is not correct, delay time of this transition
        /// will be set by default ( 1 )
        /// <param name="arcsValues">array of all weights of arcs</param>
        /// <param name="objectsValues">array of all delay time of transitions and tokens amount of locations</param>
        /// </summary>
        public double ExecuteFormula()
        {
            try
            {
                if (Formula != null)
                {
                    Delay = doubleFormula.ExecuteFormula(PNObjectRepository.PNObjects.DoubleValues, PNObjectRepository.PNObjects.BooleanValues);
                }
            }
            catch (Exception)
            {
                Delay = 1;
            }
            return Delay;
        }


        public void ResetDelayCounter()
        {
            DelayCounter = -1;
        }


        public bool CanBeActivatedForTime(double time)
        {
            return DelayCounter != -1 && DelayCounter <= time;
        }
    }
}
