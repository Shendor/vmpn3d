using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Controls;
using System.Data;
using System.Windows.Media.Imaging;
using System.Collections.Generic;
using PNCreator.Modules.Properties;
using PNCreator.ManagerClasses;
using System.Collections;

namespace PNCreator.PNObjectsIerarchy
{
    public class PNObject : Meshes3D.Mesh3D, IDisposable, IEquatable<PNObject>, IEqualityComparer<PNObject>
    {
        public static readonly string DOUBLE_FORMAT = "{0:0.####}";

        protected bool isReadOnly;
        protected string name;
        protected long id;

        private PNObjectTypes type;
        protected bool allowSaveHistory;
        protected long group;

        private Dictionary<string, DataTable> objectHistory;

        protected TextBlock valueInCanvas;
        protected TextBlock nameInCanvas;

        public PNObject(PNObjectTypes type)
        {
            group = -1;
            this.id = IdGenerator.ID;
            this.name = PNObjectNameUtil.GetPNObjectName(type) + id.ToString();
            this.type = type;
            allowSaveHistory = false;
            objectHistory = new Dictionary<string, DataTable>();

            nameInCanvas = new TextBlock();
            nameInCanvas.Text = name;
            valueInCanvas = new TextBlock();
        }

        public PNObject()
            : this(PNObjectTypes.None)
        {
        }

        #region Properties

        #region Name property

