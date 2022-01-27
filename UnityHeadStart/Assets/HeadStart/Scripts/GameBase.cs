using Assets.HeadStart.Core;
using Assets.HeadStart.Core.Player;
using Assets.HeadStart.Core.SceneService;
using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBase : MonoBehaviour, IGame
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.6";
#pragma warning restore 0414 //
    private User _user;
    public Player Player;
    [HideInInspector]
    public DataService DataService;
    private int _uniqueId;
    private bool _isGamePaused;

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
            _user = new User()
            {
                Id = 0,
                LocalId = 0,
                IsFirstTime = true,
                Name = "no-name-user",
                ToiletPaper = 10,
                UserType = UserType.CASUAL,
                IsUsingSound = true,
                Language = "en"
            };
            DataService.CreateUser(_user);
        }
        if (Main._.ConsoleLog)
        {
            Debug.Log(JsonUtility.ToJson(_user.Debug()));
        }
        return _user;
    }

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

    internal void LoadScene(SCENE scene)
    {
        SceneReference sceneRef = __.SceneService.GetScene(scene);
        __.ClearSceneDependencies();
        sceneRef.LoadScene();
    }

    public User DeviceUser()
    {
        if (_user == null)
        {
            LoadUser();
        }
        return _user;
    }
}
