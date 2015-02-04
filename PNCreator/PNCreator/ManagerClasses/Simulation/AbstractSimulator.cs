using System;
using System.Collections.Generic;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using PNCreator.ManagerClasses.FormulaManager;

namespace PNCreator.ManagerClasses.Simulation
{
    public abstract class AbstractSimulator
    {
        //        protected SimulationArgsChangedHandler simulationArgsChangedHandler;
        protected EventPublisher eventPublisher;
        private Dictionary<long, List<Location>> outcomeLocations;
        private Dictionary<KeyValuePair<long, long>, long> incomeArcs;

        protected AbstractSimulator()
        {
            eventPublisher = App.GetObject<EventPublisher>();
        }

        /// <summary>
        /// First step of simulation.
        /// From locations to transitions
        /// </summary>
        /// <param name="args">Simulator arguments</param>
        protected void FirstStep(SimulationArgs args)
        {
            DiscreteLocation location;
            DiscreteTransition transition;
            ContinuousLocation cLocation;
            ContinuousTransition cTransition;
            Location localLocation;
            Arc3D localArc;

            args.ActiveTransitionQuantity = 0;  // Reset active transition counter
            Dictionary<long, double> locationValues = new Dictionary<long, double>();
            CalculateMinTimeInterval(args);

            foreach (Shape3D shape in args.Shapes)   // 1) Check all transitions
            {
                /* if (shape.Type == PNObjectTypes.DiscreteLocation)
                 {
                     location = (DiscreteLocation)shape;
                     ExecuteHistoryDataAddedEvent(args, location, location.Tokens);
                 }
                 else if (shape.Type == PNObjectTypes.ContinuousLocation)
                 {
                     cLocation = (ContinuousLocation)shape;
                     ExecuteHistoryDataAddedEvent(args, cLocation, cLocation.Level);
                 }else*/
                //********************* Check discrete transition *************************************
                #region CHECK DISCRETE TRANSITION
                if (shape.Type == PNObjectTypes.DiscreteTransition)
                {
                    transition = (DiscreteTransition)shape;
                    //                    ExecuteHistoryDataAddedEvent(args, transition, transition.Delay);
                    var isCapacityOverflow = IsCapacityOverflow(transition);
                    if (!isCapacityOverflow && transition.Guard)
                    {
                        int activityCounter = 0;
                        int iArcsAmount = 0;

                        #region LOOP ABOUT ALL INCOME LOCATIONS AT THIS TRANSITION
                        foreach (long incomeLocationId in transition.IncomeLocationsID)
                        {
                            localLocation = PNObjectRepository.GetByKey(incomeLocationId) as Location;
                            localArc = PNObjectRepository.GetByKey(transition.IncomeArcsID[incomeLocationId]) as Arc3D;

                            if (!locationValues.ContainsKey(localLocation.ID))
                            {
                                locationValues.Add(localLocation.ID,
                                                   PNObjectRepository.PNObjects.DoubleValues[localLocation.ID]);
                            }
                            if (localLocation.Type == PNObjectTypes.DiscreteLocation) //------- IF INCOME LOCATION IS DISCRETE
                            {
                                location = (DiscreteLocation)localLocation;

                                if (localArc.Type == PNObjectTypes.DiscreteArc)
                                {
                                    if (location.Tokens - location.MinCapacity >= localArc.Weight)
                                        activityCounter += 1;
                                }
                                else if (localArc.Type == PNObjectTypes.DiscreteTestArc &&
                                         locationValues.ContainsKey(location.ID) &&
                                         locationValues[location.ID] - location.MinCapacity >= localArc.Weight)
                                {
                                    activityCounter += 1;
                                }
                                else if (localArc.Type == PNObjectTypes.DiscreteInhibitorArc)
                                {
                                    iArcsAmount += 1;
                                    if (location.Tokens - location.MinCapacity >= localArc.Weight)
                                        activityCounter -= 1;
                                }
                            }
                            else if (localLocation.Type == PNObjectTypes.ContinuousLocation)  //---------- IF INCOME LOCATION IS CONTINUOUS
                            {
                                cLocation = (ContinuousLocation)localLocation;
                                if (localArc.Type == PNObjectTypes.ContinuousFlowArc ||
                                    localArc.Type == PNObjectTypes.ContinuousTestArc)
                                {
                                    if (cLocation.Level - cLocation.MinCapacity >= localArc.Weight)
                                        activityCounter += 1;
                                }
                                else if (localArc.Type == PNObjectTypes.ContinuousInhibitorArc)
                                {
                                    iArcsAmount += 1;
                                    if (cLocation.Level - cLocation.MinCapacity >= localArc.Weight)
                                        activityCounter -= 1;
                                }
                            }
                        }
                        #endregion

                        // if all income location of the transition have tokens then the transition is active
                        #region MAKE THE TRANSITION ACTIVE
                        if (activityCounter == transition.IncomeLocationsID.Count - iArcsAmount)
                        {
                            transition.IsValid = true; //TODO: looks like irrelevant
                            args.ActiveTransitionQuantity += 1;
                            if (transition.DelayCounter == -1)
                            {
                                transition.DelayCounter = transition.Delay + args.Timer - 1;
                            }
                            if (transition.CanBeActivatedForTime(args.Timer))
                            {
                                transition.IsActive = true;
                                foreach (long incomeLocationId in transition.IncomeLocationsID)
                                {
                                    localLocation = PNObjectRepository.GetByKey(incomeLocationId) as Location;
                                    localArc = PNObjectRepository.GetByKey(transition.IncomeArcsID[incomeLocationId]) as Arc3D;

                                    if (localLocation.Type == PNObjectTypes.DiscreteLocation)
                                    {
                                        location = (DiscreteLocation)localLocation;
                                        if (localArc.Type == PNObjectTypes.DiscreteArc &&
                                            location.Tokens - location.MinCapacity >= localArc.Weight)
                                        {
                                            location.Tokens -= (Int32)localArc.Weight;
                                        }

                                    }
                                    else if (localLocation.Type == PNObjectTypes.ContinuousLocation)
                                    {
                                        cLocation = (ContinuousLocation)localLocation;
                                        if (localArc.Type == PNObjectTypes.ContinuousFlowArc &&
                                            cLocation.Level - cLocation.MinCapacity >= localArc.Weight)
                                        {
                                            cLocation.Level -= localArc.Weight;
                                        }
                                    }
                                }
                                transition.ResetDelayCounter();
                            }

                        }
                        #endregion
                        else
                        {
                            transition.IsActive = false;
                        }
                    }
                    else
                    {
                        transition.IsActive = false;
                        transition.IsValid = false;
                    }

                }
                #endregion

                //********************* Check continuous transition *************************************
                #region CHECK CONTINUOUS TRANSITION
                else if (shape.Type == PNObjectTypes.ContinuousTransition)
                {
                    cTransition = (ContinuousTransition)shape;
                    var isCapacityOverflow = IsCapacityOverflow(cTransition);
                    //                    ExecuteHistoryDataAddedEvent(args, cTransition, cTransition.Expectance);
                    if (!isCapacityOverflow && cTransition.Guard)
                    {
                        int activityCounter = 0;
                        int iArcsAmount = 0;        // quantity of income inghibitor arcs

                        #region LOOP ABOUT ALL INCOME LOCATIONS AT THIS TRANSITION
                        foreach (long incomeLocationId in cTransition.IncomeLocationsID)
                        {
                            localLocation = PNObjectRepository.GetByKey(incomeLocationId) as Location;
                            localArc = PNObjectRepository.GetByKey(cTransition.IncomeArcsID[incomeLocationId]) as Arc3D;

                            if (!locationValues.ContainsKey(localLocation.ID))
                            {
                                locationValues.Add(localLocation.ID,
                                                   PNObjectRepository.PNObjects.DoubleValues[localLocation.ID]);
                            }
                            if (localLocation.Type == PNObjectTypes.DiscreteLocation)
                            {
                                location = (DiscreteLocation)localLocation;
                                if (localArc.Type == PNObjectTypes.DiscreteTestArc &&
                                    locationValues.ContainsKey(location.ID) &&
                                    locationValues[location.ID] - location.MinCapacity >= localArc.Weight)
                                {
                                    //                                    if (location.Tokens >= localArc.Weight)
                                    activityCounter += 1;
                                }
                                else if (localArc.Type == PNObjectTypes.DiscreteInhibitorArc)
                                {
                                    iArcsAmount += 1;
                                    if (location.Tokens - location.MinCapacity >= localArc.Weight)
                                        activityCounter -= 1;
                                }

                            }
                            else if (localLocation.Type == PNObjectTypes.ContinuousLocation)
                            {
                                cLocation = (ContinuousLocation)localLocation;
                                if (localArc.Type == PNObjectTypes.ContinuousArc ||
                                    localArc.Type == PNObjectTypes.ContinuousTestArc)
                                {
                                    if (cLocation.Level - cLocation.MinCapacity >=
                                        cTransition.Expectance * localArc.Weight)
                                        activityCounter += 1;
                                }
                                else if (localArc.Type == PNObjectTypes.ContinuousInhibitorArc)
                                {
                                    iArcsAmount += 1;
                                    if (cLocation.Level - cLocation.MinCapacity >=
                                        cTransition.Expectance * localArc.Weight)
                                        activityCounter -= 1;
                                }
                            }
                        }
                        #endregion

                        // if all income location of the transition have tokens then the transition is active
                        #region MAKE THE TRANSITION ACTIVE
                        if (activityCounter == cTransition.IncomeLocationsID.Count - iArcsAmount)
                        {
                            cTransition.IsActive = true;
                            args.ActiveTransitionQuantity += 1;
                            foreach (long incomeLocationId in cTransition.IncomeLocationsID)
                            {
                                localLocation = (Location)PNObjectRepository.GetByKey(incomeLocationId);
                                localArc = (Arc3D)PNObjectRepository.GetByKey(cTransition.IncomeArcsID[incomeLocationId]);

                                if (localLocation.Type == PNObjectTypes.ContinuousLocation)
                                {
                                    cLocation = (ContinuousLocation)localLocation;
                                    if (localArc.Type == PNObjectTypes.ContinuousArc &&
                                        cLocation.Level - cLocation.MinCapacity >=
                                        localArc.Weight * args.MinimumTimeInterval)
                                    {
                                        cLocation.Level -= cTransition.Expectance * localArc.Weight * args.MinimumTimeInterval;
                                    }
                                }
                            }
                        }
                        #endregion
                        else
                            cTransition.IsActive = false;
                    }
                    else
                        cTransition.IsActive = false;
                }
                #endregion

            }

            /* foreach (Arc3D arc in args.Arcs)
             {
                 ExecuteHistoryDataAddedEvent(args, arc, arc.Weight);
             }*/
        }

