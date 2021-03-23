using System;
using Assets.Scripts.utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    private static Game _game;
    public static Game _ { get { return _game; } }
    public LevelController LevelController;
    public Player Player;
    public DataService DataService;
    public User User;
    public User PlayingUser;
    public AfterLoading AfterLoading;
    public HighScoreType HighScoreType;
    private string LevelToLoad;
    private int _uniqueId;
    public bool GameOver;

    void Awake()
    {
        _game = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init()
    {
        Debug.Log("Game - Init");

        LoadUser();

        UIController._.Init();
        LevelController.Init();

        if (LevelController.LevelType == LevelType.MainMenu)
        {
            if (User.IsFirstTime)
            {
                UIController._.ShowInputNameView();
            }
            else
            {
                UIController._.InitMainMenu();
            }
        }
        else
        {
            UIController._.DestroyMainMenu();
        }
    }

    private void LoadUser()
    {
        DataService = new DataService();
        DataService.CreateDBIfNotExists();
        User = DataService.GetTheUser();

        if (User == null)
        {
            User = new User()
            {
                Id = 1,
                Name = "no-name-user",
                IsFirstTime = true,
                IsUsingSound = true,
                Language = "en"
            };
            DataService.CreateUser(User);
        }
        Debug.Log("User: " + User.Id + " " + User.Name);
    }

    public void Restart()
    {
        AfterLoading = AfterLoading.RestartLevel;
        LevelToLoad = LevelController.LevelName;
        LoadScene(SCENE.Loading);
    }

    public void GoToMainMenu()
    {
        AfterLoading = AfterLoading.Nothing;
        LevelToLoad = SCENE.MainMenu;
        Debug.Log("LevelToLoad: " + LevelToLoad);
        LoadScene(LevelToLoad);
    }

    public void LoadWaitedLevel()
    {
        switch (AfterLoading)
        {
            case AfterLoading.RestartLevel:
                LoadScene(LevelToLoad);
                break;
            case AfterLoading.GoToGame:
                LoadScene(SCENE.Game);
                break;
            case AfterLoading.Nothing:
            default:
                break;
        }
        AfterLoading = AfterLoading.Nothing;
    }

    public void LoadFirstLevel()
    {
        AfterLoading = AfterLoading.GoToGame;
        UIController._.LoadingController.TransitionOverlay(show: false, instant: false, () =>
        {
            LoadScene(SCENE.Loading);
        });
    }

    private void LoadScene(string level)
    {
        GameOver = false;
        SceneManager.LoadScene(level);
    }

    internal void OnGameOver()
    {
        GameOver = true;
        UIController._.DialogController.ShowDialog(true, GameplayState.Failed);

        int points = 0;
        // points = Level<LevelRandomRanked>().Points;
        WeekDetails week = __data.GetWeekDetails();

        if (HighScoreType == HighScoreType.RANKED)
        {
            TryUpdateLatestWeekScore(week, points);
        }
        UpdateHighScore(week, points);
    }

    private void TryUpdateLatestWeekScore(WeekDetails week, int points)
    {
        WeekScore weekScore = DataService.GetHighestWeekScore(week.Id);
        if (weekScore == null)
        {
            weekScore = new WeekScore()
            {
                Id = week.Id,
                Points = points,
                Year = week.Year,
                Week = week.Nr,
                UserId = User.Id
            };
            DataService.AddWeekHighScore(weekScore);
        }
        else
        {
            if (weekScore.Points >= points)
            {
                Debug.Log("weekScore: " + weekScore.Points);
            }
            else
            {
                weekScore.Points = points;
                DataService.UpdateWeekHighScore(weekScore);
            }
        }
    }

    private void UpdateHighScore(WeekDetails week, int points)
    {
        HighScore highScore = new HighScore()
        {
            Points = points,
            Type = HighScoreType,
            WeekId = week.Id,
            UserId = PlayingUser.Id,
            UserName = PlayingUser.Name
        };
        DataService.AddHighScore(highScore);
    }

    public T Level<T>()
    {
        return (T)Convert.ChangeType(LevelController.Level, typeof(T));
    }

    public int GetUniqueId()
    {
        _uniqueId++;
        return _uniqueId;
    }
}

public enum AfterLoading
{
    Nothing, RestartLevel, GoToGame
}
