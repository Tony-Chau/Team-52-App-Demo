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
    public class HomeActivity : Activity
    {
        DSGridView dsGrid;
        static int numOfMonths = 12;
        private int listnum = 19;
        Random rand = new Random();
        private string[] Months = new string[]
        {
            "Jan", "Feb" , "Mar", "Apr", "May", "Jun",
            "Jul", "Aug" , "Sep", "Oct", "Nov", "Dec"
        };
        private int[] mP_Fst = new int[numOfMonths];
        private int[] mP_Snd = new int[numOfMonths];
        private int[] mP_Trd = new int[numOfMonths];

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Home);

            monthsProfitInit(mP_Fst);
            monthsProfitInit(mP_Snd);
            monthsProfitInit(mP_Trd);

            dsGrid = FindViewById<DSGridView>(Resource.Id.dataGrid);
            if (dsGrid != null)
            {
                dsGrid.DataSource = new DataSet(this, listnum);
                dsGrid.TableName = "DT";
            }

            Button btnCollect = FindViewById<Button>(Resource.Id.CollectBtn);
            btnCollect.Click += BtnCollect_Click;

            Button btnGraph = FindViewById<Button>(Resource.Id.GraphBtn);
            btnGraph.Click += BtnGraph_Click;
        }

        private void BtnCollect_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(CollectActivity)));
            listIncrement();
        }

        private void BtnGraph_Click(object sender, EventArgs e)
        {
            LineGraph();
        }      
        protected override void OnResume()
        {
            base.OnResume();
            if (dsGrid != null)
            {
                dsGrid.DataSource = new DataSet(this, listnum);
                dsGrid.TableName = "DT";
            }
        }

        public void listIncrement()
        {
            listnum++;
        }

        public void monthsProfitInit(int[] months)
        {
            int maxProfit = 10000;
            for (int i = 0; i < numOfMonths; i++)
            {
                months[i] = rand.Next(maxProfit);
            }
        }

        private void LineGraph()
        {

            int[] x = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

            XYMultipleSeriesDataset dataSet = new XYMultipleSeriesDataset();

            XYSeries mP_FstSeries = new XYSeries("Year 2017");
            XYSeries mP_SndSeries = new XYSeries("Year 2018");
            XYSeries mP_TrdSeries = new XYSeries("Year 2019");
            for (int i = 0; i < x.Length; i++)
            {
                mP_FstSeries.Add(i, mP_Fst[i]);
                mP_SndSeries.Add(i, mP_Snd[i]);
                mP_TrdSeries.Add(i, mP_Trd[i]);
            }
            dataSet.AddSeries(mP_FstSeries);
            dataSet.AddSeries(mP_SndSeries);
            dataSet.AddSeries(mP_TrdSeries);

            XYSeriesRenderer renderFst = singleRenderer(255, 255, 000);
            XYSeriesRenderer renderSnd = singleRenderer(000, 255, 000);
            XYSeriesRenderer renderTrd = singleRenderer(000, 255, 255);

            XYMultipleSeriesRenderer mRenderer = new XYMultipleSeriesRenderer();
            mRenderer.SetMargins(new int[] { 20, 30, 20, 10 });
            mRenderer.XLabels = 0;
            mRenderer.ChartTitle = "Monthly Profit";
            mRenderer.XTitle = "Months";
            mRenderer.YTitle = "Profits ($)";
            mRenderer.AxisTitleTextSize = 32;
            mRenderer.ChartTitleTextSize = 40;
            mRenderer.LabelsTextSize = 20;
            mRenderer.LegendTextSize = 20;
            mRenderer.PointSize = 3;
            mRenderer.ZoomButtonsVisible = true;
            mRenderer.BackgroundColor = Color.Transparent;
            mRenderer.AxesColor = Color.DarkGray;
            mRenderer.LabelsColor = Color.LightGray;
            for (int i = 0; i < x.Length; i++)
            {
                mRenderer.AddXTextLabel(i, Months[i]);
            }
            mRenderer.AddSeriesRenderer(renderFst);
            mRenderer.AddSeriesRenderer(renderSnd);
            mRenderer.AddSeriesRenderer(renderTrd);

            Intent intent = ChartFactory.GetLineChartIntent(this, dataSet, mRenderer);
            StartActivity(intent);
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

