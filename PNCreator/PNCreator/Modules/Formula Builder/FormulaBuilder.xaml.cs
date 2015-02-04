using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PNCreator.Controls.Progress;
using PNCreator.ManagerClasses;
using PNCreator.ManagerClasses.EventManager.Events;
using PNCreator.PNObjectsIerarchy;
using WindowsControl;
using RuntimeCompiler.FormulaCompiler;
using PNCreator.Properties;
using PNCreator.ManagerClasses.Exception;
using PNCreator.ManagerClasses.FormulaManager;
using PNCreator.ManagerClasses.EventManager;

namespace PNCreator.Modules.FormulaBuilder
{
    /// <summary>
    /// Логика взаимодействия для FormulaBuilder.xaml
    /// </summary>
    public partial class FormulaBuilder : Window
    {
        private GeometryFormulaBuilder gfb;
        private string formulaString;
        private FormulaTypes formulaType;
        private double result;
        private bool boolResult;
        private int previousCaretPosition;
        private PNObject pnObject;

        public FormulaBuilder(FormulaTypes formulaType, PNObject pnObject)
        {
            if (!(pnObject is IFormula))
                throw new IllegalPNObjectException("This object does not have formula");
            InitializeComponent();

            this.pnObject = pnObject;
            this.formulaType = formulaType;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillNameList("");
            ParseFormula();
        }