        private bool IsCapacityOverflow(Transition transition)
        {
            bool isCapacityOverflow = false;
            if (outcomeLocations.ContainsKey(transition.ID))
            {
                List<Location> outcomeLocationList = outcomeLocations[transition.ID];
                foreach (var outcomeLocation in outcomeLocationList)
                {
                    long arcId = incomeArcs[new KeyValuePair<long, long>(transition.ID, outcomeLocation.ID)];
                    Arc3D incomeArc = (Arc3D)PNObjectRepository.GetByKey(arcId);
                    double value = 0;
                    if (outcomeLocation.Type == PNObjectTypes.DiscreteLocation)
                    {
                        value = ((DiscreteLocation)outcomeLocation).Tokens;
                    }
                    else if (outcomeLocation.Type == PNObjectTypes.ContinuousLocation)
                    {
                        value = ((ContinuousLocation)outcomeLocation).Level;
                    }

                    if (transition.Type == PNObjectTypes.DiscreteTransition)
                    {
                        if (outcomeLocation.MaxCapacity < incomeArc.Weight + value)
                        {
                            isCapacityOverflow = true;
                        }
                    }
                    else
                    {
                        if (outcomeLocation.MaxCapacity <
                            ((ContinuousTransition)transition).Expectance * incomeArc.Weight + value)
                        {
                            isCapacityOverflow = true;
                        }
                    }

                }
            }
            return isCapacityOverflow;
        }

