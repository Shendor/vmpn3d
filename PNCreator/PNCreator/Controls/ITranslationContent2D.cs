using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace PNCreator.Controls
{
    public interface ITranslationContent2D
    {
        /// <summary>
        /// Translate 2D content at Viewport3D
        /// </summary>
        void TranslateContent2D(Viewport3D viewport);
    }
}
