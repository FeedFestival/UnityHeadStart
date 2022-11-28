using GameScrypt.GSUtils;
using System.IO;
using UnityEngine;

namespace GameScrypt.JsonData
{
    public static class GSJsonService
    {
        public static string GetJsonFromFile(string assetsFilePath)
        {
            var filepath = GSStreamingAssets.GetStreamingAssetsFilePath(assetsFilePath, true);
            using (StreamReader reader = new StreamReader(filepath))
            {
                var playerJson = reader.ReadToEnd();
                reader.Close();
                return playerJson;
            }
        }

        public static void WriteJsonToFile(string assetsFilePath, object obj)
        {
            var filepath = GSStreamingAssets.GetStreamingAssetsFilePath(assetsFilePath, true);
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                var jsonString = JsonUtility.ToJson(obj);
                GSJsonService.writeJson(writer, jsonString);
            }
        }

        public static string FormatJson(string jsonString)
        {
            jsonString = jsonString.Replace(",\"", @",
""");
            jsonString = jsonString.Replace(":{", @":{
");
            jsonString = jsonString.Replace("}}", @"}
}");
            return jsonString;
        }

        private static void writeJson(StreamWriter writer, string jsonString)
        {
            jsonString = GSJsonService.FormatJson(jsonString);
            writer.Write(jsonString);
            writer.Close();
        }
    }
}
