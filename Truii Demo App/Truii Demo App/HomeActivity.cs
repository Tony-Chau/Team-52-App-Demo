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
using Android.Content.Res;

namespace Truii_Demo_App
{
    [Activity(Label = "Truii", MainLauncher = false, Icon = "@drawable/icon")]
    public class HomeActivity : Activity
    {
        DSGridView dsGrid;
        Button btnCollect;
        Button btnGraph;
        Button btnReset;
        DataDB db;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bundle">Used for Generating the page</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Home);
            db = new DataDB(this);

            dsGrid = FindViewById<DSGridView>(Resource.Id.dataGrid);
            if (dsGrid != null)
            {
                dsGrid.DataSource = new DataSet(this);
                dsGrid.TableName = "DT";
            }
            dsGrid.SetMinimumHeight(Resources.DisplayMetrics.HeightPixels / 2);
            btnCollect = FindViewById<Button>(Resource.Id.btnCollect);
            btnCollect.Click += BtnCollect_Click;

            btnGraph = FindViewById<Button>(Resource.Id.btnGraph);
            btnGraph.Click += BtnGraph_Click;

            btnReset = FindViewById<Button>(Resource.Id.btnReset);
            btnReset.Click += BtnReset_Click;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCollect_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(CollectActivity)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGraph_Click(object sender, EventArgs e)
        {
            LineGraph();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            db.CreateDatabase();
            OnResume();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            if (dsGrid != null)
            {
                dsGrid.DataSource = new DataSet(this);
                dsGrid.TableName = "DT";
            }

            if (db.Count() < 2)
            {
                btnGraph.Enabled = false;
            }
            else
            {
                btnGraph.Enabled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LineGraph()
        {
            XYMultipleSeriesDataset dataSet = new XYMultipleSeriesDataset();
            
            dataSet.AddSeries(SeriesCreate("DataOne"));
            dataSet.AddSeries(SeriesCreate("DataTwo"));
            dataSet.AddSeries(SeriesCreate("DataThree"));

            XYSeriesRenderer renderOne = singleRenderer(255, 255, 000);
            XYSeriesRenderer renderTwo = singleRenderer(000, 255, 000);
            XYSeriesRenderer renderThree = singleRenderer(000, 255, 255);

            XYMultipleSeriesRenderer mRenderer = new XYMultipleSeriesRenderer();
            mRenderer.SetMargins(new int[] { 20, 30, 20, 10 });
            mRenderer.XLabels = 0;
            mRenderer.ChartTitle = "Data Chart";
            mRenderer.XTitle = "UserID";
            mRenderer.YTitle = "Num of Data's";
            mRenderer.AxisTitleTextSize = 32;
            mRenderer.ChartTitleTextSize = 40;
            mRenderer.LabelsTextSize = 20;
            mRenderer.LegendTextSize = 20;
            mRenderer.PointSize = 3;
            mRenderer.ZoomButtonsVisible = true;
            mRenderer.BackgroundColor = Color.Transparent;
            mRenderer.AxesColor = Color.DarkGray;
            mRenderer.LabelsColor = Color.LightGray;
            for (int i = 0; i < db.Count(); i++)
            {
                mRenderer.AddXTextLabel(i, db.readPrimary("UserID", i));
            }
            mRenderer.AddSeriesRenderer(renderOne);
            mRenderer.AddSeriesRenderer(renderTwo);
            mRenderer.AddSeriesRenderer(renderThree);

            Intent intent = ChartFactory.GetLineChartIntent(this, dataSet, mRenderer);
            StartActivity(intent);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public XYSeries SeriesCreate(string name)
        {
            XYSeries series = new XYSeries(name);
            for (int i = 0; i < db.Count(); i++)
            {
                series.Add(i, db.readData(name, i));
            }
            return series;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Red"></param>
        /// <param name="Green"></param>
        /// <param name="Blue"></param>
        /// <returns></returns>
        public XYSeriesRenderer singleRenderer(int Red, int Green, int Blue)
        {
            XYSeriesRenderer sRender = new XYSeriesRenderer();
            sRender.FillPoints = true;
            sRender.LineWidth = 1;
            sRender.DisplayChartValues = true;
            sRender.ChartValuesTextSize = 25;
            sRender.PointStyle = PointStyle.Circle;
            sRender.Color = Color.Rgb(Red, Green, Blue);
            return sRender;
        }
    }
}