        private void CalculateMinTimeInterval(SimulationArgs args)
        {
            args.MinimumTimeInterval = 1;
            foreach (Shape3D shape in args.Shapes)
            {
                if (shape.Type == PNObjectTypes.DiscreteTransition)
                {
                    DiscreteTransition transition = (DiscreteTransition)shape;
                    if (transition.Delay < args.MinimumTimeInterval)
                    {
                        args.MinimumTimeInterval = transition.Delay;
                    }
                }
            }
        }

        /// <summary>
        /// Second step of simulation.
        /// From transitions to locations
        /// </summary>
        /// <param name="args">Simulator arguments</param>
        protected void SecondStep(SimulationArgs args)
        {
            DiscreteLocation location;
            DiscreteTransition transition;
            ContinuousLocation cLocation;
            Transition localTransition;
            Arc3D localArc;

            if (args.ActiveTransitionQuantity > 0)
            {
                foreach (Shape3D shape in args.Shapes)   // 2) Take accepted tokens from active transitions
                {
                    #region DISCRETE LOCATION
                    if (shape.Type == PNObjectTypes.DiscreteLocation)
                    {
                        location = (DiscreteLocation)shape;
                        foreach (long incomeTransitionId in location.IncomeTransitionsID)
                        {
                            localTransition = (Transition)PNObjectRepository.GetByKey(incomeTransitionId);
                            localArc = (Arc3D)PNObjectRepository.GetByKey(location.IncomeArcsID[incomeTransitionId]);
                            transition = (DiscreteTransition)localTransition;

                            if (transition.IsActive && transition.DelayCounter <= args.Timer)
                                location.Tokens += (int)localArc.Weight;
                        }
                    }
                    #endregion

                    #region CONTINUOUS LOCATION
                    else if (shape.Type == PNObjectTypes.ContinuousLocation)
                    {
                        cLocation = (ContinuousLocation)shape;
                        foreach (long key in cLocation.IncomeTransitionsID)
                        {
                            localTransition = (Transition)PNObjectRepository.GetByKey(key);
                            localArc = (Arc3D)PNObjectRepository.GetByKey(cLocation.IncomeArcsID[key]);

                            if (localTransition.IsActive)
                            {
                                if (localTransition.Type == PNObjectTypes.ContinuousTransition)
                                {
                                    cLocation.Level += ((ContinuousTransition)localTransition).Expectance *
                                                       localArc.Weight * args.MinimumTimeInterval;
                                }
                                else if (localTransition.Type == PNObjectTypes.DiscreteTransition)
                                {
                                    if (((DiscreteTransition)localTransition).DelayCounter <= args.Timer)
                                        cLocation.Level += localArc.Weight;
                                }
                            }
                        }
                    }

                    #endregion
                }

            }
        }

