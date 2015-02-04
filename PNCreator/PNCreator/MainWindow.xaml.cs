using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.Modules.NavigationMap;
using PNCreator.Modules.Properties;
using PNCreator.Modules.Searching;
using PNCreator.PNObjectsIerarchy;
using WindowsControl;
using _3DTools;
using Meshes3D;
using PNCreator.Properties;
using PNCreator.Commands;
using PNCreator.ManagerClasses.Exception;
using PNCreator.Controls;
using PNCreator.ManagerClasses.MeshPicker;
using PNCreator.ManagerClasses.EventManager;
using PNCreator.Controls.Positioniser;
using PNCreator.Controls.Progress;

namespace PNCreator
{
    public partial class MainWindow
    {

        #region Variables

        private readonly WindowsFactory windowsFactory;
        private EventPublisher eventPublisher;
        public PNObjectManager objectManager;

        private BoundingBox selectionBox;
        public PNObjectPicker pnObjectPicker;
        private SelectionRectangle selectionRectangle;
        private PNObjectPositioniser positioniser;
        private bool isMultiselectionMode;
        private ProgressWindow progressWindow;
        private List<SelectionRectangle> drawnSelectionRectangles;

        #endregion

        public MainWindow()
        {
            drawnSelectionRectangles = new List<SelectionRectangle>();
            pnObjectPicker = new PNObjectPicker();
            objectManager = App.GetObject<PNObjectManager>();
            windowsFactory = App.GetObject<WindowsFactory>();

            InitializeComponent();

            DataContext = objectManager;

            RegisterEvents();
        }

        #region Events

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            eventPublisher.ExecuteEvents(NewNetEventArgs.Empty);

            pnViewport.Trackball.InitializeCamera(new Vector3D(0, 0, 1250), new Point3D(0, 0, -650),
                                                  new Vector3D(0, 0, -650), new Vector3D(0, 0, -50), 0, 19.2);
            pnViewport.SetCameraView(CameraView.Perspective);
            PNProperties.LoadProperties();
        }

        private void OptionsButtonsClick(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.OriginalSource;
            if (btn.Equals(objOptionsBtn))
            {
                if (objectOptionPanel.Visibility == Visibility.Visible)
                {
                    objectOptionPanel.Visibility = Visibility.Hidden;
                    return;
                }
                objectOptionPanel.Visibility = Visibility.Visible;
                positioniserOptionPanel.Visibility = Visibility.Hidden;
            }
            else if (btn.Equals(positioniserOptionBtn))
            {
                if (positioniserOptionPanel.Visibility == Visibility.Visible)
                {
                    positioniserOptionPanel.Visibility = Visibility.Hidden;
                    return;
                }
                positioniserOptionPanel.Visibility = Visibility.Visible;
                objectOptionPanel.Visibility = Visibility.Hidden;
            }
        }

        private void ViewportMouseMove(object sender, MouseEventArgs e)
        {
            if (isMultiselectionMode)
            {
                var drawPoint = e.GetPosition(pnViewport.Viewport3D);
                selectionRectangle.Draw(drawPoint);
            }
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (pnViewport.Trackball.IsFocused || pnViewport.Content2D.IsFocused)

                switch (e.Key)
                {
                    //case Key.Q: PNCreator.Test.OverloadTest.Test(objectManager, pnViewport); break;
                    case Key.Delete:
                        new DeletePNObjectsCommand().Delete(pnObjectPicker.SelectedObjects);
                        break;
                    case Key.M:
                        new NavigationMap(objectManager, pnViewport.Trackball, this, pnViewport.Viewport3D).Show();
                        break;
                    case Key.Add:
                        pnViewport.Trackball.Zoom(120);
                        break;
                    case Key.Subtract:
                        pnViewport.Trackball.Zoom(-120);
                        break;
                    case Key.Z:
                        {
                            if (pnObjectPicker.SelectedObject != null)
                                pnViewport.SetCameraPosition(pnObjectPicker.SelectedObject.Position);
                        }
                        break;
                }

        }

        /// <summary>
        /// Add new PN object
        /// </summary>
        private void MainPanelMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            pnViewport.Trackball.Focus();

