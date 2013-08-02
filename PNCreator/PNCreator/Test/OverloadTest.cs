using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.ManagerClasses;
using System.Windows.Media.Media3D;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Controls;
using WindowsControl;

namespace PNCreator.Test
{
    class OverloadTest
    {
        public static void Test(PNObjectManager objectManager, PNViewport trackball)
        {
            Random r = new Random();
            Timer timer = new Timer();
            timer.Start();
            objectManager.TypeOfAddObject = PNObjectTypes.ContinuousLocation;
            for (int i = 0; i < 1000; i++)
            {
                trackball.AddPNObject(objectManager.BuildShape(new Point3D(r.Next(-200, 200), r.Next(-200, 200), r.Next(-900, -500))));
                //objectManager.AddDiscreteTransition(new Vector3D(r.Next(-100, 100), r.Next(-100, 100), r.Next(-700, -500)), pnViewport.Viewport3D, pnViewport.Content2D);
            }
            //for (int i = 0; i < 100; i++)
            //{
            //    objectManager.Arcs.Add(new Arc3D(i, i + 1, "a" + i.ToString(),
            //        new Point3D(objectManager.Shapes[i].Position.X, objectManager.Shapes[i].Position.Y, objectManager.Shapes[i].Position.Z),
            //        new Point3D(objectManager.Shapes[i + 1].Position.X, objectManager.Shapes[i + 1].Position.Y, objectManager.Shapes[i + 1].Position.Z),
            //        PNObjectTypes.DiscreteArc));
            //        pnViewport.Viewport3D.Children.Add(objectManager.Arcs[i]);
            //}

            timer.Stop();
            DialogWindow.Alert("Elapsed time = " + timer.Duration.ToString());
        }
    }
}
