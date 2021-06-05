using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Setc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Setc.Adapters
{
    class OrdenesListAdapter : BaseAdapter<OrdenModel>
    {
        private readonly Activity _context;
        private readonly List<OrdenModel> _items;

        public OrdenesListAdapter(Activity context, List<OrdenModel> items) : base()
        {
            _context = context;
            _items = items;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            if (convertView == null)
            {
                convertView = _context.LayoutInflater
                    .Inflate(Resource.Layout.ordenitem_layout, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.numeroTextView).Text =  item.orderNo.ToString();
            convertView.FindViewById<TextView>(Resource.Id.descripcionTextView).Text = "PRUEBA";
            convertView.FindViewById<TextView>(Resource.Id.estadoTextView).Text = item.statusUe.ToString();
            Color color = Color.Black;
            switch (item.statusUe)
            {
                case 0:
                    color = Color.DarkGreen;
                    break;
                case 1:
                    color = Color.Orange;
                    break;
            }
            convertView.FindViewById<TextView>(Resource.Id.estadoTextView).SetTextColor(color);
            return convertView;
        }

        public override int Count => _items.Count;

        public override OrdenModel this[int position]
            => _items[position];
    }
}