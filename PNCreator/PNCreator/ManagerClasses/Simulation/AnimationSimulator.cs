using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Media.Media3D;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using PNCreator.ManagerClasses.FormulaManager;
using System.Windows.Threading;
using System.Windows.Media;

namespace PNCreator.ManagerClasses.Simulation
{
    public class AnimationSimulator : AbstractSimulator, ISimulator
    {
        private static double ACTIVE_ARC_THICKNESS = Modules.Properties.PNProperties.ArcsThickness / 2;

        #region ISimulator Members

        public void Run(SimulationArgs args)
        {
            var animationArgs = (AnimationSimulationArgs)args;

            var formulaManager = App.GetObject<IFormulaManager>();
            ACTIVE_ARC_THICKNESS = Modules.Properties.PNProperties.ArcsThickness / 2;
            int step = 0;

            PreRunStep(args);
            do
            {
                RunFormulas(formulaManager, args);
                Thread.Sleep(100);

                FirstStep(args);

                RunFirstAnimation(animationArgs);

                Thread.Sleep(100);
                SecondStep(args);

                Thread.Sleep(TimeSpan.FromSeconds(animationArgs.AnimationSpeed));
                RunSecondAnimation(animationArgs);

                Thread.Sleep(TimeSpan.FromSeconds(animationArgs.AnimationSpeed));

                ThirdStep(args);

                args.Timer += args.MinimumTimeInterval;
                ++step;

                eventPublisher.ExecuteEvents(new SimulationProgressEventArgs(args));
                eventPublisher.ExecuteEvents(new MeshesRemovedEventArgs<Token>(animationArgs.Tokens));

                if (args.IsInterrupted && step.Equals(2))
                    args.Thread.Abort();
            }
            while (args.ActiveTransitionQuantity > 0);

            eventPublisher.ExecuteEvents(new SimulationFinishedEventArgs(args));
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Run first animation
        /// </summary>
        /// <param name="args">Simulation arguments</param>
        private void RunFirstAnimation(AnimationSimulationArgs args)
        {
            DiscreteLocation location;
            DiscreteTransition transition;
            ContinuousLocation cLocation;
            ContinuousTransition cTransition;
            Location localLocation;
            Arc3D localArc;
            Token token;

            args.Dispatcher.BeginInvoke
                  (DispatcherPriority.Normal,
                  (ThreadStart)delegate
                  {
                      foreach (Arc3D arc in args.Arcs)
                      {
                          arc.ValueInCanvas.Text = arc.Weight.ToString("F4");
                          arc.Thickness = Modules.Properties.PNProperties.ArcsThickness;
                      }
                      // animation: from location to transition   
                      foreach (Shape3D shape in args.Shapes)
                      {
                          if (shape.Type == PNObjectTypes.StructuralMembrane)   //---- FOR STRUCTURAL MEMBRANE
                          {
                              ((StructuralMembrane)shape).Animate(args.AnimationSpeed);
                          }
                          else if (shape.Type == PNObjectTypes.DiscreteTransition)   //---- FOR DISCRETE TRANSITION
                          {
                              transition = (DiscreteTransition)shape;
                              transition.ValueInCanvas.Text = transition.Delay.ToString();
                              //if (transition.IsActive == true && transition.DelayCounter.Equals(timer))
                              if (transition.IsActive && transition.DelayCounter <= args.Timer)
                              {
                                  shape.Geometry.Material = PNObjectMaterial.GetMaterial(Colors.OrangeRed);
                                  foreach (long incomeLocationId in transition.IncomeLocationsID)
                                  {
                                      localLocation = (Location)PNObjectRepository.GetByKey(incomeLocationId);
                                      localArc = (Arc3D)PNObjectRepository.GetByKey(transition.IncomeArcsID[incomeLocationId]);

                                      if (!transition.SALocations.Contains(localLocation.ID))
                                      {
                                          if (localLocation.Type == PNObjectTypes.DiscreteLocation)
                                          {
                                              location = (DiscreteLocation)localLocation;
                                              location.ValueInCanvas.Text = location.Tokens.ToString();
                                              token = new Token(localArc.StartPoint, localArc.MiddlePoint, localArc.EndPoint);
                                              token.Seconds = args.AnimationSpeed;
                                              eventPublisher.ExecuteEvents(new MeshAddedEventArgs<Token>(token));
                                              args.Tokens.Add(token);
                                              token.StartAnimation();
                                          }
                                      }
                                      if (localArc.Type == PNObjectTypes.ContinuousFlowArc)
                                          localArc.Thickness = ACTIVE_ARC_THICKNESS;
                                  }
                              }
                          }
                          else if (shape.Type == PNObjectTypes.ContinuousTransition)    // FOR CONTINUOUS TRANSITION
                          {
                              cTransition = (ContinuousTransition)shape;
                              cTransition.ValueInCanvas.Text = cTransition.Expectance.ToString("F");
                              if (cTransition.IsActive)
                              {
                                  shape.Geometry.Material = PNObjectMaterial.GetMaterial(Colors.OrangeRed);
                                  foreach (long incomeLocationId in cTransition.IncomeLocationsID)
                                  {
                                      localLocation = PNObjectRepository.GetByKey(incomeLocationId) as Location;
                                      localArc = PNObjectRepository.GetByKey(cTransition.IncomeArcsID[incomeLocationId]) as Arc3D;
                                      localArc.Thickness = ACTIVE_ARC_THICKNESS;
                                      if (!cTransition.SALocations.Contains(localLocation.ID))
                                      {
                                          cLocation = (ContinuousLocation) localLocation;
                                          localLocation.ValueInCanvas.Text = cLocation.Level.ToString("F4");
                                      }
                                  }
                              }
                              else
                              {
                                  shape.Geometry.Material = cTransition.Material;
                              }
                          }
                      }
                  });
        }

        /// <summary>
        /// Run second animation
        /// </summary>
        /// <param name="args">Simulation arguments</param>
        private void RunSecondAnimation(AnimationSimulationArgs args)
        {
            DiscreteLocation location;
            DiscreteTransition transition;
            ContinuousLocation cLocation;
            Transition localTransition;
            Arc3D localArc;
            Token token;

            args.Dispatcher.BeginInvoke
                 (DispatcherPriority.Normal,
                 (ThreadStart)delegate()
                 {
                     // animation: from transition to location
                     if (args.ActiveTransitionQuantity > 0)
                     {
                         foreach (Arc3D arc in args.Arcs)
                         {
                             if (arc.AllowSaveHistory)
                                 arc.AddNewRowOfHistory(args.SimulationName, args.Timer, arc.Weight);
                             arc.ValueInCanvas.Text = arc.Weight.ToString("F4");
                             arc.Thickness = Modules.Properties.PNProperties.ArcsThickness;
                         }
                         foreach (Shape3D shape in args.Shapes)
                         {
                             if (shape.Type == PNObjectTypes.DiscreteLocation)
                             {
                                 location = (DiscreteLocation)shape;
                                 location.ValueInCanvas.Text = location.Tokens.ToString();
                                 foreach (long incomeTransitionId in location.IncomeTransitionsID)
                                 {
                                     localTransition = PNObjectRepository.GetByKey(incomeTransitionId) as Transition;
                                     localArc = PNObjectRepository.GetByKey(location.IncomeArcsID[incomeTransitionId]) as Arc3D;

                                     transition = (DiscreteTransition)localTransition;
                                     localTransition.Geometry.Material = PNObjectMaterial.GetMaterial(localTransition.MaterialColor);

                                     if (transition.IsActive && 
                                         transition.DelayCounter <= args.Timer)
                                     {
                                         token = new Token(transition.Position, localArc.Position, location.Position);
                                         token.Seconds = args.AnimationSpeed;
                                         eventPublisher.ExecuteEvents(new MeshAddedEventArgs<Token>(token));
                                         args.Tokens.Add(token);
                                         token.StartAnimation();
                                     }
                                 }
                             }
                             else if (shape.Type == PNObjectTypes.ContinuousLocation)
                             {
                                 cLocation = (ContinuousLocation)shape;
                                 cLocation.ValueInCanvas.Text = cLocation.Level.ToString("F4");
                                 for (int j = 0; j < cLocation.IncomeTransitionsID.Count; j++)
                                 {
                                     long incomeTransitionId = cLocation.IncomeTransitionsID[j];
                                     localTransition = PNObjectRepository.GetByKey(incomeTransitionId) as Transition;

                                     if (localTransition.Type == PNObjectTypes.ContinuousTransition)
                                     {
                                         if ((localTransition).IsActive)
                                         {
                                             localArc = PNObjectRepository.GetByKey(cLocation.IncomeArcsID[incomeTransitionId]) as Arc3D;
                                             localArc.Thickness = ACTIVE_ARC_THICKNESS;
                                         }
                                     }
                                     else
                                     {
                                         if ((localTransition).IsActive &&
                                             ((DiscreteTransition)localTransition).DelayCounter <= args.Timer)
                                         {
                                             localArc = PNObjectRepository.GetByKey(cLocation.IncomeArcsID[incomeTransitionId]) as Arc3D;
                                             if (localArc.Type == PNObjectTypes.ContinuousFlowArc)
                                                 localArc.Thickness = ACTIVE_ARC_THICKNESS;
                                         }
                                     }
                                     localTransition.Geometry.Material = PNObjectMaterial.GetMaterial(localTransition.MaterialColor);
                                 }
                             }
                         }
                     }
                 });
        }

        #endregion

    }
}
