using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses.EventManager.Events
{
    public class FormulaProgressEventArgs : ProgressEventArgs
    {
        private int p;

        public FormulaProgressEventArgs(int progress, int maxValue)
            : base(progress)
        {
            MaximumProgress = maxValue;
        }
    }
}
