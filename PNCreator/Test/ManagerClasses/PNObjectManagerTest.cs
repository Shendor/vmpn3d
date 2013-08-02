using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNCreator.PNObjectsIerarchy;
using System.Windows.Media.Media3D;
using Meshes3D;
using Moq;

namespace Test.ManagerClasses
{
    [TestClass]
    public class PNObjectManagerTest
    {
        [TestMethod]
        public void CoverObjectsWithMembranesTest()
        {
            var mock = new Mock<GeometryModel3D>();

            Membrane membrane = new Membrane(new Point3D());
            membrane.Geometry = new GeometryModel3D(MeshFactory.GetMesh(typeof(EllipsoidGeometry)), null);
            membrane.Size = 5;

            Membrane membrane2 = new Membrane(new Point3D());
            membrane2.Geometry = new GeometryModel3D(MeshFactory.GetMesh(typeof(EllipsoidGeometry)), null);

            

            Assert.IsTrue(membrane.IsIntersectWith(membrane2));
        }
    }
}
