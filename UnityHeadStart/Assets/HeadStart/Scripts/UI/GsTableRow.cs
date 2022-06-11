using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GsTableRow : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.1.0";
#pragma warning restore 0414 //
    public List<GsTableCell> RowCells;

    internal void SetData(GsTableData rowData = null)
    {
        if (RowCells == null || RowCells.Count == 0)
        {
            foreach (Transform children in transform)
            {
                var cell = children.GetComponent<GsTableCell>();
                cell.Text = children.GetComponent<Text>();
                RowCells.Add(cell);
            }
        }

        int i = 0;
        foreach (GsTableCell cell in RowCells)
        {
            if (cell.Disabled)
            {
                continue;
            }
            cell.Text.text = rowData == null ? "" : rowData.Values[i];
            i++;
        }
    }
}
