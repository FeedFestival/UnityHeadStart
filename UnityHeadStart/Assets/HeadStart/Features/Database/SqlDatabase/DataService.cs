using System;
using SQLite4Unity3d;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using GameScrypt.GSUtils;

namespace Assets.HeadStart.Features.Database
{
    public class DataService
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        public string DefaultDatabaseName = "Database.db";
        private SQLiteConnection _connection;
        private readonly bool DEBUG_LOG = false;

        public DataService(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = GSStreamingAssets.GetStreamingAssetsFilePath(DefaultDatabaseName, DEBUG_LOG);
            }

            _connection = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
            if (DEBUG_LOG) Debug.Log("Final PATH: " + path);
        }

        public void CleanUpUsers()
        {
            _connection.DropTable<User>();
            _connection.CreateTable<User>();
            if (DEBUG_LOG) Debug.Log("Removed all from User");
        }

        public void CleanDB()
        {
            _connection.DropTable<User>();
            _connection.DropTable<Score>();
            _connection.DropTable<WeekScore>();
            _connection.DropTable<ChallengerScore>();
            if (DEBUG_LOG) Debug.Log("Dropped Tables: User, Score, WeekScore, ChallengerScore");
        }

        public void CreateDB()
        {
            _connection.CreateTable<User>();
            _connection.CreateTable<Score>();
            _connection.CreateTable<WeekScore>();
            _connection.CreateTable<ChallengerScore>();
            if (DEBUG_LOG) Debug.Log("Created Tables: User, Score, WeekScore, ChallengerScore");
        }

        public void CreateDBIfNotExists()
        {
            try
            {
                _connection.Table<User>().Where(x => x.LocalId == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (DEBUG_LOG) Debug.LogWarning(ex.Message);
                _connection.CreateTable<User>();
            }
            try
            {
                _connection.Table<Score>().Where(x => x.Id == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (DEBUG_LOG) Debug.LogWarning(ex.Message);
                _connection.CreateTable<Score>();
            }
            try
            {
                _connection.Table<ChallengerScore>().Where(x => x.Id == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (DEBUG_LOG) Debug.LogWarning(ex.Message);
                _connection.CreateTable<ChallengerScore>();
            }
            try
            {
                _connection.Table<WeekScore>().Where(x => x.Id == 1).FirstOrDefault();
            }
            catch (Exception ex)
            {
                if (DEBUG_LOG) Debug.LogWarning(ex.Message);
                _connection.CreateTable<WeekScore>();
            }
        }

        /*
         * User
         * * --------------------------------------------------------------------------------------------------------------------------------------
         */

        public int CreateUser(User user)
        {
            return _connection.Insert(user);
        }

        public void UpdateUser(User user)
        {
            int rowsAffected = _connection.Update(user);
        }

        public User GetUser()
        {
            try
            {
                return _connection.Table<User>().Where(x => x.LocalId == 1).FirstOrDefault();
            }
            catch (Exception)
            {
                _connection.CreateTable<User>();
                return _connection.Table<User>().Where(x => x.LocalId == 1).FirstOrDefault();
            }
        }

        public User GetTheUser()
        {
            if (_connection.Table<User>().Count() > 0)
            {
                return _connection.Table<User>().First();
            }
            return null;
        }

        public List<User> GetUsers()
        {
            List<User> users = _connection.Query<User>(@"
SELECT
    usr.Id,
    usr.Name
FROM User as usr
ORDER BY usr.Id DESC
LIMIT 8
        ");
            return users;
        }

        public User GetUserByName(string name)
        {
            string sql = @"
SELECT *
FROM User as usr
WHERE lower(usr.Name) = """ + name.ToLower() + @"""
ORDER BY usr.Id DESC
LIMIT 1
        ";
            List<User> users = _connection.Query<User>(sql);
            if (users == null || users.Count == 0)
            {
                return null;
            }
            return users[0];
        }

        public void AddToiletPaper(int userLocalId, int toiletPaper)
        {
            string sql = "UPDATE User SET ToiletPaper = " + toiletPaper + " WHERE LocalId = " + userLocalId;
            _connection.Execute(sql);
        }

        //------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------
        //      CHALLENGE
        //----------------------------------------------
        //-----------------------

        public ChallengerResult GetChallengerScore(int userLocalId)
        {
            List<ChallengerResult> challengerScores = _connection.Query<ChallengerResult>(@"
SELECT
    c.Id, c.ScoreId, s.UserLocalId, s.Points
FROM ChallengerScore as c
    INNER JOIN Score as s ON s.Id = c.ScoreId
WHERE
    s.UserLocalId = " + userLocalId + @"
ORDER BY Points DESC
        ");
            if (challengerScores != null && challengerScores.Count > 0)
            {
                return challengerScores.First();
            }
            return null;
        }

        public void UpdateChallengerScore(int challengeId, int newScoreId)
        {
            ChallengerScore challenge = new ChallengerScore()
            {
                Id = challengeId,
                ScoreId = newScoreId
            };
            int rowsAffected = _connection.Update(challenge);
        }

        public void AddChallengerScore(Score score)
        {
            ChallengerScore challenge = new ChallengerScore()
            {
                ScoreId = score.Id
            };
            _connection.Insert(challenge);
        }

        public List<HighScore> GetChallengersHighscores()
        {
            List<HighScore> highscores = _connection.Query<HighScore>(@"
SELECT
    u.Name as Name,
	s.Points as Points
FROM ChallengerScore as c
	INNER JOIN Score as s ON s.Id = c.ScoreId
	INNER JOIN User as u ON u.LocalId = s.UserLocalId
GROUP BY s.UserLocalId
ORDER BY Points DESC
LIMIT 10;
        ");
            return highscores;
        }

        //-----------------------
        //----------------------------------------------
        //      CHALLENGE - END
        //--------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------
        //      WEEKSCORE
        //----------------------------------------------
        //-----------------------

        public WeekScoreResult GetHighestScoreThisWeek(int userLocalId, League league)
        {
            string sql = @"
SELECT
    w.Id, w.ScoreId, s.Points
FROM WeekScore as w
    INNER JOIN Score as s ON s.Id = w.ScoreId
WHERE
    s.UserLocalId = " + userLocalId + @" AND
    s.Week  = " + league.Week + @" AND
    s.Year  = " + league.Year + @"
ORDER BY Points DESC
        ";
            List<WeekScoreResult> weekScoreResult = _connection.Query<WeekScoreResult>(sql);
            return weekScoreResult.FirstOrDefault();
        }

        public int AddWeekScore(WeekScore weekScore)
        {
            return _connection.Insert(weekScore);
        }

        public void UpdateWeekScore(WeekScore weekScore)
        {
            int rowsAffected = _connection.Update(weekScore);
            if (DEBUG_LOG) Debug.Log("(UPDATE WeekScore) rowsAffected : " + rowsAffected);
        }

        //-----------------------
        //----------------------------------------------
        //      WEEKSCORE - END
        //--------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------

        public int AddScore(Score weekScore)
        {
            return _connection.Insert(weekScore);
        }

        internal void AddHighScore(HighScore highScore)
        {
            int rowsAffected = _connection.Insert(highScore);
            if (DEBUG_LOG) Debug.Log("(CREATED HighScore) rowsAffected : " + rowsAffected);
        }
    }
}
