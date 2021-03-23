using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GsTable : MonoBehaviour
{
    public List<GsTableRow> Rows;
    private bool _initialized;
    void Init()
    {
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

        Timer._.InternalWait(() =>
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