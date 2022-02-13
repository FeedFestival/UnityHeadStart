using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

[ExecuteInEditMode]
public class DomainLogic : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.7";
#pragma warning restore 0414 //
    public void DeleteDataBase()
    {
        var dataService = new DataService();
        dataService.CleanDB();
    }
    public void RecreateDataBase()
    {
        var dataService = new DataService();
        dataService.CleanDB();
        dataService.CreateDB();
    }

    public void CleanUpUsers()
    {
        var dataService = new DataService();
        dataService.CleanUpUsers();
    }
}
