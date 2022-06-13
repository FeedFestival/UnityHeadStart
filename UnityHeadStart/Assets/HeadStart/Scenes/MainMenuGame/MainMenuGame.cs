using IngameDebugConsole;
using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGame : GameBase
{
    [Header("MainMenuGame")]
    private readonly float TIMEWAIT_INIT = 0.1F;

    void Start()
    {
        DebugLogConsole.AddCommand(
            "test",
            "Run Tests On The Application",
            RunTestTheApp
        );
    }

    public override void PreStartGame()
    {
        MenuEnvironment.S.Init();
    }

    public override void StartGame()
    {
        __.Time.RxWait(() =>
        {
            bool isFirstTime = DevicePlayer().isFirstTime;
            __.Transition.Do(Transition.END);
            if (isFirstTime)
            {
                MenuEnvironment.S.SwitchView(VIEW.InputName);
            }
            else
            {
                MenuEnvironment.S.SwitchView(VIEW.MainMenu);
            }
        }, TIMEWAIT_INIT);
    }

    private void RunTestTheApp()
    {
        SceneManager.LoadScene(2);
    }
}
