using System;
using GameScrypt.GSCamera;
using UnityEngine;
using UnityEngine.Events;

public class CameraHelper : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public Transform TLIn;
    public Transform TRIn;
    public Transform BLIn;
    public Transform BRIn;
    private GSCamera _camera;
    //
    private float _toCameraSize;
    protected float _multiplierCameraSize;
    private int? _alignCameraToHelperTwid;
    private bool _foundTheSweetSpot;
    private UnityAction _foundSpot;
    private bool _foundBoundsFlag = false;

    public void Init(GSCamera camera, UnityAction foundSpot)
    {
        _multiplierCameraSize = 3;
        _camera = camera;
        _foundSpot = foundSpot;
    }

    public void ZoomOut()
    {
        _toCameraSize = _camera.OrthographicSize + _multiplierCameraSize;

        _alignCameraToHelperTwid = LeanTween.value(
            _camera.gameObject,
            _camera.OrthographicSize,
            _toCameraSize,
            GSCamera.CAMERA_SETUP_TIME
        ).id;
        LeanTween.descr(_alignCameraToHelperTwid.Value).setEase(LeanTweenType.linear);
        LeanTween.descr(_alignCameraToHelperTwid.Value).setOnUpdate((float val) =>
        {
            if (_foundBoundsFlag) { return; }

            _camera.OrthographicSize = val;
            // LoadingVersion?.ChangeVersion(val);
            if (canSeeBounds())
            {
                _foundBoundsFlag = true;
                _foundTheSweetSpot = true;

                LeanTween.cancel(_alignCameraToHelperTwid.Value);
                _alignCameraToHelperTwid = null;

                _foundSpot();
            }
        });
        LeanTween.descr(_alignCameraToHelperTwid.Value).setOnComplete(() =>
        {
            if (_foundTheSweetSpot == false)
            {
                ZoomOut();
                return;
            }
        });
    }

    public bool canSeeBounds()
    {
        Vector3 tLp = Camera.main.WorldToViewportPoint(TLIn.position);
        Vector3 tRp = Camera.main.WorldToViewportPoint(TRIn.position);
        Vector3 bLp = Camera.main.WorldToViewportPoint(BLIn.position);
        Vector3 bRp = Camera.main.WorldToViewportPoint(BRIn.position);

        return isInView(tLp) && isInView(tRp) && isInView(bLp) && isInView(bRp);
    }

    private bool isInView(Vector3 screenPos)
    {
        bool inView = (screenPos.z > 0
            && screenPos.x > 0
            && screenPos.x < 1
            && screenPos.y > 0
            && screenPos.y < 1);
        return inView;
    }
}
