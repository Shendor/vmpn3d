using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meshes3D;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.IO;
using System.Windows.Controls;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using System.Windows;
using PNCreator.Modules.FormulaBuilder;
using WindowsControl;
using PNCreator.ManagerClasses.Constructor;
using PNCreator.Controls;
using PNCreator.Properties;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.ManagerClasses.FormulaManager;
using PNCreator.ManagerClasses.Exception;

namespace PNCreator.ManagerClasses
{
    public class PNObjectManager : FrameworkElement
    {
        public static DependencyProperty IsNotReadonlyProperty =
            DependencyProperty.Register("IsNotReadonly", typeof (bool), typeof (PNObjectManager),
                                        new PropertyMetadata(true, OnIsNotReadonlyChanged));

        private Shape3D arcStartObject;
        private readonly EventPublisher eventPublisher;

        public PNObjectManager()
        {
            IsAllowModify = true;
            TypeOfAddObject = PNObjectTypes.None;
            eventPublisher = App.GetObject<EventPublisher>();
        }

        #region Properties

        public bool IsNotReadonly
        {
            get { return (bool) GetValue(IsNotReadonlyProperty); }
            set{SetValue(IsNotReadonlyProperty, value);}
        }


        private static void OnIsNotReadonlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!e.NewValue.Equals(e.OldValue))
            {
                d.SetValue(IsNotReadonlyProperty, e.NewValue);
            }
        }

        /// <summary>
        /// Type of added object at the main window
        /// </summary>
        public PNObjectTypes TypeOfAddObject
        {
            get;
            set;
        }

        /// <summary>
        /// If model modification is available
        /// </summary>
        public bool IsAllowModify
        {
            get;
            set;
        }

        #endregion

        #region Object values installer
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pnObject"></param>
        /// <param name="value"></param>
        public void SetDoubleValue(PNObject pnObject, double value)
        {
            if (pnObject == null) // Investigate why ?
                return;

            if (pnObject is Arc3D)
            {
                ((Arc3D)pnObject).Weight = value;
            }
            else
            {
                switch (pnObject.Type)
                {
                    case PNObjectTypes.DiscreteLocation:
                        ((DiscreteLocation)pnObject).Tokens = (int)value;
                        break;
                    case PNObjectTypes.DiscreteTransition:
                        ((DiscreteTransition)pnObject).Delay = value;
                        ((DiscreteTransition)pnObject).DelayCounter = value;
                        break;
                    case PNObjectTypes.ContinuousLocation:
                        ((ContinuousLocation)pnObject).Level = value;
                        break;
                    case PNObjectTypes.ContinuousTransition:
                        ((ContinuousTransition)pnObject).Expectance = value;
                        break;
                    case PNObjectTypes.StructuralMembrane:
                        ((StructuralMembrane)pnObject).Speed = value;
                        break;
                }
            }
            pnObject.ValueInCanvas.Text = value.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="transition"></param>
        /// <param name="value"></param>
        public void SetBooleanValue(Transition transition, bool value)
        {
            transition.Guard = value;
        }
        #endregion

        #region PNObject builder

        /// <summary>
        /// Build shape
        /// </summary>
        /// <param name="position">Shape position</param>
        public Shape3D BuildShape(Point3D position)
        {
            arcStartObject = null;

            AbstractPNObjectBuilder builder = PNBuilderFactory.GetPNObjectBuilder(TypeOfAddObject);
            Shape3D pnObject = ((AbstractShapeBuilder)builder).BuildShape(position);
            PNObjectRepository.PNObjects.Add(pnObject.ID, pnObject);

            return pnObject;
        }

        /// <summary>
        /// Build arc
        /// </summary>
        /// <param name="selectedObject">Selected object. The method defines is it start or end object
        /// depending of method invocation quantity</param>
        public Arc3D BuildArc(Shape3D selectedObject)
        {
            if (arcStartObject == null)
            {
                arcStartObject = selectedObject;
                return null;
            }

            AbstractPNObjectBuilder builder = PNBuilderFactory.GetPNObjectBuilder(TypeOfAddObject);
            Arc3D arc;
            try
            {
                arc = ((ArcBuilder) builder).BuildArc(arcStartObject, selectedObject);
            }
            catch (IllegalPNObjectException)
            {
                arcStartObject = null;
                throw;
            }
            PNObjectRepository.PNObjects.Add(arc.ID, arc);

            arcStartObject = null;

            return arc;
        }

        #endregion

        #region Object managing methods

        #region Membrane methods
        /// <summary>
        /// Set membrane if it covers this object
        /// </summary>
        public void CoverObjectByMembrane(PNObject obj)
        {
            //HashSet<PNObject> ignoreList = new HashSet<PNObject>();

            //foreach (Shape3D shape in Shapes)
            //{

            //    if (shape.Type == PNObjectTypes.Membrane ||
            //        shape.Type == PNObjectTypes.StructuralMembrane)
            //    {
            //        bool isIgnore = false;
            //        foreach (PNObject ignoredObject in ignoreList)
            //        {
            //            if (ignoredObject.Group != null &&
            //                ignoredObject.Group.Equals(shape.Group))
            //            {
            //                ignoreList.Add(shape);
            //                isIgnore = true;
            //                break;
            //            }
            //        }

            //        if (!isIgnore && shape.IsIntersectWith(obj))
            //        {
            //            obj.Group = shape;
            //            ignoreList.Add(shape);
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Cover objects with membranes
        /// </summary>
        public void CoverObjectsByAllMembranes()
        {
            //ISet<Membrane> membranes = new HashSet<Membrane>();

            //InitializeMembranesTree(membranes);

            //foreach (Membrane membrane in membranes)
            //{
            //    CoverObjectsByMembrane(membrane);
            //}

            //// FIXME Find a better way to detect identical names
            //foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            //{
            //    if (pnObject.Group == -1)
            //    {
            //        var value = PNObjectRepository.PNObjects.Values.FirstOrDefault((n) => n.ID != pnObject.ID &&
            //                                                                        n.Group == -1 && n.Name.Equals(pnObject.Name));
            //        if (value != null)
            //            throw new IllegalPNObjectException(String.Format(Messages.Default.BadObjectForMembrane, "<Skin>", pnObject));
            //    }
            //}

            //foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            //{
               
            //}

            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                pnObject.Group = -1;
            }
        }

        /// <summary>
        /// Cover objects by membrane
        /// </summary>
        /// <param name="pnObject">Membrane</param>
        private void CoverObjectsByMembrane(Membrane membrane)
        {
            foreach (PNObject internalMembrane in membrane.PNObjects)
            {
                if (internalMembrane.Type == PNObjectTypes.Membrane ||
                    internalMembrane.Type == PNObjectTypes.StructuralMembrane)
                {
                    CoverObjectsByMembrane((Membrane)internalMembrane);
                }
            }

            foreach (PNObject obj in PNObjectRepository.PNObjects.Values)
            {
                if (!membrane.Equals(obj) && obj.Group == -1)
                {
                    try
                    {
                        AddObjectToMembrane(membrane, obj);
                    }
                    catch (IllegalPNObjectException ex)
                    {
                        ResetMembraneObjects(membrane);
                        throw ex;
                    }
                }
            }
            eventPublisher.ExecuteEvents(new PNObjectChangedEventArgs(membrane));
        }

        /// <summary>
        /// Clear PN objects at the membrane and set their group as for Skin membrane
        /// </summary>
        /// <param name="membrane">Membrane to reset</param>
        private static void ResetMembraneObjects(Membrane membrane)
        {
            foreach (PNObject pnObject in membrane.PNObjects)
                pnObject.Group = -1;
            membrane.PNObjects.Clear();
        }

        /// <summary>
        /// Initialize membrane hierarchy
        /// </summary>
        /// <param name="membranes">Membranes</param>
        private void InitializeMembranesTree(ISet<Membrane> membranes)
        {
            ClearGroupInfo(membranes);

            foreach (Membrane membrane1 in membranes)
            {
                foreach (Membrane membrane2 in membranes)
                {
                    Membrane parentMembrane = (Membrane)PNObjectRepository.GetByKey(membrane2.Group);
                    if (!membrane1.Equals(membrane2) && (membrane2.Group == -1 ||
                        (membrane2.Group != -1 && parentMembrane.PNObjects.Contains(membrane1))))
                    {
                        AddObjectToMembrane(membrane1, membrane2);
                    }
                }
            }
        }

        /// <summary>
        /// Clear group parameter for membanes
        /// </summary>
        /// <param name="membranes">Membranes</param>
        private void ClearGroupInfo(ISet<Membrane> membranes)
        {
            foreach (PNObject obj in PNObjectRepository.PNObjects.Values)
            {
                obj.Group = -1;
                if (obj is Membrane)
                {
                    Membrane membrane = (Membrane)obj;
                    membrane.Group = -1;
                    membrane.PNObjects = new HashSet<PNObject>();
                    membranes.Add(membrane);
                }

            }
        }

        /// <summary>
        /// Covers objects which are inside of the indicated membrane
        /// </summary>
        /// <param name="membrane">Membrane</param>
        private void CoverObjectsByMembrane(Membrane membrane, ISet<Membrane> ignoredMembranes)
        {
            foreach (PNObject obj in PNObjectRepository.PNObjects.Values)
            {
                if (!membrane.Equals(obj))
                {
                    if (obj.Group == -1)
                    {
                        AddObjectToMembrane(membrane, obj);
                    }
                }
            }
        }

        /// <summary>
        /// Check wether membrane contains object and add to membrane
        /// </summary>
        /// <param name="membrane">Membrane</param>
        /// <param name="pnObject">Object</param>
        /// <exception cref="IllegalPNObjectException">Throws exception if the object with same name 
        /// is already presented at the membrane</exception>
        /// <returns>If added successfully</returns>
        private bool AddObjectToMembrane(Membrane membrane, PNObject pnObject)
        {
            if (membrane.IsIntersectWith(pnObject))
            {
                var result = membrane.PNObjects.FirstOrDefault((n) => n.Name.Equals(pnObject.Name));
                if (result != null)
                    throw new IllegalPNObjectException(String.Format(Messages.Default.BadObjectForMembrane, membrane, pnObject ));

                pnObject.Group = membrane.ID;
                membrane.PNObjects.Add(pnObject);

                return true;
            }
            else
                return false;
        }

        #endregion

        /// <summary>
        /// Delete selected object
        /// </summary>
        private void DeleteObject(PNObject pnObject)
        {
            long idToRemove = pnObject.ID;

            var objectsToRemove = new List<Mesh3D>();
            objectsToRemove.Add(pnObject);

            foreach (Arc3D arc in PNObjectRepository.GetPNObjects<Arc3D>())
            {
                if (arc.ID == idToRemove || arc.StartID == idToRemove || arc.EndID == idToRemove)
                {
                    objectsToRemove.Add(arc);
                    PNObject startArcObj = PNObjectRepository.GetByKey(arc.StartID);
                    PNObject endArcObj = PNObjectRepository.GetByKey(arc.EndID);

                    if (startArcObj is Location && endArcObj is Transition)
                    {
                        Transition transition = (Transition)endArcObj;
                        transition.IncomeLocationsID.Remove(startArcObj.ID);
                        transition.SALocations.Remove(startArcObj.ID);
                    }
                    else if (startArcObj is Transition && endArcObj is Location)
                    {
                        Location location = (Location)endArcObj;
                        location.IncomeTransitionsID.Remove(startArcObj.ID);
                    }
                }
            }

            //if (!(pnObject is Arc3D))
            //{

            //    viewport.RemovePNObject(pnObject);
            //    PNObjectRepository.PNObjects.Remove(idToRemove);
            //}

            foreach (PNObject objToRemove in objectsToRemove)
            {
                ChangeNameInFormulas(objToRemove.ToString(), null);
                //foreach (PNObject obj in PNObjectRepository.PNObjects.Values)
                //{
                //    if (obj is IFormula)
                //    {
                //        IFormula formula = (IFormula)obj;
                //        if (formula.Formula != null)
                //        {
                //            if (formula.Formula.Contains(objToRemove.ToString()))
                //            {
                //                formula.Formula = null;
                //            }
                //        }
                //    }
                //    if (obj is IExtendedFormula)
                //    {
                //        IExtendedFormula extFormula = (IExtendedFormula)obj;
                //        if (extFormula.TransitionGuardFormula != null)
                //        {
                //            if (extFormula.TransitionGuardFormula.Contains(objToRemove.ToString()))
                //            {
                //                extFormula.TransitionGuardFormula = null;
                //            }
                //        }
                //    }
                //}

//                viewport.RemovePNObject(objToRemove);
                PNObjectRepository.PNObjects.Remove(objToRemove.ID);
            }
            eventPublisher.ExecuteEvents(new PNObjectsDeletedEventArgs(objectsToRemove));

        }

        /// <summary>
        /// Delete objects
        /// </summary>
        /// <param name="pnObjects">Pn objects</param>
        public void DeleteObjects(List<Mesh3D> pnObjects)
        {
            if (IsNotReadonly)
            {
                foreach (PNObject pnObject in pnObjects)
                    DeleteObject(pnObject);

                if (pnObjects.Count > 0)
                {
                    IFormulaManager formulaManager = App.GetObject<IFormulaManager>();
                    formulaManager.IsNeedToCompile = true;
                }

                eventPublisher.ExecuteEvents(new PNObjectsDeletedEventArgs(pnObjects));
            }
        }

        /// <summary>
        /// Change object name in finded formulas of all available objects.
        /// If new value is null then formula will be removed (it's used in DeleteObject method)
        /// </summary>
        /// <param name="oldName">Old object name</param>
        /// <param name="newName">New object name. If new value is null then formula will be removed</param>
        private void ChangeNameInFormulas(string oldName, string newName)
        {
            foreach (PNObject pnObject in PNObjectRepository.PNObjects.Values)
            {
                if (pnObject is IExtendedFormula)
                {
                    IExtendedFormula ieFormula = (IExtendedFormula)pnObject;
                    if (ieFormula.TransitionGuardFormula != null && ieFormula.TransitionGuardFormula.Contains(oldName))
                    {
                        ieFormula.TransitionGuardFormula =
                            (newName == null) ? null : ieFormula.TransitionGuardFormula.Replace(oldName, newName);
                    }
                }
                if (pnObject is IFormula)
                {
                    IFormula iFormula = (IFormula)pnObject;
                    if (iFormula.Formula != null && iFormula.Formula.Contains(oldName))
                    {
                        iFormula.Formula = (newName == null) ? null : iFormula.Formula.Replace(oldName, newName);
                    }
                }
            }
        }

        /// <summary>
        /// Set new name for indicated PN object
        /// </summary>
        /// <param name="pnObject">PN object</param>
        /// <param name="name">Name</param>
        public void SetPNObjectName(PNObject pnObject, string name)
        {
            string oldName = pnObject.ToString();

            IsNameValid(PNObjectRepository.PNObjects.Values, pnObject, name);

            pnObject.Name = name;
            ChangeNameInFormulas(oldName, pnObject.ToString());
        }

        /// <summary>
        /// Check whether provided name is valid and can be accepted as a new name of PN object.
        /// Throws ArgumentException if name is not eligible
        /// </summary>
        /// <param name="pnObjects">PN objects</param>
        /// <param name="pnObject">PN object. It's used to strict searching area by concrete membrane
        /// using its group id and also to skip it during searching</param>
        /// <param name="name">Name to check</param>
        /// <exception cref="ArgumentException">Throws ArgumentException if name is not eligible because it's
        /// already exists in membrane</exception>
        /// <returns>Is valid</returns>
        private bool IsNameValid(ICollection<PNObject> pnObjects, PNObject pnObject, string name)
        {
            foreach (PNObject membraneObject in pnObjects)
            {
                if (pnObject.ID != membraneObject.ID &&
                    membraneObject.Group == pnObject.Group && membraneObject.Name.Equals(name))
                    throw new ArgumentException(Properties.Messages.Default.NameAlreadyExists);
            }
            return true;
        }


        /// <summary>
        /// Clear history data from all objects
        /// </summary>
        public void ClearHistory()
        {
            foreach (PNObject obj in PNObjectRepository.PNObjects.Values)
            {
                if (obj.AllowSaveHistory)
                    obj.ObjectHistory.Clear();
                obj.AllowSaveHistory = false;
            }
        }
        #endregion
    }


}
