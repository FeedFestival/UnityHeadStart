using Assets.HeadStart.Core;
using UnityEngine;

public class MainMenuPhase_FirstTime
{
    private MainMenuGameTest _parentTest;

    public MainMenuPhase_FirstTime(MainMenuGameTest parentTest)
    {
        _parentTest = parentTest;
    }

    public void Test()
    {
        Debug.Log("MainMenuPhase_FirstTime");
        _parentTest.LoadInputNameRef();
        _parentTest.InputNameRef.uiViewFocussed += step1;
    }

    private void step1()
    {
        _parentTest.InputNameRef.uiViewFocussed -= step1;

        __.Timeout.RxSeconds(() =>
        {
            _parentTest.InputNameRef.InputNameCanvas.InputFieldChange += step2;

            var go = _parentTest.GetGo("InputFieldCustom_InputName");
            var input = go.GetComponent<InputFieldCustom>();

            input.OnFocus();
            input.InputField.text = "AutomatedTester";
            __.Timeout.RxSeconds(() =>
            {
                input.OnBlur();
            }, _parentTest.TimeBetweenActions);

        }, _parentTest.TimeBetweenActions);
    }

    private void step2(object obj)
    {
        if (obj == null) { return; }

        _parentTest.InputNameRef.ButtonPlay.MouseDown();
    }
}