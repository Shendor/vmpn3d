using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PNCreator.Controls;
using _3DTools;
using System.Windows.Media.Media3D;
using Meshes3D;
using PNCreator.ManagerClasses;
using PNCreator.Controls.CarcassControl;
using PNCreator.Controls.VectorControl;
using PNCreator.Controls.SectorControl;

namespace PNCreator.Modules.FormulaBuilder
{

    public partial class GeometryFormulaBuilder
    {
        private const double DEFAULT_VECTOR_RADIUS = 85;

        private CustomMesh selectedMesh;
        private CustomMesh selectedPrimitive;
        private CarcassPoint selectedPoint;
        private CustomVector selectedVector;
        private Sector selectedSector;
        private int selectedMeshIndex;
        private readonly SectoredCircle frontSectoredCircle;
        private readonly SectoredCircle topSectoredCircle;
        private readonly SectoredCircle leftSectoredCircle;
        private SectoredCircle selectedSectoredCircle;
        private readonly VectorCarcass[] vectorCarcasses;
        private readonly SectoredCircle[] sectors;
        private BoundingBox frontSelectionBox;
        private BoundingBox leftSelectionBox;
        private BoundingBox topSelectionBox;
        private BoundingBox perspectiveSelectionBox;

        public GeometryFormulaBuilder()
        {
            frontSectoredCircle = new SectoredCircle();
            frontSectoredCircle.Name = "Front";
            topSectoredCircle = new SectoredCircle(new Point3D(), new Vector3D(0, 90, 0));
            topSectoredCircle.Name = "Top";
            leftSectoredCircle = new SectoredCircle(new Point3D(), new Vector3D(90, 0, 0));
            leftSectoredCircle.Name = "Left";
            frontSelectionBox = new BoundingBox();
            topSelectionBox = new BoundingBox();
            leftSelectionBox = new BoundingBox();
            perspectiveSelectionBox = new BoundingBox();

            InitializeComponent();

            vectorCarcasses = new[] { perspectiveProjection, frontProjection, topProjection, leftProjection };
            sectors = new [] { frontSectoredCircle, topSectoredCircle };
            selectedMeshIndex = -1;

            frontProjection.Viewport3D.Children.Add(frontSectoredCircle);
            topProjection.Viewport3D.Children.Add(topSectoredCircle);
            leftProjection.Viewport3D.Children.Add(leftSectoredCircle);
            frontProjection.AddMesh(frontSelectionBox);
            topProjection.AddMesh(topSelectionBox);
            leftProjection.AddMesh(leftSelectionBox);
            perspectiveProjection.AddMesh(perspectiveSelectionBox);
        }

        #region Events

        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.OriginalSource;
            switch (btn.Name)
            {
                case "objOptionsBtn":
                    {
                        if (vectorOptionPanel.Visibility == Visibility.Visible)
                        {
                            vectorOptionPanel.Visibility = Visibility.Collapsed;
                            return;
                        }
                        vectorOptionPanel.Visibility = Visibility.Visible;
                    }
                    break;
                case "okBtn":
                    GetSelectedValue();
                    break;
                case "cancelBtn":
                    this.Hide();
                    break;
            }

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                RemoveObject();
        }

        private void vectorCarcass_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HidePopupPanels();