            if (objectManager.TypeOfAddObject == PNObjectTypes.None)
            {
                selectionRectangle = new SelectionRectangle(e.GetPosition(pnViewport.Viewport3D));
                isMultiselectionMode = true;
                pnViewport.Content2D.Children.Add(selectionRectangle.Rectangle);
                drawnSelectionRectangles.Add(selectionRectangle);
            }

            if (objectManager.IsNotReadonly)
            {
                try
                {
                    PNObject newPnObject = null;
                    switch (objectManager.TypeOfAddObject)
                    {
                        case PNObjectTypes.None:
                            return;
                        case PNObjectTypes.Membrane:
                        case PNObjectTypes.StructuralMembrane:
                        case PNObjectTypes.DiscreteLocation:
                        case PNObjectTypes.DiscreteTransition:
                        case PNObjectTypes.ContinuousLocation:
                        case PNObjectTypes.ContinuousTransition:
                            {
                                if (pnObjectPicker.HitPoint != null)
                                    newPnObject = objectManager.BuildShape((Point3D)pnObjectPicker.HitPoint);
                                break;
                            }

                        default:
                            if (pnObjectPicker.SelectedObject is Shape3D)
                                newPnObject = objectManager.BuildArc((Shape3D)pnObjectPicker.SelectedObject);
                            break;
                    }
                    if (newPnObject != null)
                    {
                        pnViewport.AddPNObject(newPnObject);
                    }
                }
                catch (IllegalPNObjectException ex)
                {
                    UnselectObject();
                    ExceptionHandler.HandleException(ex);
                }
            }

        }

        private void UnselectObject()
        {
            selectionBox.Clear();
            pnObjectPicker.SelectedObjects.Clear();
        }

        /// <summary>
        /// Select many PN objects
        /// </summary>
        private void MainPanelMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isMultiselectionMode)
            {
                if (selectionRectangle.IsAbleToSelect)
                {
                    pnObjectPicker.SelectMultipleMeshes(selectionRectangle.Bound, pnViewport.Viewport3D);
                    ShowSelectedObjectProperties(pnObjectPicker.SelectedObjects);
                }
                foreach (var rectangle in drawnSelectionRectangles)
                {
                    pnViewport.Content2D.Children.Remove(rectangle.Rectangle);
                }
                drawnSelectionRectangles.Clear();
                selectionRectangle = null;
                isMultiselectionMode = false;
                pnViewport.Trackball.Focus();
            }
        }

        /// <summary>
        /// Select PN object
        /// </summary>
        public void ViewportMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePosition = e.GetPosition(pnViewport.Viewport3D);
            //            try
            //            {
            pnViewport.Trackball.Focus();
            var pnObject = (PNObject)pnObjectPicker.SelectMesh(mousePosition, pnViewport.Viewport3D);
            if (pnObject != null)
                ShowSelectedObjectProperties(pnObject);
            //            }
            //            catch (Exception ex)
            //            {
            //                ExceptionHandler.HandleException(ex);
            //            }
        }

        private void PositioniserPropertyParametersChanged(object sender, RoutedPropertyChangedEventArgs<Grid3DParameters> e)
        {
            positioniser.Build(e.NewValue);
        }

        private void WindowClosed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        #endregion

        #region Other Methods

        public void ShowSelectedObjectProperties(PNObject pnObject)
        {
            if (pnObjectPicker.SelectedObject != null && pnObject.Equals(pnObjectPicker.SelectedObject))
            {
                pnObjectPropertiesPanel.ShowPropertiesPanelForPNObject(pnObject);
                selectionBox.BuildBoundingBox(pnObject.Bounds);
            }
        }

        public void ShowSelectedObjectProperties(List<Mesh3D> selectedObjects)
        {
            selectionBox.BuildBoundingBox(selectedObjects);
            pnObjectPropertiesPanel.ShowPropertiesPanelForManyPNObjects(selectedObjects);
        }

        protected override void Exit()
        {
            if (PNProperties.IsConfirmExit)
            {
                ButtonPressed bp = DialogWindow.Confirm(Messages.Default.ExitFromProgramm);
                if (bp == ButtonPressed.Yes)
                    Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
            }
            else
                Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void RegisterEvents()
        {
            eventPublisher = App.GetObject<EventPublisher>();

            eventPublisher.Register((MeshAddedEventArgs<Token> args) => pnViewport.Viewport3D.Children.Add(args.Mesh.Model));

            eventPublisher.Register((MeshesRemovedEventArgs<Token> args) =>
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)delegate
                        {
                            foreach (Token token in args.Meshes)
                            {
                                pnViewport.Viewport3D.Children.Remove(token.Model);
                            }
                            args.Meshes.Clear();
                        }));

            eventPublisher.Register((PNObjectsDeletedEventArgs args) =>
            {
                foreach (PNObject pnObhject in args.Meshes)
                {
                    if (pnObjectPicker.SelectedObjects.Contains(pnObhject))
                    {
                        selectionBox.Clear();
                    }
                    pnViewport.RemovePNObject(pnObhject);
                }
            });

            eventPublisher.Register((PNObjectChangedEventArgs args) => ShowSelectedObjectProperties(args.PNObject));

            eventPublisher.Register((BooleanEventArgs args) =>
                {
                    positioniser.SetVisible(args.IsChecked);
                    positioniserOptionPanel.IsEnabled = args.IsChecked;
                });

            eventPublisher.Register((OpenNetEventArgs args) =>
            {
                foreach (var pnObject in args.PNObjects.Values)
                {
                    pnViewport.AddPNObject(pnObject);
                }
            });

            eventPublisher.Register((NewNetEventArgs args) =>
            {
                pnViewport.ClearAll();

                selectionBox = new BoundingBox();
                positioniser = new PNObjectPositioniser();
                pnViewport.AddMesh(selectionBox);
                pnViewport.AddMesh(positioniser);
            });

            eventPublisher.Register((MeshTransformChangedEventArgs args) =>
                selectionBox.BuildBoundingBox(args.Meshes));

            eventPublisher.Register((PNObjectsValuesChangedEventArgs args) =>
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                    (ThreadStart)(() =>
                    {
                        foreach (var pnObject in args.PNObjects)
                        {
                            pnObject.ValueInCanvas.Text = string.Format(PNObject.DOUBLE_FORMAT, PNObjectRepository.PNObjects.GetDoubleValue(pnObject));
                        }
                    }));
            });

            eventPublisher.Register((FormulaProgressEventArgs progressArgs) =>
                                        Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                                            (ThreadStart)(() =>
                                            {
                                                if (progressWindow == null || !progressWindow.IsVisible)
                                                {
                                                    progressWindow = new ProgressWindow();
                                                    progressWindow.ShowDialog();
                                                }
                                                progressWindow.Minimum = progressArgs.MinimumProgress;
                                                progressWindow.Maximum = progressArgs.MaximumProgress;
                                                progressWindow.Progress = progressArgs.Progress;
                                            })));
        }

        #endregion

        #region Commands

        private void CanCommandExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = App.GetObject<PNObjectManager>().IsNotReadonly;
        }

        private void CommandsExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var cmd = e.Command as RoutedCommand;
            if (cmd == null)
                return;
            if (cmd == MPNCommands.PerspectiveView)
            {
                pnViewport.SetCameraView(CameraView.Perspective);
            }
            else if (cmd == MPNCommands.FrontView)
            {
                pnViewport.SetCameraView(CameraView.Front);
            }
            if (cmd == ApplicationCommands.New)
            {
                new NewNetCommand().NewNet();
            }
            else if (cmd == ApplicationCommands.Open)
            {
                new OpenNetCommand().OpenNet();
            }
            else if (cmd == ApplicationCommands.Save)
            {
                new SaveNetCommand().SaveNet();
            }
            else if (cmd == ApplicationCommands.SaveAs)
            {
                new SaveNetCommand().SaveNetAs();
            }
            else if (cmd == ApplicationCommands.Delete)
            {
                new DeletePNObjectsCommand().Delete(pnObjectPicker.SelectedObjects);
            }
            else if (cmd == ApplicationCommands.Find)
            {
                windowsFactory.GetWindow(typeof(Searching)).Show();
            }
            else if (cmd == ApplicationCommands.Properties)
            {
                windowsFactory.GetWindow(typeof(Modules.Properties.Properties)).Show();
            }
        }
        #endregion

    }
}
