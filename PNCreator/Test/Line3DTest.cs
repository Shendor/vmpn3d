using _3DTools.primitives3d;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media.Media3D;

namespace Test
{


    /// <summary>
    ///This is a test class for Line3DTest and is intended
    ///to contain all Line3DTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Line3DTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for ComputePlaneIntersection
        ///</summary>
        [TestMethod()]
        public void ComputePlaneIntersectionTest1()
        {
            Point3D point = new Point3D(6, 6, 0);
            Vector3D direction = new Vector3D(1, 1, 0);
            Line3D target = new Line3D(point, direction);
            Point3D planePoint3D1 = new Point3D(8, 3, 0);
            Point3D planePoint3D2 = new Point3D(1, 3, 0);
            Point3D planePoint3D3 = new Point3D(6, 3, 3);
            Face3D plane = new Face3D(planePoint3D1, planePoint3D2, planePoint3D3);
            Point3D expected = new Point3D(3, 3, 0);
            Point3D actual = target.ComputePlaneIntersection(plane);
            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void ComputePlaneIntersectionTest2()
        {
            Point3D point = new Point3D(-1, 10, -603);
            Vector3D direction = new Vector3D(-190.7, -321.8, -1188.5);
            Line3D line = new Line3D(point, direction);

            Point3D v1 = new Point3D(-1000, 0, -1000);
            Point3D v2 = new Point3D(1000, 0, -1000);
            Point3D v3 = new Point3D(-1000, 0, 1000);

            Point3D expected = new Point3D(-6.717299578059071, 0.0, -620.6461723930079);
            Point3D actual = line.ComputePlaneIntersection(new Face3D(v1, v2, v3));

            Assert.AreEqual(expected.X, actual.X, 0.00001);
            Assert.AreEqual(expected.Y, actual.Y, 0.00001);
            Assert.AreEqual(expected.Z, actual.Z, 0.00001);

        }

        [TestMethod()]
        public void MoveLineAlongLineTest1()
        {
            Point3D point = new Point3D(5, 3, 0);
            Vector3D direction = new Vector3D(-3, -2, 0);
            Line3D line = new Line3D(point, direction);

            Point3D expected = new Point3D(13, 8, 0);
            Point3D actual = line.MovePointAlongLine(2);

            Assert.AreEqual(expected.X, line.Point.X, 0.00001);
            Assert.AreEqual(expected.Y, line.Point.Y, 0.00001);
            Assert.AreEqual(expected.Z, line.Point.Z, 0.00001);

        }
    }
}