            if (selectBtn.IsChecked == true)
            {
                SelectMesh(sender as MultiobjectCarcass);
            }
            else if (deleteBtn.IsChecked == true)
            {
                RemoveObject();
            }
        }

        private void Popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HidePopupPanels();
        }

        private void HidePopupPanels()
        {
            positionOptionPanel.IsOpen = false;
            sectorOptionPanel.IsOpen = false;
        }

        private void NumericBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            NumericBox.NumericBox nb = sender as NumericBox.NumericBox;
            int value = (int)nb.Value;
            if (nb == frontSectors)
            {
                frontSectoredCircle.SectorCount = value;
            }
            else if (nb == topSectors)
            {
                topSectoredCircle.SectorCount = value;
            }
            else if (nb == leftSectors)
            {
                leftSectoredCircle.SectorCount = value;
            }
            else if (nb == frontVectors)
            {
                AddVectors(VectorType.Axis, frontProjection, value);
            }
            else if (nb == topVectors)
            {
                AddVectors(VectorType.Axis, topProjection, value);
            }
            else if (nb == leftVectors)
            {
                AddVectors(VectorType.Axis, leftProjection, value);
            }
            else if (nb == perspeciveVectors)
            {
                AddVectors(VectorType.Vector, perspectiveProjection, value);
            }
            else if (nb == sectorAngle)
            {
                selectedSectoredCircle.SetAngle(selectedSector, value);
            }
        }

        private void AddVectors(VectorType vectorType, VectorCarcass vectorCarcass, int count)
        {
            frontSelectionBox.Clear();
            topSelectionBox.Clear();
            leftSelectionBox.Clear();
            perspectiveSelectionBox.Clear();

            if (vectorCarcass == perspectiveProjection)
            {
                if (vectorCarcasses == null)
                    return;
                frontProjection.RemoveAllVectors(VectorType.Vector);
                topProjection.RemoveAllVectors(VectorType.Vector);
                leftProjection.RemoveAllVectors(VectorType.Vector);

                Point3D[] circlePoints = MathUtils.GetCirclePoints(count);

                Point3D point = new Point3D();
                foreach (VectorCarcass carcass in vectorCarcasses)
                {
                    AddVectors(vectorType, carcass, count, point, circlePoints);
                }
            }
            else
            {
                Point3D[] circlePoints = MathUtils.GetCirclePoints(count,
                                                vectorCarcass.ViewportOrientation, DEFAULT_VECTOR_RADIUS);
                Point3D orientation = vectorCarcass.ViewportOrientation;
                AddVectors(vectorType, vectorCarcass, count,
                    new Point3D(-orientation.X, -orientation.Y, -orientation.Z), circlePoints);
            }
        }

        private void AddVectors(VectorType vectorType, VectorCarcass vectorCarcass, int count,
                                    Point3D startPointBasis, Point3D[] circlePoints)
        {
            vectorCarcass.RemoveAllVectors(vectorType);
            for (int i = 0; i < count; i++)
            {
                Point3D startPoint = new Point3D(startPointBasis.X * circlePoints[i].X,
                    startPointBasis.Y * circlePoints[i].Y, startPointBasis.Z * circlePoints[i].Z);
                vectorCarcass.AddVector(vectorType, vectorType.ToString() + i, startPoint, circlePoints[i]);
            }
        }

        private void VectorPosition_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectBtn.IsChecked == false || ((NumericBox.NumericBox)sender).IsEnabled == false)
                return;

            if (selectedVector != null)
            {
                double value = ((NumericBox.NumericBox)sender).Value;

                Point3D startPoint = selectedVector.StartPoint;
                Point3D endPoint = selectedVector.EndPoint;

                if (sender == vectorStartX)
                    startPoint.X = value;
                else if (sender == vectorStartY)
                    startPoint.Y = value;
                else if (sender == vectorStartZ)
                    startPoint.Z = value;
                else if (sender == vectorEndX)
                    endPoint.X = value;
                else if (sender == vectorEndY)
                    endPoint.Y = value;
                else if (sender == vectorEndZ)
                    endPoint.Z = value;

                selectedVector.ChangeVectorPosition(startPoint, endPoint);

                int index = GetIndexOfSelectedVector();
                if (index != -1)
                {
                    foreach (VectorCarcass carcass in vectorCarcasses)
                    {
                        Point3D startPoint2 = MathUtils.MultiplyPoints(carcass.ViewportOrientation, startPoint);
                        Point3D endPoint2 = MathUtils.MultiplyPoints(carcass.ViewportOrientation, endPoint);
                        CustomVector vector = carcass.Vectors[VectorType.Vector][index];
                        vector.ChangeVectorPosition(startPoint2, endPoint2);
                        //carcass.Vectors[VectorType.Vector][index].ChangeNamePosition();
                    }
                }
                frontSelectionBox.BuildBoundingBox(selectedVector);
                topSelectionBox.BuildBoundingBox(selectedVector);
                leftSelectionBox.BuildBoundingBox(selectedVector);
                perspectiveSelectionBox.BuildBoundingBox(selectedVector);
            }
        }

        private int GetIndexOfSelectedVector()
        {
            foreach (VectorCarcass carcass in vectorCarcasses)
            {
                int index = carcass.GetIndexOfSelectedVector(VectorType.Vector);
                if (index != -1)
                {
                    return index;
                }
            }
            return -1;
        }

        private void Position_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectBtn.IsChecked == false || positionOptionPanel.IsOpen == false)
                return;

            if (selectedPoint != null)
            {
                selectedPoint.Position = new Point3D(xPosition.Value, yPosition.Value, zPosition.Value);

                foreach (CustomVector vector in perspectiveProjection.SelectedVectors)
                {
                    Point3D position = new Point3D(xPosition.Value, yPosition.Value, zPosition.Value);
                    vector.ChangeVectorPosition(new Point3D(), position);
                }

                perspectiveProjection.DestroyCarcassWire();
            }
            else if (selectedMesh != null)
            {
                perspectiveProjection.SelectedMesh.Position = new Point3D(xPosition.Value, yPosition.Value, zPosition.Value);
            }
            else if (selectedPrimitive != null)
            {
                selectedPrimitive.Position = new Point3D(xPosition.Value, yPosition.Value, zPosition.Value);
            }
        }

        private void Size_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectBtn.IsChecked == false || positionOptionPanel.IsOpen == false)
                return;

            if (selectedPoint != null)
            {
                selectedPoint.Size = size.Value;
            }
            else if (selectedMesh != null)
            {
                perspectiveProjection.SelectedMesh.Size =
                    topProjection.SelectedMesh.Size =
                     frontProjection.SelectedMesh.Size =
                       leftProjection.SelectedMesh.Size = size.Value;
            }
            else if (selectedPrimitive != null)
            {
                selectedPrimitive.Size = size.Value;
            }

        }

        private void Rotation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (selectBtn.IsChecked == false || positionOptionPanel.IsOpen == false)
                return;

            if (selectedPoint != null)
            {
                selectedPoint.AngleX = xRotation.Value;
                selectedPoint.AngleY = yRotation.Value;
                selectedPoint.AngleZ = zRotation.Value;
            }
            else if (selectedMesh != null)
            {
                perspectiveProjection.SelectedMesh.AngleX =
                    topProjection.SelectedMesh.AngleX =
                    frontProjection.SelectedMesh.AngleX =
                    leftProjection.SelectedMesh.AngleX = xRotation.Value;

                perspectiveProjection.SelectedMesh.AngleY =
                    topProjection.SelectedMesh.AngleY =
                    frontProjection.SelectedMesh.AngleY =
                    leftProjection.SelectedMesh.AngleY = yRotation.Value;

                perspectiveProjection.SelectedMesh.AngleZ =
                    topProjection.SelectedMesh.AngleZ =
                    frontProjection.SelectedMesh.AngleZ =
                    leftProjection.SelectedMesh.AngleZ = zRotation.Value;
            }
            else if (selectedPrimitive != null)
            {
                selectedPrimitive.AngleX = xRotation.Value;
                selectedPrimitive.AngleY = yRotation.Value;
                selectedPrimitive.AngleZ = zRotation.Value;
            }
        }

        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            switch (btn.Name)
            {
                case "newBtn":
                    PNDocument.NewGeometryFormula(new List<VectorProjectionCarcass>() { perspectiveProjection, topProjection, frontProjection, leftProjection });
                    break;
                case "openBtn":
                    PNDocument.OpenGeometryFormula(vectorCarcasses, sectors,
                                                    DialogBoxes.OpenDialog(null, DocumentFormat.XML));
                    break;
                case "saveBtn":
                    PNDocument.SaveGeometryFormula(vectorCarcasses, sectors,
                                                    DialogBoxes.SaveDialog(null, DocumentFormat.XML));
                    break;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;

            switch (lb.Name)
            {
                case "projectionList":
                    anglesList.SelectedItem = null;
                    break;
                case "anglesList":
                    projectionList.SelectedItem = null;
                    break;
            }
        }

        private void ChangeNamesPosition()
        {
            foreach (VectorCarcass carcass in vectorCarcasses)
            {
                foreach (List<CustomVector> vectors in carcass.Vectors.Values)
                {
                    foreach (CustomVector vector in vectors)
                    {
                        vector.ChangeNamePosition();
                    }
                }
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeNamesPosition();
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            ChangeNamesPosition();
        }

        private void vectorName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (selectedVector != null)
                {
                    selectedVector.Name.Text = vectorName.Text;
                    int index = GetIndexOfSelectedVector();
                    if (index != -1)
                    {
                        foreach (VectorCarcass carcass in vectorCarcasses)
                        {
                            carcass.Vectors[VectorType.Vector][index].Name.Text = vectorName.Text;
                        }
                    }
                }
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, EventArgs e)
        {
            if (selectedVector != null)
            {
                selectedVector.SetMaterial(vectorColor.SelectedColor);
                int index = GetIndexOfSelectedVector();
                if (index != -1)
                {
                    foreach (VectorCarcass carcass in vectorCarcasses)
                    {
                        carcass.Vectors[VectorType.Vector][index].SetMaterial(vectorColor.SelectedColor);
                    }
                }
            }
            else if (selectedPoint != null)
            {
                selectedPoint.MaterialColor = meshColor.SelectedColor;
            }
            else if (selectedMesh != null)
            {
                selectedMesh.MaterialColor = meshColor.SelectedColor;
            }
            else if (selectedPrimitive != null)
            {
                selectedPrimitive.MaterialColor = meshColor.SelectedColor;
            }
            else if (selectedSector != null)
            {
                selectedSector.MaterialColor = sectorColor.SelectedColor;
            }
        }

        private void Grid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UnselectMesh();
        }

        #endregion

        #region Private methods

        private void EnableVectorPositionControls(bool isEnabled)
        {
            vectorStartX.IsEnabled = isEnabled;
            vectorStartY.IsEnabled = isEnabled;
            vectorStartZ.IsEnabled = isEnabled;

            vectorEndX.IsEnabled = isEnabled;
            vectorEndY.IsEnabled = isEnabled;
            vectorEndZ.IsEnabled = isEnabled;
        }

        private void SelectMesh(AbstractMultiobjectCarcass abstractCarcass)
        {
            foreach (VectorCarcass carcass in vectorCarcasses)
            {
                if (carcass != abstractCarcass)
                    carcass.UnselectAllObjects();
            }

            selectedVector = null;
            selectedMesh = null;
            selectedPoint = null;
            selectedPrimitive = null;
            selectedSector = null;

            ClearVectorData();
            VectorCarcass vCarcass = (VectorCarcass)abstractCarcass;
            selectedMesh = vCarcass.SelectedMesh;
            selectedMeshIndex = vCarcass.Meshes.IndexOf(selectedMesh);

            bool isEnable = (GetIndexOfSelectedVector() != -1 && abstractCarcass == perspectiveProjection) ||
                (GetIndexOfSelectedVector() == -1 && abstractCarcass != perspectiveProjection);

            EnableVectorPositionControls(isEnable);
            if (vCarcass.SelectedVector != null)
            {
                InitializeVectorData(vCarcass);
                frontSelectionBox.BuildBoundingBox(vCarcass.SelectedVector);
                topSelectionBox.BuildBoundingBox(vCarcass.SelectedVector);
                perspectiveSelectionBox.BuildBoundingBox(vCarcass.SelectedVector);
            }
            if (abstractCarcass != perspectiveProjection)
            {
                selectedSector = frontSectoredCircle.GetSectorByMesh(abstractCarcass.SelectedGeometry);
                if (selectedSector == null)
                    selectedSector = topSectoredCircle.GetSectorByMesh(abstractCarcass.SelectedGeometry);
                if (selectedSector == null)
                    selectedSector = leftSectoredCircle.GetSectorByMesh(abstractCarcass.SelectedGeometry);

                if (selectedSector != null)
                {
                    InitializeSectorProperties(abstractCarcass);
                }
            }
            else
            {
                selectedMesh = vCarcass.SelectedMesh;
                selectedPrimitive = vCarcass.SelectedPrimitive;
                selectedMeshIndex = vCarcass.Meshes.IndexOf(selectedMesh);
            }

            if (selectedMesh != null)
            {
                SelectMesh();
                InitializeSelectedMeshTransformations(perspectiveProjection.SelectedMesh);
            }
            else if (selectedPrimitive != null)
            {
                InitializeSelectedMeshTransformations(selectedPrimitive);
            }

            abstractCarcass.SelectedGeometry = null;
        }

        private void InitializeVectorData(VectorCarcass vCarcass)
        {
            vectorOptionPanel.IsEnabled = true;

            selectedVector = vCarcass.SelectedVector;
            InitializeSelectedVectorPosition();
            vectorOptionPanel.DataContext = vCarcass.VectorData;
            if (vCarcass.SelectedVector.Color.Equals(PNColors.Selection))
                vectorColor.SelectedColor = vCarcass.VectorData.Color;

        }

        private void SelectMesh()
        {
            if (selectedMeshIndex.Equals(-1))
                return;
            perspectiveProjection.SelectMesh(selectedMeshIndex);
            //topProjection.SelectMesh(selectedMeshIndex);
            frontProjection.SelectMesh(selectedMeshIndex);
            //leftProjection.SelectMesh(selectedMeshIndex);
        }

        private void ClearVectorData()
        {
            vectorOptionPanel.DataContext = null;
            vectorOptionPanel.IsEnabled = false;
        }

        private void UnselectMesh()
        {
            if (selectedMeshIndex.Equals(-1))
                return;
            perspectiveProjection.UnselectAllObjects();
            topProjection.UnselectAllObjects();
            frontProjection.UnselectAllObjects();
            leftProjection.UnselectAllObjects();
        }

        private void GetSelectedValue()
        {
            double value = 0;
            if (anglesList.SelectedItem != null)
            {
                if (anglesList.SelectedItem.GetType().Equals(typeof(VectorAngle)))
                    if (this.Owner.GetType().Equals(typeof(FormulaBuilder)))
                    {
                        value = ((VectorAngle)anglesList.SelectedItem).Angle;
                    }
            }
            else if (projectionList.SelectedItem != null)
            {
                value = ((VectorProjection)projectionList.SelectedItem).Projection;
            }

            if (this.Owner.GetType().Equals(typeof(FormulaBuilder)))
            {
                ((FormulaBuilder)this.Owner).formulaTB.Text += value;
                this.Hide();
            }
        }

        private void RemoveObject()
        {
            if (perspectiveProjection.SelectedVector != null)
            {
                //int index = perspectiveProjection.Vectors.IndexOf(perspectiveProjection.SelectedVector);

                //topProjection.RemoveVector(index);
                //frontProjection.RemoveVector(index);
                //leftProjection.RemoveVector(index);

                //perspectiveProjection.RemoveVector(perspectiveProjection.SelectedVector);
                //vectorOptionPanel.DataContext = null;
            }
            else if (!selectedMeshIndex.Equals(-1))
            {
//                RemoveSelectedMesh();
            }
            else if (selectedPrimitive != null)
            {
//                topProjection.RemoveSelectedPrimitive();
//                frontProjection.RemoveSelectedPrimitive();
//                leftProjection.RemoveSelectedPrimitive();

            }
            //UnselectMesh();
        }

        private void RemoveSelectedMesh()
        {
            perspectiveProjection.RemoveSelectedMesh();
            topProjection.RemoveSelectedMesh();
            frontProjection.RemoveSelectedMesh();
            leftProjection.RemoveSelectedMesh();
        }

        private void InitializeSelectedVectorPosition()
        {
            vectorStartX.Value = selectedVector.StartPoint.X;
            vectorStartY.Value = selectedVector.StartPoint.Y;
            vectorStartZ.Value = selectedVector.StartPoint.Z;

            vectorEndX.Value = selectedVector.EndPoint.X;
            vectorEndY.Value = selectedVector.EndPoint.Y;
            vectorEndZ.Value = selectedVector.EndPoint.Z;
        }

        private void InitializeSectorProperties(AbstractMultiobjectCarcass carcass)
        {
            SetPopupPosition(new Point3D(), sectorOptionPanel);
            if (carcass == frontProjection)
            {
                selectedSectoredCircle = frontSectoredCircle;
            }
            else if (carcass == topProjection)
            {
                selectedSectoredCircle = topSectoredCircle;
            }
            else if (carcass == leftProjection)
            {
                selectedSectoredCircle = leftSectoredCircle;
            }
            sectorAngle.Value = selectedSectoredCircle.GetAngle(selectedSector);
        }

        private void SetPopupPosition(Point3D position, System.Windows.Controls.Primitives.Popup popup)
        {
            popup.IsOpen = true;
            //Point point = MathUtils.Convert3DPoint(position, vectorCarcass.Viewport3D);
            // popup.HorizontalOffset = point.X;
            //popup.VerticalOffset = point.Y;
        }

        private void InitializeSelectedMeshTransformations(Mesh3D mesh)
        {
            xPosition.Value = mesh.Position.X;
            yPosition.Value = mesh.Position.Y;
            zPosition.Value = mesh.Position.Z;

            xRotation.Value = mesh.AngleX;
            yRotation.Value = mesh.AngleY;
            zRotation.Value = mesh.AngleZ;

            size.Value = mesh.Size;

            positionOptionPanel.IsOpen = true;
        }

        #endregion

        #region Commands
        private void OnWindowClose(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.CloseWindow(this);
        }

        private void OnWindowMaximize(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
                this.WindowState = System.Windows.WindowState.Normal;
            else
                Microsoft.Windows.Shell.SystemCommands.MaximizeWindow(this);
        }

        private void OnWindowMinimize(object sender, ExecutedRoutedEventArgs e)
        {
            Microsoft.Windows.Shell.SystemCommands.MinimizeWindow(this);
        }
        #endregion

    }
}
