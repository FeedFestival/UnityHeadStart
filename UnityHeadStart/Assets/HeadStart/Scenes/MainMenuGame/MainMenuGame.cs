using System.Collections.Generic;
using Assets.HeadStart.Core;
using UnityEngine;

public class MainMenuGame : GameBase
{
    [Header("MainMenuGame")]
    private readonly float TIMEWAIT_INIT = 0.1F;

    public override void PreStartGame()
    {
        MenuEnvironment._.Init();
    }

    public override void StartGame()
    {
        __.Time.RxWait(() =>
        {
            bool isFirstTime = DevicePlayer().isFirstTime;
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
