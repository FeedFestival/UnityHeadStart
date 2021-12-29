using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonUpload;
    public GameButton ButtonPlay;
    private bool _isInitialized;
    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            MenuEnvironment._.Back();
        });
        ButtonUpload.OnClick(() =>
        {
            MenuEnvironment._.SwitchView(VIEW.HighScore);
        });
        ButtonPlay.OnClick(() =>
        {
            Debug.Log("Play Game");
        });

        _isInitialized = true;
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    void IUiView.Focus()
    {
        if (_isInitialized == false)
        {
            Init();
        }
    }
}
