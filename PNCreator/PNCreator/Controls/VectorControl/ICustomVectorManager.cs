using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using PNCreator.Controls.CarcassControl;

namespace PNCreator.Controls.VectorControl
{
    public interface ICustomVectorManager
    {
        void RemoveAllVectors(VectorType vectorType);
        void RemoveVector(CustomVector vector);
        void RemoveVector(int index);
        void AddVector(VectorType vectorType, string name, Point3D startPoint, Point3D endPoint);
        void AddVector(VectorType vectorType, string name, Color color, Point3D startPoint, Point3D endPoint);
    }
}
