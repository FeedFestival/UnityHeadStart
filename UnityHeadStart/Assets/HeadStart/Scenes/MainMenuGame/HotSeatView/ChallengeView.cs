using Assets.HeadStart.Core;
using UnityEngine;

public class ChallengeView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    private ChallengeCanvas _challengeCanvas;
    public ChallengeSettingsSO ChallengeSettings;
    [Header("Canvas Points")]
    public Transform TLPoint;
    public Transform BRPoint;
    private bool _isInitialized;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            _challengeCanvas.gameObject.SetActive(false);
            MenuEnvironment._.Back();
        });
        ButtonPlay.OnClick(() =>
        {
            _challengeCanvas.gameObject.SetActive(false);
            MenuEnvironment._.InputNameForChallenge = true;
            MenuEnvironment._.SwitchView(VIEW.InputName);
        });

        var go = Instantiate(
            ChallengeSettings.ChallengeConvas,
            Vector3.zero,
            Quaternion.identity,
            parent: Main._.CoreCamera.Views
        );
        (go.transform as RectTransform).localPosition = Vector3.zero;
        _challengeCanvas = go.GetComponent<ChallengeCanvas>();

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
            _challengeCanvas.gameObject.SetActive(true);
            _challengeCanvas.Show(TLPoint, BRPoint);
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
