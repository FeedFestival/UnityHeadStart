using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    public GameObject GameDialog;
    public Text Title;
    public Text Info;
    public GameObject RetryButton;
    public GameObject ContinueButton;

    public void ShowDialog(bool value, GameplayState gameplayState = GameplayState.Starting)
    {
        GameDialog.SetActive(value);

        if (value)
        {
            switch (gameplayState)
            {
                case GameplayState.Finished:
                    Title.text = "Congrats!";
                    Info.text = "You've completed the level!";
                    RetryButton.SetActive(false);
                    ContinueButton.SetActive(true);
                    break;
                case GameplayState.Failed:
                default:
                    Title.text = "Too bad!";
                    Info.text = "You lost...";
                    RetryButton.SetActive(true);
                    ContinueButton.SetActive(true);
                    break;
            }
        }
    }

    public void OnContinue()
    {
        Game._.GoToMainMenu();
    }

    public void OnRestart()
    {
        Game._.Restart();
    }
}
