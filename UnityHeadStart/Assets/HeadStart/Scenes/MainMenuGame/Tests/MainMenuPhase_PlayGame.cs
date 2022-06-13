using Assets.HeadStart.Core;
using UnityEngine;

public class MainMenuPhase_PlayGame
{
    private MainMenuGameTest _parentTest;

    public MainMenuPhase_PlayGame(MainMenuGameTest parentTest)
    {
        _parentTest = parentTest;
    }

    public void Test()
    {
        _parentTest.LoadMainMenuRef();

        _parentTest.MainMenuRef.uiViewFocussed += PlayGame1;
    }

    private void PlayGame1()
    {
        _parentTest.MainMenuRef.uiViewFocussed -= PlayGame1;

        __.Time.RxWait(() =>
        {
            _parentTest.MainMenuRef.ButtonPlay.MouseDown();
        }, _parentTest.TimeBetweenActions);
    }
}
