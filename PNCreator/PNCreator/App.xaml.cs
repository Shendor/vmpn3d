using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.ManagerClasses.FormulaManager;

namespace PNCreator
{
    public partial class App
    {

        private static readonly IDictionary<Type, Object> objectDictionary;

        static App()
        {
            objectDictionary = new Dictionary<Type, Object>();
        }

        public App()
        {

            Telerik.Windows.Controls.StyleManager.ApplicationTheme = new Telerik.Windows.Controls.Expression_DarkTheme();

            InitializeComponent();

            var eventPublisher = new EventPublisher();
            RegisterObject(eventPublisher);
            RegisterObject(new WindowsFactory());
            RegisterObject(new PNDocument());
            RegisterObject(new PNObjectManager());
            RegisterObject(new FormulaManager(new FormulaManagerCalculator(eventPublisher)));

            //            var binding = new CommandBinding(MyCommands.DoSomethingCommand, DoSomething, CanDoSomething);

            // Register CommandBinding for all windows.
            //            CommandManager.RegisterClassCommandBinding(typeof(Window), binding);
        }

        /// <summary>
        /// Get object by type
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <returns>Object</returns>
        public static T GetObject<T>()
        {
            foreach (var obj in objectDictionary.Values)
            {
                if (obj is T)
                {
                    return (T)obj;
                }
            }
            return default(T);
        }

        public static Object GetObject(Type type)
        {
            return objectDictionary[type];
        }

        /// <summary>
        /// Register object
        /// </summary>
        /// <param name="obj">Registered object</param>
        public static void RegisterObject(Object obj)
        {
            if (objectDictionary.ContainsKey(obj.GetType()))
            {
                throw new ArgumentException(obj.GetType() + " is already presented at the objects factory");
            }
            objectDictionary.Add(obj.GetType(), obj);
        }

        public static MainWindow GetMainWindow()
        {
            return Current.MainWindow as MainWindow;
        }

    }

}
