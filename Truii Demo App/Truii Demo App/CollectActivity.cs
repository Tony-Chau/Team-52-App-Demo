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

namespace Truii_Demo_App
{
    [Activity(Label = "Truii", MainLauncher = false, Icon = "@drawable/icon")]
    public class CollectActivity : Activity
    {
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Collect);

            Button btn = FindViewById<Button>(Resource.Id.graphBtn);
            btn.Click += (s, e) =>
            {
                
            };

        }
    }
}

