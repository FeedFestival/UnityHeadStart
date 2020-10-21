﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public bool IsThisTheLoadingScene;
    public GameObject HiddenSettingsPrefab;

    [Header("GOs")]
    public LevelController LevelController;
    private Game _game;
    private IEnumerator _waitForLevelLoad;

    void Awake()
    {
        GameObject go;
        var settings = FindObjectOfType<HiddenSettings>();
        if (settings == null)
        {
            go = Instantiate(HiddenSettingsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            settings = go.GetComponent<HiddenSettings>();
        }

        var prefabBank = FindObjectOfType<PrefabBank>();
        if (prefabBank == null)
        {
            go = Instantiate(settings.PrefabBankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            prefabBank = go.GetComponent<PrefabBank>();
        }

        _game = FindObjectOfType<Game>();
        if (_game == null)
        {
            go = Instantiate(prefabBank.GamePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            _game = go.GetComponent<Game>();
        }

        if (FindObjectOfType<Timer>() == null)
        {
            go = Instantiate(prefabBank.TimerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (FindObjectOfType<MusicManager>() == null)
        {
            go = Instantiate(prefabBank.MusicManagerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            MusicManager._.Init();
        }

        if (IsThisTheLoadingScene == false)
        {
            if (_game.LevelController == null)
            {
                _game.LevelController = LevelController;
            }

            var player = FindObjectOfType<Player>();
            if (_game.Player == null)
            {
                _game.Player = player;
            }
        }
        go = null;
    }

    void Start()
    {
        Debug.Log("Main - Check Completed, starting...");
        Timer._.InternalWait(() =>
        {
            if (IsThisTheLoadingScene)
            {
                _waitForLevelLoad = WaitForLevelLoad();
                StartCoroutine(_waitForLevelLoad);
            }
            else
            {
                _game.Init();
            }
        }, 0.1f);
    }

    private IEnumerator WaitForLevelLoad()
    {
        yield return new WaitForSeconds(_game != null && _game.RestartLevel ? 0.5f : 2f);

        _game.LoadWaitedLevel();
        StopCoroutine(_waitForLevelLoad);
        _waitForLevelLoad = null;
    }
}
