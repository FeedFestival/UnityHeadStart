using System;
using UnityEngine;
using UnityEngine.UI;

public class CoreCamera : MonoBehaviour
{
    public RectTransform Canvas;
    public RectTransform Views;
    public Image LoadingOverlay;
    // TODO: add Logo functionality to HeadStart
    public Transform LogoT;
    public CoreCameraSettings CoreCameraSettings;
    private Camera _camera;
    private CameraHelper _cameraHelper;
    public delegate void OnCameraSetupDone();
    private OnCameraSetupDone _onCameraSetupDone;
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

    void Start()
    {
        // TODO: Test !
        // Main._.CoreCamera = this;

        _camera = gameObject.GetComponent<Camera>();
        _currentCameraSize = PlayerPrefs.GetFloat("orthographicSize");
        if (_currentCameraSize == 0)
        {
            Debug.LogWarning("Please run MainMenu for [Initial Setup] Camera Adjustment");
            _currentCameraSize = 3;
        }
        _camera.orthographicSize = _currentCameraSize;
    }

    internal float GetCameraCurrentSize()
    {
        return _currentCameraSize;
    }

    internal void InitSetup(CameraHelper cameraHelper, OnCameraSetupDone onCameraSetupDone)
    {
        _camera.orthographicSize = _currentCameraSize / 2;
        _cameraHelper = cameraHelper;
        _onCameraSetupDone = onCameraSetupDone;

        LogoT.localScale = new Vector3(4, 4, 4);
        RevealLogo();

        zoomOut();
    }

    public void DestroyLogo()
    {
        Destroy(LogoT.gameObject);
    }

    private void zoomOut()
    {
        _currentCameraSize = _camera.orthographicSize;
        _toCameraSize = _currentCameraSize * 2;
        _toLogoSize = (_toLogoSize + (TO_LOGO_SIZE * 2));
        Debug.Log("_toLogoSize: " + _toLogoSize);

        EnlargeLogo();

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

        _currentCameraSize = _camera.orthographicSize;
        PlayerPrefs.SetFloat("orthographicSize", _currentCameraSize);

        HideLogo();
        _onCameraSetupDone();
    }

    private void EnlargeLogo()
    {
        _enlargeLogoTwid = LeanTween.scale(LogoT.gameObject, _toLogoSize, CAMERA_SETUP_TIME).id;
    }

    private void RevealLogo()
    {
        LeanTween.alpha(LogoT.gameObject, 1f, CAMERA_SETUP_TIME);
    }

    private void HideLogo()
    {
        LeanTween.alpha(LogoT.gameObject, 0f, HIDE_LOGO_TIME)
            .setOnComplete(() =>
            {
                DestroyLogo();
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
