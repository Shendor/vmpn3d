using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PNCreator.ManagerClasses
{
    public enum DocumentFormat
    {
        /// <summary>
        /// Txt file (.txt)
        /// </summary>
        Txt,
        /// <summary>
        /// XML file (.xml)
        /// </summary>
        XML,
        /// <summary>
        /// MS Excel 2003 and earlier file (.xls)
        /// </summary>
        MSExcel,
        /// <summary>
        /// MS Word 2003 and earlier file (.doc)
        /// </summary>
        MSWord,
        /// <summary>
        /// MS Excel file (.csv)
        /// </summary>
        MSExcelCsv,
        /// <summary>
        /// MS Excel file (.xml)
        /// </summary>
        MSExcelML,
        /// <summary>
        /// PN Net file (.mpn)
        /// </summary>
        Net,
        /// <summary>
        /// Image file (.jpg)
        /// </summary>
        Image,
        /// <summary>
        /// File with chart (.png, .bmp, .xps, xls)
        /// </summary>
        Chart,
        /// <summary>
        /// File with 3D shapes for solid Petri Nets objects (.obj, .dxf)
        /// </summary>
        Mesh,
        /// <summary>
        /// File with textures for Petri Nets arcs (.png, .bmp, .jpg)
        /// </summary>
        Texture
    }
}
