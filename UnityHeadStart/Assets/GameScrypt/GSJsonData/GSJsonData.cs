using System;
using UnityEngine;

namespace GameScrypt.JsonData
{
    public class GSJsonData : IJSONDatabase
    {
        protected string _path = "";

        public GSJsonData(string path)
        {
            _path = path;
        }

        public virtual void Recreate(string path = null, object obj = null)
        {
            throw new Exception("Extend in orderd to use this method.");
        }

        public virtual T GetJsonObj<T>()
        {
            var actualJson = GSJsonService.GetJsonFromFile(_path);
            return JsonUtility.FromJson<T>(actualJson);
        }
    }

    public interface IJSONDatabase
    {
        public void Recreate(string path = null, object obj = null);
        public T GetJsonObj<T>();
    }
}