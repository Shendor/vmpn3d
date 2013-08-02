using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Document;

namespace PNCreator.Modules.ODLEditor
{
    class CompletionData : ICompletionData
    {
        public CompletionData(string text, string description = null,
            string defaultValue = "", CompletionDataType dataType = CompletionDataType.Text)
        {
            this.Text = text;
            this.Description = description;
            this.DataType = dataType;
            this.DefaultValue = defaultValue;
        }

        public CompletionDataType DataType
        {
            get;
            set;
        }

        public System.Windows.Media.ImageSource Image
        {
            get { return null; }
        }

        public string Text { get; private set; }

        // Use this property if you want to show a fancy UIElement in the drop down list.
        public object Content
        {
            get { return this.Text; }
        }

        public object Description
        {
            get;
            set;
        }

        public string DefaultValue
        {
            get;
            set;
        }

        public double Priority { get { return 0; } }

        public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
        {
            int caretOffset = 0;

            if (this.DataType == CompletionDataType.Attribute)
                this.Text += "=\"" + this.DefaultValue + "\"";
            else
            {
                caretOffset += this.Text.Length + 4;
                this.Text += " Name=\"" + this.Text + this.GetHashCode() + "\" ></" + this.Text + ">";
            }

            textArea.Document.Replace(completionSegment, this.Text);

            textArea.Caret.Position =
                new ICSharpCode.AvalonEdit.TextViewPosition(textArea.Caret.Position.Line, textArea.Caret.Position.Column - caretOffset);
        }
    }
    enum CompletionDataType
    {
        Text,
        Element,
        Attribute
    }
}
