using System;
using System.Collections.Generic;
using System.Windows;
using PNCreator.Modules.About;
using PNCreator.Modules.Analizing;
using PNCreator.Modules.FormulaBuilder;
using PNCreator.Modules.ModelConfiguration;
using PNCreator.Modules.ModelInformation;
using PNCreator.Modules.Searching;
using PNCreator.Modules.ServerConnection;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses
{
    public class WindowsFactory
    {

        public Window GetWindow(Type windowType)
        {
            return (Window)Activator.CreateInstance(windowType);
        }

        public T GetWindow<T>() where T : Window
        {
            return (T) Activator.CreateInstance(typeof(T));
        }

        public Window GetFormulaBuilderWindow(FormulaTypes formulaType, PNObject pnObject)
        {
            return new FormulaBuilder(formulaType, pnObject);
        }


        public Window GetNavigationMapWindow(PNObjectManager objManager, _3DTools.TrackballDecorator trackball,
                                                    MainWindow mainWnd, System.Windows.Controls.Viewport3D viewport)
        {
            return new Modules.NavigationMap.NavigationMap(objManager, trackball, mainWnd,viewport);
        }
    }
}
