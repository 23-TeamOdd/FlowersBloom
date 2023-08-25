using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public class TSV
{
    private string[] buff;
    private string ppat;
    public TSV(string filePath)
    {
        ppat = Path.Combine(Application.streamingAssetsPath, filePath);

        WWW reader = new WWW(ppat);
        while (!reader.isDone) { }
        buff = reader.text.Split('\n');
    }
    private DataTable LoadAllTSV(string lineName)
    {
        DataTable table=new DataTable();

        bool addColumns = false;

        foreach (string d in buff)
        {
            string[] split = d.Split('\t');

            if (split.Length > 0)
            {
                if (split[0] == lineName)
                {
                    // ��� ���� ���, �÷��� �߰��մϴ�.
                    foreach (string colName in split)
                    {
                        table.Columns.Add(colName);
                    }
                    addColumns = true;
                }
                else if (addColumns)
                {
                    // �÷��� �߰��Ǿ��� ��쿡�� ������ ������ �߰��մϴ�.
                    table.Rows.Add(split);
                }
            }
        }

        return table;
    }


    public DataTable limitTSV(string command)
    {
        DataTable tb = LoadAllTSV("Command");
        DataTable limTb = tb.Clone(); // ������ ��Ű���� ���� �� DataTable�� �����մϴ�.

        // Linq�� ����Ͽ� ���õ� ���� ���͸��մϴ�.
        DataRow[] selectedRows = tb.Select("Command = '" + command + "'");
        foreach (DataRow row in selectedRows)
        {
            limTb.ImportRow(row); // ���õ� ���� �� DataTable�� �߰��մϴ�.
        }

        return limTb;
    }
}