using System;
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
using _3DTools;
using PNCreator.Controls;
namespace PNCreator.PNObjectsIerarchy
{
    public class Shape3D : PNObject, ITranslationContent2D
    {
        protected string meshName;

        //===================================================================================
        public Shape3D(Point3D position, PNObjectTypes objectType)
            : base(objectType)
        {
            this.Position = position;

            SetCanvasProperties();
        }

        #region Color property
       
        protected override void SetMaterial(Color color)
        {
            if (Type == PNObjectTypes.StructuralMembrane || Type == PNObjectTypes.Membrane)
            {
                Geometry.Material = ManagerClasses.PNObjectMaterial.GetSemiTransparentMaterial(color);
                //Geometry.BackMaterial = PNCreator.ManagerClasses.PNObjectMaterial.GetSemiTransparentMaterial(color);
                //PNCreator.ManagerClasses.PNObjectMaterial.GetSemiTransparentMaterial(this);
            }
            else
            {
                Geometry.Material = ManagerClasses.PNObjectMaterial.GetMaterial(color);
                //Geometry.BackMaterial = PNCreator.ManagerClasses.PNObjectMaterial.GetMaterial(color);
            }

            materialColor = color;

            /* Geometry.Material = PNCreator.ManagerClasses.PNObjectMaterial.GetMaterial(color);
             Geometry.BackMaterial = PNCreator.ManagerClasses.PNObjectMaterial.GetMaterial(color);
             MaterialColor = color;*/

        }
        public override void ResetMaterial()
        {
            if (Type == PNObjectTypes.StructuralMembrane || Type == PNObjectTypes.Membrane)
            {
                Geometry.Material = ManagerClasses.PNObjectMaterial.GetSemiTransparentMaterial(MaterialColor);
            }
            else
            {
                Geometry.Material = ManagerClasses.PNObjectMaterial.GetMaterial(MaterialColor);
            }
        }
        public void MakeSemiTransparent()
        {
            Geometry.Material = PNCreator.ManagerClasses.PNObjectMaterial.GetSemiTransparentMaterial(MaterialColor);
            //PNCreator.ManagerClasses.PNObjectMaterial.GetSemiTransparentMaterial(this);
        }
        #endregion

        #region Mesh name property
        public string MeshName
        {
            get { return meshName; }
            set { meshName = value; }
        }
        public void SetMesh(string filename)
        {
            Mesh = PNCreator.ManagerClasses.PNObjectMesh.GetMesh(PNCreator.ManagerClasses.PNDocument.ApplicationPath + filename);
            MeshName = filename;
        }
        #endregion

        #region ITranslationContent2D Members

        public void TranslateContent2D(Viewport3D viewport)
        {
            Point point2D = MathUtils.Convert3DPoint(Position, viewport);
            if (nameInCanvas != null)
            {
                Canvas.SetTop(nameInCanvas, point2D.Y - 20);
                Canvas.SetLeft(nameInCanvas, point2D.X - 20);
            }
            if (valueInCanvas != null && !valueInCanvas.Text.Equals(""))
            {
                Canvas.SetTop(valueInCanvas, point2D.Y);
                Canvas.SetLeft(valueInCanvas, point2D.X);
            }
        }

        #endregion
    }
}
