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
        private interface OnItemClickListener
        {
            void onItemClick(View view, int position);
        };
        private OnItemClickListener _OnItemClickListener = null;
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
            if (holder is MyViewHolder)
            {
                MyViewHolder myViewHolder = holder as MyViewHolder;
                myViewHolder.tvTitle.Text = data[position].orderNo.ToString();
                myViewHolder.ItemView.Tag = position;
            }          
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            if (viewType == VIEW_ITEM)
            {
                var itemView = LayoutInflater.From(_context).Inflate(Resource.Layout.orden_layout, parent, false);
                MyViewHolder myViewHolder = new MyViewHolder(itemView);

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
            MyViewHolder myViewHolder = holder as MyViewHolder;
        }
    }
    public class MyViewHolder : RecyclerView.ViewHolder
    {
        public TextView tvTitle;
        public MyViewHolder(View itemView) : base(itemView)
        {
            tvTitle = itemView.FindViewById<TextView>(Resource.Id.numeroTextView);
        }
    }
    public class FootViewHolder : RecyclerView.ViewHolder
    {
        public FootViewHolder(View view) : base(view)
        {
        }
    }
}