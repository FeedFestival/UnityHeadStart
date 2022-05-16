using Assets.HeadStart.Core;
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
            MenuEnvironment._.ClearChallengeSession();
            MenuEnvironment._.Back();
        });

        ButtonPlay.Init();
        ButtonPlay.OnClick(() =>
        {
            InputNameCanvas.CancelChallenge();
            InputNameCanvas.gameObject.SetActive(false);

            if (Main._.Game.DeviceUser().IsFirstTime)
            {
                User changedUser = Main._.Game.DeviceUser();
                changedUser.Name = _userInputChangedName;
                changedUser.IsFirstTime = false;
                Main._.Game.DataService.UpdateUser(changedUser);
                Main._.Game.LoadUser();
            }
            else
            {
                User playingUser = MenuEnvironment._.GetChallengeSession().User;
                if (playingUser.LocalId == 0)
                {
                    playingUser.LocalId = Main._.Game.DataService.CreateUser(playingUser);
                    MenuEnvironment._.UpdateSessionUserId(playingUser.LocalId);
                    if (Main._.ConsoleLog) Debug.Log(JsonUtility.ToJson(MenuEnvironment._.GetChallengeSession().User.Debug()));
                }
                else
                {
                    if (Main._.ConsoleLog) Debug.Log("User exists");
                }
                MenuEnvironment._.GetChallengeSession().IsChallenge = true;
            }
            MenuEnvironment._.SwitchView(VIEW.GameSession);
        });

        var go = Instantiate(
            InputNameSettings.InputNameConvas,
            Vector3.zero,
            Quaternion.identity,
            parent: Main._.CoreCamera.Views
        );
        (go.transform as RectTransform).localPosition = Vector3.zero;
        InputNameCanvas = go.GetComponent<InputNameCanvas>();

        uiViewFocussed += onFocussed;

        _isInitialized = true;
    }

    private void ReInit()
    {
        bool isFirstTime = Main._.Game.DeviceUser().IsFirstTime;
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

        if (Main._.Game.DeviceUser().IsFirstTime)
        {
            _userInputChangedName = (obj as string);
            return;
        }

        if (isValid)
        {
            SessionOpts sessionOpts = obj as SessionOpts;
            MenuEnvironment._.SetChallengeSession(sessionOpts);
        }
    }

    private void ResetActions()
    {
        ButtonPlay.Reset();
    }
}
