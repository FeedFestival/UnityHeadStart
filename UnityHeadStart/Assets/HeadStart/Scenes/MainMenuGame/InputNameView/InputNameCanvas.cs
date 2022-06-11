using System.Collections.Generic;
using Assets.HeadStart.Core;
using Assets.Scripts.utils;
using UnityEngine;
using UnityEngine.Events;

public class InputNameCanvas : MonoBehaviour
{
    public InputFieldCustom InputFieldCustom;
    public GameObject ChallengersContainer;
    public List<ButtonWrapper> PredefinedNames;
    private List<User> _users;
    private string _name;
    private RectTransform _rt;
    public event UnityAction<object> InputFieldChange;
    private bool _isInitialized;

    void Awake()
    {
        _rt = transform as RectTransform;
        InputFieldCustom.gameObject.SetActive(false);
        ChallengersContainer.SetActive(false);
    }

    private void Init(WorldCanvasPoint worldCanvasPoint)
    {
        if (worldCanvasPoint)
        {
            __world2d.PositionRtBasedOnScreenAnchors(
                worldCanvasPoint, rt: _rt,
                screenSize: Main._.CoreCamera.CanvasRt.sizeDelta
            );
        }

        InputFieldCustom.Init(_rt.sizeDelta.x);

        if (Main._.Game.DevicePlayer().isFirstTime)
        {
            InputFieldCustom.OnBlurDelegate = () =>
            {
                _name = InputFieldCustom.InputField.text;
                if (string.IsNullOrWhiteSpace(_name) || _name.Length > 25)
                {
                    InputFieldChange.Invoke(null);
                    return;
                }

                InputFieldChange.Invoke(_name);
            };

            return;
        }

        InputFieldCustom.OnBlurDelegate = () =>
        {
            _name = InputFieldCustom.InputField.text;
            if (string.IsNullOrWhiteSpace(_name))
            {
                InputFieldChange.Invoke(null);
            }
            else
            {
                SessionOpts sessionOpts = PlayChallenge();
                InputFieldChange.Invoke(sessionOpts);
            }
        };

        _isInitialized = true;
    }

    public void Show(
        WorldCanvasPoint worldCanvasPoint
    )
    {
        if (_isInitialized == false)
        {
            Init(worldCanvasPoint);
        }

        InputFieldCustom.InputField.text = "";
        _name = "";

        InputFieldCustom.gameObject.SetActive(true);

        if (Main._.Game.DevicePlayer().isFirstTime)
        {
            return;
        }
        ShowPreviousChallengers();
    }

    public void ShowPreviousChallengers()
    {
        _users = Main._.Game.DataService.GetUsers();

        bool noUsers = _users == null || _users.Count == 0;
        if (noUsers)
        {
            return;
        }

        ChallengersContainer.SetActive(true);

        int i = 0;
        foreach (ButtonWrapper btnName in PredefinedNames)
        {
            btnName.Id = i;
            if (i >= _users.Count)
            {
                btnName.Unsubscribe();
                btnName.gameObject.SetActive(false);
                btnName.Txt.text = "";
                continue;
            }
            btnName.gameObject.SetActive(true);
            btnName.Txt.text = _users[i].Name;
            btnName.Subscribe(OnSelectedPredefined);

            i++;
        }
    }

    public void OnSelectedPredefined(int Id)
    {
        if (Id >= _users.Count)
        {
            return;
        }
        InputFieldCustom.InputField.text = _users[Id].Name;
        InputFieldCustom.OnBlur();
    }

    public SessionOpts PlayChallenge()
    {
        User playingUser = Main._.Game.DataService.GetUserByName(InputFieldCustom.InputField.text);
        if (playingUser == null)
        {
            playingUser = new User()
            {
                Name = InputFieldCustom.InputField.text
            };
        }

        return new SessionOpts()
        {
            User = playingUser
        };
    }

    public void CancelChallenge()
    {
        // TODO: do we need to unsubscribe?
        // foreach (ButtonWrapper btnName in PredefinedNames)
        // {
        //     btnName.Unsubscribe();
        // }
    }
}
