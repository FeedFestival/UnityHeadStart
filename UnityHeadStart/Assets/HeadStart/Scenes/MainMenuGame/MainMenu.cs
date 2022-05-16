using Assets.HeadStart.Features.Database;
using Assets.Scripts.utils;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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

    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;

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

        uiViewFocussed += onFocussed;

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
        UserNameTxt.text = user.Name;
        League league = __data.GetThisWeeksLeague();
        if (league != null)
        {
            WeekScoreResult weekScoreResult = Main._.Game.DataService.GetHighestScoreThisWeek(user.LocalId, league);
            if (weekScoreResult != null)
            {
                ToiletPaperTxt.text = user.ToiletPaper.ToString();
                HighWeekPointsTxt.text = weekScoreResult.Points.ToString();
            }
            return;
        }

        ToiletPaperTxt.text = string.Empty;
        HighWeekPointsTxt.text = string.Empty;
    }

    private void onFocussed()
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
