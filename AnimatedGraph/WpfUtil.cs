/*
The MIT License

Copyright (c) 2008 Kevin Moore (http://j832.com)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using J832.Common;

namespace AnimatedGraph.WPFUtils
{
    public static class WpfUtil
    {
        public static Color HsbToRgb(float hue, float saturation, float brightness)
        {
            Util.RequireArgumentRange(hue >= 0f && hue <= 1f, "hue");
            Util.RequireArgumentRange(saturation >= 0f && saturation <= 1f, "saturation");
            Util.RequireArgumentRange(brightness >= 0f && brightness <= 1f, "brightness");

            float red = 0, green = 0, blue = 0;

            if (saturation == 0f)
            {
                red = green = blue = brightness;
            }
            else
            {
                float num = (float)((hue - Math.Floor((double)hue)) * 6.0);
                int num2 = (int)num;
                float num3 = num - num2;
                float num4 = brightness * (1f - saturation);
                float num5 = brightness * (1f - (saturation * num3));
                float num6 = brightness * (1f - (saturation * (1f - num3)));
                switch ((num2 % 6))
                {
                    case 0:
                        red = brightness;
                        green = num6;
                        blue = num4;
                        break;

                    case 1:
                        red = num5;
                        green = brightness;
                        blue = num4;
                        break;

                    case 2:
                        red = num4;
                        green = brightness;
                        blue = num6;
                        break;

                    case 3:
                        red = num4;
                        green = num5;
                        blue = brightness;
                        break;

                    case 4:
                        red = num6;
                        green = num4;
                        blue = brightness;
                        break;

                    case 5:
                        red = brightness;
                        green = num4;
                        blue = num5;
                        break;
                }

            }

            red = Math.Min(1f, Math.Max(0f, red));
            green = Math.Min(1f, Math.Max(0f, green));
            blue = Math.Min(1f, Math.Max(0f, blue));

            return Color.FromScRgb(1, red, green, blue);
        }

        public static bool Animate(
            double currentValue, double currentVelocity, double targetValue,
            double attractionFator, double dampening,
            double terminalVelocity, double minValueDelta, double minVelocityDelta,
            out double newValue, out double newVelocity)
        {
            Debug.Assert(currentValue.IsRational());
            Debug.Assert(currentVelocity.IsRational());
            Debug.Assert(targetValue.IsRational());

            Debug.Assert(dampening.IsRational());
            Debug.Assert(dampening > 0 && dampening < 1);

            Debug.Assert(attractionFator.IsRational());
            Debug.Assert(attractionFator > 0);

            Debug.Assert(terminalVelocity > 0);

            Debug.Assert(minValueDelta > 0);
            Debug.Assert(minVelocityDelta > 0);

            double diff = targetValue - currentValue;

            if (diff.Abs() > minValueDelta || currentVelocity.Abs() > minVelocityDelta)
            {
                newVelocity = currentVelocity * (1 - dampening);
                newVelocity += diff * attractionFator;
                newVelocity *= (currentVelocity.Abs() > terminalVelocity) ? terminalVelocity / currentVelocity.Abs() : 1;

                newValue = currentValue + newVelocity;

                return true;
            }
            else
            {
                newValue = targetValue;
                newVelocity = 0;
                return false;
            }
        }

        public static bool Animate(
            Point currentValue, Vector currentVelocity, Point targetValue,
            double attractionFator, double dampening,
            double terminalVelocity, double minValueDelta, double minVelocityDelta,
            out Point newValue, out Vector newVelocity)
        {
            Debug.Assert(currentValue.IsRational());
            Debug.Assert(currentVelocity.IsRational());
            Debug.Assert(targetValue.IsRational());

            Debug.Assert(dampening.IsRational());
            Debug.Assert(dampening > 0 && dampening < 1);

            Debug.Assert(attractionFator.IsRational());
            Debug.Assert(attractionFator > 0);

            Debug.Assert(terminalVelocity > 0);

            Debug.Assert(minValueDelta > 0);
            Debug.Assert(minVelocityDelta > 0);

            Vector diff = targetValue - currentValue;

            if (diff.Length > minValueDelta || currentVelocity.Length > minVelocityDelta)
            {
                newVelocity = currentVelocity * (1 - dampening);
                newVelocity += diff * attractionFator;
                newVelocity *= (currentVelocity.Length > terminalVelocity) ? terminalVelocity / currentVelocity.Length : 1;

                newValue = currentValue + newVelocity;

                return true;
            }
            else
            {
                newValue = targetValue;
                newVelocity = new Vector();
                return false;
            }
        }

        public static bool Animate(
            Point3D currentValue, Vector3D currentVelocity, Point3D targetValue,
            double attractionFator, double dampening,
            double terminalVelocity, double minValueDelta, double minVelocityDelta,
            out Point3D newValue, out Vector3D newVelocity)
        {
            Debug.Assert(currentValue.IsRational());
            Debug.Assert(currentVelocity.IsRational());
            Debug.Assert(targetValue.IsRational());

            Debug.Assert(dampening.IsRational());
            Debug.Assert(dampening > 0 && dampening < 1);

            Debug.Assert(attractionFator.IsRational());
            Debug.Assert(attractionFator > 0);

            Debug.Assert(terminalVelocity > 0);

            Debug.Assert(minValueDelta > 0);
            Debug.Assert(minVelocityDelta > 0);

            Vector3D diff = targetValue - currentValue;

            if (diff.Length > minValueDelta || currentVelocity.Length > minVelocityDelta)
            {
                newVelocity = currentVelocity * (1 - dampening);
                newVelocity += diff * attractionFator;
                newVelocity *= (currentVelocity.Length > terminalVelocity) ? terminalVelocity / currentVelocity.Length : 1;

                newValue = currentValue + newVelocity;

                return true;
            }
            else
            {
                newValue = targetValue;
                newVelocity = new Vector3D();
                return false;
            }
        }

        public static bool Animate(
            Point currentValue, Vector currentVelocity, Vector force,
            double dampening, double terminalVelocity, double minValueDelta, double minVelocityDelta,
            out Point newValue, out Vector newVelocity)
        {
            Debug.Assert(currentValue.IsRational());
            Debug.Assert(currentVelocity.IsRational());

            Debug.Assert(dampening.IsRational());
            Debug.Assert(dampening > 0 && dampening < 1);

            Debug.Assert(terminalVelocity > 0);

            Debug.Assert(minValueDelta > 0);
            Debug.Assert(minVelocityDelta > 0);

            if (force.Length > minValueDelta || currentVelocity.Length > minVelocityDelta)
            {
                newVelocity = currentVelocity * (1 - dampening);
                newVelocity += force;
                newVelocity *= (currentVelocity.Length > terminalVelocity) ? terminalVelocity / currentVelocity.Length : 1;

                newValue = currentValue + newVelocity;

                return true;
            }
            else
            {
                newValue = currentValue;
                newVelocity = new Vector();
                return false;
            }

        }

        public static bool IsRational(this double value)
        {
            return !double.IsInfinity(value) && !double.IsNaN(value);
        }

        public static bool IsRational(this Point value)
        {
            return value.X.IsRational() && value.Y.IsRational();
        }

        public static bool IsRational(this Vector value)
        {
            return value.X.IsRational() && value.Y.IsRational();
        }

        public static bool IsRational(this Point3D value)
        {
            return value.X.IsRational() && value.Y.IsRational() && value.Z.IsRational();
        }

        public static bool IsRational(this Vector3D value)
        {
            return value.X.IsRational() && value.Y.IsRational() && value.Z.IsRational();
        }

        public static void SetToVector(this TranslateTransform3D translateTransform3D, Vector3D vector3D)
        {
            translateTransform3D.OffsetX = vector3D.X;
            translateTransform3D.OffsetY = vector3D.Y;
            translateTransform3D.OffsetZ = vector3D.Z;
        }

        public static void SetToVector(this TranslateTransform translateTransform, Vector vector)
        {
            translateTransform.X = vector.X;
            translateTransform.Y = vector.Y;
        }

        public static Vector ToVector(this Transform transform)
        {
            return new Vector(transform.Value.OffsetX, transform.Value.OffsetY);
        }

        public static Point ToPoint(this Transform transform)
        {
            return new Point(transform.Value.OffsetX, transform.Value.OffsetY);
        }

        public static Point ToPoint(this Vector vector)
        {
            return (Point)vector;
        }

        public static Vector CenterVector(this Size size)
        {
            return ((Vector)size) * .5;
        }

        public static double AngleRad(Point point1, Point point2, Point point3)
        {
            Debug.Assert(point1.IsRational());
            Debug.Assert(point2.IsRational());
            Debug.Assert(point3.IsRational());

            double rad = AngleRad(point2 - point1, point2 - point3);

            double rad2 = AngleRad(point2 - point1, (point2 - point3).RightAngle());

            if (rad2 < (Math.PI / 2))
            {
                return rad;
            }
            else
            {
                return (Math.PI * 2) - rad;
            }
        }

        public static Vector RightAngle(this Vector vector)
        {
            return new Vector(-vector.Y, vector.X);
        }

        public static double Dot(Vector v1, Vector v2)
        {
            Debug.Assert(v1.IsRational());
            Debug.Assert(v2.IsRational());

            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static double AngleRad(Vector v1, Vector v2)
        {
            Debug.Assert(v1.IsRational());
            Debug.Assert(v2.IsRational());

            double dot = Dot(v1, v2);
            double dotNormalize = dot / (v1.Length * v2.Length);
            double acos = Math.Acos(dotNormalize);

            return acos;
        }

        /// <param name="angleRadians">The angle, in radians, from 3-o'clock going counter-clockwise.</param>
        public static void DrawLine(this DrawingContext drawingContext, Pen pen, Point startPoint, double angleRadians, double length)
        {
            Util.RequireNotNull(drawingContext, "drawingContext");
            Util.RequireArgument(startPoint.IsRational(), "startPoint");
            Util.RequireNotNull(pen, "Pen");

            drawingContext.DrawLine(pen, startPoint, startPoint + GetVectorFromAngle(angleRadians, length));
        }

        public static Vector GetVectorFromAngle(double angleRadians, double length)
        {
            Util.RequireArgument(angleRadians.IsRational(), "angleRadians");
            Util.RequireArgument(length.IsRational(), "length");

            double x = Math.Cos(angleRadians) * length;
            double y = -Math.Sin(angleRadians) * length;

            return new Vector(x, y);
        }

        public static readonly Size SizeInfinite = new Size(double.PositiveInfinity, double.PositiveInfinity);
        public static readonly Size SizeZero = new Size();

        #region Implementation

        private static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        #endregion

    }

    public struct Line
    {
        public Line(Point p1, Point p2)
        {
            Util.RequireArgument(p1.IsRational(), "p1");
            Util.RequireArgument(p2.IsRational(), "p2");

            m_p1 = p1;
            m_p2 = p2;
        }

        public Point P1
        {
            get { return m_p1; }
            set
            {
                Util.RequireArgument(value.IsRational(), "value");
                m_p1 = value;
            }
        }

        public Point P2
        {
            get { return m_p2; }
            set
            {
                Util.RequireArgument(value.IsRational(), "value");
                m_p2 = value;
            }
        }

        public Vector Vector { get { return m_p2 - m_p1; } }

        public double Length { get { return (m_p1 - m_p2).Length; } }

        public double Dot(Line other)
        {
            return Dot(this, other);
        }

        public static double Dot(Line l1, Line l2)
        {
            return WpfUtil.Dot(l1.Vector, l2.Vector);
        }

        private Point m_p1, m_p2;
    }

} //*** namespace J832.Wpf