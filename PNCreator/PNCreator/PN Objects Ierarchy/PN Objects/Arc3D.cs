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
using _3DTools;
using RuntimeCompiler.FormulaCompiler;
using PNCreator.Controls;
namespace PNCreator.PNObjectsIerarchy
{
    public class Arc3D : PNObject, IFormula, ITranslationContent2D
    {
        public const double ARC_THICKNESS = 7;

        private string textureName;

        private long startId;
        private long endId;
        private double weight = 1;

        private Matrix3D _visualToScreen;
        private Matrix3D _screenToVisual;

        protected DoubleFormula doubleFormula;

        public Arc3D(Shape3D startObject, Shape3D endObject, PNObjectTypes arcType)
            : base(arcType)
        {
            this.Points = new Point3DCollection();

            CompositionTarget.Rendering += OnRender;

            Point3D startPoint = startObject.Position;
            Point3D endPoint = endObject.Position;

            Points.Add(startPoint);
            Point3D middlePoint = 
                new Point3D((startPoint.X + endPoint.X) / 2, (startPoint.Y + endPoint.Y) / 2, (startPoint.Z + endPoint.Z) / 2);
            Points.Add(middlePoint);
            Points.Add(middlePoint);
            Points.Add(endPoint);
            Thickness = PNCreator.Modules.Properties.PNProperties.ArcsThickness;

            this.startId = startObject.ID;
            this.endId = endObject.ID;

            SetCanvasProperties();

            ValueInCanvas.Text = Weight.ToString();
            TextureName = PNCreator.Modules.Properties.PNProperties.GetTextureByArcType(Type);
            SetTexture(TextureName);
            
            doubleFormula = new DoubleFormula();

        }

        public Arc3D()
            : base()
        {
            this.Points = new Point3DCollection();

            CompositionTarget.Rendering += OnRender;

            Thickness = PNCreator.Modules.Properties.PNProperties.ArcsThickness;

            this.startId = -1;
            this.endId = -1;
            SetCanvasProperties();
            ValueInCanvas.Text = Weight.ToString();
            doubleFormula = new DoubleFormula();
        }

        #region Properties
        
        #region Private
        

        //==================================================================================================
        /// <summary>
        /// Get Start point 
        /// </summary>
        public Point3D StartPoint
        {
            get { return Points[0]; }
        }
        //==================================================================================================
        /// <summary>
        /// Get Middle point 
        /// </summary>
        public Point3D MiddlePoint
        {
            get { return Points[1]; }
        }
        //==================================================================================================
        /// <summary>
        /// Get End point 
        /// </summary>
        public Point3D EndPoint
        {
            get { return Points[Points.Count - 1]; }
        }

        public override Point3D Position
        {
            get
            {
                return Points[1];
            }
            set
            {
                this.Points[1] = this.Points[2] = value;
            }
        }
        //==================================================================================================
        /// <summary>
        /// Start point id
        /// </summary>
        public long StartID
        {
            get { return startId; }
            set { startId = value; }
        }
        //==================================================================================================
        /// <summary>
        /// End point id
        /// </summary>
        public long EndID
        {
            get { return endId; }
            set { endId = value; }
        }
        //==================================================================================================
        /// <summary>
        /// Get or set weight of arc
        /// </summary>
        public double Weight
        {
            get { return weight; }
            set 
            { 
                weight = value;
                //if (valueInCanvas != null)
                //    valueInCanvas.Text = weight.ToString();

                PNCreator.ManagerClasses.PNObjectRepository.PNObjects.SetDoubleValue(ID, weight);
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
                doubleFormula.Expression = value;
            }
        }
        #endregion

        #region Texture property
        //==================================================================================================
        /// <summary>
        /// Set or Get texture name 
        /// </summary>
        public string TextureName
        {
            get { return textureName; }
            set { textureName = value; }
        }
        public void SetTexture(string filename)
        {
            try
            {
                MaterialGroup material = PNCreator.ManagerClasses.PNObjectMaterial.GetTexture(PNCreator.ManagerClasses.PNDocument.ApplicationPath + filename);
                Geometry.Material = material;
                Geometry.BackMaterial = material;
                this.textureName = filename;
            }
            catch { }

        }
        public void SetMaterial(Color color)
        {
            Geometry.Material = PNCreator.ManagerClasses.PNObjectMaterial.GetMaterial(color);
        }
         #endregion

