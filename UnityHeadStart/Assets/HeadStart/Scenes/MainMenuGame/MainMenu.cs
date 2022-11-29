using Assets.HeadStart.Features.Database;
using HeadStart;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

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
        initValues();

        ButtonPlay.Init();
        ButtonPlay.OnClick(() =>
        {
            MenuEnvironment.S.SwitchView(VIEW.GameSession);
        });

        ButtonHighscore.Init();
        // ButtonHighscore.OnClick(() =>
        // {
        //     MenuEnvironment.S.SwitchView(VIEW.HighScore);
        // });

        ButtonChallenge.Init();
        ButtonChallenge.OnClick(() =>
        {
            MenuEnvironment.S.SwitchView(VIEW.Challenge);
        });
        ButtonSettings.OnClick(() =>
        {
            MenuEnvironment.S.SwitchView(VIEW.Settings);
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

        PlayerSettings playerSettings = null; // Main.S.Game.PlayerSettings();
        User user = null; // Main.S.Game.DeviceUser();
        UserNameTxt.text = playerSettings.name;
        League league = League.GetThisWeeksLeague();
        if (league != null)
        {
            WeekScoreResult weekScoreResult = null; // Main.S.Game.DataService.GetHighestScoreThisWeek(user.LocalId, league);
            if (weekScoreResult != null)
            {
                ToiletPaperTxt.text = playerSettings.toiletPaper.ToString();
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

    private void initValues()
    {
        UserNameTxt.text = "";
        ToiletPaperTxt.text = "";
        HighWeekPointsTxt.text = "";
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
