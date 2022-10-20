using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Assets.HeadStart.Core;

public class Main : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    private static Main _instance;
    public static Main S { get { return _instance; } }
    public bool ConsoleLog;
    public bool CheckForUpdates;
    public GameBase Game;
    [HideInInspector]
    public CoreCamera CoreCamera;
    public string CoreExtension = "";
    private Subject<bool> _checkVersionSub__ = new Subject<bool>();
    private Subject<bool> _preStartGameSub__ = new Subject<bool>();
    public Subject<bool> _CameraReady__ = new Subject<bool>();
    public Subject<bool> _EnvironmentReady__ = new Subject<bool>();
    private IDisposable _combineLatestObs__;
    private const float MILISECONDS_BETWEEN_CHECKS = 100;

    void Awake()
    {
        _instance = this;

        _combineLatestObs__ = Observable
            .CombineLatest(_CameraReady__, _EnvironmentReady__, (cam, env) => cam && env)
            .Subscribe(continueStartingGame =>
            {
                _combineLatestObs__.Dispose();
                
                if (!continueStartingGame) return;

                Game.StartGame();
            });
    }

    void Start()
    {
        CoreCamera = Camera.main.GetComponent<CoreCamera>();
        __.Transition.PitchBlack();

        if (ConsoleLog) Debug.Log("Starting... Checking to Make Sure Everything is running");

        _checkVersionSub__
            .Delay(TimeSpan.FromMilliseconds(MILISECONDS_BETWEEN_CHECKS))
            .Subscribe((bool _) =>
            {
#if UNITY_ANDROID
                CheckForUpdates = false;
#endif
#if UNITY_EDITOR
                if (CheckForUpdates)
                {
                    VersionChecker versionChecker = gameObject.AddComponent<VersionChecker>();
                    versionChecker.Check();
                    Destroy(gameObject.GetComponent<VersionChecker>());
                }
#endif
                _preStartGameSub__.OnNext(true);
            });

        _preStartGameSub__
            .Delay(TimeSpan.FromMilliseconds(MILISECONDS_BETWEEN_CHECKS))
            .Subscribe((bool _) =>
            {
                _checkVersionSub__.Dispose();
                _preStartGameSub__.Dispose();

                Game.PreStartGame();
            });

        _checkVersionSub__.OnNext(true);
    }
}
