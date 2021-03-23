using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button Start;
    public Button HighCharts;
    public Button Options;
    public Button Back;
    public ActualMainMenuView ActualMainMenuView;
    public OptionsView OptionsView;
    public HighScoreView HighScoreView;
    public HotSeatView HotSeatView;
    public InputNameView InputNameView;
    Dictionary<string, IUiView> Views;

    void Awake()
    {
        InitViews();
        SetHeaderView(HEADVIEW.Nothing);
        SwitchView(VIEW.Nothing);
    }

    public void Init(bool showMenu = true, string overrideView = VIEW.Main)
    {
        MusicManager._.PlayBackgroundMusic("MainMenuMusic");
        gameObject.SetActive(showMenu);
        SwitchView(overrideView);
    }

    public void SwitchView(string view)
    {
        if (MusicManager._ != null)
        {
            MusicManager._.PlaySound("Click");
        }
        switch (view)
        {
            case VIEW.Nothing:
                foreach (KeyValuePair<string, IUiView> pair in Views)
                {
                    pair.Value.Gobject.SetActive(false);
                }
                break;
            case VIEW.InputName:
            case VIEW.Main:
            case VIEW.Options:
            case VIEW.HotSeat:
            case VIEW.HighScore:
                foreach (KeyValuePair<string, IUiView> pair in Views)
                {
                    bool isActiveView = pair.Key == view;
                    pair.Value.Gobject.SetActive(isActiveView);
                    if (isActiveView)
                    {
                        pair.Value.OnShow();
                    }
                }
                break;
            default:
                Game._.HighScoreType = HighScoreType.RANKED;
                Game._.PlayingUser = Game._.User;
                Game._.LoadFirstLevel();
                break;
        }
    }

    public void SetHeaderView(HEADVIEW headerView)
    {
        switch (headerView)
        {
            case HEADVIEW.Nothing:
                Back.gameObject.SetActive(false);
                Options.gameObject.SetActive(false);
                break;
            case HEADVIEW.All:
                Back.gameObject.SetActive(true);
                Options.gameObject.SetActive(true);
                break;
            case HEADVIEW.BackAndCog:
                Back.gameObject.SetActive(true);
                Options.gameObject.SetActive(true);
                break;
            case HEADVIEW.Back:
                Back.gameObject.SetActive(true);
                Options.gameObject.SetActive(false);
                break;
            case HEADVIEW.Cog:
                Back.gameObject.SetActive(false);
                Options.gameObject.SetActive(true);
                break;
            default:

                break;
        }
    }

    private void InitViews()
    {
        Views = new Dictionary<string, IUiView>();
        Views.Add(VIEW.Main, ActualMainMenuView);
        Views.Add(VIEW.Options, OptionsView);
        Views.Add(VIEW.HighScore, HighScoreView);
        Views.Add(VIEW.HotSeat, HotSeatView);
        Views.Add(VIEW.InputName, InputNameView);
    }
}

public enum HEADVIEW
{
    Nothing, All, BackAndCog, Back, Cog
}
