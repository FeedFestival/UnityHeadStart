using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenSettings : MonoBehaviour
{
    static private HiddenSettings _hiddenSettings;
    static public HiddenSettings _ { get { return _hiddenSettings; } }

    void Awake()
    {
        _hiddenSettings = this;
        DontDestroyOnLoad(this.gameObject);
    }

    internal float GetTime(float normalTime)
    {
        return InstantDebug ? 0f : normalTime;
    }

    public GameObject GetAnInstantiated(GameObject prefab)
    {
        return Instantiate(prefab);
    }

    [Header("Default")]
    public Vector2Int ActualScreenSize;

    [Header("Debug Settings")]
    public bool InstantDebug;

    [Header("Colors")]
    public GameObject PrefabBankPrefab;
    public Color BlackColor;
    public Color TransparentColor;
    public Color RedColor;
    public Color PinkColor;
    public Color GreyColor;
    public Color BlueColor;
    public Color YellowColor;
    public Color LightBlueColor;
}
