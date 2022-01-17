using System;
using SQLite4Unity3d;

public class ChallengerScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int ScoreId { get; set; }

    internal string Debug()
    {
        return @"
        Id: " + Id + @",
        ScoreId: " + ScoreId + @"
        ";
    }
}

public class ChallengerResult
{
    public int Id { get; set; }
    public int ScoreId { get; set; }
    public int UserLocalId { get; set; }
    public int Points { get; set; }
}