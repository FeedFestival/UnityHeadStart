using UnityEngine;

namespace Assets.HeadStart.Features.Database.JSON
{
    public class JSONDatabase : MonoBehaviour
    {
        public JsonDbSettings JsonDbSettings;
        
        public PlayerExtension GetPlayer()
        {
            PlayerJson playerJson = JsonUtility.FromJson<PlayerJson>(JsonDbSettings.PlayerJson.text);

            if (playerJson != null && playerJson.gameSettings != null)
            {
                Debug.Log("player.IsRegistered: " + playerJson.Player.IsRegistered);
                Debug.Log("player.CompletedPercentage: " + playerJson.Player.CompletedPercentage);
                Debug.Log("gameSettings.cameraSize2D: " + playerJson.gameSettings.cameraSize2D);
            }

            return playerJson.Player;
        }
    }
}

[System.Serializable]
public class PlayerJson
{
    public PlayerExtension Player;
    public GameSettings gameSettings;
}

[System.Serializable]
public class PlayerExtension : User
{
    public float CompletedPercentage { get; set; }
}

[System.Serializable]
public class GameSettings
{
    public int cameraSize3D;
    public int cameraSize2D;
}
