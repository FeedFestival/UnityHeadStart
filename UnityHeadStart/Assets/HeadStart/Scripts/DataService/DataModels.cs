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
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }

    // Game Settings
    public bool IsFirstTime { get; set; }
    public bool HasSavedGame { get; set; }
    public bool IsUsingSound { get; set; }
    public string Language { get; set; }

    //[Ignore]
    //public FacebookApp FacebookApp { get; set; }

    public static User FillData(string properties)
    {
        return new User
        {
            Id = DataUtils.GetIntDataValue(properties, "ID:"),
            Name = DataUtils.GetDataValue(properties, "Name:"),
            IsUsingSound = DataUtils.GetBoolDataValue(properties, "IsUsingSound:")
            //FacebookApp = new FacebookApp
            //{
            //    FacebookId = utils.GetLongDataValue(properties, "FacebookId:")
            //}
        };
    }
}
