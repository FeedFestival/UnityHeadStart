using System.IO;
using UnityEngine;

namespace GameScrypt.GSUtils
{
    public static class GSStreamingAssets
    {
        public static readonly string ASSETS_PATH = "Assets/";

        public static string GetStreamingAssetsFilePath(string assetsFilePath, bool debugLog = false)
        {
#if UNITY_EDITOR
            return string.Format(ASSETS_PATH + @"StreamingAssets/{0}", assetsFilePath);
#else
            // check if file exists in Application.persistentDataPath
            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, assetsFilePath);
            if (!File.Exists(filepath))
            {
                filepath = CorrectLocation(filepath, assetsFilePath);
                if (debugLog) Debug.Log("Database written");
            }
            return filepath;
#endif
        }

        [System.Obsolete]
        public static string CorrectLocation(string filepath, string assetsFilePath)
        {
#if UNITY_ANDROID
            var androidStreamingAssetsPath = "jar:file://" + Application.dataPath + "!/assets/" + assetsFilePath;
            if (!File.Exists(androidStreamingAssetsPath))
            {
                androidStreamingAssetsPath = Path.Combine(Application.streamingAssetsPath, assetsFilePath);
            }
            var loadedPath = new WWW(androidStreamingAssetsPath);
#elif UNITY_IOS
            var loadedPath = Application.dataPath + "/Raw/" + assetsFilePath;
#elif UNITY_WP8
            var loadedPath = Application.dataPath + "/StreamingAssets/" + assetsFilePath;
#elif UNITY_WINRT
            var loadedPath = Application.dataPath + "/StreamingAssets/" + assetsFilePath;
#else
            var loadedPath = Application.dataPath + "/StreamingAssets/" + assetsFilePath;
#endif
            return CopyToFilePath(loadedPath, filepath);
        }

        [System.Obsolete]
        public static string CopyToFilePath(WWW www, string filepath)
        {
            while (!www.isDone) { }
            // CAREFUL here, for safety reasons you shouldn't let this while loop unattended,
            // place a timer and error check then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, www.bytes);
            return filepath;
        }

        public static string CopyToFilePath(string loadedPath, string filepath)
        {
            File.Copy(loadedPath, filepath);
            return filepath;
        }
    }
}