using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PNCreator.ManagerClasses;
using PNCreator.PNObjectsIerarchy;

namespace Test.ManagerClasses
{
    /// <summary>
    /// Summary description for PNObjectRepositoryTest
    /// </summary>
    [TestClass]
    public class PNObjectRepositoryTest
    {
        public PNObjectRepositoryTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestGetObjectsByType()
        {
            PNObject o1 = new DiscreteLocation();
            PNObject o2 = new ContinuousTransition();
            PNObject o3 = new Membrane();
            PNObject o4 = new Arc3D();

            PNObjectRepository.PNObjects.Add(o1.ID, o1);
            PNObjectRepository.PNObjects.Add(o2.ID, o2);
            PNObjectRepository.PNObjects.Add(o3.ID, o3);
            PNObjectRepository.PNObjects.Add(o4.ID, o4);

            Assert.AreEqual(4, PNObjectRepository.PNObjects.Count);
            Assert.AreEqual(1, PNObjectRepository.GetPNObjectsByType(PNObjectTypes.DiscreteLocation).Count());
            foreach (DiscreteLocation loc in PNObjectRepository.GetPNObjects<DiscreteLocation>())
            {
                Assert.AreEqual(o1, loc);
            }
            foreach (Location loc in PNObjectRepository.GetPNObjects<Location>())
            {
                Assert.AreEqual(o1, loc);
            }
        }
    }
}
