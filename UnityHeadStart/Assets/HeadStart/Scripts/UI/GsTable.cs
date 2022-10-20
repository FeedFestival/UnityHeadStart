using System.Collections.Generic;
using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.UI;

public class GsTable : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public GsTableRow DefaultRow;
    public List<GsTableRow> Rows;
    private bool _initialized;
    private float _rowWidth;
    private float _rowHeight;

    void Init()
    {
        var newSize = new Vector2(_rowWidth, _rowHeight);
        (DefaultRow.transform as RectTransform).sizeDelta = newSize;
        GameObject go;
        GsTableRow gsRow;
        for (var i = 0; i < 9; i++)
        {
            go = Instantiate(
                DefaultRow.gameObject,
                Vector3.zero,
                Quaternion.identity,
                transform
            );
            var localP = (go.transform as RectTransform).localPosition;
            (go.transform as RectTransform).localPosition = Vector3.zero;
            (go.transform as RectTransform).sizeDelta = newSize;
            gsRow = go.GetComponent<GsTableRow>();
            if (i % 2 != 0)
            {
                gsRow.GetComponent<Image>().enabled = false;
            }
            Rows.Add(gsRow);
        }

        DefaultRow.GetComponent<Image>().enabled = false;
        Rows.Insert(0, DefaultRow);

        foreach (GsTableRow row in Rows)
        {
            row.SetData();
        }
        _initialized = true;
    }

    public void SetData(List<GsTableData> tableData)
    {
        if (_initialized == false)
        {
            Init();
        }
        __.Time.RxWait(() =>
        {
            // __debug.DebugList(tableData, "tableData", (GsTableData row) => {
            //     return __debug.GetDebugList(row.Values, "row: ");
            // });
            int i = 0;
            foreach (GsTableRow row in Rows)
            {
                if (i >= tableData.Count)
                {
                    break;
                }
                row.SetData(tableData[i]);
                i++;
            }
        }, 0.1f);
    }

    public void CalculateRowHeight(float maxWidth, float maxHeight, int maxRows)
    {
        _rowHeight = maxHeight / maxRows;
        _rowWidth = maxWidth;
    }
}

public class GsTableData
{
    public List<string> Values;
}