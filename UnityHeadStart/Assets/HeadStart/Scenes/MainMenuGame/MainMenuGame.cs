using IngameDebugConsole;
using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuGame : GSGameScene
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

    public override void Init()
    {
        MenuEnvironment.S.Init();
    }

    public override void StartGameScene()
    {
        __.Timeout.RxSeconds(() =>
        {
            bool isFirstTime = false; // PlayerSettings().isFirstTime;
            //__.Transition.Do(Transition.END);
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
