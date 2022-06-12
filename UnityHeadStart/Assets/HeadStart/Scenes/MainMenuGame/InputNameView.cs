using Assets.HeadStart.Core;
using Assets.HeadStart.Features.Database.JSON;
using UnityEngine;
using UnityEngine.Events;

public class InputNameView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    public WorldCanvasPoint WorldCanvasPoint;
    internal InputNameCanvas InputNameCanvas;
    public InputNameSettings InputNameSettings;
    private bool _isInitialized;
    private bool _isInputInitialized;
    private string _userInputChangedName;

    UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
    public event UnityAction uiViewFocussed;

    private void Init()
    {
        ButtonBack.OnClick(() =>
        {
            InputNameCanvas.gameObject.SetActive(false);
            InputNameCanvas.CancelChallenge();
            MenuEnvironment.S.ClearChallengeSession();
            MenuEnvironment.S.Back();
        });

        ButtonPlay.Init();
        ButtonPlay.OnClick(() =>
        {
            InputNameCanvas.CancelChallenge();
            InputNameCanvas.gameObject.SetActive(false);

            if (Main.S.Game.DevicePlayer().isFirstTime)
            {
                DevicePlayer changedDevicePlayer = Main.S.Game.DevicePlayer();
                changedDevicePlayer.localId = 1;
                changedDevicePlayer.name = _userInputChangedName;
                changedDevicePlayer.isFirstTime = false;
                __json.Database.UpdatePlayer(changedDevicePlayer);
                User deviceUser = Main.S.Game.DeviceUser();
                deviceUser.Name = _userInputChangedName;
                Main.S.Game.DataService.UpdateUser(deviceUser);
                Main.S.Game.LoadDevicePlayer();
            }
            else
            {
                User playingUser = MenuEnvironment.S.GetChallengeSession().User;
                if (playingUser.LocalId == 0)
                {
                    playingUser.LocalId = Main.S.Game.DataService.CreateUser(playingUser);
                    MenuEnvironment.S.UpdateSessionUserId(playingUser.LocalId);
                }
                MenuEnvironment.S.GetChallengeSession().IsChallenge = true;
            }
            MenuEnvironment.S.SwitchView(VIEW.GameSession);
        });

        var go = Instantiate(
            InputNameSettings.InputNameConvas,
            Vector3.zero,
            Quaternion.identity,
            parent: Main.S.CoreCamera.Views
        );
        (go.transform as RectTransform).localPosition = Vector3.zero;
        InputNameCanvas = go.GetComponent<InputNameCanvas>();

        uiViewFocussed += onFocussed; 

        _isInitialized = true;
    }

    private void ReInit()
    {
        bool isFirstTime = Main.S.Game.DevicePlayer().isFirstTime;
        if (isFirstTime)
        {
            ButtonBack.gameObject.SetActive(false);
        }
        else
        {
            ButtonBack.gameObject.SetActive(true);
        }
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

        ReInit();
        ResetActions();
    }

    public void onFocussed()
    {
        InitNameCanvas();
    }

    private void InitNameCanvas()
    {
        if (_isInputInitialized)
        {
            InputNameCanvas.gameObject.SetActive(true);
            return;
        }
        _isInputInitialized = true;
        InputNameCanvas.gameObject.SetActive(true);
        InputNameCanvas.Show(WorldCanvasPoint);
        InputNameCanvas.InputFieldChange += onInputFieldChange;

        if (WorldCanvasPoint == null)
        {
            return;
        }
        Destroy(WorldCanvasPoint.gameObject);
        WorldCanvasPoint = null;
    }

    private void onInputFieldChange(object obj)
    {
        bool isValid = obj != null;
        ButtonPlay.Interactable = isValid;

        if (Main.S.Game.DevicePlayer().isFirstTime)
        {
            _userInputChangedName = (obj as string);
            return;
        }

        if (isValid)
        {
            SessionOpts sessionOpts = obj as SessionOpts;
            MenuEnvironment.S.SetChallengeSession(sessionOpts);
        }
    }

    private void ResetActions()
    {
        ButtonPlay.Reset();
    }
}
