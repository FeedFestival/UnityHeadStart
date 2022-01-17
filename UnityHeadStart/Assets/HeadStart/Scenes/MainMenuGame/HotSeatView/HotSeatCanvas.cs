using System.Collections.Generic;
using Assets.Scripts.utils;
using UnityEngine;

public class HotSeatCanvas : MonoBehaviour
{
    public GsTable HotSeatTable;
    private bool _isInitialized;

    void Awake()
    {
        HotSeatTable.gameObject.SetActive(false);
    }

    public void Init(Transform TLPoint, Transform BRPoint)
    {
        __world2d.PositionRtBasedOnScreenAnchors(
            topLeftAnchor: Camera.main.WorldToScreenPoint(TLPoint.position),
            bottomRightAnchor: Camera.main.WorldToScreenPoint(BRPoint.position),
            rt: (transform as RectTransform),
            screenSize: Main._.CoreCamera.Canvas.sizeDelta
        );

        _isInitialized = true;
    }

    public void Show(Transform TLPoint, Transform BRPoint)
    {
        if (_isInitialized == false)
        {
            Init(TLPoint, BRPoint);
        }

        List<HighScore> highscores = Main._.Game.DataService.GetChallengersHighscores();

        if (highscores == null || highscores.Count == 0)
        {
            return;
        }

        List<GsTableData> tableData = new List<GsTableData>();
        var orderNr = 1;
        foreach (HighScore hs in highscores)
        {
            tableData.Add(new GsTableData()
            {
                Values = new List<string>() {
                    orderNr.ToString(),
                    hs.Name.ToString(),
                    hs.Points.ToString()
                }
            });
            orderNr++;
        }

        HotSeatTable.gameObject.SetActive(true);
        HotSeatTable.SetData(tableData);
    }
}
