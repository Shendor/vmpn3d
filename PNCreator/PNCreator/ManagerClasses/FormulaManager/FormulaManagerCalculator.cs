using System.Collections.Generic;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using PNCreator.Modules.FormulaBuilder;
using System;
using PNCreator.Properties;
using System.Threading.Tasks;

namespace PNCreator.ManagerClasses.FormulaManager
{
    public class FormulaManagerCalculator : IFormulaManager
    {
        private EventPublisher eventPublisher;

        public FormulaManagerCalculator(EventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        /// <summary>
        /// 
        /// </summary>
        private List<ObjectWithFormula> FillObjectsWithFormula()
        {
            List<ObjectWithFormula> pnObjectsWithFormula = new List<ObjectWithFormula>();
            EventPublisher eventPublisher = App.GetObject<EventPublisher>();
            int progress = 0;
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject is IFormula)
                {
                    IFormula objectWithFormula = (IFormula)pnObject;

                    if (objectWithFormula.Formula != null)
                    {
                        pnObjectsWithFormula.Add(new ObjectWithFormula(pnObject));
                        if (PNProgramStorage.IsNeedToCompile)
                            objectWithFormula.CompileFormula(FormulaConverter.ToArrayOfValues(objectWithFormula.Formula));
                    }
                    if (objectWithFormula is IExtendedFormula)
                    {
                        IExtendedFormula objectWithBoolFormula = (IExtendedFormula)objectWithFormula;
                        if (objectWithBoolFormula.TransitionGuardFormula != null)
                        {
                            pnObjectsWithFormula.Add(new ObjectWithFormula(pnObject));
                            if (PNProgramStorage.IsNeedToCompile)
                                objectWithBoolFormula.CompileBooleanFormula(FormulaConverter.ToArrayOfValues(objectWithBoolFormula.TransitionGuardFormula));
                        }
                    }
                }
                eventPublisher.ExecuteEvents(new FormulaProgressEventArgs(progress++, PNObjectRepository.PNObjects.Values.Count));
            }
            return pnObjectsWithFormula;
        }

        /// <summary>
        /// - 0 index - other formulas
        /// - 1 index - Guard formula
        ///     - 0 index - List of objects which are contained in Formula
        ///     - 1 index - List of objects which are contained in GuardFormula
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private List<List<List<PNObject>>> GetListOfObjectsInFormula(PNObject obj)
        {
            IFormula objectWithFormula = (IFormula)obj;
            if (objectWithFormula is Transition)
            {
                return new List<List<List<PNObject>>> 
                {
                    FormulaConverter.ToArrayOfObjects(objectWithFormula.Formula),
                    FormulaConverter.ToArrayOfObjects(((Transition)obj).TransitionGuardFormula)
                };
            }
            else
            {
                return new List<List<List<PNObject>>> 
                {
                    FormulaConverter.ToArrayOfObjects(objectWithFormula.Formula)
                };
            }

        }

        public List<ObjectWithFormula> GetObjectsWithFormula()
        {
            Dictionary<PNObject, bool[]> isFormulaLaunched = new Dictionary<PNObject, bool[]>();
            Dictionary<PNObject, List<List<List<PNObject>>>> objectsInFormula = new Dictionary<PNObject, List<List<List<PNObject>>>>();
            List<ObjectWithFormula> tempList = FillObjectsWithFormula();
            List<ObjectWithFormula> orderedObjectsWithFormula = new List<ObjectWithFormula>();

            int totalCount = 0;
            foreach (ObjectWithFormula obj in tempList)
            {
                if (obj.Object is Transition)
                {
                    isFormulaLaunched.Add(obj.Object,
                                          new bool[] {
                                              (((Transition)obj.Object).Formula == null) ? true : false, 
                                              (((Transition)obj.Object).TransitionGuardFormula == null) ? true : false 
                                          });

                    if (isFormulaLaunched[obj.Object][0] == false)
                        totalCount++;

                    if (isFormulaLaunched[obj.Object][1] == false)
                        totalCount++;
                }
                else
                {
                    isFormulaLaunched.Add(obj.Object, new bool[] { false });
                    totalCount++;
                }
                objectsInFormula.Add(obj.Object, GetListOfObjectsInFormula(obj.Object));
            }

            if (tempList.Count == 0)
                return orderedObjectsWithFormula;

            bool isAllowToLaunch = true;

            for (int i = 0; i < totalCount; i++)
            {
                foreach (ObjectWithFormula o in tempList)
                {
                    for (int c = 0; c < objectsInFormula[o.Object].Count; c++)  // Type of formula (Guard or others). Not more than 2 types in the collection
                    {
                        if (objectsInFormula[o.Object][c] == null)
                            continue;
                        if (c == 1)
                            o.FormulaType = FormulaTypes.Guard;
                        isAllowToLaunch = true;
                        for (int k = 0; k < 2; k++)  // List of objects in the apropriate formula (0 - other values, 1 - Guard values)
                        {
                            List<PNObject> objInFormulaList = objectsInFormula[o.Object][c][k];

                            for (int j = 0; j < objInFormulaList.Count; j++)
                            {
                                if (isFormulaLaunched.ContainsKey(objInFormulaList[j]))
                                {
                                    if (!isFormulaLaunched[objInFormulaList[j]][k])
                                    {
                                        isAllowToLaunch = false;
                                        j = objInFormulaList.Count;
                                        k = 2;
                                    }
                                }
                            }
                        }
                        if (isAllowToLaunch)
                        {
                            if (orderedObjectsWithFormula.Count == 0 || orderedObjectsWithFormula.Find(
                                (obj) => (o.Object.ID.Equals(obj.Object.ID) && o.FormulaType.Equals(obj.FormulaType))) == null)
                            {
                                orderedObjectsWithFormula.Add(new ObjectWithFormula(o.Object, (c == 1) ? FormulaTypes.Guard : FormulaTypes.Value));

                            }
                            if (orderedObjectsWithFormula.Count.Equals(totalCount))
                                return orderedObjectsWithFormula;
                        }
                        isFormulaLaunched[o.Object][c] = isAllowToLaunch;
                    }
                }

            }
            tempList = null;
            isFormulaLaunched = null;
            objectsInFormula = null;

            throw new FormatException(Messages.Default.LoopAtFormula);

        }

        #region IFormulaManager Members


        public bool IsNeedToCompile
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion


        public void UpdateObjectsWithFormula(List<ObjectWithFormula> objectsWithFormula)
        {

            int progress = 0;
            foreach (ObjectWithFormula objWithFormula in objectsWithFormula)
            {
                if (objWithFormula.FormulaType == FormulaTypes.Guard)
                {
                    ((IExtendedFormula)objWithFormula.Object).ExecuteGuardFormula();
                }
                else
                {
                    var result = ((IFormula)objWithFormula.Object).ExecuteFormula();
                }
                eventPublisher.ExecuteEvents(new FormulaProgressEventArgs(progress++, objectsWithFormula.Count));
            }
            eventPublisher.ExecuteEvents(new PNObjectsValuesChangedEventArgs(PNObjectRepository.PNObjects.Values));
        }
    }

    public class ObjectWithFormula
    {
        public ObjectWithFormula(PNObject obj, FormulaTypes formulaType = FormulaTypes.Value)
        {
            Object = obj;
            FormulaType = formulaType;
        }

        public PNObject Object
        {
            get;
            set;
        }

        public FormulaTypes FormulaType
        {
            get;
            set;
        }

    }
}
