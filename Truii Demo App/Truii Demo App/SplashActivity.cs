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
using System.Threading.Tasks;

namespace Truii_Demo_App
{
    [Activity(Label = "Truii", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar.Fullscreen")]
    public class SplashActivity : Activity
    {
        /// <summary>
        /// As a Splash Screen the App Waits on this page for a period of time then opens the home page
        /// </summary>
        /// <param name="bundle">Used for Generating the page</param>
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Splash);
            await Task.Delay(1000);
            DataDB db = new DataDB(this);
            if (!db.checkDb())
            {
                db.CreateDatabase();
            }
            StartActivity(new Intent(Application.Context, typeof(HomeActivity)));
        }
        
        
    }
}

