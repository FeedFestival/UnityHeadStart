using UnityEngine;
using UniRx;
using System.Collections.Generic;
using Assets.HeadStart.Core;

public class HighScoreView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonUpload;
    public GameButton ButtonPlay;
    private bool _isInitialized;
    private Dictionary<string, string> _requestHeaders = new Dictionary<string, string>();
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

        // TODO: check if user is registered

        // if not -> 
        User user = new User()
        {
            LocalId = 1,
            Name = "Dani Sim"
        };
        string jsonUser = JsonUtility.ToJson(user.Debug());
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(jsonUser);
        var auth = System.Convert.ToBase64String(plainTextBytes);
        _requestHeaders.Add("Authorization", auth);

        // if user registered -> GetUserLeague.php

        var latitude = 44.177269f;
        var longitude = 28.652880f;

        var url = "http://localhost/gameScrypt/be/Ranked/GetUserLeague.php";
        var secret = "a=gameScrypt";
        var latReq = "lat=" + latitude;
        var longReq = "lng=" + longitude;
        var fullUrl = url + "?" + secret + "&" + latReq + "&" + longReq;
        Debug.Log("fullUrl: " + fullUrl);

        ObservableUnityWebRequest
            .GetAsObservable(fullUrl, _requestHeaders)
            .Subscribe(responseBody =>
            {
                Debug.Log(responseBody);
                RankingLeague rLeague = JsonUtility.FromJson<RankingLeague>(responseBody);
                CheckLeague(rLeague);
            });
    }

    public void OnFocussed()
    {
        __.Time.RxWait(() =>
        {
            // ButtonHighscore.Interactable = false;
        }, 1f);
    }

    private void CheckLeague(RankingLeague rLeague)
    {
        var url = "http://localhost/gameScrypt/be/Ranked/CheckLeague.php";
        var secret = "a=gameScrypt";
        var points = "p=" + "1812";
        var fullUrl = url + "?" + secret + "&" + rLeague.ToRouteParams() + "&" + points;
        Debug.Log("fullUrl: " + fullUrl);

        ObservableUnityWebRequest
            .GetAsObservable(fullUrl, _requestHeaders)
            .Subscribe(responseBody => Debug.Log(responseBody));
    }
}
