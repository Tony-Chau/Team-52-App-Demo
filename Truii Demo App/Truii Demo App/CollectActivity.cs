using Android.App;
using Android.Widget;
using Android.OS;
using DSoft.UI.Grid;
using System;
using Truii_Demo_App.Data.Grid;
using AChartEngine.Models;
using AChartEngine.Renderers;
using Android.Graphics;
using Android.Content;
using AChartEngine;
using AChartEngine.Charts;
using Java.Lang;

namespace Truii_Demo_App
{
    [Activity(Label = "Truii", MainLauncher = false, Icon = "@drawable/icon")]
    public class CollectActivity : Activity
    {
        Button btnFinish;
        EditText dataOne;
        EditText dataTwo;
        EditText dataThree;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle">Used for Generating the page</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Collect);

            btnFinish = FindViewById<Button>(Resource.Id.btnFinish);
            btnFinish.Click += BtnFinish_Click;
            btnFinish.Enabled = false;
            dataOne = FindViewById<EditText>(Resource.Id.txtDataOne);
            dataTwo = FindViewById<EditText>(Resource.Id.txtDataTwo);
            dataThree = FindViewById<EditText>(Resource.Id.txtDataThree);

            dataOne.AfterTextChanged += AfterTextChanged;
            dataTwo.AfterTextChanged += AfterTextChanged;
            dataThree.AfterTextChanged += AfterTextChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            if (dataOne.Text.Length > 0 && dataTwo.Text.Length > 0 && dataThree.Text.Length > 0)
            {
                btnFinish.Enabled = true; //This was meant to be true lol
            }
            else
            {
                btnFinish.Enabled = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFinish_Click(object sender, EventArgs e)
        {
            DataDB db = new DataDB(this);

            int intDataOne = Integer.ParseInt(dataOne.Text);
            int intDataTwo = Integer.ParseInt(dataTwo.Text);
            int intDataThree = Integer.ParseInt(dataThree.Text);
            
            db.InserData(intDataOne, intDataTwo, intDataThree);
            Finish();
        }
    }
}

