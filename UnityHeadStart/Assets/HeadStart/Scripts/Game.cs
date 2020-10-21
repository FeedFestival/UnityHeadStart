using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game _game;
    public static Game _ { get { return _game; } }

    public LevelController LevelController;
    public Player Player;
    public DataService DataService;
    public User User;
    public bool RestartLevel;
    private string LevelToLoad;

    void Awake()
    {
        _game = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Init()
    {
        Debug.Log("Game - Init");

        GetUser();
        Debug.Log(User.Name);

        UIController._.Init();

        LevelController.Init();

        UIController._.LoadingController.TransitionOverlay(show: true, instant: false);
    }

    private void GetUser()
    {
        DataService = new DataService();
        User = DataService.GetLastUser();
        if (User == null)
        {
            User = new User()
            {
                Id = 1,
                Name = "no-name-user",
                IsFirstTime = true,
                HasSavedGame = false,
                IsUsingSound = true,
                Language = "en"
            };
            DataService.CreateUser(User);
        }
    }

    public void Restart()
    {
        RestartLevel = true;
        LevelToLoad = LevelController.LevelName;
        SceneManager.LoadScene("Loading");
    }

    public void LoadWaitedLevel()
    {
        if (RestartLevel)
        {
            RestartLevel = false;
            SceneManager.LoadScene(LevelToLoad);
            return;
        }
    }

    public void LoadFirstLevel()
    {
        Debug.Log("Load First Level");
    }
}
