using System;
using System.Collections.Generic;
using Assets.HeadStart.Core;
using MyBox;
using UnityEngine;

public class MenuEnvironment : MonoBehaviour
{
    private static MenuEnvironment _this;
    public static MenuEnvironment _ { get { return _this; } }
    void Awake() { _this = this; }

    [Serializable]
    public struct UiViewDictionary
    {
        public VIEW View;
        public GameObject UiViewGo;
    }
    public UiViewDictionary[] UiList;
    public Dictionary<VIEW, IUiView> Views;
    public EnvEllipse[] Ellipses;
    private MenuHistory _history;
    public VIEW View;
    private int? _moveCameraTwid;
    private Vector3 _moveCameraTo;
    public readonly float MOVE_CAMERA_TIME = 2f;
    private int? _scaleCameraTwid;
    private const float TO_CAMERA_SIZE = 30f;
    [HideInInspector]
    public bool InputNameForChallenge = false;
    private SessionOpts _sessionOpts;

    public void Init()
    {
        InitViews();
        bool hasCoreSession = CoreSession._ != null;
        if (hasCoreSession)
        {
            SwitchView(VIEW.GameSession, instant: true);
        }
        else
        {
            SwitchView(VIEW.Initial, instant: true);
        }
        playBackgroundAnimations();
    }

    private void InitViews()
    {
        Views = new Dictionary<VIEW, IUiView>();
        foreach (var obj in UiList)
        {
            var uiView = obj.UiViewGo.GetComponent<IUiView>();
            uiView.GO().SetActive(true);
            Views.Add(obj.View, uiView);
        }
        UiList = null;
    }

    public void SwitchView(VIEW view, bool instant = false, bool storeHistory = true)
    {
        if (_history == null)
        {
            _history = new MenuHistory(new List<VIEW>() { VIEW.Initial, VIEW.GameSession });
        }
        if (storeHistory)
        {
            _history.Push(View);
        }

        View = view;
        Views[View].Focus();

        _moveCameraTo = Views[View].GO().transform.position;
        if (instant)
        {
            Main._.CoreCamera.transform.position = new Vector3(
                _moveCameraTo.x,
                _moveCameraTo.y,
                Main._.CoreCamera.transform.position.z
            );
            __.Time.RxWait(() => { Views[View].OnFocussed(); }, 1);
            return;
        }

        playCameraTransition();
    }

    internal void SetChallengeSession(SessionOpts sessionOpts)
    {
        _sessionOpts = sessionOpts;
    }

    internal void UpdateSessionUserId(int userLocalId)
    {
        _sessionOpts.User.LocalId = userLocalId;
    }

    internal SessionOpts GetChallengeSession()
    {
        return _sessionOpts;
    }

    internal void ClearChallengeSession()
    {
        _sessionOpts = null;
    }

    internal void SetupBackToMainMenuFor(VIEW view)
    {
        switch (view)
        {
            case VIEW.HighScore:
            case VIEW.Challenge:
                _history.Clear();
                _history.Push(VIEW.MainMenu);
                break;
            default:
                break;
        }
    }

    internal void Back()
    {
        VIEW lastView = _history.Pop();
        SwitchView(lastView, storeHistory: false);
    }

    private void playCameraTransition()
    {
        _moveCameraTwid = LeanTween.move(
            Main._.CoreCamera.gameObject,
            _moveCameraTo,
            MOVE_CAMERA_TIME
        ).id;
        LeanTween.descr(_moveCameraTwid.Value).setEase(LeanTweenType.easeInOutQuart);
        LeanTween.descr(_moveCameraTwid.Value).setOnComplete(() =>
        {
            Views[View].OnFocussed();
        });

        _scaleCameraTwid = LeanTween.value(
            Main._.CoreCamera.gameObject,
            Main._.CoreCamera.GetCameraCurrentSize(),
            TO_CAMERA_SIZE,
            MOVE_CAMERA_TIME / 2
        ).id;
        LeanTween.descr(_scaleCameraTwid.Value).setEase(LeanTweenType.easeInOutQuart);
        LeanTween.descr(_scaleCameraTwid.Value).setOnUpdate((float val) =>
        {
            Camera.main.orthographicSize = val;
        });
        LeanTween.descr(_scaleCameraTwid.Value).setOnComplete(() =>
        {
            _scaleCameraTwid = LeanTween.value(
                Main._.CoreCamera.gameObject,
                TO_CAMERA_SIZE,
                Main._.CoreCamera.GetCameraCurrentSize(),
                MOVE_CAMERA_TIME / 2
            ).id;
            LeanTween.descr(_scaleCameraTwid.Value).setOnUpdate((float val) =>
            {
                Camera.main.orthographicSize = val;
            });
        });
    }

    private void playBackgroundAnimations()
    {
        __.Time.AsyncForEach(Ellipses, (object obj) =>
        {
            obj.As<EnvEllipse>().Play();
        }, 0.25f);
    }
}
