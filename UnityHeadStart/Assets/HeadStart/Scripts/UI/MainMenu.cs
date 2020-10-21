using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button Continue;
    public Button Start;
    public Button HighCharts;
    public Button Options;
    public Button Back;
    public GameObject ActualMainMenuView;
    public GameObject OptionsView;
    public GameObject HighScoreView;
    private bool _hasSavedGame;

    public void Init(bool showMenu = true, bool hasSavedGame = true)
    {
        _hasSavedGame = hasSavedGame;
        MusicManager._.PlayBackgroundMusic("MainMenuMusic");
        gameObject.SetActive(showMenu);
        SwitchView("Main");
    }

    public void SwitchView(string view)
    {
        MusicManager._.PlaySound("Click");
        switch (view)
        {
            case "Main":
                ActualMainMenuView.SetActive(true);
                OptionsView.SetActive(false);
                HighScoreView.SetActive(false);
                Continue.interactable = _hasSavedGame;
                Options.gameObject.SetActive(true);
                Back.gameObject.SetActive(false);
                break;
            case "Options":
                ActualMainMenuView.SetActive(false);
                OptionsView.SetActive(true);
                HighScoreView.SetActive(false);
                Options.gameObject.SetActive(false);
                Back.gameObject.SetActive(true);
                break;
            case "HighScore":
                ActualMainMenuView.SetActive(false);
                OptionsView.SetActive(false);
                HighScoreView.SetActive(true);
                Options.gameObject.SetActive(false);
                Back.gameObject.SetActive(true);
                break;
            default:
                Game._.LoadFirstLevel();
                break;
        }
    }
}
