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
using Android.Views;
using AT.Markushi.UI;

namespace Truii_Demo_App
{
    public class ChartActivityAdapter : BaseAdapter<TableItem>
    {
        List<TableItem> items;
        Activity context;
        LinearLayout DD;
        CircleButton Circle;
        TextView DS;
        TextView DT;

        public ChartActivityAdapter(Activity context, List<TableItem> items)
            : base()
        {
            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        public override TableItem this[int position]
        {
            get { return items[position]; }
        }
        public override int Count
        {
            get { return items.Count; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = items[position];

            View view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomView, null);
            }

            //view.Background

            DD = view.FindViewById<LinearLayout>(Resource.Id.DataDot);
            Circle = new CircleButton(context) { Id = item.ID };
            Circle.SetMinimumHeight(100);
            Circle.SetMaxHeight(100);
            Circle.SetMinimumWidth(100);
            Circle.SetMaxWidth(100);
            Circle.Clickable = false;
            Circle.Pressed = false;
            Circle.SetColor(Color.Rgb(item.Red, item.Green, item.Blue));
            var param = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
            DD.AddView(Circle, param);

            DS = view.FindViewById<TextView>(Resource.Id.DataSpace);
            DS.Text = " ";

            DT = view.FindViewById<TextView>(Resource.Id.DataText);
            DT.Text = item.DataName;
            
            return view;
        }
    }
}