        /// <summary>
        /// 
        /// </summary>
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }
        /// <summary>
        /// 
        /// </summary>
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            objectsList = null;
            functionsList = null;
            GC.Collect();
        }

        /// <summary>
        /// Parse formula. Transfrom it to object names if there are some
        /// </summary>
        private void ParseFormula()
        {
            string formulaString;
            switch (formulaType)
            {
                case FormulaTypes.Guard:
                    formulaString = ((IExtendedFormula)pnObject).TransitionGuardFormula;
                    break;
                default:
                    formulaString = ((IFormula)pnObject).Formula;
                    break;
            }
            formulaTB.Text = FormulaConverter.ToObjectNames(formulaString);
        }

        /// <summary>
        /// 
        /// </summary>
        private void objectsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PasteTextToFormula(objectsList.SelectedItem.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        private void Buttons_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            PasteTextToFormula(btn.Content.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        private void OtherButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)e.OriginalSource;
            switch (btn.Name)
            {
                case "okBtn":
                    try
                    {
                        AttachFormula();
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.HandleException(ex);
                    }
                    break;
                case "cancelBtn":
                    this.Close();
                    break;
                case "checkBtn":
                    if (CheckFormula())
                        DialogWindow.Alert(Messages.Default.FormulaIsOK);
                    break;
                case "geometryBtn":
                    {
                        if (gfb == null || gfb.Visibility == System.Windows.Visibility.Visible)
                        {
                            gfb = new GeometryFormulaBuilder();
                            gfb.Owner = this;
                            gfb.Show();
                        }
                        else if (gfb.Visibility == System.Windows.Visibility.Hidden)
                            gfb.Show();
                    }
                    break;
                case "clearBtn":
                    {
                        formulaString = "";
                        formulaTB.Text = "";
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void functionsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                formulaTB.Text += ((ListBoxItem)functionsList.SelectedItem).Content.ToString();
            }
            catch (NullReferenceException)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void objectNameTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                objectsList.Items.Clear();
                FillNameList(objectNameTB.Text);
            }
        }

        #region Private methods

        /// <summary>
        /// Load all available object names into listbox
        /// </summary>
        private void FillNameList(string filter)
        {
            foreach (PNObject obj in PNObjectRepository.PNObjects.Values)
            {
                if (obj.Name.Contains(filter))
                    objectsList.Items.Add(obj);
            }
        }

        /// <summary>
        /// Check current formula
        /// </summary>
        /// <returns>true / false if formula is correct or not</returns>
        private bool CheckFormula()
        {
            try
            {
                if (formulaTB.Text.Equals(""))
                    return true;

                formulaString = FormulaConverter.ToArrayOfValues(formulaTB.Text);
                if (formulaString != null)
                {
                    if (formulaType == FormulaTypes.Guard)
                    {
                        boolResult =
                            FormulaCompiler.CompileBoolFormula(formulaString).ExecuteBooleanFormula(PNObjectRepository.PNObjects.DoubleValues,
                                                                                                     PNObjectRepository.PNObjects.BooleanValues);
                    }
                    else
                    {
                        result = FormulaCompiler.CompileFormula(formulaString).ExecuteFormula(PNObjectRepository.PNObjects.DoubleValues,
                                                                                              PNObjectRepository.PNObjects.BooleanValues);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (NotSupportedException ex)
            {
                ExceptionHandler.HandleException(ex);
                return false;
            }
        }
        private void PasteTextToFormula(string text)
        {
            previousCaretPosition = formulaTB.CaretIndex;
            if (formulaTB.SelectionLength > 0)
            {
                formulaTB.SelectedText = text;
                formulaTB.CaretIndex += formulaTB.SelectedText.Length;
                formulaTB.SelectionLength = 0;
            }
            else
            {
                formulaTB.Text = formulaTB.Text.Insert(formulaTB.CaretIndex, text);
                formulaTB.CaretIndex = previousCaretPosition + text.Length;
            }
            //previousCaretPosition = formulaTB.CaretIndex;
        }

        /// <summary>
        /// Save and attach current formula to selected object
        /// </summary>
        private void AttachFormula()
        {
            var eventPublisher = App.GetObject<EventPublisher>();
           /* ProgressWindow progressWindow = null;
            eventPublisher.Register((ProgressEventArgs progressArgs) =>
                {
                    if (progressWindow == null || !progressWindow.IsVisible)
                    {
                        progressWindow = new ProgressWindow();
                        progressWindow.ShowDialog();
                    }
                    progressWindow.Maximum = PNObjectRepository.Count;
                    progressWindow.Progress = progressArgs.Progress;
                });*/

            if (formulaTB.Text.Equals(""))
            {
                SetFormula(formulaTB.Text, pnObject, false);
                this.Close();
            }
            else if (CheckFormula())
            {
                var formulaMng = App.GetObject<FormulaManager>();
            
                SetFormula(formulaTB.Text, pnObject, true);
                formulaMng.IsNeedToCompile = true;

                var result = UpdateObjectsWithFormula(formulaMng);
                if (result != null)
                {
                    DialogWindow.Alert(result);
                }

                formulaMng.IsNeedToCompile = false;
            }
           /* eventPublisher.ExecuteEvents(new ProgressEventArgs(PNObjectRepository.Count));
            eventPublisher.UnRegister(typeof(ProgressEventArgs));*/
            eventPublisher.ExecuteEvents(new PNObjectChangedEventArgs(pnObject));
            Close();
        }

        private string UpdateObjectsWithFormula(FormulaManager formulaMng)
        {
            foreach (ObjectWithFormula objWithFormula in formulaMng.GetObjectsWithFormula())
            {
                if (objWithFormula.FormulaType == FormulaTypes.Guard)
                {
                    bool booleanResult = ((IExtendedFormula)objWithFormula.Object).ExecuteGuardFormula();
                    if (pnObject.Equals(objWithFormula.Object))
                    {
                        boolResult = booleanResult;
                        return Messages.Default.CorrectFormula + boolResult.ToString();
                    }
                }
                else
                {
                    double doubleResult = ((IFormula)objWithFormula.Object).ExecuteFormula();
                    if (pnObject.Equals(objWithFormula.Object))
                    {
                        result = doubleResult;
                        return Messages.Default.CorrectFormula + result.ToString();
                    }
                }
                // eventPublisher.ExecuteEvents(new ProgressEventArgs(progress++));
            }
            return null;
        }

        /// <summary>
        /// Set formula string to object
        /// </summary>
        /// <param name="formula">Formula string</param>
        /// <param name="pnObject">PN object</param>
        /// <param name="isCompile">Is compilation required</param>
        private void SetFormula(string formula, PNObject pnObject, bool isCompile)
        {
            if (formulaType == FormulaTypes.Guard)
            {
                ((IExtendedFormula)pnObject).TransitionGuardFormula = formula;
                if(isCompile)
                    ((IExtendedFormula)pnObject).CompileBooleanFormula(formulaString);
            }
            else
            {
                ((IFormula)pnObject).Formula = formula;
                if (isCompile)
                {
                    ((IFormula)pnObject).CompileFormula(formulaString);
                }
            }
        }

        #endregion
    }
}
