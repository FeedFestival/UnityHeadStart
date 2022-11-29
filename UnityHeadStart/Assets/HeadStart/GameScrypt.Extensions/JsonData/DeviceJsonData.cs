using GameScrypt.JsonData;
using GameScrypt.GSUtils;
using System.IO;
using UnityEngine;

namespace HeadStart 
{
    public class DeviceJsonData : GSJsonData
    {
        public static readonly PlayerSettings DEFAULT_PLAYER_SETTINGS = new PlayerSettings()
        {
            localId = 0,
            name = "no-name-user",
            toiletPaper = 10,
            isFirstTime = true,
            playedTutorial = false,
            isRegistered = false,
            completedPercentage = 0
        };

        public static readonly GameSettings DEFAULT_GAME_SETTINGS = new GameSettings()
        {
            language = "en",
            isUsingSound = true,
            isCameraSetForThisDevice = false,
            cameraSize2D = 3,
            cameraSize3D = 3
        };

        public DeviceJsonData(string path) : base(path)
        {
        }

        public override void Recreate(string path = null, object obj = null)
        {
            var playerJsonObj = JsonUtility.ToJson(new DeviceJson()
            {
                playerSettings = DEFAULT_PLAYER_SETTINGS,
                gameSettings = DEFAULT_GAME_SETTINGS
            });
            GSJsonService.WriteJsonToFile(base._path, playerJsonObj);
        }

        public void UpdatePlayer(PlayerSettings PlayerSettings)
        {
            var playerJsonObj = base.GetJsonObj<DeviceJson>();
            playerJsonObj.playerSettings = PlayerSettings;
            GSJsonService.WriteJsonToFile(base._path, playerJsonObj);
        }

        public void UpdateGameSettings(GameSettings gameSettings)
        {
            var playerJsonObj = base.GetJsonObj<DeviceJson>();
            playerJsonObj.gameSettings = gameSettings;
            GSJsonService.WriteJsonToFile(base._path, playerJsonObj);
        }

        public PlayerSettings GetPlayer()
        {
            var playerJsonObj = base.GetJsonObj<DeviceJson>();
            if (playerJsonObj == null)
            {
                Debug.LogWarning("No PlayerJson");
                Recreate();
                playerJsonObj = base.GetJsonObj<DeviceJson>();
            }
            return playerJsonObj.playerSettings;
        }

        public GameSettings GetGameSettings()
        {
            var playerJsonObj = base.GetJsonObj<DeviceJson>();
            if (playerJsonObj == null)
            {
                Debug.LogWarning("No PlayerJson");
                Recreate();
                playerJsonObj = base.GetJsonObj<DeviceJson>();
            }
            return playerJsonObj.gameSettings;
        }
    }

    [System.Serializable]
    public class DeviceJson
    {
        public PlayerSettings playerSettings;
        public GameSettings gameSettings;

        public DeviceJson() { }

        public DeviceJson(PlayerSettings _PlayerSettings, GameSettings _gameSettings)
        {
            playerSettings = _PlayerSettings;
            gameSettings = _gameSettings;
        }
    }

    [System.Serializable]
    public class PlayerSettings
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
}
