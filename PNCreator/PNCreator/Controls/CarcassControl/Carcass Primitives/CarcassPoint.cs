using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using Meshes3D;
using PNCreator.ManagerClasses;

namespace PNCreator.Controls.CarcassControl
{
    public abstract class CarcassPoint : CustomMesh
    {
        protected int id;

        public CarcassPoint(Point3D point)
            :base(point)
        {
            Material = PNObjectMaterial.GetPlainMaterial(PNColors.CarcassPoint);

            MaterialColor = PNColors.CarcassPoint;

            meshFilename = "";

            id = GetHashCode();
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public void Hide()
        {
            Geometry.Material = null;
        }

        public void Show()
        {
            this.ResetMaterial();
        }

       
    }
}
