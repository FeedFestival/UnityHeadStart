using Assets.HeadStart.Core;
using UnityEngine;

public class HotSeatView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    private HotSeatCanvas _hotseatCanvas;
    public HotseatSettingsSO HotseatSettings;
    [Header("Canvas Points")]
    public Transform TLPoint;
    public Transform BRPoint;
    private bool _isInitialized;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            _hotseatCanvas.gameObject.SetActive(false);
            MenuEnvironment._.Back();
        });
        ButtonPlay.OnClick(() =>
        {
            _hotseatCanvas.gameObject.SetActive(false);
            MenuEnvironment._.InputNameForChallenge = true;
            MenuEnvironment._.SwitchView(VIEW.InputName);
        });

        var go = Instantiate(
            HotseatSettings.HotseatConvas,
            Vector3.zero,
            Quaternion.identity,
            parent: Main._.CoreCamera.Views
        );
        (go.transform as RectTransform).localPosition = Vector3.zero;
        _hotseatCanvas = go.GetComponent<HotSeatCanvas>();

        _isInitialized = true;
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    public void Focus()
    {
        if (_isInitialized == false)
        {
            Init();
        }

        __.Time.RxWait(() =>
        {
            _hotseatCanvas.gameObject.SetActive(true);
            _hotseatCanvas.Show(TLPoint, BRPoint);
            if (TLPoint == null)
            {
                return;
            }
            Destroy(TLPoint.gameObject);
            TLPoint = null;
            Destroy(BRPoint.gameObject);
            BRPoint = null;
        }, MenuEnvironment._.MOVE_CAMERA_TIME);

    }
}
