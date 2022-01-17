using System;
using System.Text;
using Assets.Scripts.utils;
using UnityEngine;

public class RankingLeague
{
    public int id;
    public string code;
    public string countryName;
    public string countryId;
    public string name;
    public string rankedId;
    public string checkCode;
    public RankingLeague() { }

    internal string ToRouteParams()
    {
        return new StringBuilder().AppendFormat(
            "cc={0}&c={1}&cid={2}&rid={3}",
            checkCode, countryName, countryId, rankedId
        ).ToString();
    }
}
