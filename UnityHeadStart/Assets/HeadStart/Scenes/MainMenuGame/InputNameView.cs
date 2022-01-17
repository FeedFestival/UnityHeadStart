﻿using Assets.HeadStart.Core;
using UnityEngine;

public class InputNameView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    public Transform TLPoint;
    public Transform BRPoint;
    private InputNameCanvas _inputNameCanvas;
    public InputNameSettings InputNameSettings;
    private bool _isInitialized;
    private string _userInputChangedName;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            _inputNameCanvas.gameObject.SetActive(false);
            _inputNameCanvas.CancelChallenge();
            MenuEnvironment._.ClearChallengeSession();
            MenuEnvironment._.Back();
        });
        ButtonPlay.OnClick(() =>
        {
            _inputNameCanvas.CancelChallenge();
            _inputNameCanvas.gameObject.SetActive(false);

            if (Main._.Game.DeviceUser().IsFirstTime)
            {
                User changedUser = Main._.Game.DeviceUser();
                changedUser.Name = _userInputChangedName;
                changedUser.IsFirstTime = false;
                Main._.Game.DataService.UpdateUser(changedUser);
                Main._.Game.LoadUser();
            }
            else
            {
                User playingUser = MenuEnvironment._.GetChallengeSession().User;
                if (playingUser.LocalId == 0)
                {
                    playingUser.LocalId = Main._.Game.DataService.CreateUser(playingUser);
                    MenuEnvironment._.UpdateSessionUserId(playingUser.LocalId);
                    Debug.Log(JsonUtility.ToJson(MenuEnvironment._.GetChallengeSession().User.Debug()));
                }
                else
                {
                    Debug.Log("User exists");
                }
                MenuEnvironment._.GetChallengeSession().IsChallenge = true;
            }
            MenuEnvironment._.SwitchView(VIEW.GameSession);
        });

        var go = Instantiate(
            InputNameSettings.InputNameConvas,
            Vector3.zero,
            Quaternion.identity,
            parent: Main._.CoreCamera.Views
        );
        (go.transform as RectTransform).localPosition = Vector3.zero;
        _inputNameCanvas = go.GetComponent<InputNameCanvas>();

        _isInitialized = true;
    }

    private void ReInit()
    {
        bool isFirstTime = Main._.Game.DeviceUser().IsFirstTime;
        if (isFirstTime)
        {
            ButtonBack.gameObject.SetActive(false);
        }
        else
        {
            ButtonBack.gameObject.SetActive(true);
        }
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

        ReInit();
        ButtonPlay.Interactable = false;

        __.Time.RxWait(() =>
        {
            _inputNameCanvas.gameObject.SetActive(true);
            _inputNameCanvas.Show(TLPoint, BRPoint, (object obj) =>
            {
                bool isValid = obj != null;
                ButtonPlay.Interactable = isValid;

                if (Main._.Game.DeviceUser().IsFirstTime)
                {
                    _userInputChangedName = (obj as string);
                    return;
                }

                if (isValid)
                {
                    SessionOpts sessionOpts = obj as SessionOpts;
                    MenuEnvironment._.SetChallengeSession(sessionOpts);
                }
            });

            ButtonPlay.Interactable = true;

            if (TLPoint == null)
            {
                return;
            }
            Destroy(TLPoint.gameObject);
            TLPoint = null;
            Destroy(BRPoint.gameObject);
            BRPoint = null;
        }, MenuEnvironment._.MOVE_CAMERA_TIME);
    }
}
