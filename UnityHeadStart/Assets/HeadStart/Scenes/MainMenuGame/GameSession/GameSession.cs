using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SceneService;
using Assets.Scripts.utils;
using UnityEngine;

public class GameSession : MonoBehaviour, IUiView
{
    private bool _isInitialized;

    private void Init()
    {
        _isInitialized = true;
    }

    GameObject IUiView.GO()
    {
        return gameObject;
    }

    public void Focus()
    {
        if (_isInitialized == false)
        {
            Init();
        }

        bool hasCoreSession = CoreSession._ != null;
        if (hasCoreSession)
        {
            AfterTheGame();
            return;
        }

        __.Time.RxWait(() =>
        {
            SessionOpts sessionOpts = MenuEnvironment._.GetHotseatSession();

            if (sessionOpts == null)
            {
                sessionOpts = new SessionOpts()
                {
                    HighScoreType = HighScoreType.RANKED,
                    User = Main._.Game.DeviceUser()
                };
            }
            CoreIoC.IoCDependencyResolver.CreateSession(sessionOpts);

            __.Transition.Do(Transition.START, () =>
            {
                Main._.Game.LoadScene(SCENE.Game);
            });
        }, MenuEnvironment._.MOVE_CAMERA_TIME);
    }

    private void AfterTheGame()
    {
        Main._.Game.InitDatabaseConnection();

        __.Transition.Do(Transition.END, () =>
        {
            SessionOpts sessionOpts = CoreSession._.SessionOpts;
            Destroy(CoreSession._.gameObject);

            if (sessionOpts.HighScoreType == HighScoreType.RANKED)
            {
                WeekDetails week = __data.GetWeekDetails();
                TryUpdateLatestWeekScore(week, sessionOpts.Points);
                UpdateHighScore(week, sessionOpts);

                // If ranked and the game was a highscore that week
                // If player wants to save points
                // If he has enough toilet paper

                // Get the points saved in the database

                // Send them to SERVER for first check

                // then -> move the player to Ranked Ladder View
                // there the second check on the SERVER for authenticity
            }
            else
            {
                WeekDetails week = __data.GetWeekDetails();
                UpdateHighScore(week, sessionOpts);

                MenuEnvironment._.SetupBackToMainMenuFor(VIEW.HotSeat);
                MenuEnvironment._.SwitchView(VIEW.HotSeat);
            }
        });
    }

    private void TryUpdateLatestWeekScore(WeekDetails week, int points)
    {
        WeekScore weekScore = Main._.Game.DataService.GetHighestWeekScore(week.Id);
        if (weekScore == null)
        {
            weekScore = new WeekScore()
            {
                Id = week.Id,
                Points = points,
                Year = week.Year,
                Week = week.Nr,
                UserId = Main._.Game.DeviceUser().Id
            };
            Main._.Game.DataService.AddWeekHighScore(weekScore);
        }
        else
        {
            if (weekScore.Points >= points)
            {
                Debug.Log("weekScore: " + weekScore.Points);
            }
            else
            {
                weekScore.Points = points;
                Main._.Game.DataService.UpdateWeekHighScore(weekScore);
            }
        }
    }

    private void UpdateHighScore(WeekDetails week, SessionOpts sessionOpts)
    {
        HighScore highScore = new HighScore()
        {
            Points = sessionOpts.Points,
            Type = sessionOpts.HighScoreType,
            WeekId = week.Id,
            UserId = sessionOpts.User.Id,
            UserName = sessionOpts.User.Name
        };
        Main._.Game.DataService.AddHighScore(highScore);
    }
}
