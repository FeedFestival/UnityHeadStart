﻿using System.Collections.Generic;
using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.UI;

public class GsTable : MonoBehaviour
{
    public GsTableRow DefaultRow;
    public List<GsTableRow> Rows;
    private bool _initialized;
    void Init()
    {
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
}

public class GsTableData
{
    public List<string> Values;
}