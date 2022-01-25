using System;
using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SceneService;
using Assets.Scripts.utils;
using UniRx;
using UnityEngine;

public class GameSession : MonoBehaviour, IUiView
{
    private bool _isInitialized;
    private SessionOpts _sessionOpts;

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
            SessionOpts sessionOpts = MenuEnvironment._.GetChallengeSession();
            MenuEnvironment._.ClearChallengeSession();

            if (sessionOpts == null)
            {
                sessionOpts = new SessionOpts()
                {
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

    public void OnFocussed()
    {
        __.Time.RxWait(() =>
        {
            // ButtonHighscore.Interactable = false;
        }, 1f);
    }

    private void AfterTheGame()
    {
        Main._.Game.InitDatabaseConnection();

        __.Transition.Do(Transition.END, () =>
        {
            _sessionOpts = CoreSession._.SessionOpts;
            Destroy(CoreSession._.gameObject);

            Score score = _sessionOpts.GetScore();
            score.Id = Main._.Game.DataService.AddScore(score);

            UpdateToiletPaper();

            User deviceUser = Main._.Game.DeviceUser();
            bool isDeviceUser = deviceUser.LocalId == _sessionOpts.User.LocalId;

            if (_sessionOpts.IsChallenge)
            {
                TryUpdateChallengerScore(score);
                if (isDeviceUser)
                {
                    TryUpdateWeekScore(deviceUser, score);
                }

                MenuEnvironment._.SetupBackToMainMenuFor(VIEW.Challenge);
                MenuEnvironment._.SwitchView(VIEW.Challenge);

                return;
            }

            if (isDeviceUser)
            {
                TryUpdateChallengerScore(score);

                if (deviceUser.UserType == UserType.CASUAL)
                {
                    bool wasAHighScore = TryUpdateWeekScore(deviceUser, score);
                    if (wasAHighScore || _sessionOpts.User.IsFirstTime)
                    {
                        ShowUserRankedPossibility();
                        return;
                    }

                    MenuEnvironment._.SwitchView(VIEW.MainMenu);
                }
                else
                {
                    if (_sessionOpts.User.IsRegistered == false)
                    {
                        string url = "http://localhost/gameScrypt/be/Ranked/RegisterUser.php";
                        var secret = "a=gameScrypt";
                        var usernameReq = "username=" + _sessionOpts.User.Name;
                        string emailReq = "";
                        if (string.IsNullOrWhiteSpace(_sessionOpts.User.Email))
                        {
                            // TODO: IF no email, login to Google Play Services
                            // emailReq = "&email=" + _sessionOpts.User.Name + "@gmail.com";
                            emailReq = "&email=simionescudani07@gmail.com";
                        }
                        var fullUrl = url + "?" + secret + "&" + usernameReq + emailReq;
                        ObservableUnityWebRequest
                            .GetAsObservable(fullUrl)
                            .Subscribe(responseBody =>
                            {
                                UserDebug user = JsonUtility.FromJson<UserDebug>(responseBody);
                                _sessionOpts.User.LocalId = user.LocalId;
                                _sessionOpts.User.Name = user.Name;

                                Main._.Game.DataService.UpdateUser(_sessionOpts.User);
                                Main._.Game.LoadUser();

                                UpdateHighScore();
                            });
                        return;
                    }

                    UpdateHighScore();
                }
            }
        });
    }

    private void UpdateToiletPaper()
    {
        User deviceUser = Main._.Game.DeviceUser();
        Main._.Game.DataService.AddToiletPaper(deviceUser.LocalId, deviceUser.ToiletPaper + _sessionOpts.ToiletPaper);
        Main._.Game.LoadUser();
    }

    private void TryUpdateChallengerScore(Score score)
    {
        ChallengerResult challengerResult = Main._.Game.DataService.GetChallengerScore(score.UserLocalId);
        if (challengerResult != null)
        {
            if (score.Points > challengerResult.Points)
            {
                Main._.Game.DataService.UpdateChallengerScore(challengerResult.Id, score.Id);
            }
        }
        else
        {
            Main._.Game.DataService.AddChallengerScore(score);
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
        League league = __data.GetThisWeeksLeague();
        WeekScoreResult weekScoreResult = Main._.Game.DataService.GetHighestScoreThisWeek(deviceUser.LocalId, league);

        if (weekScoreResult != null)
        {
            if (score.Points > weekScoreResult.Points)
            {
                WeekScore weekScore = new WeekScore()
                {
                    Id = weekScoreResult.Id,
                    ScoreId = score.Id
                };
                Main._.Game.DataService.UpdateWeekScore(weekScore);
                return true;
            }
        }
        else
        {
            WeekScore weekScore = new WeekScore()
            {
                ScoreId = score.Id
            };
            Main._.Game.DataService.AddWeekScore(weekScore);
            return true;
        }
        return false;
    }

    private void ShowUserRankedPossibility()
    {
        // TODO - make the real functionality here
        // for now we are going to redirect to Main Menu

        Debug.Log("Ask user if he wants to go Ranked");

        MenuEnvironment._.SwitchView(VIEW.MainMenu);
    }
}
