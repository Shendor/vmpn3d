using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.Controls.Positioniser;
using System.Windows.Controls;

namespace PNCreator.Modules.Positioniser
{
    public interface IPositioniserProperty
    {
        void SetParameters(Grid3DParameters parameters);
    }
}
