using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    public ChallengeCanvas ChallengeCanvas;
    public ChallengeSettingsSO ChallengeSettings;
    private bool _isInitialized;

    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            ChallengeCanvas.gameObject.SetActive(false);
            MenuEnvironment.S.Back();
        });

        ButtonPlay.Init();
        ButtonPlay.OnClick(() =>
        {
            ChallengeCanvas.gameObject.SetActive(false);
            MenuEnvironment.S.InputNameForChallenge = true;
            MenuEnvironment.S.SwitchView(VIEW.InputName);
        });
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

        __.Timeout.RxSeconds(() =>
        {
            ChallengeCanvas.gameObject.SetActive(true);
            ChallengeCanvas.Show();
        }, MenuEnvironment.S.MOVE_CAMERA_TIME);
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
}
