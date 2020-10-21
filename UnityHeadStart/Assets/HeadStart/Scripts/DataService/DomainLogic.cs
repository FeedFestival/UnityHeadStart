﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

[ExecuteInEditMode]
public class DomainLogic : MonoBehaviour
{
    public void RecreateDataBase()
    {
        var dataService = new DataService();
        dataService.CreateDB();
    }

    public void CleanUpUsers()
    {
        var dataService = new DataService();
        dataService.CleanUpUsers();
    }
}
