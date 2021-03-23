using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    public GameObject HiddenSettingsPrefab;

    [Header("GOs")]
    public LevelController LevelController;
    private Game _game;
    private IEnumerator _waitForLevelLoad;

    private IEnumerator _firstCheck;
    private IEnumerator _afterCheck;

    void Awake()
    {
        Debug.Log("Starting... Checking to Make Sure Everything is running");

        _firstCheck = FirstCheck();
        StartCoroutine(_firstCheck);
    }

    IEnumerator FirstCheck()
    {
        yield return new WaitForSeconds(0.1f);

        var domainLogic = FindObjectOfType<DomainLogic>();
        if (domainLogic != null)
        {
            Destroy(domainLogic.gameObject);
        }

        GameObject go;
        var settings = FindObjectOfType<HiddenSettings>();
        if (settings == null)
        {
            go = Instantiate(HiddenSettingsPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            settings = go.GetComponent<HiddenSettings>();
            settings.ActualScreenSize = new Vector2Int(CameraResolution._.ScreenSizeX, CameraResolution._.ScreenSizeY);
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

        if (FindObjectOfType<ColorBank>() == null)
        {
            go = Instantiate(prefabBank.ColorBankPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ColorBank._.CalculateColors();
        }

        if (_game.LevelController == null)
        {
            _game.LevelController = LevelController;
        }

        Debug.Log("_game.AfterLoading: " + _game.AfterLoading);

        if (_game.AfterLoading == AfterLoading.GoToGame || _game.AfterLoading == AfterLoading.Nothing)
        {
            var player = FindObjectOfType<Player>();
            if (player == null)
            {
                Debug.LogError("No Player");
            }
            else
            {
                if (_game.Player == null)
                {
                    _game.Player = player;
                }
            }
        }
        go = null;

#if UNITY_EDITOR
        VersionChecker versionChecker = gameObject.AddComponent<VersionChecker>();
        versionChecker.Check();
        Destroy(gameObject.GetComponent<VersionChecker>());
#endif

        _afterCheck = AfterCheck();
        StartCoroutine(_afterCheck);
    }

    IEnumerator AfterCheck()
    {
        Debug.Log("Main - Check Completed, starting...");

        yield return new WaitForSeconds(0.1f);

        if (_game.LevelController.LevelType == LevelType.Loading)
        {
            _waitForLevelLoad = WaitForLevelLoad();
            StartCoroutine(_waitForLevelLoad);
        }
        else
        {
            _game.Init();
        }
    }

    private IEnumerator WaitForLevelLoad()
    {
        float loadingWait = _game != null
            && _game.AfterLoading == AfterLoading.RestartLevel ? 0.5f : 2f;
        Debug.Log("loadingWait: " + loadingWait);
        yield return new WaitForSeconds(loadingWait);

        _game.LoadWaitedLevel();
        StopCoroutine(_waitForLevelLoad);
        _waitForLevelLoad = null;
    }
}
