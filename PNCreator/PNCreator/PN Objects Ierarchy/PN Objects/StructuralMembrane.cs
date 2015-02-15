using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using RuntimeCompiler.FormulaCompiler;
using PNCreator.ManagerClasses;

namespace PNCreator.PNObjectsIerarchy
{
    public class StructuralMembrane : Membrane, IFormula
    {

        private double speed;
        private DoubleFormula doubleFormula;

        public StructuralMembrane(Point3D position = new Point3D())
            : base(position)
        {
            this.Type = PNObjectTypes.StructuralMembrane;

            SetMesh(PNCreator.Modules.Properties.PNProperties.MembranesForm);
            
            materialColor = PNCreator.ManagerClasses.PNColors.Membrane;
            SetMaterial(MaterialColor);

            speed = DEFAULT_SPEED;
            valueInCanvas.Text = speed.ToString();

            doubleFormula = new DoubleFormula();
        }

        public double Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                PNObjectRepository.PNObjects.SetDoubleValue(ID, speed);
            }
        }
        

        public void Animate(double seconds)
        {
            DoubleAnimation sizeAnimation = new DoubleAnimation();
            sizeAnimation.DecelerationRatio = 0.2;
            sizeAnimation.SpeedRatio = (Speed <= 0) ? Math.E : Speed;
            sizeAnimation.Duration = TimeSpan.FromSeconds(seconds);
            sizeAnimation.RepeatBehavior = new RepeatBehavior(Speed);
            sizeAnimation.FillBehavior = FillBehavior.Stop;
            sizeAnimation.AutoReverse = true;

            sizeAnimation.From = Speed;
            sizeAnimation.To = 1.2 * Speed;

            scale.BeginAnimation(ScaleTransform3D.ScaleXProperty, sizeAnimation);
            scale.BeginAnimation(ScaleTransform3D.ScaleYProperty, sizeAnimation);
            scale.BeginAnimation(ScaleTransform3D.ScaleZProperty, sizeAnimation);

        }

        //==================================================================================================
        /// <summary>
        /// Compile formula
        /// </summary>
        public void CompileFormula(string expression)
        {
            doubleFormula.CompileFormula(expression);
        }

        public double ExecuteFormula()
        {
            try
            {
                if (Formula != null)
                {
                    Speed = doubleFormula.ExecuteFormula(PNObjectRepository.PNObjects.DoubleValues, PNObjectRepository.PNObjects.BooleanValues);
                }
            }
            catch 
            {
                Speed = DEFAULT_SPEED;
            }
            return Speed;
        }

        public string Formula
        {
            get { return doubleFormula.Expression; }
            set
            {
                doubleFormula.Expression = value;
            }
        }

    }
}
