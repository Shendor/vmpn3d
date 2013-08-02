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

namespace ChartControl
{
    /// <summary>
    /// Логика взаимодействия для Chart.xaml
    /// </summary>
    public partial class Chart : UserControl
    {
        private const double ELLIPSE_HEIGHT = 1.5;
        private const double ELLIPSE_WIDTH = 1.5;

        private double minXWidth;
        private double maxXWidth;
        private double minYHeight;
        private double maxYHeight;

        private Polyline line;

        private List<Ellipse> visualPoints;

        public Chart()
        {
            InitializeComponent();
            ConfigureChart();
            visualPoints = new List<Ellipse>();
        }

        #region Properties

        #region Visual property
        public List<Ellipse> VisualPoints
        {
            get { return visualPoints; }
        }
        #endregion

        #region Axises label properies

        #region AxisXLabelProperty
        public static readonly DependencyProperty AxisXLabelProperty =
           DependencyProperty.Register("AxisXLabelProperty", typeof(string), typeof(Chart), new PropertyMetadata("Axis X", OnAxisXLabelChanged));

        private static void OnAxisXLabelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetAxisXLabel((string)args.NewValue);
        }

        public void SetAxisXLabel(string source)
        {
            axisXLabel.Text = source;
        }
        //=============================================================================
        /// <summary>
        /// Set or get text of axis X label
        /// </summary>
        public string AxisXLabel
        {
            get { return (string)GetValue(AxisXLabelProperty); }
            set { SetValue(AxisXLabelProperty, value); }
        }
        #endregion

        #region AxisYLabelProperty
        public static readonly DependencyProperty AxisYLabelProperty =
           DependencyProperty.Register(
               "AxisYLabelProperty",
               typeof(string),
               typeof(Chart),
               new PropertyMetadata("Axis Y", OnAxisYLabelChanged));

