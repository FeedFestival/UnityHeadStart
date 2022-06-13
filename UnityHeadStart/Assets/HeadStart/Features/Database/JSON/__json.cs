using UnityEngine;

namespace Assets.HeadStart.Features.Database.JSON
{
    public static class __json
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.1.0";
#pragma warning restore 0414 //

        private static JSONDatabase _jsonDatabase;
        public static JSONDatabase Database
        {
            get
            {
                if (_jsonDatabase == null)
                {
                    var go = UnityEngine.Object.Instantiate(Resources.Load("Features/JSONDatabase")) as GameObject;
                    go.name = "FEATURE JSONDatabase";
                    _jsonDatabase = go.GetComponent<JSONDatabase>();
                }
                return _jsonDatabase;
            }
        }

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

        public static void DumpToJsonConsole(IJsonConsole[] jsonConsoles)
        {
            foreach (var json in jsonConsoles)
            {
                Debug.Log(json.ToJsonString());
            }
        }

        private static string FixJson(string value)
        {
            value = "{\"Items\":" + value + "}";
            return value;
        }
    }

    public interface IJsonConsole
    {
        string ToJsonString();
    }
}
