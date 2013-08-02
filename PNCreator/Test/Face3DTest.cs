using _3DTools.primitives3d;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Media.Media3D;

namespace Test
{
    
    
    /// <summary>
    ///This is a test class for Face3DTest and is intended
    ///to contain all Face3DTest Unit Tests
    ///</summary>
    [TestClass()]
    public class Face3DTest
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
        ///A test for Face3D Constructor
        ///</summary>
        [TestMethod()]
        public void Face3DConstructorTest()
        {
            Point3D point1 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point2 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point3 = new Point3D(); // TODO: Initialize to an appropriate value
            Face3D target = new Face3D(point1, point2, point3);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for CrossProduct
        ///</summary>
        [TestMethod()]
        [DeploymentItem("3DTools.dll")]
        public void CrossProductTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            Face3D_Accessor target = new Face3D_Accessor(param0); // TODO: Initialize to an appropriate value
            Vector3D v1 = new Vector3D(); // TODO: Initialize to an appropriate value
            Vector3D v2 = new Vector3D(); // TODO: Initialize to an appropriate value
            Vector3D expected = new Vector3D(); // TODO: Initialize to an appropriate value
            Vector3D actual;
            actual = target.CrossProduct(v1, v2);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetNormal
        ///</summary>
        [TestMethod()]
        public void GetNormalTest()
        {
            Point3D point1 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point2 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point3 = new Point3D(); // TODO: Initialize to an appropriate value
            Face3D target = new Face3D(point1, point2, point3); // TODO: Initialize to an appropriate value
            Vector3D expected = new Vector3D(); // TODO: Initialize to an appropriate value
            Vector3D actual;
            actual = target.GetNormal();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for HasPoint
        ///</summary>
        [TestMethod()]
        public void HasPointTest()
        {
            Point3D point1 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point2 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point3 = new Point3D(); // TODO: Initialize to an appropriate value
            Face3D target = new Face3D(point1, point2, point3); // TODO: Initialize to an appropriate value
            Point3D point = new Point3D(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.HasPoint(point);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Point1
        ///</summary>
        [TestMethod()]
        public void Point1Test()
        {
            Point3D point1 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point2 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point3 = new Point3D(); // TODO: Initialize to an appropriate value
            Face3D target = new Face3D(point1, point2, point3); // TODO: Initialize to an appropriate value
            Point3D expected = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D actual;
            target.Point1 = expected;
            actual = target.Point1;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Point2
        ///</summary>
        [TestMethod()]
        public void Point2Test()
        {
            Point3D point1 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point2 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point3 = new Point3D(); // TODO: Initialize to an appropriate value
            Face3D target = new Face3D(point1, point2, point3); // TODO: Initialize to an appropriate value
            Point3D expected = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D actual;
            target.Point2 = expected;
            actual = target.Point2;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Point3
        ///</summary>
        [TestMethod()]
        public void Point3Test()
        {
            Point3D point1 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point2 = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D point3 = new Point3D(); // TODO: Initialize to an appropriate value
            Face3D target = new Face3D(point1, point2, point3); // TODO: Initialize to an appropriate value
            Point3D expected = new Point3D(); // TODO: Initialize to an appropriate value
            Point3D actual;
            target.Point3 = expected;
            actual = target.Point3;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
