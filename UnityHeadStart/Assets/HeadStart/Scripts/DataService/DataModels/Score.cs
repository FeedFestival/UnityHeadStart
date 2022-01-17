using SQLite4Unity3d;

public class Score
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int UserLocalId { get; set; }
    public int Week { get; set; }
    public int Year { get; set; }
    public int Points { get; set; }

    public ScoreDebug Debug()
    {
        return new ScoreDebug()
        {
            Id = Id,
            UserLocalId = UserLocalId,
            Week = Week,
            Year = Year,
            Points = Points,
        };
    }
}

public class ScoreDebug
{
    public int Id;
    public int UserLocalId;
    public int Week;
    public int Year;
    public int Points;
}
