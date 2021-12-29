using System.Collections.Generic;
using Assets.HeadStart.Core;
using Assets.Scripts.utils;
using UnityEngine;

public class InputNameCanvas : MonoBehaviour
{
    public InputFieldCustom InputFieldCustom;
    public GameObject ChallengersContainer;
    public List<ButtonWrapper> PredefinedNames;
    private List<User> _users;
    private string _name;
    private RectTransform _rt;
    private CoreObjCallback _coreCallback;
    private bool _isInitialized;

    void Awake()
    {
        InputFieldCustom.gameObject.SetActive(false);
        ChallengersContainer.SetActive(false);
    }

    private void Init(Transform TLPoint, Transform BRPoint)
    {
        __world2d.PositionRtBasedOnScreenAnchors(
            topLeftAnchor: Camera.main.WorldToScreenPoint(TLPoint.position),
            bottomRightAnchor: Camera.main.WorldToScreenPoint(BRPoint.position),
            rt: (transform as RectTransform),
            screenSize: Main._.CoreCamera.Canvas.sizeDelta
        );

        InputFieldCustom.OnBlurDelegate = () =>
        {
            _name = InputFieldCustom.InputField.text;
            if (string.IsNullOrWhiteSpace(_name))
            {
                _coreCallback(null);
            }
            else
            {
                SessionOpts sessionOpts = PlayChallenge();
                _coreCallback(sessionOpts);
            }
        };

        _isInitialized = true;
    }

    public void Show(
        Transform TLPoint,
        Transform BRPoint,
        CoreObjCallback coreCallback
    )
    {
        if (_isInitialized == false)
        {
            Init(TLPoint, BRPoint);
        }

        _coreCallback = coreCallback;

        InputFieldCustom.InputField.text = "";
        _name = "";

        InputFieldCustom.gameObject.SetActive(true);
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
            Debug.Log("NEW USER");
            playingUser = new User()
            {
                Name = InputFieldCustom.InputField.text
            };
            Main._.Game.DataService.CreateUser(playingUser);
        }
        Debug.Log("playingUser: " + playingUser.Name);

        return new SessionOpts()
        {
            User = playingUser,
            HighScoreType = HighScoreType.HOTSEAT
        };
    }

    public void CancelChallenge()
    {
        foreach (ButtonWrapper btnName in PredefinedNames)
        {
            btnName.Unsubscribe();
        }
    }
}