        #region Thickness propoerty
        public static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register(
                "Thickness",
                typeof(double),
                typeof(Arc3D),
                new PropertyMetadata(
                    1.0,
                    OnThicknessChanged));

        private static void OnThicknessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Arc3D)sender).GeometryDirty();
        }

        public double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }
        #endregion

        #region Points property
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register(
                "Points",
                typeof(Point3DCollection),
                typeof(Arc3D),
                new PropertyMetadata(
                    null,
                    OnPointsChanged));

        private static void OnPointsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Arc3D)sender).GeometryDirty();
        }

        public Point3DCollection Points
        {
            get { return (Point3DCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        #endregion

        #endregion

        #region Methods
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
        /// Change start point position
        /// </summary>
        public void ChangeStartPoint(Point3D startPoint)
        {
            this.Points[0] = startPoint;
        }
        //==================================================================================================
        /// <summary>
        /// Change middle point position
        /// </summary>
        public void ChangeMiddlePoint(Point3D middlePoint)
        {
            this.Points[1] = this.Points[2] = middlePoint;
        }
        //==================================================================================================
        /// <summary>
        /// Change end point position
        /// </summary>
        public void ChangeEndPoint(Point3D endPoint)
        {
            this.Points[Points.Count - 1] = endPoint;
        }
        
        #region ITranslationContent2D Members

        public void TranslateContent2D(Viewport3D viewport)
        {
            Point point2D = MathUtils.Convert3DPoint(Position, viewport);

            Canvas.SetTop(valueInCanvas, point2D.Y);
            Canvas.SetLeft(valueInCanvas, point2D.X);

            Canvas.SetTop(nameInCanvas, point2D.Y - 20);
            Canvas.SetLeft(nameInCanvas, point2D.X - 10);
        }

        #endregion

        #region Moving

        public void MoveX(double value)
        {
            ChangeMiddlePoint(new Point3D(MiddlePoint.X + value, MiddlePoint.Y, MiddlePoint.Z));
        }

        public void MoveY(double value)
        {
            ChangeMiddlePoint(new Point3D(MiddlePoint.X, MiddlePoint.Y + value, MiddlePoint.Z));
        }

        public void MoveZ(double value)
        {
            ChangeMiddlePoint(new Point3D(MiddlePoint.X, MiddlePoint.Y, MiddlePoint.Z + value));
        }

        #endregion

        //===================================================================================
        /// <summary>
        /// Launch formula which was built by Formula Builder. If formula is not correct, weight of this arc
        /// will be set by default ( 1 )
        /// </summary>
        public double ExecuteFormula()
        {
            try
            {
                if (doubleFormula.Expression != null)
                {
                    if (Type == PNObjectTypes.DiscreteArc || Type == PNObjectTypes.DiscreteInhibitorArc || Type == PNObjectTypes.DiscreteTestArc)
                        Weight = (int)doubleFormula.ExecuteFormula(PNCreator.ManagerClasses.PNObjectRepository.PNObjects.DoubleValues,
                                                                    PNCreator.ManagerClasses.PNObjectRepository.PNObjects.BooleanValues);
                    else
                        Weight = doubleFormula.ExecuteFormula(PNCreator.ManagerClasses.PNObjectRepository.PNObjects.DoubleValues,
                            PNCreator.ManagerClasses.PNObjectRepository.PNObjects.BooleanValues);
                }
            }
            catch (Exception)
            {
                Weight = 1;
            }
            return Weight;
        }

        #region Mesh and geometry
        private void OnRender(object sender, EventArgs e)
        {
            if (Points.Count == 0 && Mesh.Positions.Count == 0)
            {
                return;
            }

            if (UpdateTransforms())
            {
                RebuildGeometry();
            }
        }

        private void GeometryDirty()
        {
            // Force next call to UpdateTransforms() to return true.
            _visualToScreen = MathUtils.ZeroMatrix;
        }

        private void RebuildGeometry()
        {
            double halfThickness = Thickness / 2.0;
            int numLines = Points.Count / 2; 

            Point3DCollection positions = new Point3DCollection(numLines * 4);

            for (int i = 0; i < numLines; i++)
            {
                int startIndex = i * 2;

                Point3D startPoint = Points[startIndex];
                Point3D endPoint = Points[startIndex + 1];

                AddSegment(positions, startPoint, endPoint, halfThickness);
            }

            positions.Freeze();
            Mesh.Positions = positions;

            Int32Collection indices = new Int32Collection(Points.Count * 3);

            for (int i = 0; i < Points.Count / 2; i++)
            {
                indices.Add(i * 4 + 2);
                indices.Add(i * 4 + 1);
                indices.Add(i * 4 + 0);

                indices.Add(i * 4 + 2);
                indices.Add(i * 4 + 3);
                indices.Add(i * 4 + 1);
            }

            indices.Freeze();
            Mesh.TriangleIndices = indices;

            PointCollection textureCoordinates = new PointCollection(); //0,1, 0,0, 1,1, 1,0, 1,0
            textureCoordinates.Add(new Point(0, 1));
            textureCoordinates.Add(new Point(0, 0));
            textureCoordinates.Add(new Point(1, 1));
            textureCoordinates.Add(new Point(1, 0));
            //textureCoordinates.Add(new Point(1, 0));

            textureCoordinates.Add(new Point(0, 1));
            textureCoordinates.Add(new Point(0, 0));
            textureCoordinates.Add(new Point(1, 1));
            textureCoordinates.Add(new Point(1, 0));
           // textureCoordinates.Add(new Point(1, 0));


            Mesh.TextureCoordinates = textureCoordinates;
        }

        private void AddSegment(Point3DCollection positions, Point3D startPoint, Point3D endPoint, double halfThickness)
        {
            // NOTE: We want the vector below to be perpendicular post projection so
            //       we need to compute the line direction in post-projective space.
            Vector3D lineDirection = endPoint * _visualToScreen - startPoint * _visualToScreen;
            lineDirection.Z = 0;
            lineDirection.Normalize();

            // NOTE: Implicit Rot(90) during construction to get a perpendicular vector.
            Vector delta = new Vector(-lineDirection.Y, lineDirection.X);
            delta *= halfThickness;

            Point3D pOut1, pOut2;

            Widen(startPoint, delta, out pOut1, out pOut2);

            positions.Add(pOut1);
            positions.Add(pOut2);

            Widen(endPoint, delta, out pOut1, out pOut2);

            positions.Add(pOut1);
            positions.Add(pOut2);
        }

        private void Widen(Point3D pIn, Vector delta, out Point3D pOut1, out Point3D pOut2)
        {
            Point4D pIn4 = (Point4D)pIn;
            Point4D pOut41 = pIn4 * _visualToScreen;
            Point4D pOut42 = pOut41;

            pOut41.X += delta.X * pOut41.W;
            pOut41.Y += delta.Y * pOut41.W;

            pOut42.X -= delta.X * pOut42.W;
            pOut42.Y -= delta.Y * pOut42.W;

            pOut41 *= _screenToVisual;
            pOut42 *= _screenToVisual;

            // NOTE: Z is not modified above, so we use the original Z below.

            pOut1 = new Point3D(
                pOut41.X / pOut41.W,
                pOut41.Y / pOut41.W,
                pOut41.Z / pOut41.W);

            pOut2 = new Point3D(
                pOut42.X / pOut42.W,
                pOut42.Y / pOut42.W,
                pOut42.Z / pOut42.W);
        }

        private bool UpdateTransforms()
        {
            Viewport3DVisual viewport;
            bool success;

            Matrix3D visualToScreen = MathUtils.TryTransformTo2DAncestor(this, out viewport, out success);

            if (!success || !visualToScreen.HasInverse)
            {
                Mesh.Positions = null;
                return false;
            }

            if (visualToScreen == _visualToScreen)
            {
                return false;
            }

            _visualToScreen = _screenToVisual = visualToScreen;
            _screenToVisual.Invert();

            return true;
        }
        #endregion

        #endregion

    }
}
