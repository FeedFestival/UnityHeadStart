using System.Collections.Generic;

namespace Assets.HeadStart.Core
{
    public class CoreObservedValues
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        public CoreValues CoreValues;

        public CoreObservedValues() { }

        public void SetValue(object key, object value)
        {
            var newCoreValues = new CoreValues()
            {
                Values = new Dictionary<int, object>()
            };
            newCoreValues.Set(key, value);

            CoreValues = newCoreValues;
        }
    }

    public class CoreValues
    {
        public Dictionary<int, object> Values;

        public CoreValues() { }

        public void Set(object key, object value)
        {
            int intKey = (int)key;
            if (Contains(intKey))
            {
                Values[intKey] = value;
                return;
            }
            Values.Add(intKey, value);
        }

        public bool Contains(object key)
        {
            int intKey = (int)key;
            return Values.ContainsKey(intKey);
        }

        public bool Contains(int intKey)
        {
            return Values.ContainsKey(intKey);
        }

        public object GetValue(object key)
        {
            if (key == null)
            {
                return null;
            }

            int intKey = (int)key;
            if (Contains(intKey) == false)
            {
                return null;
            }
            return Values[intKey];
        }

        public int GetIntValue(object key)
        {
            if (key == null)
            {
                return 0;
            }

            int intKey = (int)key;

            if (Values.ContainsKey(intKey) == false)
            {
                return 0;
            }
            int? val = Values[intKey] as int?;
            return val.HasValue ? val.Value : 0;
        }
    }
}