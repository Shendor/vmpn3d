using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Editing;
using System.Collections;

namespace PNCreator.Modules.ODLEditor
{
    static class CompletionDataFactory
    {
        private static CompletionWindow completionWindow;

        static CompletionDataFactory()
        {
            completionWindow = null;
        }

        public static CompletionWindow GetInstance(IEnumerable<CompletionData> items, TextArea textArea, 
                                                   CompletionDataType dataType = CompletionDataType.Text)
        {
            if (!items.GetEnumerator().MoveNext()) return null; 
            completionWindow = new CompletionWindow(textArea);
            completionWindow.CompletionList.CompletionData.Clear();

            foreach (CompletionData obj in items)
                completionWindow.CompletionList.CompletionData.Add(obj);

            return completionWindow;
        }
    }
}