        /// <summary>
        /// Third step of simulation.
        /// Recalculate Delay time for Discrete transitions. Add history data
        /// </summary>
        /// <param name="args">Simulator arguments</param>
        protected void ThirdStep(SimulationArgs args)
        {
            AddHistoryData(args);

            /*if (args.HasDiscreteTransitions)
            {
                foreach (Shape3D shape in args.Shapes)
                {
                    if (shape.Type == PNObjectTypes.DiscreteTransition)
                    {
                        DiscreteTransition t = (DiscreteTransition)shape;
                        if (t.IsActive && t.IsValid && t.DelayCounter <= args.Timer)
                        {
                            //t.DelayCounter += ((DiscreteTransition)shape).Delay;
                            t.DelayCounter = t.Delay + args.Timer;
                        }
                    }
                }
            }*/
        }

        /// <summary>
        /// Execute all formulas
        /// </summary>
        protected void RunFormulas(IFormulaManager formulaManager, SimulationArgs args)
        {
            foreach (ObjectWithFormula obj in formulaManager.GetObjectsWithFormula())
            {
                ExecuteFormulaForModels(obj.Object, obj.FormulaType, args);
            }
        }

        /// <summary>
        /// Execute formula for object
        /// </summary>
        /// <param name="pnObject">PNObject</param>
        /// <param name="formulaType">Formula type</param>
        private void ExecuteFormulaForModels(PNObject pnObject, FormulaTypes formulaType, SimulationArgs args)
        {
            if (formulaType == FormulaTypes.Guard)
            {
                ((IExtendedFormula)pnObject).ExecuteGuardFormula();
            }
            else
            {
                ((IFormula)pnObject).ExecuteFormula();
            }

            if (pnObject.Type == PNObjectTypes.DiscreteTransition)
            {
                DiscreteTransition transition = (DiscreteTransition)pnObject;
                if (!transition.Delay.Equals(PNObjectRepository.PNObjects.DoubleValues[pnObject.ID]))
                    transition.DelayCounter = args.Timer + transition.Delay;
                args.MinimumTimeInterval = transition.Delay;
            }
        }

