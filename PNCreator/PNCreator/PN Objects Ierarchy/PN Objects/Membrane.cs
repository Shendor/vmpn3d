using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using PNCreator.Controls;

namespace PNCreator.PNObjectsIerarchy
{
    public class Membrane : Shape3D
    {
        protected static double DEFAULT_SPEED = 0.4;

        private ISet<PNObject> pnObjects;
        private bool isWired = false;
        private double actualSize;
        private BoundingBox membraneWire;

        public Membrane(Point3D position = new Point3D())
            : base(position, PNObjectTypes.Membrane)
        {

            SetMesh(PNCreator.Modules.Properties.PNProperties.MembranesForm);

            materialColor = PNCreator.ManagerClasses.PNColors.Membrane;
            SetMaterial(MaterialColor);

            valueInCanvas.Text = "";
            //Scale = DEFAULT_SIZE;

            pnObjects = new HashSet<PNObject>();
           
        }


        public ISet<PNObject> PNObjects
        {
            get { return pnObjects; }
            set { pnObjects = value; }
        }

        public bool IsWired
        {
            get { return isWired; }
            set
            {
                isWired = value;

                if (isWired)
                {
                    membraneWire = new BoundingBox();
                    actualSize = Size;
                    membraneWire.BuildBoundingBox(Bounds);
                    membraneWire.Color = MaterialColor;
                   
                    Children.Add(membraneWire);
                    Size = 0;
                    IsReadOnly = true;
                }
                else
                {
                    IsReadOnly = false;
                    Size = actualSize;
                    Children.Remove(membraneWire);
                    membraneWire = null;
                }
            }
        }

    }
}
