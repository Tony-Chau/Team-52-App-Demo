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

        public DataTable(String Name, int[] mP_Fst, int[] mP_Snd, int[] mP_Trd) : base(Name)
        {
            var dataColumns = new Dictionary<string, float>();

            dataColumns.Add("  Year", 100);
            dataColumns.Add("January", 100);
            dataColumns.Add("February", 100);
            dataColumns.Add("March", 100);
            dataColumns.Add("April", 100);
            dataColumns.Add("May", 100);
            dataColumns.Add("June", 100);
            dataColumns.Add("July", 100);
            dataColumns.Add("August", 100);
            dataColumns.Add("September", 100);
            dataColumns.Add("October", 100);
            dataColumns.Add("November", 100);
            dataColumns.Add("December", 100);

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

            for (int Loop = 0; Loop < 3; Loop++)
            {
                var dataRows = new DSDataRow();
                int[] mP = new int[12];
                switch (Loop)
                {
                    case 0:
                        mP = mP_Fst;
                        break;
                    case 1:
                        mP = mP_Snd;
                        break;
                    case 2:
                        mP = mP_Trd;
                        break;
                }

                dataRows["  Year"] = "  " + (Loop + 2017);
                dataRows["January"] = " $ " + mP[0];
                dataRows["February"] = " $ " + mP[1];
                dataRows["March"] = " $ " + mP[2];
                dataRows["April"] = " $ " + mP[3];
                dataRows["May"] = " $ " + mP[4];
                dataRows["June"] = " $ " + mP[5];
                dataRows["July"] = " $ " + mP[6];
                dataRows["August"] = " $ " + mP[7];
                dataRows["September"] = " $ " + mP[8];
                dataRows["October"] = " $ " + mP[9];
                dataRows["November"] = " $ " + mP[10];
                dataRows["December"] = " $ " + mP[11];

                Rows.Add(dataRows);
            }
        }
    }
}