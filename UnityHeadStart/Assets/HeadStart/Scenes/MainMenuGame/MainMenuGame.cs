using System.Collections.Generic;
using Assets.HeadStart.Core;
using UnityEngine;

public class MainMenuGame : GameBase
{
    [Header("MainMenuGame")]
    public float TIMEWAIT_INIT;
    public Dictionary<int, string> test;

    public override void PreStartGame()
    {
        // __.SFX.PlayBackgroundMusic("MainMenuMusic");
        MenuEnvironment._.Init();
    }

    public override void StartGame()
    {
        __.Time.RxWait(() =>
        {
            bool isFirstTime = DeviceUser().IsFirstTime;
            __.Transition.Do(Transition.END);
            if (isFirstTime)
            {
                MenuEnvironment._.SwitchView(VIEW.InputName);
            }
            else
            {
                MenuEnvironment._.SwitchView(VIEW.MainMenu);
            }
        }, TIMEWAIT_INIT);
    }
}
