using Assets.Scripts.utils;
using SQLite4Unity3d;

public enum UserType
{
    CASUAL, RANKED
}

[System.Serializable]
public class User
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
    [PrimaryKey, AutoIncrement]
    public int LocalId { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    // Game Specifics
    public int ToiletPaper { get; set; }
    public bool IsFirstTime { get; set; }
    public bool IsRegistered { get; set; }
    public int UserTypeId { get; set; }
    [Ignore]
    public UserType UserType { get { return (UserType)UserTypeId; } set { UserTypeId = (int)value; } }
    public bool IsUsingSound { get; set; }
    public string Language { get; set; }

    public UserDebug Debug()
    {
        return new UserDebug()
        {
            LocalId = this.LocalId,
            Id = this.Id,
            Name = this.Name,
            ToiletPaper = this.ToiletPaper,
            IsFirstTime = this.IsFirstTime,
            UserType = this.UserType
        };
    }

    public static User FillData(string properties)
    {
        return new User
        {
            Id = __data.GetIntDataValue(properties, "ID:"),
            Name = __data.GetDataValue(properties, "Name:"),
            IsUsingSound = __data.GetBoolDataValue(properties, "IsUsingSound:")
        };
    }
}

public class UserDebug
{
    public int LocalId;
    public int Id;
    public string Name;
    public int ToiletPaper;
    public bool IsFirstTime;
    public UserType UserType;
}