using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    private ChallengeCanvas _challengeCanvas;
    public ChallengeSettingsSO ChallengeSettings;
    [Header("Canvas Points")]
    public WorldCanvasPoint tableWCP;
    private bool _isInitialized;

    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            _challengeCanvas.gameObject.SetActive(false);
            MenuEnvironment._.Back();
        });

        ButtonPlay.Init();
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

        uiViewFocussed += onFocussed;

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

        ResetActions();

        __.Time.RxWait(() =>
        {
            _challengeCanvas.gameObject.SetActive(true);
            _challengeCanvas.Show(tableWCP);
            if (tableWCP == null)
            {
                return;
            }
            CleanUp();
        }, MenuEnvironment._.MOVE_CAMERA_TIME);
    }

    public void onFocussed()
    {
        EnableActions();
    }


    private void ResetActions()
    {
        ButtonPlay.Reset();
    }

    private void EnableActions()
    {
        ButtonPlay.Enable();
    }

    private void CleanUp()
    {
        Destroy(tableWCP.gameObject);
        tableWCP = null;
    }
}
