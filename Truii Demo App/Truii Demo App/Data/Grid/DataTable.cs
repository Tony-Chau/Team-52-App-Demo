using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DSoft.Datatypes.Grid.Data;

namespace Truii_Demo_App.Data.Grid
{
    public class DataTable : DSDataTable
    {
        public DataTable()
        {

        }

        public DataTable(String Name, int y) : base(Name)
        {
            var dataColumns = new Dictionary<string, float>();

            dataColumns.Add("  Year", 100);
            dataColumns.Add("Youtubers", 100);
            dataColumns.Add("Workers", 100);
            dataColumns.Add("Students", 100);

            foreach (var key in dataColumns.Keys)
            {
                var dc = new DSDataColumn(key);
                dc.Caption = key;
                dc.ReadOnly = true;
                dc.DataType = typeof(string);
                dc.AllowSort = true;
                dc.Width = dataColumns[key];
                Columns.Add(dc);
            }

            int[] Y_W_S = new int[3];
            Y_W_S[0] = 50000;
            Y_W_S[1] = 250000;
            Y_W_S[2] = 75000;

            for (int Loop = 0; Loop < y; Loop++)
            {
                var dataRows = new DSDataRow();

                YWS(Y_W_S);
                dataRows["  Year"] = "  " + (Loop + 2017);
                dataRows["Youtubers"] = Y_W_S[0];
                dataRows["Workers"] = Y_W_S[1];
                dataRows["Students"] = Y_W_S[2];

                Rows.Add(dataRows);
            }
        }

        public void YWS(int[] num)
        {
            int maxInc = 500;
            //Random rand = new Random();
            for (int i = 0; i < num.Length; i++)
            {
                //num[i] += rand.Next(maxInc);
                num[i] += maxInc;
            }
        }
    }
}