        /// <summary>
        /// Get Id
        /// </summary>
        public long ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        public void SetName(string name)
        {
            if (name.Length >= 1)
            {
                this.name = name;
                int startIndex = 0, i = 0;

                while (name[i].Equals('0') || name[i].Equals('1') || name[i].Equals('2') || name[i].Equals('2') || name[i].Equals('3') ||
                      name[i].Equals('4') || name[i].Equals('5') || name[i].Equals('6') || name[i].Equals('7') || name[i].Equals('8') || name[i].Equals('9'))
                {
                    ++startIndex;
                    ++i;
                }

                if (startIndex != 0)
                {
                    name = "";
                    for (int j = startIndex; j < this.name.Length; j++)
                        name += this.name[j];
                }
                this.name = "";
                char[] separators = { '\\', '+', '-', '*', '-', '(', ')', '[', ']', '{', '}', '&', '^', '%', '$', '#', '@', '!', '~', '`', '№', ':', ';', ',', '.',
                                       '=', '?', '|', '/', '"', '\'', '"', ' ' };
                string[] namePieces = name.Split(separators);
                foreach (string s in namePieces)
                {
                    this.name += s;
                }
                separators = null;
                namePieces = null;
                GC.Collect();
            }
        }
        /// <summary>
        /// Set or get name of object
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                SetName(value);
                if (nameInCanvas != null)
                    NameInCanvas.Text = Name;
            }
        }
        #endregion

        #region Group

        public long Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }

        #endregion

        #region Type property

        public PNObjectTypes Type
        {
            get
            {
                return this.type;
            }
            set
            {
                type = value;
            }
        }
        #endregion

        #region Visual properties

        //public MeshGeometry3D Mesh
        //{
        //    get { return geometry.Geometry as MeshGeometry3D; }
        //    set { geometry.Geometry = value; }
        //}

        //public GeometryModel3D Geometry
        //{
        //    get { return geometry; }
        //    set { geometry = value; }
        //}
        //===================================================================================
        /// <summary>
        /// Get or set object name in canvas
        /// </summary>
        public TextBlock NameInCanvas
        {
            get
            {
                return nameInCanvas;
            }
            set
            {
                nameInCanvas = value;
            }
        }
        //===================================================================================
        /// <summary>
        /// Get or set value in canvas
        /// </summary>
        public TextBlock ValueInCanvas
        {
            get
            {
                return valueInCanvas;
            }
            set
            {
                valueInCanvas = value;
            }
        }
        /// <summary>
        /// Get or set whether object is on readonly status.
        /// This means that this object cannot be selected and modified
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                isReadOnly = value;
            }
        }
        #endregion

        #region History property
        /// <summary>
        /// Allow or restrict to save history of work of object
        /// </summary>
        public bool AllowSaveHistory
        {
            get
            {
                return allowSaveHistory;
            }
            set
            {
                allowSaveHistory = value;
            }
        }
        //==================================================================================================
        /// <summary>
        /// Add new value of token quantity for each iteration
        /// </summary>
        public Dictionary<string, DataTable> ObjectHistory
        {
            get
            {
                return objectHistory;
            }
            set
            {
                objectHistory = value;
            }
        }
        #endregion


        #endregion

        #region History
        public void AddNewHistoryTable(string name)
        {
            DataTable table = new DataTable(name);
            table.Columns.Add("Time", typeof(double));
            table.Columns.Add("Value", typeof(double));

            if (objectHistory.ContainsKey(name))
            {
                objectHistory.Remove(name);
            }
            objectHistory.Add(name, table);
        }

        public void AddNewRowOfHistory(string table, double time, double value)
        {
            objectHistory[table].Rows.Add(time, value);
        }

        public IDictionary<string, double> GetMaxValuesFromHistory()
        {
            var maxValues = new Dictionary<string, double>();
            foreach (var objectHistoryItem in objectHistory)
            {
                DataTable table = objectHistoryItem.Value;
                double maxValue = Convert.ToDouble(table.Rows[0][1]);
                for (int j = 1; j < table.Rows.Count; j++)
                {
                    double convertedValue = Convert.ToDouble(table.Rows[j][1]);
                    if (convertedValue > maxValue)
                        maxValue = convertedValue;
                }
                maxValues.Add(objectHistoryItem.Key, maxValue);
            }

            return maxValues;
        }

        public ICollection<String> GetTableNames()
        {
            return objectHistory.Keys;
        }
        #endregion

        protected virtual void SetCanvasProperties()
        {
            //if (Type == PNObjectTypes.Membrane) return;

            valueInCanvas = new TextBlock();
            valueInCanvas.FontSize = 10;
            valueInCanvas.Text = "0";

            nameInCanvas = new TextBlock();
            nameInCanvas.FontSize = 10;
            nameInCanvas.Text = Name;

            if (Type == PNObjectTypes.DiscreteTransition ||
               Type == PNObjectTypes.DiscreteLocation)
            {
                if (PNProperties.DiscreteObjectsNamesVisibility == false)
                    nameInCanvas.Foreground = PNColors.Transparent;
                if (PNProperties.DiscreteObjectsValuesVisibility == false)
                    valueInCanvas.Foreground = PNColors.Transparent;
            }
            else if (Type == PNObjectTypes.ContinuousTransition ||
               Type == PNObjectTypes.ContinuousLocation)
            {
                if (PNProperties.ContinuousObjectsNamesVisibility == false)
                    nameInCanvas.Foreground = PNColors.Transparent;
                if (PNProperties.ContinuousObjectsValuesVisibility == false)
                    valueInCanvas.Foreground = PNColors.Transparent;
            }
            else
            {
                if (PNProperties.ArcsNamesVisibility == false)
                    nameInCanvas.Foreground = PNColors.Transparent;
                if (PNProperties.ArcsValuesVisibility == false)
                    valueInCanvas.Foreground = PNColors.Transparent;
            }
        }

        public void IsNameVisible(bool isVisible)
        {
            if (isVisible == true)
            {
                nameInCanvas.Foreground = PNColors.TextBrush;
            }
            else
            {
                nameInCanvas.Foreground = PNColors.Transparent;
            }
        }

        public void IsValueVisible(bool isVisible)
        {
            if (isVisible == true)
            {
                valueInCanvas.Foreground = PNColors.TextBrush;
            }
            else
            {
                valueInCanvas.Foreground = PNColors.Transparent;
            }
        }

        public override string ToString()
        {
            PNObject membane = PNObjectRepository.GetByKey(group);
            return name + ((membane != null) ? "[" + membane.Name + "]" : "");
        }

        #region Члены IDisposable

        public void Dispose()
        {
            //objectHistory.Clear();
            objectHistory = null;
            //geometry = null;
            //mesh = null;
            this.Content = null;
            GC.Collect();
        }

        #endregion

        #region IEquatable<PNObject> Members

        public bool Equals(PNObject other)
        {
            return this.ID == other.ID;
        }

        #endregion



        #region IEqualityComparer<PNObject> Members

        public bool Equals(PNObject x, PNObject y)
        {
            return x.ID == y.ID;
        }

        public int GetHashCode(PNObject obj)
        {
            return obj.ID.GetHashCode();
        }

        #endregion
    }
}
