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
    [Activity(Label = "Truii", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Splash);

            await Task.Delay(8000);
            
            StartActivity(new Intent(Application.Context, typeof(HomeActivity)));
        }

        
    }
}

