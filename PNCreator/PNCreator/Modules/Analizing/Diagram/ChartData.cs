using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.Modules.Analizing
{
    class ChartData
    {
        private double _xValue;
        private double _yValue;

        public double XValue
        {
            get
            {
                return this._xValue;
            }
            set
            {
                this._xValue = value;
            }
        }

        public double YValue
        {
            get
            {
                return this._yValue;
            }
            set
            {
                this._yValue = value;
            }
        }

        public ChartData(int xValue, double yValue)
        {
            this.XValue = xValue;
            this.YValue = yValue;
        }
    }
}
