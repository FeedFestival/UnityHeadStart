using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.utils;

public class User
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    // Game Settings
    public bool IsFirstTime { get; set; }
    public bool IsUsingSound { get; set; }
    public string Language { get; set; }

    //[Ignore]
    //public FacebookApp FacebookApp { get; set; }

    public static User FillData(string properties)
    {
        return new User
        {
            Id = __data.GetIntDataValue(properties, "ID:"),
            Name = __data.GetDataValue(properties, "Name:"),
            IsUsingSound = __data.GetBoolDataValue(properties, "IsUsingSound:")
            //FacebookApp = new FacebookApp
            //{
            //    FacebookId = __utils.GetLongDataValue(properties, "FacebookId:")
            //}
        };
    }
}


public class WeekScore
{
    [PrimaryKey]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Week { get; set; }
    public int Year { get; set; }
    public int Points { get; set; }
}

public class HighScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public int TypeId { get; set; }
    [Ignore]
    public HighScoreType Type
    {
        get
        {
            return (HighScoreType)TypeId;
        }
        set
        {
            TypeId = (int)value;
        }
    }
    public int WeekId { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int Points { get; set; }
}

public enum HighScoreType
{
    RANKED, HOTSEAT
}

public class WeekDetails
{
    public int Id;
    public int Year;
    public int Nr;
}
