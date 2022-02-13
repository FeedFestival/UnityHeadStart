﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Assets.HeadStart.Time;
using Assets.Scripts.utils;
using SQLite4Unity3d;
using UnityEngine;

public class VersionChecker : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.7";
#pragma warning restore 0414 //
#if UNITY_EDITOR
    readonly List<VersionCheckFile> versionCheckFiles = new List<VersionCheckFile>()
    {
        new VersionCheckFile("__utils",
            "HeadStart/Assets/HeadStart/Scripts/utils/__utils.cs",
            __utils._version
        ),
        new VersionCheckFile("__data",
            "HeadStart/Assets/HeadStart/Scripts/utils/__data.cs",
            __data._version
        ),
        new VersionCheckFile("FpsDisplay",
            "HeadStart/Assets/HeadStart/Scripts/utils/FpsDisplay.cs",
            FpsDisplay._version
        ),
        new VersionCheckFile("__json",
            "HeadStart/Assets/HeadStart/Scripts/utils/__json.cs",
            __json._version
        ),
        new VersionCheckFile("Main",
            "HeadStart/Assets/HeadStart/Scripts/Main.cs",
            Main._version
        ),
        new VersionCheckFile("TimeBase",
            "HeadStart/Assets/HeadStart/Scripts/TimeBase.cs",
            TimeBase._version
        ),
        new VersionCheckFile("DataService",
            "HeadStart/Assets/HeadStart/Scripts/DataService/DataService.cs",
            DataService._version
        ),
        // DomainLogic              - GameSpecific
        new VersionCheckFile("SQLite",
            "HeadStart/Assets/HeadStart/Scripts/DataService/SQLite.cs",
            SQLiteConnection._version
        )
    };
    private const string _onlineUrl = "https://raw.githubusercontent.com/FeedFestival/UnityHeadStart/main/Unity";   // no / required
    private const string _key = "_version = \"";
    private const string _endKey = "\";";
    private WebClient _webClient;

    internal void Check()
    {
        string version = string.Empty;
        VERSION_CHANGE versionChange = VERSION_CHANGE.OUTDATED;
        foreach (VersionCheckFile versionCheckFile in versionCheckFiles)
        {
            try
            {
                version = GetOnlineVersion(_onlineUrl + versionCheckFile.Url);
            }
            catch (Exception)
            {
                Debug.LogWarning("HeadStart." + versionCheckFile.Name + " class CAN'T_BE_CHECKED for a version(" +
                    versionCheckFile.CurrentVersion + "). <a href=\"https://raw.githubusercontent.com/FeedFestival/UnityHeadStart\">" +
                    "Check Online https://raw.githubusercontent.com/FeedFestival/UnityHeadStart</a>");
                continue;
            }
            try
            {
                versionChange = CompareVersions(versionCheckFile.CurrentVersion, version);
            }
            catch (Exception)
            {
                continue;
            }
            if (versionChange != VERSION_CHANGE.SAME)
            {
                Debug.LogWarning("HeadStart." + versionCheckFile.Name + " class is " + versionChange.ToString() +
                    "(" + versionCheckFile.CurrentVersion + "). Online version is " + version + ".");
            }
        }
    }

    private string GetOnlineVersion(string url)
    {
        var fileContent = GetOnlineFile(url);
        var index = fileContent.IndexOf(_key) + _key.Length;
        var endIndex = fileContent.IndexOf(_endKey);
        var versionLength = endIndex - index;
        return fileContent.Substring(index, versionLength);
    }

    private string GetOnlineFile(string url)
    {
        if (_webClient == null)
        {
            _webClient = new WebClient();
        }
        using (Stream stream = _webClient.OpenRead(url))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    private VERSION_CHANGE CompareVersions(string thisVersion, string onlineVersion)
    {
        thisVersion = thisVersion.Replace('.', '0');
        double thisV = Convert.ToDouble(thisVersion);
        onlineVersion = onlineVersion.Replace('.', '0');
        double onlineV = Convert.ToDouble(onlineVersion);
        if (thisV < onlineV)
        {
            return VERSION_CHANGE.OUTDATED;
        }
        else if (thisV > onlineV)
        {
            return VERSION_CHANGE.UPDATED;
        }
        return VERSION_CHANGE.SAME;
    }
#endif
}
#if UNITY_EDITOR
public enum VERSION_CHANGE { SAME, OUTDATED, UPDATED }
public class VersionCheckFile
{
    public string Name;
    public string Url;
    public string CurrentVersion;
    public VersionCheckFile(string name, string url, string currentVersion)
    {
        Name = name;
        Url = url;
        CurrentVersion = currentVersion;
    }
}
#endif
