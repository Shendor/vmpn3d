using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace PNCreator.Controls.Positioniser
{
    public class Grid3DParameters
    {
        private double length;
        private double width;
        private double height;
        private double xCellSize;
        private double yCellSize;
        private double zCellSize;

        private int rowsQuantity;

        private double xPosition;
        private double yPosition;
        private double zPosition;

        private Color color;
        private double thickness;

        public Grid3DParameters()
        {
            length = 80;
            width = 80;
            height = 80;

            xCellSize = 10;
            yCellSize = 10;
            zCellSize = 10;

            rowsQuantity = 1;

            color = Colors.Gray;
            thickness = 4;

            xPosition = -length/2;
            yPosition = -height/2;
            ZPosition = -650;
        }

        public double Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
                RunParametersChangedMethod();
            }
        }

        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                width = value;
                RunParametersChangedMethod();
            }
        }

        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                height = value;
                RunParametersChangedMethod();
            }
        }

        public double XCellSize
        {
            get
            {
                return xCellSize;
            }
            set
            {
                xCellSize = value;
                RunParametersChangedMethod();
            }
        }

        public double YCellSize
        {
            get
            {
                return yCellSize;
            }
            set
            {
                yCellSize = value;
                RunParametersChangedMethod();
            }
        }

        public double ZCellSize
        {
            get
            {
                return zCellSize;
            }
            set
            {
                zCellSize = value;
                RunParametersChangedMethod();
            }
        }

        public double XPosition
        {
            get
            {
                return xPosition;
            }
            set
            {
                xPosition = value;
                RunParametersChangedMethod();
            }
        }

        public double YPosition
        {
            get
            {
                return yPosition;
            }
            set
            {
                yPosition = value;
                RunParametersChangedMethod();
            }
        }

        public double ZPosition
        {
            get
            {
                return zPosition;
            }
            set
            {
                zPosition = value;
                RunParametersChangedMethod();
            }
        }

        public int RowsQuantity
        {
            get
            {
                return rowsQuantity;
            }
            set
            {
                rowsQuantity = value;
                RunParametersChangedMethod();
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                RunParametersChangedMethod();
            }
        }

        public double Thickness
        {
            get
            {
                return thickness;
            }
            set
            {
                thickness = value;
                RunParametersChangedMethod();
            }
        }

        public ParametersChanged ParametersChangedMethod
        {
            get;
            set;
        }


        private void RunParametersChangedMethod()
        {
            if (ParametersChangedMethod != null)
                ParametersChangedMethod(this);
        }

        public delegate void ParametersChanged(Grid3DParameters newValue);
    }
}
