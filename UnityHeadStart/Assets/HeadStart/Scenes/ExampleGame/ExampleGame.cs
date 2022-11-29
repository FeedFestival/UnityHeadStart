using Assets.HeadStart.Core;
using UnityEngine;

namespace Example1.ExampleGame
{
    public class ExampleGame : GSGameScene
    {
        [Header("ExampleGame")]
        public ExampleRandomPoints ExampleRandomPoints;

        public override void Init()
        {
            bool hasCoreSession = CoreSession.S != null;
            if (hasCoreSession == false)
            {
                //SessionOpts sessionOpts = new SessionOpts()
                //{
                //    User = LoadUser()
                //};
                //CoreIoC.IoCDependencyResolver.CreateSession(sessionOpts);
            }
        }

        public override void StartGameScene()
        {
            //__.Transition.Do(Transition.END);
            ExampleRandomPoints.Init();
        }

        //public override void GameOver()
        //{
        //    DialogOptions options = new DialogOptions()
        //    {
        //        Title = "Congrats " + CoreSession.S.SessionOpts.User.Name + "!",
        //        Info = CoreSession.S.SessionOpts.Points.ToString(),
        //        ContinueCallback = () =>
        //        {
        //            __.Transition.Do(Transition.START, () =>
        //            {
        //                // Main.S.Game.GoToMainMenu();
        //            });
        //        },
        //        RetryCallback = () =>
        //        {
        //            // Main.S.Game.Restart();
        //        }
        //    };
        //    __.Dialog.Show(options);
        //}
    }
}
