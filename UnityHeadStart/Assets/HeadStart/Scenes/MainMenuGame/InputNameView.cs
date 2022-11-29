using Assets.HeadStart.Core;
using HeadStart;
using UnityEngine;
using UnityEngine.Events;

public class InputNameView : MonoBehaviour, IUiView
{
    public GameButton ButtonBack;
    public GameButton ButtonPlay;
    public InputNameCanvas InputNameCanvas;
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

            if (false)// Main.S.Game.PlayerSettings().isFirstTime)
            {
                PlayerSettings changedPlayerSettings = null; // Main.S.Game.PlayerSettings();
                changedPlayerSettings.localId = 1;
                changedPlayerSettings.name = _userInputChangedName;
                changedPlayerSettings.isFirstTime = false;
                var deviceJsonData = new DeviceJsonData("player.json");
                deviceJsonData.UpdatePlayer(changedPlayerSettings);
                User deviceUser = null; // Main.S.Game.DeviceUser();
                deviceUser.Name = _userInputChangedName;
                // Main.S.Game.DataService.UpdateUser(deviceUser);
                // Main.S.Game.LoadPlayerSettings();
            }
            else
            {
                User playingUser = MenuEnvironment.S.GetChallengeSession().User;
                if (playingUser.LocalId == 0)
                {
                    playingUser.LocalId = 0; // Main.S.Game.DataService.CreateUser(playingUser);
                    MenuEnvironment.S.UpdateSessionUserId(playingUser.LocalId);
                }
                MenuEnvironment.S.GetChallengeSession().IsChallenge = true;
            }
            MenuEnvironment.S.SwitchView(VIEW.GameSession);
        });
        uiViewFocussed += onFocussed; 
        _isInitialized = true;
    }

    private void ReInit()
    {
        bool isFirstTime = false; // Main.S.Game.PlayerSettings().isFirstTime;
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
        InputNameCanvas.Show();
        InputNameCanvas.InputFieldChange += onInputFieldChange;
    }

    private void onInputFieldChange(object obj)
    {
        bool isValid = obj != null;
        ButtonPlay.Interactable = isValid;

        if (false)// Main.S.Game.PlayerSettings().isFirstTime)
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
