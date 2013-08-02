using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using Meshes3D;
using System.Windows;

namespace PNCreator.PNObjectsIerarchy
{
    public class Token
    {
        private Point3D startPoint;
        private Point3D middlePoint;
        private Point3D endPoint;
        private double seconds;

        private TranslateTransform3D move;
        private MaterialGroup material;
        private ModelVisual3D model;

        public Token(Point3D startPoint, Point3D middlePoint, Point3D endPoint)
        {
            this.startPoint = startPoint;
            this.middlePoint = middlePoint;
            this.endPoint = endPoint;

            seconds = 2.15;

            move = new TranslateTransform3D((Vector3D)startPoint);

            material = new MaterialGroup();
            material.Children.Add(new DiffuseMaterial(Brushes.Orange));

            EllipsoidGeometry tokenSphere = new EllipsoidGeometry();
            tokenSphere.XLength = 0.16;
            tokenSphere.YLength = 0.16;
            tokenSphere.ZLength = 0.16;
            tokenSphere.PhiDiv = 5;
            tokenSphere.ThetaDiv = 5;
            
            GeometryModel3D geometry = new GeometryModel3D();
            geometry.Material = material;
            geometry.Transform = move;
            geometry.Geometry = tokenSphere.Mesh3D;

            model = new ModelVisual3D();
            model.Content = geometry;
        }
        //===================================================================================
        /// <summary>
        /// Get or set start point animation
        /// </summary>
        public Point3D StartPoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }
        //===================================================================================
        /// <summary>
        /// Get or set middle point animation
        /// </summary>
        public Point3D MiddlePoint
        {
            get { return startPoint; }
            set { startPoint = value; }
        }
        //===================================================================================
        /// <summary>
        /// Get or set end point animation
        /// </summary>
        public Point3D EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
        //===================================================================================
        /// <summary>
        /// Get or set object model
        /// </summary>
        public ModelVisual3D Model
        {
            get { return model; }
            set { model = value; }
        }
        //===================================================================================
        /// <summary>
        /// Get or set how much time long animation (seconds)
        /// </summary>
        public double Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }
        //===================================================================================
        /// <summary>
        /// Launch animation
        /// </summary>
        public void StartAnimation()
        {
            // set 1 animation properties
            
           /* DoubleAnimation animationX = new DoubleAnimation(endPoint.X, new Duration(TimeSpan.FromSeconds(seconds)));
            DoubleAnimation animationY = new DoubleAnimation(endPoint.Y, new Duration(TimeSpan.FromSeconds(seconds)));
            DoubleAnimation animationZ = new DoubleAnimation(endPoint.Z, new Duration(TimeSpan.FromSeconds(seconds)));
            
            Storyboard.SetTarget(animationX, move);
            Storyboard.SetTarget(animationY, move);
            Storyboard.SetTarget(animationZ, move);

            Storyboard.SetTargetProperty(animationX, new System.Windows.PropertyPath(TranslateTransform3D.OffsetXProperty));
            Storyboard.SetTargetProperty(animationY, new System.Windows.PropertyPath(TranslateTransform3D.OffsetYProperty));
            Storyboard.SetTargetProperty(animationZ, new System.Windows.PropertyPath(TranslateTransform3D.OffsetZProperty));

            Storyboard storyboard = new Storyboard();

            storyboard.Children.Add(animationX);
            storyboard.Children.Add(animationY);
            storyboard.Children.Add(animationZ);

            storyboard.Begin();*/

            // set 2 animation properties
            DoubleAnimation animationX2 = new DoubleAnimation();
            DoubleAnimation animationY2 = new DoubleAnimation();
            DoubleAnimation animationZ2 = new DoubleAnimation();

            animationX2.DecelerationRatio = 0.2;
            animationX2.Duration = TimeSpan.FromSeconds(seconds);
            animationX2.AutoReverse = false;

            animationY2.DecelerationRatio = 0.2;
            animationY2.Duration = TimeSpan.FromSeconds(seconds);
            animationY2.AutoReverse = false;

            animationZ2.DecelerationRatio = 0.2;
            animationZ2.Duration = TimeSpan.FromSeconds(seconds);
            animationZ2.AutoReverse = false;

            // start animation

            animationX2.From = middlePoint.X;
            animationX2.To = endPoint.X;

            animationY2.From = middlePoint.Y;
            animationY2.To = endPoint.Y;

            animationZ2.From = middlePoint.Z;
            animationZ2.To = endPoint.Z;

            move.BeginAnimation(TranslateTransform3D.OffsetXProperty, animationX2);
            move.BeginAnimation(TranslateTransform3D.OffsetYProperty, animationY2);
            move.BeginAnimation(TranslateTransform3D.OffsetZProperty, animationZ2);
        }
    }
}
