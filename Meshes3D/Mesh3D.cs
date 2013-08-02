using System;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace Meshes3D
{
    public class Mesh3D : ModelVisual3D, IMesh
    {
        //protected GeometryModel3D geometry;
        //protected MeshGeometry3D mesh;

        protected TranslateTransform3D position;
        protected ScaleTransform3D scale;
        protected AxisAngleRotation3D rotationX;
        protected AxisAngleRotation3D rotationY;
        protected AxisAngleRotation3D rotationZ;
        protected RotateTransform3D rotateX;
        protected RotateTransform3D rotateY;
        protected RotateTransform3D rotateZ;

        protected Color materialColor;

        public Mesh3D(string name, GeometryModel3D geometry, Point3D position = new Point3D())
        {
            Name = name;

            this.Content = geometry;

            InitializeTransformations();

            Position = position;
        }

        public Mesh3D()
        {
            Name = "";

            GeometryModel3D geometry = new GeometryModel3D();

            this.Content = geometry;
            Mesh = new MeshGeometry3D();
            geometry.Geometry = Mesh;

            InitializeTransformations();
        }

        private void InitializeTransformations()
        {
            Transform3DGroup transformGroup = new Transform3DGroup();

            position = new TranslateTransform3D();
            scale = new ScaleTransform3D();

            rotationX = new AxisAngleRotation3D(new Vector3D(0, 1, 0), 0);
            rotationY = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);
            rotationZ = new AxisAngleRotation3D(new Vector3D(0, 0, 1), 0);
            rotateX = new RotateTransform3D(rotationX);
            rotateY = new RotateTransform3D(rotationY);
            rotateZ = new RotateTransform3D(rotationZ);

            rotateX.CenterX = rotateY.CenterX = rotateZ.CenterX = position.OffsetX;
            rotateX.CenterY = rotateY.CenterY = rotateZ.CenterY = position.OffsetY;
            rotateX.CenterZ = rotateY.CenterZ = rotateZ.CenterZ = position.OffsetZ;

            transformGroup.Children.Add(position);
            transformGroup.Children.Add(scale);
            transformGroup.Children.Add(rotateX);
            transformGroup.Children.Add(rotateY);
            transformGroup.Children.Add(rotateZ);

            this.Geometry.Transform = transformGroup;
        }

        /// <summary>
        /// Check wether this object contains another object inside
        /// </summary>
        /// <param name="mesh">Mesh</param>
        /// <returns></returns>
        public bool IsIntersectWith(Mesh3D mesh)
        {
            return this.Geometry.Bounds.Contains(mesh.Geometry.Bounds);
        }

        public Rect3D Bounds
        {
            get
            {
                return (this.Geometry != null) ? this.Geometry.Bounds : new Rect3D();
            }
        }

        public Color MaterialColor
        {
            get
            {
                return materialColor;
            }
            set
            {
                SetMaterial(value);
            }
        }

        public Material Material
        {
            get
            {
                return ((GeometryModel3D)this.Content).Material;
            }
            set
            {
                ((GeometryModel3D)this.Content).Material = value;
            }
        }

        public MeshGeometry3D Mesh
        {
            get
            {
                return ((GeometryModel3D)this.Content).Geometry as MeshGeometry3D;
            }
            set
            {
                ((GeometryModel3D)this.Content).Geometry = value;
            }
        }

        public String Name
        {
            get;
            set;
        }

        public virtual Point3D Position
        {
            get
            {
                return new Point3D(position.OffsetX, position.OffsetY, position.OffsetZ);
            }
            set
            {
                SetPosition(value.X, value.Y, value.Z);
            }
        }

        public TranslateTransform3D Translation
        {
            get { return position; }
        }

        public double AngleX
        {
            get
            {
                return rotationX.Angle;
            }
            set
            {
                rotationX.Angle = value;
            }
        }
        public double AngleY
        {
            get
            {
                return rotationY.Angle;
            }
            set
            {
                rotationY.Angle = value;
            }
        }
        public double AngleZ
        {
            get
            {
                return rotationZ.Angle;
            }
            set
            {
                rotationZ.Angle = value;
            }
        }


        //===================================================================================
        /// <summary>
        /// Get rotate transformation
        /// </summary>
        public AxisAngleRotation3D RotateX
        {
            get
            {
                return rotationX;
            }
            set
            {
                rotationX = value;
            }
        }
        //===================================================================================
        /// <summary>
        /// Get rotate transformation
        /// </summary>
        public AxisAngleRotation3D RotateY
        {
            get
            {
                return rotationY;
            }
            set
            {
                rotationY = value;
            }
        }
        //===================================================================================
        /// <summary>
        /// Get rotate transformation
        /// </summary>
        public AxisAngleRotation3D RotateZ
        {
            get
            {
                return rotationZ;
            }
            set
            {
                rotationZ = value;
            }
        }
        public double Size
        {
            get
            {
                return scale.ScaleX;
            }
            set
            {
                scale.ScaleX = scale.ScaleY = scale.ScaleZ = value;
            }
        }

        private void SetPosition(double x, double y, double z)
        {
            position.OffsetX = x;
            position.OffsetY = y;
            position.OffsetZ = z;

            scale.CenterX = x;
            scale.CenterY = y;
            scale.CenterZ = z;

            rotateX.CenterX = rotateY.CenterX = rotateZ.CenterX = x;
            rotateX.CenterY = rotateY.CenterY = rotateZ.CenterY = y;
            rotateX.CenterZ = rotateY.CenterZ = rotateZ.CenterZ = z;
        }

        public GeometryModel3D Geometry
        {
            get
            {
                return this.Content as GeometryModel3D;
            }
            set
            {
                this.Content = value;
            }
        }

        public virtual void ResetMaterial()
        {
            Geometry.Material = Material;
        }


        protected virtual void SetMaterial(Color color)
        {
            materialColor = color;
        }
    }
}
