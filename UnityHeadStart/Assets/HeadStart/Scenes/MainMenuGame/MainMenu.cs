using System.Collections.Generic;
using Assets.HeadStart.Core.SceneService;
using UnityEngine;
using UnityEngine.UI;

public delegate void Clicked();

public class MainMenu : MonoBehaviour, IUiView
{
    public GameButton ButtonPlay;
    public GameButton ButtonHighscore;
    public GameButton ButtonChallenge;
    public GameButton ButtonSettings;

    private bool _isInitialized;

    private void Init()
    {
        ButtonPlay.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.GameSession);
        });
        ButtonHighscore.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.HighScore);
        });
        ButtonChallenge.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.HotSeat);
        });
        ButtonSettings.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.Settings);
        });

        _isInitialized = true;
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    public void Focus()
    {
        if (_isInitialized == false)
        {
            Init();
        }
    }
}
