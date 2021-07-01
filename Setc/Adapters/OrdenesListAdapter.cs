using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Setc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Setc.Adapters
{
    public class OrdenesListAdapter : RecyclerView.Adapter
    {
        private List<OrdenModel> data;
        private Context _context;
        private const int VIEW_ITEM = 0;
        private const int VIEW_FOOTER = 1;
        public delegate void ItemClick(View v, int position);
        public event ItemClick OnItemClick;
        private interface IOnItemClickListener
        {
            void onItemClick(View view, int position);
        };
       // private IOnItemClickListener _OnItemClickListener = null;
        public OrdenesListAdapter(List<OrdenModel> list, Context context)
        {
            data = list;
            _context = context;
        }
        public override int ItemCount
        {
            get
            {
                return data.Count == 0 ? 0 : data.Count + 1;
            }
        }
        public override int GetItemViewType(int position)
        {
            if (position + 1 == ItemCount)
                return VIEW_FOOTER;
            return VIEW_ITEM;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            if (holder is OrderHolder)
            {
                OrderHolder orden = holder as OrderHolder;
                orden.NoOrden.Text = data[position].orderNo.ToString();
                orden.Cliente.Text = data[position].customerName;
                orden.ItemView.Tag = position;
            }          
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == VIEW_ITEM)
            {
                var itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.orden_layout, parent, false);
                OrderHolder myViewHolder = new OrderHolder(itemView);

                itemView.Click += delegate
                {
                    OnItemClick(itemView, (int)itemView.Tag);
                };
                return myViewHolder;
            }
            else if (viewType == (int)VIEW_FOOTER)
            {
                View view = LayoutInflater.From(_context).Inflate(Resource.Layout.item_recyclerView_foot, parent, false);
                return new FootViewHolder(view);
            }
            return null;
        }
        
        public override void OnViewRecycled(Java.Lang.Object holder)
        {
            base.OnViewRecycled(holder);
            OrderHolder myViewHolder = holder as OrderHolder;
        }
    }
    public class OrderHolder : RecyclerView.ViewHolder
    {
        public TextView NoOrden;
        public TextView Cliente;
        public OrderHolder(View itemView) : base(itemView)
        {
            NoOrden = itemView.FindViewById<TextView>(Resource.Id.numeroTextView);
            Cliente = itemView.FindViewById<TextView>(Resource.Id.clienteTextView);
        }
    }
    public class FootViewHolder : RecyclerView.ViewHolder
    {
        public FootViewHolder(View view) : base(view)
        {
        }
    }
}