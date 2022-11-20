using System.IO;
using UnityEngine;

namespace Assets.HeadStart.Features.Database.JSON
{
    public interface IJSONDatabase
    {
        void RecreateDatabase();
        DevicePlayer GetPlayer();
    }

    public class JSONDatabase : MonoBehaviour
    {
        private static readonly string ASSETS_FILE_PATH = "player.json";

        public void RecreateDatabase(string filepath = null)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                filepath = FileUtils.GetStreamingAssetsFilePath(ASSETS_FILE_PATH, true);
            }

            using (StreamWriter writer = new StreamWriter(filepath))
            {
                var jsonString = JsonUtility.ToJson(new PlayerJson()
                {
                    player = JSONConst.DEFAULT_PLAYER,
                    gameSettings = JSONConst.DEFAULT_SETTINGS
                });

                this.writeJson(writer, jsonString);
            }
        }

        public void UpdatePlayer(DevicePlayer devicePlayer)
        {
            var assetsFilePath = "player.json";
            var filepath = FileUtils.GetStreamingAssetsFilePath(assetsFilePath, true);
            var playerJson = GetPlayerJson();
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                playerJson.player = devicePlayer;
                var jsonString = JsonUtility.ToJson(playerJson);
                this.writeJson(writer, jsonString);
            }
        }

        public void UpdateGameSettings(GameSettings gameSettings)
        {
            var assetsFilePath = "player.json";
            var filepath = FileUtils.GetStreamingAssetsFilePath(assetsFilePath, true);
            var playerJson = GetPlayerJson();
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                playerJson.gameSettings = gameSettings;
                var jsonString = JsonUtility.ToJson(playerJson);
                this.writeJson(writer, jsonString);
            }
        }

        public PlayerJson GetPlayerJson()
        {
            var filepath = FileUtils.GetStreamingAssetsFilePath(ASSETS_FILE_PATH, true);
            using (StreamReader reader = new StreamReader(filepath))
            {
                var playerJson = reader.ReadToEnd();
                reader.Close();
                return JsonUtility.FromJson<PlayerJson>(playerJson);
            }
        }

        public DevicePlayer GetPlayer()
        {
            var playerJson = GetPlayerJson();
            if (playerJson == null) {
                Debug.LogWarning("No PlayerJson");
                RecreateDatabase();
                playerJson = GetPlayerJson();
            }
            return playerJson.player;
        }

        public GameSettings GetGameSettings()
        {
            var playerJson = GetPlayerJson();
            if (playerJson == null) {
                Debug.LogWarning("No PlayerJson");
                RecreateDatabase();
                playerJson = GetPlayerJson();
            }
            return playerJson.gameSettings;
        }

        private void writeJson(StreamWriter writer, string jsonString) {
            jsonString = JSONDatabase.formatJson(jsonString);
            writer.Write(jsonString);
            writer.Close();
        }

        private static string formatJson(string jsonString) {
            jsonString = jsonString.Replace(",\"", @",
""");
            jsonString = jsonString.Replace(":{", @":{
");
            jsonString = jsonString.Replace("}}", @"}
}");
            return jsonString;
        }
    }
}

[System.Serializable]
public class PlayerJson
{
    public DevicePlayer player;
    public GameSettings gameSettings;

    public PlayerJson() { }

    public PlayerJson(DevicePlayer _devicePlayer, GameSettings _gameSettings)
    {
        player = _devicePlayer;
        gameSettings = _gameSettings;
    }
}

[System.Serializable]
public class DevicePlayer
{
    public int localId;
    public string name;
    public int toiletPaper;
    public bool isFirstTime;
    public bool playedTutorial;
    public bool isRegistered;
    public float completedPercentage;
}

[System.Serializable]
public class GameSettings
{
    public bool isUsingSound;
    public string language;
    public bool isCameraSetForThisDevice;
    public int cameraSize3D;
    public float cameraSize2D;
}
