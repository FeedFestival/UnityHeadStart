using Assets.HeadStart.Core;
using Assets.HeadStart.Features.Database.JSON;
using UnityEngine;
using UnityEngine.UI;

public class CoreCamera : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public RectTransform CanvasRt;
    [HideInInspector]
    public Canvas Canvas;
    public RectTransform Views;
    public Image LoadingOverlay;
    // TODO: add Logo functionality to HeadStart
    public Transform LogoT;
    public CoreCameraSettings CoreCameraSettings;
    public LoadingVersion LoadingVersion;
    private Camera _camera;
    private CameraHelper _cameraHelper;
    private float _currentCameraSize;
    private float _multiplierCameraSize;
    private float _toCameraSize;
    private Vector3 _toLogoSize;
    private int? _alignCameraToHelperTwid;
    private int? _enlargeLogoTwid;
    public static readonly float CAMERA_SETUP_TIME = 8f;
    private const float HIDE_LOGO_TIME = 0.77f;
    private readonly Vector3 TO_LOGO_SIZE = new Vector3(44, 44, 44);
    private bool _foundTheSweetSpot;
    private bool _debugActivated;
    private bool _foundBoundsFlag = false;
    private float _zoomedOutCount;

    void Awake()
    {
        Canvas = CanvasRt.GetComponent<Canvas>();
    }

    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        adjustCameraForDevice();
        if (LoadingVersion) { LoadingVersion.ChangeLoading(0.1f); }
    }

    private void adjustCameraForDevice()
    {
        GameSettings gameSettings = __json.Database.GetGameSettings();
        if (gameSettings.isCameraSetForThisDevice == false)
        {
            instantiateCameraHelperAndSetup();
            if (LogoT)
            {
                revealLogo();
                zoomOut();
            }
        }
        else
        {
            _currentCameraSize = gameSettings.cameraSize2D;
            _camera.orthographicSize = _currentCameraSize;
            if (LoadingVersion) { LoadingVersion.ChangeLoading(1f); }
            __.Time.RxWait(() => { CameraIsReady(); }, 2f);
        }
    }

    public float GetCameraCurrentSize()
    {
        return _currentCameraSize;
    }

    private void instantiateCameraHelperAndSetup()
    {
        var go = Instantiate(CoreCameraSettings.CameraHelperPrefab, Vector3.zero, Quaternion.identity);
        go.transform.position = new Vector3(-30, -10, -9.5f);
        _cameraHelper = go.GetComponent<CameraHelper>();

        _multiplierCameraSize = 3;
        _currentCameraSize = 2;
        _camera.orthographicSize = _currentCameraSize / 2;
        if (LogoT)
        {
            LogoT.localScale = new Vector3(4, 4, 4);
        }
    }

    private void zoomOut()
    {
        _currentCameraSize = _camera.orthographicSize;
        _toCameraSize = _currentCameraSize + _multiplierCameraSize;
        _toLogoSize = (_toLogoSize + (TO_LOGO_SIZE * 2));

        this.enlargeLogo();
        this.changeLoading();

        _alignCameraToHelperTwid = LeanTween.value(
            gameObject,
            _currentCameraSize,
            _toCameraSize,
            CAMERA_SETUP_TIME
        ).id;
        LeanTween.descr(_alignCameraToHelperTwid.Value).setEase(LeanTweenType.linear);
        LeanTween.descr(_alignCameraToHelperTwid.Value).setOnUpdate((float val) =>
        {
            if (_foundBoundsFlag) { return; }

            _camera.orthographicSize = val;
            LoadingVersion.ChangeVersion(val);
            if (_cameraHelper.CanSeeBounds())
            {
                _foundBoundsFlag = true;
                _foundTheSweetSpot = true;
                foundSpot();
            }
        });
        LeanTween.descr(_alignCameraToHelperTwid.Value).setOnComplete(() =>
        {
            if (_foundTheSweetSpot == false)
            {
                zoomOut();
                return;
            }
        });
    }

    private void foundSpot()
    {
        LeanTween.cancel(_alignCameraToHelperTwid.Value);
        _alignCameraToHelperTwid = null;

        LeanTween.cancel(_enlargeLogoTwid.Value);
        _enlargeLogoTwid = null;

        saveCameraSize();
        CameraIsReady();
    }

    private void saveCameraSize()
    {
        _currentCameraSize = _camera.orthographicSize;

        GameSettings gameSettings = __json.Database.GetGameSettings();
        gameSettings.isCameraSetForThisDevice = true;
        gameSettings.cameraSize2D = _currentCameraSize;

        __json.Database.UpdateGameSettings(gameSettings);
    }

    private void enlargeLogo()
    {
        _enlargeLogoTwid = LeanTween.scale(LogoT.gameObject, _toLogoSize, CAMERA_SETUP_TIME).id;
    }

    private void changeLoading()
    {
        _zoomedOutCount++;
        var newVal = 1f / (1 + _zoomedOutCount);
        LoadingVersion.ChangeLoading(newVal < 0.1f ? 0.1f : newVal);
    }

    private void revealLogo()
    {
        LeanTween.alpha(LogoT.gameObject, 1f, CAMERA_SETUP_TIME);
    }

    private void hideLogo()
    {
        if (LogoT == null) { return; }

        LeanTween.alpha(LogoT.gameObject, 0f, HIDE_LOGO_TIME)
            .setOnComplete(() =>
            {
                Destroy(LogoT.gameObject);
            });
    }

    private void hideLoading()
    {
        if (!LoadingVersion) { return; }
        LoadingVersion.CleanDestroy();
    }

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
            ActivateDebug();
        }
    }

    private void ActivateDebug()
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

    private void CameraIsReady()
    {
        this.hideLogo();
        this.hideLoading();
        Main.S._CameraReady__.OnNext(true);
    }
}
