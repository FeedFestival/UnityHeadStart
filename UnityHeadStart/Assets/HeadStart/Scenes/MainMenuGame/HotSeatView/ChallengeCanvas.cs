using System.Collections.Generic;
using Assets.Scripts.utils;
using UnityEngine;

public class ChallengeCanvas : MonoBehaviour
{
    public GsTable ChallengeTable;
    private bool _isInitialized;

    void Awake()
    {
        ChallengeTable.gameObject.SetActive(false);
    }

    public void Init(WorldCanvasPoint tableWCP)
    {
        __world2d.PositionRtBasedOnScreenAnchors(
            tableWCP, rt: (transform as RectTransform),
            screenSize: Main._.CoreCamera.Canvas.sizeDelta
        );

        _isInitialized = true;
    }

    public void Show(WorldCanvasPoint tableWCP)
    {
        if (_isInitialized == false)
        {
            Init(tableWCP);
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

        ChallengeTable.gameObject.SetActive(true);
        ChallengeTable.SetData(tableData);
    }
}
