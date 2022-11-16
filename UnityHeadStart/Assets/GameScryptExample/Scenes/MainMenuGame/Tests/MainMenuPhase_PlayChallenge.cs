using Assets.HeadStart.Core;
using UnityEngine;

public class MainMenuPhase_PlayChallenge
{
    protected MainMenuGameTest _parentTest;

    public MainMenuPhase_PlayChallenge(MainMenuGameTest parentTest)
    {
        _parentTest = parentTest;
    }

    public virtual void Test()
    {
        Debug.Log("MainMenuPhase_PlayChallenge");
        _parentTest.LoadMainMenuRef();
        _parentTest.MainMenuRef.uiViewFocussed += playChallengeGame1;
    }

    private void playChallengeGame1()
    {
        _parentTest.MainMenuRef.uiViewFocussed -= playChallengeGame1;

        __.Time.RxWait(() =>
        {
            _parentTest.MainMenuRef.ButtonChallenge.MouseDown();

            _parentTest.LoadChallengeRef();
            _parentTest.ChallengeRef.uiViewFocussed += playChallengeGame2;

        }, _parentTest.TimeBetweenActions);
    }

    protected void playChallengeGame2()
    {
        _parentTest.ChallengeRef.uiViewFocussed -= playChallengeGame2;

        __.Time.RxWait(() =>
        {
            _parentTest.ChallengeRef.ButtonPlay.MouseDown();

            _parentTest.LoadInputNameRef();
            _parentTest.InputNameRef.uiViewFocussed += playChallengeGame3;

        }, _parentTest.TimeBetweenActions);
    }

    private void playChallengeGame3()
    {
        _parentTest.InputNameRef.uiViewFocussed -= playChallengeGame3;

        __.Time.RxWait(() =>
        {
            _parentTest.InputNameRef.InputNameCanvas.InputFieldChange
                += playChallengeGame4;

            var go = _parentTest.GetGo("InputFieldCustom_InputName");
            var input = go.GetComponent<InputFieldCustom>();
            this.TypeInTesterName(input);
        }, _parentTest.TimeBetweenActions);
    }

    public virtual void TypeInTesterName(InputFieldCustom input)
    {
        input.OnFocus();
        input.InputField.text = "GameTester";
        __.Time.RxWait(() =>
        {
            input.OnBlur();
        }, _parentTest.TimeBetweenActions);
    }

    private void playChallengeGame4(object obj)
    {
        if (obj == null) { return; }

        _parentTest.InputNameRef.ButtonPlay.MouseDown();
    }
}
