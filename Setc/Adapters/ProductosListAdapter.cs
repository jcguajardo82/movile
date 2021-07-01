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
                    .Inflate(Resource.Layout.producto_layout, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.productoTextView).Text =
                item.productName;

            convertView.FindViewById<TextView>(Resource.Id.cantidadTextView).Text =
                $"Cantidad: {item.quantity} {item.unitMeasure}";


            return convertView;
        }

        public override int Count => _items.Count;

        public override DetalleModel this[int position]
            => _items[position];
    }
}