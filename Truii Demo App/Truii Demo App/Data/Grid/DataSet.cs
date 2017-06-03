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
    public class DataSet : DSDataSet
    {
        public DataSet(Context EntryCode, int y)
        {
            Tables.Add(new DataTable("DT", y));
        }
    }
}