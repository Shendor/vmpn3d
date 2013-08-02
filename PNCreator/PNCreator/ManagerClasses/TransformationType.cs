using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetriNets.HelperClasses
{
    /// <summary>
    /// Types of all available object transformation
    /// </summary>
    public enum TransformationType
    {
        MoveXPlus,
        MoveXMinus,
        MoveYPlus,
        MoveYMinus,
        MoveZPlus,
        MoveZMinus,
        ScalePlus,
        ScaleMinus,
        RotateX,
        RotateY,
        RotateZ
    }
}
