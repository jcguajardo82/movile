using Android.App;
using Android.Views;
using Android.Widget;
using Setc.Models;
using System.Collections.Generic;

namespace Setc.Adapters
{
    class ProductosListAdapter : BaseAdapter<DetalleModel>
    {
        private readonly Activity _context;
        private readonly List<DetalleModel> _items;

        public ProductosListAdapter(Activity context, List<DetalleModel> items) : base()
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
                    .Inflate(Resource.Layout.orden_layout, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.numeroTextView).Text =
                item.barcode.ToString();

            convertView.FindViewById<TextView>(Resource.Id.descripcionTextView).Text =
                item.productName;



            return convertView;
        }

        public override int Count => _items.Count;

        public override DetalleModel this[int position]
            => _items[position];
    }
}