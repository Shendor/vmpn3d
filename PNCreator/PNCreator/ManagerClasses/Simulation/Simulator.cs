using System;
using PNCreator.ManagerClasses.EventManager.Events;
using System.Threading;
using PNCreator.ManagerClasses.FormulaManager;

namespace PNCreator.ManagerClasses.Simulation
{
    public class Simulator : AbstractSimulator, ISimulator
    {
        private const int PROGRESS_STEP = 10000;

        #region ISimulator Members

        public void Run(SimulationArgs args)
        {
            var formulaManager = App.GetObject<IFormulaManager>();
            
            Thread.Sleep(10);
            int runProgressCounter = 0;
            PreRunStep(args);
            do
            {
                if ((runProgressCounter++ % PROGRESS_STEP) == 0)
                {
                    eventPublisher.ExecuteEvents(new SimulationProgressEventArgs(args));
//                    Thread.Sleep(1);
                }

                RunFormulas(formulaManager, args);

                FirstStep(args);

                SecondStep(args);

                ThirdStep(args);

                args.Timer += args.MinimumTimeInterval;
                if(args.Timer >= args.EndSimulationTime) break;
            }
            while (args.ActiveTransitionQuantity > 0);

            eventPublisher.ExecuteEvents(new SimulationFinishedEventArgs(args));
        }

        
        #endregion

    }
}
