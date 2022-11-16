using Assets.HeadStart.Core;
using Assets.HeadStart.Features.Dialog;
using Assets.GameScryptExample;
using UnityEngine;

namespace Assets.GameScryptExample
{
    public class ExampleGame : GameBase
    {
        [Header("ExampleGame")]
        public ExampleRandomPoints ExampleRandomPoints;

        public override void PreStartGame()
        {
            bool hasCoreSession = CoreSession.S != null;
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
                Title = "Congrats " + CoreSession.S.SessionOpts.User.Name + "!",
                Info = CoreSession.S.SessionOpts.Points.ToString(),
                ContinueCallback = () =>
                {
                    __.Transition.Do(Transition.START, () =>
                    {
                        Main.S.Game.GoToMainMenu();
                    });
                },
                RetryCallback = () =>
                {
                    Main.S.Game.Restart();
                }
            };
            __.Dialog.Show(options);
        }
    }
}