        private static void OnAxisYLabelChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetAxisYLabel((string)args.NewValue);
        }

        public void SetAxisYLabel(string source)
        {
            axisYLabel.Text = source;
        }
        //=============================================================================
        /// <summary>
        /// Set or get text of axis Y label
        /// </summary>
        public string AxisYLabel
        {
            get { return (string)GetValue(AxisYLabelProperty); } 
            set { SetValue(AxisYLabelProperty, value); }
        }
        #endregion

        #region AxisXLabelColorProperty
        public static readonly DependencyProperty AxisXLabelColorProperty =
          DependencyProperty.Register("AxisXLabelColorProperty", typeof(Brush), typeof(Chart), new PropertyMetadata(Brushes.Black, OnAxisXLabelColorChanged));

        private static void OnAxisXLabelColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetAxisXLabelColor((Brush)args.NewValue);
        }

        public void SetAxisXLabelColor(Brush source)
        {
            axisXLabel.Foreground = source;
            x1.Foreground = source;
            x2.Foreground = source;
            x3.Foreground = source;
            x4.Foreground = source;
            x5.Foreground = source;
            x6.Foreground = source;
            x7.Foreground = source;
            x8.Foreground = source;
            x9.Foreground = source;
            x10.Foreground = source;
        }
        //=============================================================================
        /// <summary>
        /// Set or get color of axis X label
        /// </summary>
        public Brush AxisXLabelColor
        {
            get { return (Brush)GetValue(AxisXLabelColorProperty); }
            set { SetValue(AxisXLabelColorProperty, value); }
        }
        #endregion

        #region AxisYLabelColorProperty
        public static readonly DependencyProperty AxisYLabelColorProperty =
          DependencyProperty.Register("AxisYLabelColorProperty", typeof(Brush), typeof(Chart), new PropertyMetadata(Brushes.Black, OnAxisYLabelColorChanged));

        private static void OnAxisYLabelColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetAxisYLabelColor((Brush)args.NewValue);
        }

        public void SetAxisYLabelColor(Brush source)
        {
            axisYLabel.Foreground = source;
            y1.Foreground = source;
            y2.Foreground = source;
            y3.Foreground = source;
            y4.Foreground = source;
            y5.Foreground = source;
            y6.Foreground = source;
            y7.Foreground = source;
            y8.Foreground = source;
            y9.Foreground = source;
            y10.Foreground = source;
        }
        //=============================================================================
        /// <summary>
        /// Set or get color of axis Y label
        /// </summary>
        public Brush AxisYLabelColor
        {
            get { return (Brush)GetValue(AxisYLabelColorProperty); }
            set { SetValue(AxisYLabelColorProperty, value); }
        }
        #endregion

        #endregion

        #region Grid visibility
        private static readonly DependencyProperty GridVisibilityProperty =
            DependencyProperty.Register("GridVisibilityProperty", typeof(bool), typeof(Chart), new PropertyMetadata(true, OnGridVisibilityChanged));
        private static void OnGridVisibilityChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetGridVisibility((bool)args.NewValue);
        }
        public void SetGridVisibility(bool source)
        {
            if (source.Equals(true)) grid.Visibility = System.Windows.Visibility.Visible;
            else grid.Visibility = System.Windows.Visibility.Hidden;
        }
        //=============================================================================
        /// <summary>
        /// Set or get grid visibility behavior
        /// </summary>
        public bool GridVisibility
        {
            get { return (bool)GetValue(GridVisibilityProperty); }
            set { SetValue(GridVisibilityProperty, value); }
        }
        #endregion


        #region Line
        private static readonly DependencyProperty LineThicknessProperty =
            DependencyProperty.Register("LineThicknessProperty", typeof(double), typeof(Chart), new PropertyMetadata((double)0.5, OnLineThicknessChanged));
        private static void OnLineThicknessChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetLineThickness((double)args.NewValue);
        }
        public void SetLineThickness(double source)
        {
            line.StrokeThickness = source;
        }
        //=============================================================================
        /// <summary>
        /// Set or get line thickness
        /// </summary>
        public double LineThickness
        {
            get { return (double)GetValue(LineThicknessProperty); }
            set { SetValue(LineThicknessProperty, value); }
        }

        private static readonly DependencyProperty LineColorProperty =
            DependencyProperty.Register("LineColorProperty", typeof(Brush), typeof(Chart), new PropertyMetadata(Brushes.DodgerBlue, OnLineColorChanged));
        private static void OnLineColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            ((Chart)sender).SetLineColor((Brush)args.NewValue);
        }
        public void SetLineColor(Brush source)
        {
            line.Stroke = source;
        }
        //=============================================================================
        /// <summary>
        /// Set or get line color
        /// </summary>
        public Brush LineColor
        {
            get { return (Brush)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        #endregion

        #endregion

        #region Methods

        private void ConfigureChart()
        {
            line = new Polyline();
            line.Stroke = Brushes.DodgerBlue;
            line.StrokeThickness = 1;

            minXWidth = 0;
            maxXWidth = 100;
            minYHeight = -1;
            maxYHeight = 1;
        }
        //=============================================================================
        /// <summary>
        /// Build new chart clearing previous one
        /// </summary>
        /// <param name="points">Points collection for chart</param>
        public void BuildChart(PointCollection points)
        {
            line.Points.Clear();

            double minX = 0, minY = 0, maxX = 0, maxY = 0;
            
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X < minX) minX = points[i].X;
                else if (points[i].X > maxX) maxX = points[i].X;

                if (points[i].Y < minY) minY = points[i].Y;
                else if (points[i].Y > maxY) maxY = points[i].Y;
            }
            minXWidth = minX - 0.05;
            maxXWidth = maxX + 0.05;

            minYHeight = minY;
            maxYHeight = maxY;

            for (int i = 0; i < points.Count; i++)
            {
                line.Points.Add(CurvePoint(points[i]));
            }
            chartCanvas.Children.Clear();
            chartCanvas.Children.Add(line);

            float stepX = (float)((maxX - minX) / 10);
            
            x1.Text = minX.ToString();
            x2.Text = (minX + stepX).ToString();
            x3.Text = (minX + 2 * stepX).ToString();
            x4.Text = (minX + 3 * stepX).ToString();
            x5.Text = (minX + 4 * stepX).ToString();
            x6.Text = (minX + 5 * stepX).ToString();
            x7.Text = (minX + 6 * stepX).ToString();
            x8.Text = (minX + 7 * stepX).ToString();
            x9.Text = (minX + 8 * stepX).ToString();
            x10.Text = (minX + 9 * stepX).ToString();

            float stepY = (float)((maxY - minY) / 10);

            y1.Text = minY.ToString();
            y2.Text = (minY + stepY).ToString();
            y3.Text = (minY + 2 * stepY).ToString();
            y4.Text = (minY + 3 * stepY).ToString();
            y5.Text = (minY + 4 * stepY).ToString();
            y6.Text = (minY + 5 * stepY).ToString();
            y7.Text = (minY + 6 * stepY).ToString();
            y8.Text = (minY + 7 * stepY).ToString();
            y9.Text = (minY + 8 * stepY).ToString();
            y10.Text = (minY + 9 * stepY).ToString();
        }
        //=============================================================================
        /// <summary>
        /// Transform cartesian coordinates to display
        /// </summary>
        private Point CurvePoint(Point pt)
        {
            Point result = new Point();
            result.X = (pt.X - minXWidth) * chartCanvas.Width / (maxXWidth - minXWidth);
            result.Y = chartCanvas.Height - (pt.Y - minYHeight) * chartCanvas.Height / (maxYHeight - minYHeight);
            return result;
        }

        public void AddPoint(Point point, Brush stroke, Brush fill)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = ELLIPSE_HEIGHT;
            ellipse.Width = ELLIPSE_WIDTH;
            ellipse.Stroke = stroke;
            ellipse.StrokeThickness = 0.2;
            ellipse.Fill = fill;
            chartCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, CurvePoint(point).X - ELLIPSE_WIDTH / 2);
            Canvas.SetTop(ellipse, CurvePoint(point).Y - ELLIPSE_HEIGHT / 2);
            visualPoints.Add(ellipse);
        }

        public void AddPoint(Point point)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = ELLIPSE_HEIGHT;
            ellipse.Width = ELLIPSE_WIDTH;
            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = 0.2;
            ellipse.Fill = Brushes.OrangeRed;
            chartCanvas.Children.Add(ellipse);
            Canvas.SetLeft(ellipse, CurvePoint(point).X - ELLIPSE_WIDTH / 2);
            Canvas.SetTop(ellipse, CurvePoint(point).Y - ELLIPSE_HEIGHT / 2);
        }
        public void ClearPoints()
        {
            for (int i = 0; i < visualPoints.Count; i++)
            {
                chartCanvas.Children.Remove(visualPoints[i]);
            }
        }
        #endregion
    }
}
