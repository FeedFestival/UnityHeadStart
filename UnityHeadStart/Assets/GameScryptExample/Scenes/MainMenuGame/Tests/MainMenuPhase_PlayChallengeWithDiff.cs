using System.Collections;
using System.Collections.Generic;
using Assets.HeadStart.Core;
using UnityEngine;

public class MainMenuPhase_PlayChallengeWithDiff : MainMenuPhase_PlayChallenge
{
    private string _testerName;
    public MainMenuPhase_PlayChallengeWithDiff(MainMenuGameTest parentTest, string testerName) : base(parentTest)
    {
        _parentTest = parentTest;
        _testerName = testerName;
    }

    public override void Test()
    {
        Debug.Log("MainMenuPhase_PlayChallengeWith[" + _testerName + "]");
        _parentTest.LoadChallengeRef();
        _parentTest.ChallengeRef.uiViewFocussed += playChallengeGame2;
    }

    public override void TypeInTesterName(InputFieldCustom input)
    {
        input.OnFocus();
        input.InputField.text = _testerName;
        __.Time.RxWait(() =>
        {
            input.OnBlur();
        }, _parentTest.TimeBetweenActions);
    }
}
