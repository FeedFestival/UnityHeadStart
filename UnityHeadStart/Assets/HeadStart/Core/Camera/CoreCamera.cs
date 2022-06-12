using Assets.HeadStart.Features.Database.JSON;
using UnityEngine;
using UnityEngine.UI;

public class CoreCamera : MonoBehaviour
{
    public RectTransform CanvasRt;
    [HideInInspector]
    public Canvas Canvas;
    public RectTransform Views;
    public Image LoadingOverlay;
    // TODO: add Logo functionality to HeadStart
    public Transform LogoT;
    public CoreCameraSettings CoreCameraSettings;
    private Camera _camera;
    private CameraHelper _cameraHelper;
    private float _currentCameraSize;
    private float _toCameraSize;
    private Vector3 _toLogoSize;
    private int? _alignCameraToHelperTwid;
    private int? _enlargeLogoTwid;
    public const float CAMERA_SETUP_TIME = 4f;
    private const float HIDE_LOGO_TIME = 0.77f;
    private readonly Vector3 TO_LOGO_SIZE = new Vector3(44, 44, 44);
    private bool _foundTheSweetSpot;
    private bool _debugActivated;

    void Awake()
    {
        Canvas = CanvasRt.GetComponent<Canvas>();
    }

    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        adjustCameraForDevice();
    }

    private void adjustCameraForDevice()
    {
        GameSettings gameSettings = __json.Database.GetGameSettings();

        if (gameSettings.isCameraSetForThisDevice == false)
        {
            instantiateCameraHelperAndSetup();
            revealLogo();
            zoomOut();
        }
        else
        {
            _currentCameraSize = gameSettings.cameraSize2D;
            _camera.orthographicSize = _currentCameraSize;
            hideLogo();
            Main._._CameraReady__.OnNext(true);
        }
    }

    internal float GetCameraCurrentSize()
    {
        return _currentCameraSize;
    }

    private void instantiateCameraHelperAndSetup()
    {
        var go = Instantiate(CoreCameraSettings.CameraHelperPrefab, Vector3.zero, Quaternion.identity);
        go.transform.position = new Vector3(-30, -10);
        _cameraHelper = go.GetComponent<CameraHelper>();

        _currentCameraSize = 3;
        _camera.orthographicSize = _currentCameraSize / 2;
        LogoT.localScale = new Vector3(4, 4, 4);
    }

    private void zoomOut()
    {
        _currentCameraSize = _camera.orthographicSize;
        _toCameraSize = _currentCameraSize * 2;
        _toLogoSize = (_toLogoSize + (TO_LOGO_SIZE * 2));

        enlargeLogo();

        _alignCameraToHelperTwid = LeanTween.value(
            gameObject,
            _currentCameraSize,
            _toCameraSize,
            CAMERA_SETUP_TIME
        ).id;
        LeanTween.descr(_alignCameraToHelperTwid.Value).setEase(LeanTweenType.linear);
        LeanTween.descr(_alignCameraToHelperTwid.Value).setOnUpdate((float val) =>
        {
            _camera.orthographicSize = val;
            if (_cameraHelper.CanSeeBounds())
            {
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
        hideLogo();
        Main._._CameraReady__.OnNext(true);
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

    private void revealLogo()
    {
        LeanTween.alpha(LogoT.gameObject, 1f, CAMERA_SETUP_TIME);
    }

    private void hideLogo()
    {
        LeanTween.alpha(LogoT.gameObject, 0f, HIDE_LOGO_TIME)
            .setOnComplete(() =>
            {
                Destroy(LogoT.gameObject);
            });
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
}
