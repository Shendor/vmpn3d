using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Resources;

namespace PNCreator.ManagerClasses
{
    public class SelectionRectangle
    {
        private Polygon rectangle;
        private RectangleGeometry bound;
        private Point startPoint;

        public SelectionRectangle(Point startPoint)
        {
            this.startPoint = startPoint;
            bound = new RectangleGeometry();
            rectangle = new Polygon();
            rectangle.Fill = PNColors.SelectionBox;
            rectangle.Stroke = Brushes.Black;
            rectangle.SnapsToDevicePixels = true;
            rectangle.StrokeThickness = 1;
        }

        public void Draw(Point endPoint)
        {
            bound.Rect = new Rect(startPoint, endPoint);

            rectangle.Points.Clear();
            rectangle.Points.Add(startPoint);
            rectangle.Points.Add(new Point(endPoint.X, startPoint.Y));
            rectangle.Points.Add(endPoint);
            rectangle.Points.Add(new Point(startPoint.X, endPoint.Y));
        }

        public RectangleGeometry Bound
        {
            get { return bound; }
        }

        public Polygon Rectangle
        {
            get { return rectangle; }
        }

        public bool IsAbleToSelect
        {
            get { return CheckSelectionArea(); }
        }

        private bool CheckSelectionArea()
        {
            return rectangle.Points.Count > 0 && Math.Abs(rectangle.Points[0].X - rectangle.Points[1].X) > 50;
        }
    }
}
