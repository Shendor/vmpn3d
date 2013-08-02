using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses;

namespace PNCreator.PNObjectsIerarchy
{
    public class ContinuousLocation : Location, IFormula
    {
        private double level;

        public ContinuousLocation(Point3D position = new Point3D())
            : base(position, PNObjectTypes.ContinuousLocation)
        {
            level = 0;

            materialColor = PNCreator.ManagerClasses.PNColors.ContinuousObjects;
            SetMaterial(MaterialColor);
            
            SetMesh(PNCreator.Modules.Properties.PNProperties.ContinuousLocationsForm);
                   
        }

        /// <summary>
        /// Set or get level amount
        /// </summary>
        public double Level
        {
            get { return level; }
            set 
            {
                level = value;
                PNObjectRepository.PNObjects.SetDoubleValue(ID, level);
            }
        }

        #region IFormula Members


        public double ExecuteFormula()
        {
            try
            {
                Level = doubleFormula.ExecuteFormula(PNObjectRepository.PNObjects.DoubleValues, PNObjectRepository.PNObjects.BooleanValues);
            }
            catch (Exception)
            {
                Level = 1;
            }
            return Level;
        }

        #endregion
    }
}
