using SQLite4Unity3d;

public enum UserType
{
    CASUAL, RANKED
}

[System.Serializable]
public class User
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    [PrimaryKey, AutoIncrement]
    public int LocalId { get; set; }
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public int UserTypeId { get; set; }
    [Ignore]
    public UserType UserType { get { return (UserType)UserTypeId; } set { UserTypeId = (int)value; } }

    public UserDebug Debug()
    {
        return new UserDebug()
        {
            LocalId = this.LocalId,
            Id = this.Id,
            Email = this.Email,
            Name = this.Name,
            UserType = this.UserType
        };
    }
}

public class UserDebug
{
    public int LocalId;
    public int Id;
    public string Email;
    public string Name;
    public UserType UserType;
}