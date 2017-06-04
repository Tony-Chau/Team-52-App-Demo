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

        public DataTable(Context context, String Name) : base(Name)
        {
            DataDB db = new DataDB(context);

            var dataColumns = new Dictionary<string, float>();
            dataColumns.Add("  UserId", 100);
            dataColumns.Add("DataOne", 100);
            dataColumns.Add("DataTwo", 100);
            dataColumns.Add("DataThree", 100);

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

            for (int Loop = 0; Loop < db.Count(); Loop++)
            {
                var dataRows = new DSDataRow();

                dataRows["  UserID"] = "  " + (db.readData("UserID", Loop));
                dataRows["DataOne"] = db.readData("DataOne", Loop);
                dataRows["DataTwo"] = db.readData("DataTwo", Loop);
                dataRows["DataThree"] = db.readData("DataThree", Loop);

                Rows.Add(dataRows);
            }
        }
    }
}