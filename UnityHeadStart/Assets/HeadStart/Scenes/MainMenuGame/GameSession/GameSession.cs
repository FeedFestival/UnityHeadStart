using Assets.HeadStart.Core;
using Assets.HeadStart.Features.Database;
using UniRx;
using UnityEngine;
using UnityEngine.Events;

namespace HeadStart
{

    public class GameSession : MonoBehaviour, IUiView
    {
        private bool _isInitialized;
        private SessionOpts _sessionOpts;

        UnityAction IUiView.UiViewFocussed { get => uiViewFocussed; }
        public event UnityAction uiViewFocussed;

        private void Init()
        {
            uiViewFocussed += onFocussed;

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

            bool hasCoreSession = CoreSession.S != null;
            if (hasCoreSession)
            {
                AfterTheGame();
                return;
            }

            //__.Timeout.RxSeconds(() =>
            //{
            //    SessionOpts sessionOpts = MenuEnvironment.S.GetChallengeSession();
            //    MenuEnvironment.S.ClearChallengeSession();

            //    if (sessionOpts == null)
            //    {
            //        sessionOpts = new SessionOpts()
            //        {
            //            PlayerSettings = // Main.S.Game.PlayerSettings(),
            //            User = // Main.S.Game.DeviceUser()
            //        };
            //    }
            //    CoreIoC.IoCDependencyResolver.CreateSession(sessionOpts);

            //    __.Transition.Do(Transition.START, () =>
            //    {
            //        // Main.S.Game.LoadScene(SCENE.Game);
            //    });
            //}, MenuEnvironment.S.MOVE_CAMERA_TIME);
        }

        public void onFocussed()
        {
            __.Timeout.RxSeconds(() =>
            {
            // ButtonHighscore.Interactable = false;
            }, 1f);
        }

        private void AfterTheGame()
        {
            // Main.S.Game.InitDatabaseConnection();

            //__.Transition.Do(Transition.END, () =>
            //{
            //    _sessionOpts = CoreSession.S.SessionOpts;
            //    Destroy(CoreSession.S.gameObject);

            //    Score score = _sessionOpts.GetScore();
            //    score.Id = // Main.S.Game.DataService.AddScore(score);

            //    UpdateToiletPaper();

            //    User deviceUser = // Main.S.Game.DeviceUser();
            //    bool isDeviceUser = deviceUser.LocalId == _sessionOpts.User.LocalId;

            //    if (_sessionOpts.IsChallenge)
            //    {
            //        TryUpdateChallengerScore(score);
            //        if (isDeviceUser)
            //        {
            //            TryUpdateWeekScore(deviceUser, score);
            //        }

            //        MenuEnvironment.S.SetupBackToMainMenuFor(VIEW.Challenge);
            //        MenuEnvironment.S.SwitchView(VIEW.Challenge);
            //        // Main.S._EnvironmentReady__.OnNext(false);

            //        return;
            //    }

            //    if (isDeviceUser)
            //    {
            //        TryUpdateChallengerScore(score);

            //        if (deviceUser.UserType == UserType.CASUAL)
            //        {
            //            bool wasAHighScore = TryUpdateWeekScore(deviceUser, score);
            //            if (wasAHighScore || _sessionOpts.PlayerSettings.isFirstTime)
            //            {
            //                ShowUserRankedPossibility();
            //                return;
            //            }

            //            // Main.S._EnvironmentReady__.OnNext(true);
            //        }
            //        else
            //        {
            //            if (_sessionOpts.PlayerSettings.isRegistered == false)
            //            {
            //                string url = "http://localhost/gameScrypt/be/Ranked/RegisterUser.php";
            //                var secret = "a=gameScrypt";
            //                var usernameReq = "username=" + _sessionOpts.User.Name;
            //                string emailReq = "";
            //                if (string.IsNullOrWhiteSpace(_sessionOpts.User.Email))
            //                {
            //                // TODO: IF no email, login to Google Play Services
            //                // emailReq = "&email=" + _sessionOpts.User.Name + "@gmail.com";
            //                emailReq = "&email=simionescudani07@gmail.com";
            //                }
            //                var fullUrl = url + "?" + secret + "&" + usernameReq + emailReq;
            //                ObservableUnityWebRequest
            //                    .GetAsObservable(fullUrl)
            //                    .Subscribe(responseBody =>
            //                    {
            //                        UserDebug user = JsonUtility.FromJson<UserDebug>(responseBody);
            //                        _sessionOpts.User.LocalId = user.LocalId;
            //                        _sessionOpts.User.Name = user.Name;

            //                        // Main.S.Game.DataService.UpdateUser(_sessionOpts.User);
            //                        // Main.S.Game.LoadUser();

            //                        UpdateHighScore();
            //                    });
            //                return;
            //            }

            //            UpdateHighScore();
            //        }
            //    }
            //});
        }

        private void UpdateToiletPaper()
        {
            PlayerSettings PlayerSettings = null;// Main.S.Game.PlayerSettings();
            PlayerSettings.toiletPaper = PlayerSettings.toiletPaper + _sessionOpts.ToiletPaper;

            var deviceJsonData = new DeviceJsonData("player.json");
            deviceJsonData.UpdatePlayer(PlayerSettings);
            // Main.S.Game.LoadPlayerSettings();
        }

        private void TryUpdateChallengerScore(Score score)
        {
            ChallengerResult challengerResult = null; // Main.S.Game.DataService.GetChallengerScore(score.UserLocalId);
            if (challengerResult != null)
            {
                if (score.Points > challengerResult.Points)
                {
                    // Main.S.Game.DataService.UpdateChallengerScore(challengerResult.Id, score.Id);
                }
            }
            else
            {
                // Main.S.Game.DataService.AddChallengerScore(score);
            }
        }

        private void UpdateHighScore()
        {
            // If player wants to save points
            // If he has enough toilet paper

            // Get the points saved in the database

            // Send them to SERVER for first check

            // then -> move the player to Ranked Ladder View
            // there the second check on the SERVER for authenticity
        }

        private bool TryUpdateWeekScore(User deviceUser, Score score)
        {
            League league = League.GetThisWeeksLeague();
            WeekScoreResult weekScoreResult = null; // Main.S.Game.DataService.GetHighestScoreThisWeek(deviceUser.LocalId, league);

            if (weekScoreResult != null)
            {
                if (score.Points > weekScoreResult.Points)
                {
                    WeekScore weekScore = new WeekScore()
                    {
                        Id = weekScoreResult.Id,
                        ScoreId = score.Id
                    };
                    // Main.S.Game.DataService.UpdateWeekScore(weekScore);
                    return true;
                }
            }
            else
            {
                WeekScore weekScore = new WeekScore()
                {
                    ScoreId = score.Id
                };
                // Main.S.Game.DataService.AddWeekScore(weekScore);
                return true;
            }
            return false;
        }

        private void ShowUserRankedPossibility()
        {
            // TODO - make the real functionality here
            // for now we are going to redirect to Main Menu

            Debug.Log("Ask user if he wants to go Ranked");

            MenuEnvironment.S.SwitchView(VIEW.MainMenu);
        }
    }
}