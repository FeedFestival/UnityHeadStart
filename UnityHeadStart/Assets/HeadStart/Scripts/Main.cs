using System.Collections;
using UnityEngine;
using UniRx;
using System;
using Assets.HeadStart.Core;

public class Main : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.6";
#pragma warning restore 0414 //
    private static Main _instance;
    public static Main _ { get { return _instance; } }
    void Awake()
    {
        _instance = this;
    }

    public bool ConsoleLog;
    public bool CheckForUpdates;
    public GameBase Game;
    [HideInInspector]
    public CoreCamera CoreCamera;
    public string CoreExtension = "";
    private Subject<bool> _checkVersionSub__ = new Subject<bool>();
    private Subject<bool> _preStartGameSub__ = new Subject<bool>();
    private const float MILISECONDS_BETWEEN_CHECKS = 100;

    void Start()
    {
        CoreCamera = Camera.main.GetComponent<CoreCamera>();
        __.Transition.PitchBlack();

        if (ConsoleLog)
        {
            Debug.Log("Starting... Checking to Make Sure Everything is running");
        }

        _checkVersionSub__
            .Delay(TimeSpan.FromMilliseconds(MILISECONDS_BETWEEN_CHECKS))
            .Subscribe((bool _) =>
            {
#if UNITY_ANDROID
                CheckForUpdates = false;
#endif
#if UNITY_EDITOR
                if (CheckForUpdates) {
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