        protected void AddHistoryData(SimulationArgs args)
        {
            foreach (var shape in args.Shapes)
            {
                double value = 0;
                if (shape.Type == PNObjectTypes.DiscreteLocation)
                {
                    value = ((DiscreteLocation)shape).Tokens;
                }
                else if (shape.Type == PNObjectTypes.ContinuousLocation)
                {
                    value = ((ContinuousLocation)shape).Level;
                }
                else if (shape.Type == PNObjectTypes.DiscreteTransition)
                {
                    value = ((DiscreteTransition)shape).Delay;
                }
                else if (shape.Type == PNObjectTypes.ContinuousTransition)
                {
                    value = ((ContinuousTransition)shape).Expectance;
                }

                ExecuteHistoryDataAddedEvent(args, shape, value);
            }
            foreach (Arc3D arc in args.Arcs)
            {
                ExecuteHistoryDataAddedEvent(args, arc, arc.Weight);
            }
        }

        private void ExecuteHistoryDataAddedEvent(SimulationArgs args, PNObject source, double value)
        {
            if (source.AllowSaveHistory)
            {
                source.AddNewRowOfHistory(args.SimulationName, args.Timer, value);
                eventPublisher.ExecuteEvents(new HistoryDataAddedEventArgs(value, args.Timer)
                {
                    Source = source
                });
            }
        }

        protected void PreRunStep(SimulationArgs args)
        {
            incomeArcs = new Dictionary<KeyValuePair<long, long>, long>();
            outcomeLocations = new Dictionary<long, List<Location>>();
            foreach (var shape in args.Shapes)
            {
                if (shape is Location)
                {
                    Location location = (Location)shape;
                    List<long> incomeTransitionsId = location.IncomeTransitionsID;
                    IDictionary<long, long> incomeArcsId = location.IncomeArcsID;

                    for (int i = 0; i < incomeTransitionsId.Count; i++)
                    {
                        long transitionId = incomeTransitionsId[i];
                        long arcId = incomeArcsId[transitionId];

                        incomeArcs.Add(new KeyValuePair<long, long>(transitionId, shape.ID), arcId);

                        if (!outcomeLocations.ContainsKey(transitionId))
                        {
                            outcomeLocations.Add(transitionId, new List<Location>());
                        }
                        outcomeLocations[transitionId].Add(location);
                    }
                }
                else if (shape.Type == PNObjectTypes.DiscreteTransition)
                {
                    ((DiscreteTransition)shape).ResetDelayCounter();
                }
            }

            AddHistoryData(args);
            CalculateMinTimeInterval(args);
            args.Timer = args.MinimumTimeInterval;
        }

        //        /// <summary>
        //        /// SimulationArgsChanged event
        //        /// </summary>
        //        public event SimulationArgsChangedHandler SimulationArgsChanged
        //        {
        //            add
        //            {
        //                simulationArgsChangedHandler += value;
        //            }
        //            remove
        //            {
        //                simulationArgsChangedHandler -= value;
        //            }
        //        }
    }
}
