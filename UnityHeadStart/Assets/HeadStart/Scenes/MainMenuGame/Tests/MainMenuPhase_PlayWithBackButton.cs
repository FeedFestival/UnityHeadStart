using Assets.HeadStart.Core;

public class MainMenuPhase_PlayWithBackButton
{
    private MainMenuGameTest _parentTest;
    private bool _backButtonToPlay;

    public MainMenuPhase_PlayWithBackButton(MainMenuGameTest parentTest)
    {
        _parentTest = parentTest;
    }

    public void Test()
    {
        _parentTest.LoadChallengeRef();

        _parentTest.ChallengeRef.uiViewFocussed += PlayWithBackButton1;
    }

    public void PlayWithBackButton1()
    {
        _parentTest.ChallengeRef.uiViewFocussed -= PlayWithBackButton1;

        __.Time.RxWait(() =>
        {
            _parentTest.ChallengeRef.ButtonBack.MouseDown();

            _parentTest.LoadMainMenuRef();
            _parentTest.MainMenuRef.uiViewFocussed += PlayWithBackButton2;

        }, _parentTest.TimeBetweenActions);
    }

    private void PlayWithBackButton2()
    {
        _parentTest.MainMenuRef.uiViewFocussed -= PlayWithBackButton2;

        __.Time.RxWait(() =>
        {
            if (_backButtonToPlay)
            {
                _parentTest.MainMenuRef.ButtonPlay.MouseDown();
                return;
            }
            _parentTest.MainMenuRef.ButtonChallenge.MouseDown();

            _parentTest.LoadChallengeRef();
            _parentTest.ChallengeRef.uiViewFocussed += PlayWithBackButton3;

        }, _parentTest.TimeBetweenActions);
    }

    private void PlayWithBackButton3()
    {
        _parentTest.ChallengeRef.uiViewFocussed -= PlayWithBackButton3;

        __.Time.RxWait(() =>
        {
            _parentTest.ChallengeRef.ButtonPlay.MouseDown();

            _parentTest.LoadInputNameRef();
            _parentTest.InputNameRef.uiViewFocussed += PlayWithBackButton4;

        }, _parentTest.TimeBetweenActions);
    }

    private void PlayWithBackButton4()
    {
        _parentTest.InputNameRef.uiViewFocussed -= PlayWithBackButton4;

        __.Time.RxWait(() =>
        {
            _parentTest.InputNameRef.ButtonBack.MouseDown();

            _backButtonToPlay = true;
            Test();

        }, _parentTest.TimeBetweenActions);
    }
}
