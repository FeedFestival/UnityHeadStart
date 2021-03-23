using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiDataController : MonoBehaviour
{
    public Text CoinsText;
    public Text PointsText;
    public Text FpsText;
    public int avgFrameRate;

    internal void Init()
    {

    }

    public void Update()
    {
        float current = 0;
        current = Time.frameCount / Time.time;
        avgFrameRate = (int)current;
        FpsText.text = avgFrameRate.ToString();
    }

    public void UpdateText(int value, UiDataType uiDataType)
    {
        switch (uiDataType)
        {
            case UiDataType.Coin:
                CoinsText.text = value.ToString();
                break;
            case UiDataType.Point:
                PointsText.text = value.ToString();
                break;
            case UiDataType.Fps:
                FpsText.text = value.ToString();
                break;
            default:
                break;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        UIController._.DialogController.ShowDialog(true, GameplayState.Failed);
    }

    public void ShowIngameOptions()
    {
        // here we do something else for now, we stop the game
        Game._.OnGameOver();
    }
}

public enum UiDataType
{
    Coin, Point, Fps
}
