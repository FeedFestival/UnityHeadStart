using Assets.HeadStart.Core;
using Assets.HeadStart.Features.Dialog;
using UnityEngine;

public class ExampleGame : GameBase
{
    [Header("ExampleGame")]
    public ExampleRandomPoints ExampleRandomPoints;

    public override void PreStartGame()
    {
        bool hasCoreSession = CoreSession._ != null;
        if (hasCoreSession == false)
        {
            SessionOpts sessionOpts = new SessionOpts()
            {
                User = LoadUser()
            };
            CoreIoC.IoCDependencyResolver.CreateSession(sessionOpts);
        }

        StartGame();
    }

    public override void StartGame()
    {
        __.Transition.Do(Transition.END);
        ExampleRandomPoints.Init();
    }

    public override void GameOver()
    {
        DialogOptions options = new DialogOptions()
        {
            Title = "Congrats " + CoreSession._.SessionOpts.User.Name + "!",
            Info = CoreSession._.SessionOpts.Points.ToString(),
            ContinueCallback = () =>
            {
                __.Transition.Do(Transition.START, () =>
                {
                    Main._.Game.GoToMainMenu();
                });
            },
            RetryCallback = () =>
            {
                Main._.Game.Restart();
            }
        };
        __.Dialog.Show(options);
    }
}
