using System;
using System.Collections.Generic;
using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SFX;
using MyBox;
using UnityEngine;

public class MenuEnvironment : MonoBehaviour
{
    private static MenuEnvironment _this;
    public static MenuEnvironment S { get { return _this; } }
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
        initViews();
        switchViewToTheAppropriateView();
        playBackgroundAnimations();
        playBackgroundMusic();
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
            Main.S.CoreCamera.transform.position = new Vector3(
                _moveCameraTo.x,
                _moveCameraTo.y,
                Main.S.CoreCamera.transform.position.z
            );
            if (Views[View].UiViewFocussed == null) { return; }
            __.Time.RxWait(() => { Views[View].UiViewFocussed.Invoke(); }, 1);
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

    private void initViews()
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

    private void switchViewToTheAppropriateView()
    {
        bool hasCoreSession = CoreSession.S != null;
        if (hasCoreSession)
        {
            SwitchView(VIEW.GameSession, instant: true);
        }
        else
        {
            SwitchView(VIEW.Initial, instant: true);
        }
    }

    private void playCameraTransition()
    {
        _moveCameraTwid = LeanTween.move(
            Main.S.CoreCamera.gameObject,
            _moveCameraTo,
            MOVE_CAMERA_TIME
        ).id;
        LeanTween.descr(_moveCameraTwid.Value).setEase(LeanTweenType.easeInOutQuart);
        LeanTween.descr(_moveCameraTwid.Value).setOnComplete(() =>
        {
            Views[View].UiViewFocussed.Invoke();
        });

        _scaleCameraTwid = LeanTween.value(
            Main.S.CoreCamera.gameObject,
            Main.S.CoreCamera.CurrentCameraSize,
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
                Main.S.CoreCamera.gameObject,
                TO_CAMERA_SIZE,
                Main.S.CoreCamera.CurrentCameraSize,
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

    private void playBackgroundMusic()
    {
        MusicOpts opts = new MusicOpts("MainMenuMusic");
        __.SFX.PlayBackgroundMusic(opts);
    }
}
