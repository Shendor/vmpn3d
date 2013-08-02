using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RuntimeCompiler.FormulaCompiler
{
    public class CompilerMethods
    {
        public CompilerMethods()
        {
        }

        #region Math functions

        protected double Abs(double value)
        {
            return Math.Abs(value);
        }

        protected double Acos(double value)
        {
            return Math.Acos(value);
        }

        protected double Asin(double value)
        {
            return Math.Asin(value);
        }

        protected double Atan(double value)
        {
            return Math.Atan(value);
        }

        protected double Atan2(double value, double value2)
        {
            return Math.Atan2(value,value2);
        }

        protected double Ceiling(double value)
        {
            return Math.Ceiling(value);
        }

        protected double Cos(double value)
        {
            return Math.Cos(value);
        }

        protected double Cosh(double value)
        {
            return Math.Cosh(value);
        }

        protected double Exp(double value)
        {
            return Math.Exp(value);
        }

        protected double Floor(double value)
        {
            return Math.Floor(value);
        }

        protected double IEEERemainder(double value, double value2)
        {
            return Math.IEEERemainder(value,value2);
        }

        protected double Log(double value)
        {
            return Math.Log(value);
        }

        protected double Log(double value, double value2)
        {
            return Math.Log(value, value2);
        }

        protected double Log10(double value)
        {
            return Math.Log10(value);
        }

        protected double Pow(double value, double power)
        {
            return Math.Pow(value, power);
        }

        protected double Round(double value)
        {
            return Math.Round(value);
        }

        protected double Round(double value, int digits)
        {
            return Math.Round(value, digits);
        }

        protected double Sign(double value)
        {
            return Math.Sign(value);
        }

        protected double Sin(double value)
        {
            return Math.Sin(value);
        }

        protected double Sinh(double value)
        {
            return Math.Sinh(value);
        }

        protected double Sqrt(double value)
        {
            return Math.Sqrt(value);
        }

        protected double Tan(double value)
        {
            return Math.Tan(value);
        }

        protected double Tanh(double value)
        {
            return Math.Tanh(value);
        }

        protected double Truncate(double value)
        {
            return Math.Truncate(value);
        }

        protected double PI()
        {
            return Math.PI;
        }

        protected double E()
        {
            return Math.E;
        }
        #endregion

        #region My functions
        protected double Max(params double[] vals)
        {
            bool flag = true;

            double i1 = vals[0];
            int i = 1;
            while (flag)
            {
                flag = i1 >= vals[i];
                if (!flag)
                    i1 = vals[i];
                i = checked(i + 1);
                flag = i < vals.Length;
            }
            return i1;
        }

        protected double Min(params double[] vals)
        {
            bool flag = true;

            double d1 = vals[0];
            int i = 1;
            while (flag)
            {
                flag = d1 <= vals[i];
                if (!flag)
                    d1 = vals[i];
                i = checked(i + 1);
                flag = i < vals.Length;
            }
            return d1;
        }

        #endregion

        #region Main mathods

        public virtual double ExecuteFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)
        {
            return 0.0;
        }
        public virtual bool ExecuteBooleanFormula(IDictionary<long, double> doubleValues, IDictionary<long, bool> boolValues)
        {
            return false;
        }

        #endregion
    }
}
