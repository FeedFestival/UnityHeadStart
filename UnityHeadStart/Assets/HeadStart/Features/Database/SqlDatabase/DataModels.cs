using SQLite4Unity3d;

namespace Assets.HeadStart.Features.Database
{
    public class WeekScore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int ScoreId { get; set; }
    }

    public class HighScore
    {
        public string Name { get; set; }
        public int Points { get; set; }
    }

    public class WeekScoreResult
    {
        public int Id { get; set; }
        public int ScoreId { get; set; }
        public int Points { get; set; }
    }

    public class League
    {
        public int Year;
        public int Week;
        public string Name;
    }
}