using Assets.HeadStart.Core;
using Assets.HeadStart.Features.Database.JSON;
using GameScrypt.GSCamera;
using UnityEngine;

public class CoreCamera : GSCamera
{
    public CoreCameraSettings CoreCameraSettings;
    private CameraHelper _cameraHelper;

    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        GameSettings gameSettings = __json.Database.GetGameSettings();

        this.setCameraSettings(gameSettings);

        bool doWeNeedToAdjustCamera = gameSettings.isCameraSetForThisDevice == false;
        if (doWeNeedToAdjustCamera)
        {
            this.setDefaultCameraSettings();
            this.loadCameraSizerHelper();
            _cameraHelper.Init(this, foundSpot);
            GSLogo?.SetDefaultScale();
            GSLogo?.RevealLogo();
            GSLogo?.EnlargeLogo();
            this.changeLoading();
            _cameraHelper.ZoomOut();
        }
        else
        {
            LoadingVersion?.ChangeLoading(1f);
            __.Time.RxWait(() => { this.cameraIsReady(); }, 2f);
        }

        LoadingVersion?.ChangeLoading(0.1f);
    }

    private void setCameraSettings(GameSettings gameSettings) {
        CurrentCameraSize = gameSettings.cameraSize2D;
        OrthographicSize = CurrentCameraSize;
    }

    private void setDefaultCameraSettings()
    {
        CurrentCameraSize = 2;
        OrthographicSize = CurrentCameraSize / 2;
    }

    private void loadCameraSizerHelper()
    {
        var go = Instantiate(CoreCameraSettings.CameraHelperPrefab, Vector3.zero, Quaternion.identity);
        go.transform.position = new Vector3(-30, -10, -9.5f);
        _cameraHelper = go.GetComponent<CameraHelper>();
    }

    private void foundSpot()
    {
        GSLogo?.CancelAnimation();

        this.saveCameraSize();
        this.cameraIsReady();
    }

    private void saveCameraSize()
    {
        CurrentCameraSize = OrthographicSize;

        GameSettings gameSettings = __json.Database.GetGameSettings();
        gameSettings.isCameraSetForThisDevice = true;
        gameSettings.cameraSize2D = OrthographicSize;

        __json.Database.UpdateGameSettings(gameSettings);
    }

    private void changeLoading()
    {
        _zoomedOutCount++;
        var newVal = 1f / (1 + _zoomedOutCount);
        LoadingVersion?.ChangeLoading(newVal < 0.1f ? 0.1f : newVal);
    }

    private void activateDebug()
    {
        string debugConsoleName = "IngameDebugConsole";
        GameObject go = GameObject.Find(debugConsoleName);
        if (go == null)
        {
            go = Instantiate(CoreCameraSettings.IngameDebugConsole, Vector3.zero, Quaternion.identity);
        }
        go.name = debugConsoleName;
        _debugActivated = true;
        go.SetActive(true);
    }

    private void cameraIsReady()
    {
        GSLogo?.HideLogo();
        LoadingVersion?.CleanDestroy();
        Main.S._CameraReady__.OnNext(true);
    }

    // ---- testing ----

    void Update()
    {
        if (_debugActivated)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Tilde)
            || Input.GetKeyUp(KeyCode.AltGr)
            || Input.GetKeyUp(KeyCode.BackQuote)
        )
        {
            this.activateDebug();
        }
    }
}
