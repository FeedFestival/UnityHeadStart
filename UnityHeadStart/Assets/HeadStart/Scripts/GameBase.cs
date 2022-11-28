using Assets.HeadStart.Core;
using Assets.HeadStart.Core.Player;
using Assets.HeadStart.Core.SceneService;
using Assets.HeadStart.Features.Database;
using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBase : MonoBehaviour, IGame
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    private User _user;
    //private PlayerSettings _devicePlayer;
    public Player Player;
    [HideInInspector]
    public DataService DataService;
    private int _uniqueId;
    private bool _isGamePaused;

    public static readonly User DEFAULT_USER = new User()
    {
        Id = 0,
        LocalId = 0,
        Name = "no-name-user",
        UserType = UserType.CASUAL
    };

    public virtual void PreStartGame()
    {
    }
    public virtual void StartGame()
    {
        LoadUser();
    }

    public virtual void GameOver()
    {
    }

    public virtual bool IsGamePaused()
    {
        return _isGamePaused;
    }

    public virtual void PauseGame()
    {
        _isGamePaused = true;
    }

    public virtual void ResumeGame()
    {
        _isGamePaused = false;
    }

    public int GetUniqueId()
    {
        _uniqueId++;
        return _uniqueId;
    }

    public User LoadUser()
    {
        if (DataService == null)
        {
            InitDatabaseConnection();
        }

        _user = DataService.GetTheUser();

        if (_user == null)
        {
            createFirstUser();
        }
        return _user;
    }

    //public PlayerSettings LoadDevicePlayer()
    //{
    //    var deviceJsonData = new DeviceJsonData("player.json");
    //    _devicePlayer = deviceJsonData.GetPlayer();

    //    if (_devicePlayer == null)
    //    {
    //        createFirstUser();
    //    }
    //    return _devicePlayer;
    //}

    public void InitDatabaseConnection()
    {
        DataService = new DataService();
        DataService.CreateDBIfNotExists();
    }

    public void GoToMainMenu()
    {
        LoadScene(SCENE.MainMenu);
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        __.ClearSceneDependencies();
        SceneManager.LoadScene(scene.buildIndex);
    }

    public void LoadScene(SCENE scene)
    {
        SceneReference sceneRef = __.SceneService.GetScene(scene);
        __.ClearSceneDependencies();
        sceneRef.LoadScene();
    }

    //public PlayerSettings DevicePlayer()
    //{
    //    if (_devicePlayer == null)
    //    {
    //        LoadDevicePlayer();
    //    }
    //    return _devicePlayer;
    //}

    public User DeviceUser()
    {
        if (_user == null)
        {
            LoadUser();
        }
        return _user;
    }

    private void createFirstUser()
    {
        _user = GameBase.DEFAULT_USER;
        DataService.CreateUser(_user);
    }
}
