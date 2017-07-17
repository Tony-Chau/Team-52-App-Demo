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
using System.Collections.Generic;
using Truii_Demo_App.Table;
using Java.IO;
using static Android.Graphics.Bitmap;
using System.IO;

namespace Truii_Demo_App
{
    [Activity(Label = "Truii", Icon = "@drawable/icon", NoHistory = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@android:style/Theme.NoTitleBar.Fullscreen")]
    public class ChartActivity : Activity
    {
        XYMultipleSeriesDataset mDataSet = new XYMultipleSeriesDataset();
        XYMultipleSeriesRenderer mRenderer = new XYMultipleSeriesRenderer();
        GraphicalView mChartView;
        LinearLayout chartLayout;
        List<TableItem> tableItems = new List<TableItem>();
        ListView listView;
        Button btnSave;
        Bitmap chartbitmap;
        DataDB db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Chart);
            db = new DataDB(this);

            mDataSet.AddSeries(SeriesCreate("DataOne"));
            mDataSet.AddSeries(SeriesCreate("DataTwo"));
            mDataSet.AddSeries(SeriesCreate("DataThree"));
            XYSeriesRenderer renderOne = singleRenderer(255, 000, 000);
            XYSeriesRenderer renderTwo = singleRenderer(000, 255, 000);
            XYSeriesRenderer renderThree = singleRenderer(000, 000, 255);
            mRenderer.SetMargins(new int[] { 10, 60, 100, 30 });
            mRenderer.XLabels = 0;
            mRenderer.ChartTitle = "Data Chart";
            mRenderer.XTitle = "UserID";
            mRenderer.YTitle = "Data Inputs";
            mRenderer.AxisTitleTextSize = 32;
            mRenderer.ChartTitleTextSize = 40;
            mRenderer.LabelsTextSize = 32;
            mRenderer.PointSize = 3;
            mRenderer.ShowLegend = false;
            mRenderer.ShowGridX = true;
            mRenderer.ShowGridY = true;
            mRenderer.ZoomButtonsVisible = true;
            mRenderer.ApplyBackgroundColor = true;
            mRenderer.AxesColor = Color.Black;
            mRenderer.LabelsColor = Color.Black;
            mRenderer.MarginsColor = Color.White;
            mRenderer.GridColor = Color.Black;
            mRenderer.SetYLabelsColor(0, Color.Black);
            mRenderer.XLabelsColor = Color.Black;
            mRenderer.BackgroundColor = Color.White;
            for (int i = 0; i < db.Count(); i++)
            {
                mRenderer.AddXTextLabel(i, db.readPrimary("UserID", i));
            }
            mRenderer.AddSeriesRenderer(renderOne);
            mRenderer.AddSeriesRenderer(renderTwo);
            mRenderer.AddSeriesRenderer(renderThree);
            if (mChartView == null)
            {
                chartLayout = FindViewById<LinearLayout>(Resource.Id.chart);
                mChartView = ChartFactory.GetLineChartView(this, mDataSet, mRenderer);
                chartLayout.AddView(mChartView, new LinearLayout.LayoutParams(
                        Android.Views.ViewGroup.LayoutParams.FillParent,
                        Android.Views.ViewGroup.LayoutParams.FillParent));
            }

            listView = FindViewById<ListView>(Resource.Id.DataList);
            tableItems.Add(new TableItem() { DataName = "DataOne", ID = 9001, Red = 255, Green = 000, Blue = 000 });
            tableItems.Add(new TableItem() { DataName = "DataTwo", ID = 9002, Red = 000, Green = 255, Blue = 000 });
            tableItems.Add(new TableItem() { DataName = "DataThree", ID = 9003, Red = 000, Green = 000, Blue = 255 });
            listView.Adapter = new ChartActivityAdapter(this, tableItems);

            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnSave.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            chartLayout.DrawingCacheEnabled = true;
            chartLayout.BuildDrawingCache(true);
            chartbitmap = chartLayout.GetDrawingCache(true).Copy(Config.Rgb565, false);
            chartLayout.DestroyDrawingCache();

            var storage = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var filePath = System.IO.Path.Combine(storage, "Chart.png");
            var stream = new FileStream(filePath, FileMode.Create);
            chartbitmap.Compress(CompressFormat.Png, 100, stream);
            stream.Close();

            var alert = new AlertDialog.Builder(this);
            alert.SetTitle("A Graph has been saved");
            alert.SetMessage("File is located at: " + filePath.ToString());
            alert.Show();
        }

        public XYSeries SeriesCreate(string name)
        {
            XYSeries series = new XYSeries(name);
            for (int i = 0; i < db.Count(); i++)
            {
                series.Add(i, db.readData(name, i));
            }
            return series;
        }

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

