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
    public class DiscreteLocation : Location, IFormula
    {
        private int tokens;

        public DiscreteLocation(Point3D position = new Point3D())
            : base(position, PNObjectTypes.DiscreteLocation)
        {
            SetMesh(PNCreator.Modules.Properties.PNProperties.DiscreteLocationsForm);
            tokens = 0;
            ValueInCanvas.Text = tokens.ToString();
            materialColor = PNCreator.ManagerClasses.PNColors.DiscreteObjects;
            SetMaterial(MaterialColor);
            Size  = 1;
        }

        /// <summary>
        /// Set or get tokens amount
        /// </summary>
        public int Tokens
        {
            get { return tokens; }
            set
            { 
                tokens = value;
                PNObjectRepository.PNObjects.SetDoubleValue(ID, tokens);
            }
        }

        #region IFormula Members

        public double ExecuteFormula()
        {
            try
            {
                if (Formula != null)
                {
                    Tokens = (int)doubleFormula.ExecuteFormula(PNObjectRepository.PNObjects.DoubleValues, PNObjectRepository.PNObjects.BooleanValues);
                }
            }
            catch (Exception)
            {
                Tokens = 1;
            }
            return Tokens;
        }

        #endregion
       
    }
}
