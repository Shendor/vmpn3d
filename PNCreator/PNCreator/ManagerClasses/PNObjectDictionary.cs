using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PNCreator.PNObjectsIerarchy;

namespace PNCreator.ManagerClasses
{
    public class PNObjectDictionary<K, V> : IDictionary<K, V>
        where V : PNObject
    {
        private const string NO_BOOLEAN_VALUE = "This object doesn't have boolean value";

        private Dictionary<K, V> pnObjects;
        private Dictionary<K, double> doubleValues;
        private Dictionary<K, bool> booleanValues;
        private FormulaManager.FormulaManager formulaManager;

        public PNObjectDictionary()
        {
            pnObjects = new Dictionary<K, V>();
            doubleValues = new Dictionary<K, double>();
            booleanValues = new Dictionary<K, bool>();
            formulaManager = App.GetObject<FormulaManager.FormulaManager>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetDoubleValue(K key, double value)
        {
            double v = value;
            if (doubleValues.TryGetValue(key, out v))
            {
                doubleValues[key] = value;
                formulaManager.UpdateObjectsWithFormula(null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetBooleanValue(K key, bool value)
        {
            bool v = value;
            if (booleanValues.TryGetValue(key, out v))
            {
                booleanValues[key] = value;
                formulaManager.UpdateObjectsWithFormula(null);
            }
        }
        /// <summary>
        /// Find object by key and updates its double and boolean values if possible
        /// </summary>
        /// <param name="key">Key</param>
        private void UpdateValuesForObject(K key)
        {
            if (ContainsKey(key))
            {
                V value = this[key];
                if (doubleValues.ContainsKey(key))
                {
                    doubleValues[key] = GetDoubleValue(value);
                }
                if (booleanValues.ContainsKey(key))
                {
                    booleanValues[key] = GetBooleanValue(value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool GetBooleanValue(V value)
        {
            PNObject obj = value;

            switch (obj.Type)
            {
                case PNObjectTypes.DiscreteTransition:
                case PNObjectTypes.ContinuousTransition:
                    return ((Transition)obj).Guard;
                default:
                    throw new NotSupportedException(NO_BOOLEAN_VALUE);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetDoubleValue(V value)
        {
            PNObject obj = value;

            switch (obj.Type)
            {
                case PNObjectTypes.DiscreteLocation:
                    return ((DiscreteLocation)obj).Tokens;
                case PNObjectTypes.ContinuousLocation:
                    return ((ContinuousLocation)obj).Level;
                case PNObjectTypes.DiscreteTransition:
                    return ((DiscreteTransition)obj).Delay;
                case PNObjectTypes.ContinuousTransition:
                    return ((ContinuousTransition)obj).Expectance;
                case PNObjectTypes.StructuralMembrane:
                    return ((StructuralMembrane)obj).Speed;
                default:
                    return ((Arc3D)obj).Weight;
            }
        }

        /// <summary>
        /// Get double values
        /// </summary>
        public Dictionary<K, double> DoubleValues
        {
            get
            {
                return doubleValues;
            }
            set
            {
                doubleValues = value;
            }
        }

        /// <summary>
        /// Get boolean values
        /// </summary>
        public Dictionary<K, bool> BooleanValues
        {
            get
            {
                return booleanValues;
            }
            set
            {
                booleanValues = value;
            }
        }

        #region IDictionary<K,V> Members
        /// <summary>
        /// Add value
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void Add(K key, V value)
        {
            pnObjects.Add(key, value);

            PNObject obj = (PNObject)value;

            switch (value.Type)
            {
                case PNObjectTypes.DiscreteTransition:
                case PNObjectTypes.ContinuousTransition:
                    booleanValues.Add(key, GetBooleanValue(value));
                    doubleValues.Add(key, GetDoubleValue(value));
                    break;
                case PNObjectTypes.Membrane:
                case PNObjectTypes.None:
                    break;
                default:
                    doubleValues.Add(key, GetDoubleValue(value));
                    break;
            }

        }


        /// <summary>
        /// Remove value by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Is removed</returns>
        public bool Remove(K key)
        {
            doubleValues.Remove(key);
            booleanValues.Remove(key);
            return pnObjects.Remove(key);
        }

        /// <summary>
        /// Get value by key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Value</returns>
        public V this[K key]
        {
            get
            {
                return pnObjects[key];
            }
            set
            {
                pnObjects[key] = value;
                UpdateValuesForObject(key);
            }
        }

        /// <summary>
        /// If contains key
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public bool ContainsKey(K key)
        {
            return pnObjects.ContainsKey(key);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<K> Keys
        {
            get
            {
                return pnObjects.Keys;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(K key, out V value)
        {
            return pnObjects.TryGetValue(key, out value);
        }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<V> Values
        {
            get
            {
                return pnObjects.Values;
            }
        }

        #endregion

        #region ICollection<KeyValuePair<K,V>> Members

        public void Add(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            pnObjects.Clear();
            doubleValues.Clear();
            booleanValues.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get
            {
                return pnObjects.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable<KeyValuePair<K,V>> Members

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return pnObjects.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return pnObjects.GetEnumerator();
        }

        #endregion
    }
}
