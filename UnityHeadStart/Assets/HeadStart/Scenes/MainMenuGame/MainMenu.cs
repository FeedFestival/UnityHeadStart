using Assets.HeadStart.Core;
using Assets.Scripts.utils;
using TMPro;
using UnityEngine;

public delegate void Clicked();

public class MainMenu : MonoBehaviour, IUiView
{
    public GameButton ButtonPlay;
    public GameButton ButtonHighscore;
    public GameButton ButtonChallenge;
    public GameButton ButtonSettings;
    public TextMeshPro UserNameTxt;
    public TextMeshPro ToiletPaperTxt;
    public TextMeshPro HighWeekPointsTxt;

    private bool _isInitialized;

    private void Init()
    {
        ButtonPlay.Init();
        ButtonPlay.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.GameSession);
        });

        ButtonHighscore.Init();
        // ButtonHighscore.OnClick(() =>
        // {
        //     MenuEnvironment._.SwitchView(VIEW.HighScore);
        // });

        ButtonChallenge.Init();
        ButtonChallenge.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.Challenge);
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

        ResetActions();

        User user = Main._.Game.DeviceUser();
        League league = __data.GetThisWeeksLeague();
        WeekScoreResult weekScoreResult = Main._.Game.DataService.GetHighestScoreThisWeek(user.LocalId, league);
        UserNameTxt.text = user.Name;
        ToiletPaperTxt.text = user.ToiletPaper.ToString();
        HighWeekPointsTxt.text = weekScoreResult.Points.ToString();
    }

    public void OnFocussed()
    {
        EnableActions();
    }


    private void ResetActions()
    {
        ButtonPlay.Reset();
        ButtonHighscore.Reset();
        ButtonChallenge.Reset();
    }

    private void EnableActions()
    {
        ButtonPlay.Enable();
        // ButtonHighscore.Enable();
        ButtonChallenge.Enable();
    }
}
