using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.Controls.CarcassControl;

namespace PNCreator.Controls.VectorControl
{
    interface IVectorData
    {
        List<VectorAngle> CalculateVectorAngles(CustomVector vector);
        List<VectorProjection> CalculateVectorProjection(CustomVector vector);
        VectorData GetVectorData(CustomVector vector);

    }
}
