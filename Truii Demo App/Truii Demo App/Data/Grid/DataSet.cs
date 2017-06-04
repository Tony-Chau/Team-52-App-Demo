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
        /// <summary>
        /// This will initilize the spreadsheet
        /// </summary>
        /// <param name="EntryCode">This role of this parameter is to transfer the context into the DataTable</param>
        public DataSet(Context EntryCode)
        {
            Tables.Add(new DataTable(EntryCode, "DT"));
        }
    }
}