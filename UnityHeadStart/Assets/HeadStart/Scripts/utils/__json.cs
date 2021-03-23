using UnityEngine;

namespace Assets.Scripts.utils
{
    public static class __json
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
        public static T[] FromJson<T>(string json)
        {
            json = FixJson(json);
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.ItemsObj = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
            public T ItemsObj;
        }

        private static string FixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
    }
}