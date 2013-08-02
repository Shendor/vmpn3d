using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.CodeCompletion;
using WindowsControl;
using System.Xml;
using System.Xml.Schema;
using System.IO;
using System.Collections;
using System.Windows.Controls.Primitives;


namespace PNCreator.Modules.ODLEditor
{
    /// <summary>
    /// Interaction logic for ODLEditorWindow.xaml
    /// </summary>
    public partial class ODLEditorWindow : Window
    {
        private CompletionWindow completionWindow;
        public static RoutedCommand FormatCommand = new RoutedCommand();
        private XMLSchemaDataFactory xmlSchemaFactory;
        private StringBuilder currentText;
        private StringBuilder selectedElementName;

        public ODLEditorWindow()
        {
            InitializeComponent();
            currentText = new StringBuilder();
            selectedElementName = new StringBuilder();
            FormatCommand.InputGestures.Add(new KeyGesture(Key.F, ModifierKeys.Control));

            AbstractFoldingStrategy foldingStrategy = new XmlFoldingStrategy();
            textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();

            FoldingManager foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);

            textEditor.TextArea.TextEntered += textEditor_TextEntered;

            textEditor.Text += "<Model name=\"model1\"\n xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance \" xsi:schemaLocation=\"Schema.xsd\" " +
            "> \n\n</Model>";

            textEditor.TextArea.Caret.Position = new ICSharpCode.AvalonEdit.TextViewPosition(3, 1);
            textEditor.Focus();

            xmlSchemaFactory = new XMLSchemaDataFactory("Schema.xsd");
        }


        private void ShowCompletionWindow(string text)
        {
            bool isWindowShowPermitted = true;
            switch (text)
            {
                case "<":
                    {
                        completionWindow = CompletionDataFactory.GetInstance(xmlSchemaFactory.GetElementsByName(currentText.ToString()),
                                                                                 textEditor.TextArea);

                        for (int i = textEditor.TextArea.Caret.Offset - 2; i > 0; i--)
                        {
                            if (textEditor.TextArea.Document.Text[i].Equals('>')) break;
                            else if (!(textEditor.TextArea.Document.Text[i].Equals(' ') ||
                                textEditor.TextArea.Document.Text[i].Equals('\n') ||
                                textEditor.TextArea.Document.Text[i].Equals('\r')))
                            {
                                isWindowShowPermitted = false;
                                break;
                            }
                        }
                        if (isWindowShowPermitted)
                            for (int j = textEditor.TextArea.Caret.Offset; j < textEditor.TextArea.Document.Text.Length; j++)
                            {
                                if (textEditor.TextArea.Document.Text[j].Equals('<')) break;
                                if (!(textEditor.TextArea.Document.Text[j].Equals(' ') ||
                                    textEditor.TextArea.Document.Text[j].Equals('\n') ||
                                    textEditor.TextArea.Document.Text[j].Equals('\r')))
                                {
                                    isWindowShowPermitted = false;
                                    break;
                                }
                            }
                    } break;
                case ">":
                    {
                        isWindowShowPermitted = false;
                    } break;
                case "/":
                    {
                        isWindowShowPermitted = false;
                    } break;
                case " ":
                    {
                        selectedElementName.Clear();
                        int startIndex = 0;
                        short quotesCount = 0;
                        for (int i = textEditor.TextArea.Caret.Offset - 2; i > 0; i--)
                        {
                            if (textEditor.TextArea.Document.Text[i].Equals('>'))
                            {
                                isWindowShowPermitted = false;
                                break;
                            }

                            if (textEditor.TextArea.Document.Text[i].Equals('"'))
                            {
                                quotesCount++;
                                if (quotesCount > 1) quotesCount = 0;
                            }

                            if (textEditor.TextArea.Document.Text[i].Equals('<'))
                            {
                                if (quotesCount == 0)
                                {
                                    startIndex = i;
                                    break;
                                }
                            }
                        }

                        selectedElementName.Append(textEditor.TextArea.Document.Text.Substring(startIndex + 1,
                                                           textEditor.TextArea.Document.Text.IndexOf(' ', startIndex + 1) - startIndex - 1));

                        completionWindow = CompletionDataFactory.GetInstance(xmlSchemaFactory.GetAttributesOfElement(selectedElementName.ToString()),
                                                                               textEditor.TextArea, CompletionDataType.Attribute);

                    } break;
            }

            if (isWindowShowPermitted == false || completionWindow == null) return;

            completionWindow.KeyDown += new KeyEventHandler(completionWindow_KeyDown);
            //completionWindow.CompletionList.SelectionChanged += new SelectionChangedEventHandler(CompletionList_SelectionChanged);
            completionWindow.Show();
            completionWindow.Unloaded += new RoutedEventHandler(completionWindow_Unloaded);
        }

        void completionWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            //completionWindow.CompletionList.SelectionChanged -= CompletionList_SelectionChanged;
            completionWindow.KeyDown -= completionWindow_KeyDown;
            completionWindow.Unloaded -= completionWindow_Unloaded;
           
        }

        void CompletionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(completionWindow.CompletionList.SelectedItem.Text);

        }

        void textEditor_TextEntered(object sender, TextCompositionEventArgs e)
        {
            currentText.Append(e.Text);

            switch (e.Text)
            {
                case "<":
                    {
                        currentText.Clear();
                        ShowCompletionWindow(e.Text);
                    } break;
                case ">":
                    {
                    } break;
                case "/":
                    {

                    } break;
                case " ":
                    {
                        ShowCompletionWindow(e.Text);

                    } break;
            }


        }

        private void InsertText(string text, int caretOffset = 0)
        {
            textEditor.TextArea.Document.Insert(textEditor.TextArea.Caret.Offset, text);
            textEditor.TextArea.Caret.Position = new ICSharpCode.AvalonEdit.TextViewPosition(textEditor.TextArea.Caret.Position.Line,
                                                                                       textEditor.TextArea.Caret.Position.Column - caretOffset);
        }


        void completionWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                selectedElementName.Clear();
                selectedElementName.Append(completionWindow.CompletionList.SelectedItem.Text);
            }
        }

        private void toolBarButtons_Click(object sender, RoutedEventArgs e)
        {
            Button btn = e.OriginalSource as Button;

            switch (btn.Name)
            {
                case "zoomInBtn": editorSize.ScaleX = editorSize.ScaleY += 0.2 ; break;
                case "zoomOutBtn": editorSize.ScaleX = editorSize.ScaleY -= 0.2; break;
                case "closeBtn": Close(); break;
            }
           
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Back:
                    currentText.Remove(currentText.Length - 1, 1); break;
            }
        }

        private void FormatCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                textEditor.Text = XMLFormater.FormatXML(textEditor.Text);
            }
            catch (XmlException)
            {

            }
        }
    }
}
