using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Setc.Adapters;
using Setc.Api;
using Setc.Controls;
using Setc.Models;
using System.Collections.Generic;

namespace Setc
{
    [Activity(Label = "Mis Ordenes")]
    public class OrdenesActivity : AppCompatActivity
    {
        Handler handler;
        private readonly Apis api = new Apis();
        List<OrdenModel> data = new List<OrdenModel>();
        SwipeRefreshLayout refresh;
        private RecyclerView recyclerView;
        private OrdenesListAdapter adapter;
        private bool IsLoadingMore = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_ordenes);
            handler = new Handler();
            adapter = new OrdenesListAdapter(data, this);
            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.SetAdapter(adapter);
            recyclerView.AddItemDecoration(new DividerItemDecoration(this, (int)Orientation.Vertical));

            refresh = FindViewById<SwipeRefreshLayout>(Resource.Id.swipeRefreshLayout);
            refresh.Refresh += delegate
            {
                SwipeRefreshLayout_Refresh();
            };

            adapter.OnItemClick += (view, position) =>
            {                
                Intent intent = new Intent(this, typeof(DetalleActivity));
                StartActivity(intent);
            };
            LinearLayoutManager linearLayoutManager = (LinearLayoutManager)recyclerView.GetLayoutManager();
            RecyclerView.OnScrollListener scroll = new RecyclerViewOnScrollListtener(refresh, handler, linearLayoutManager, adapter, AddData, IsLoadingMore);

            recyclerView.AddOnScrollListener(scroll);

            GetOrdenes();
        }

        private void SwipeRefreshLayout_Refresh()
        {
            handler.PostDelayed(() =>
            {
                InsertData();
            }, 2000);
        }

        public override void OnBackPressed() { }
        private async void GetOrdenes()
        {
            IsLoadingMore = true;
            data.Clear();
            var ordenes = await api.GetOrders("t_juliolv", 1);
            foreach (var item in ordenes)
            {
                data.Add(item);
            }
            adapter.NotifyDataSetChanged();
            refresh.Refreshing = false;
            adapter.NotifyItemRemoved(adapter.ItemCount);
            IsLoadingMore = false;
        }
        private async void AddData()
        {
            IsLoadingMore = true;
            var ordenes = await api.GetOrders("t_juliolv", 1);
            foreach (var item in ordenes)
            {
                data.Add(item);
            }
            adapter.NotifyDataSetChanged();
            refresh.Refreshing = false;
            adapter.NotifyItemRemoved(adapter.ItemCount);
            IsLoadingMore = false;
        }
        private async void InsertData()
        {
            IsLoadingMore = true;
            var ordenes = await api.GetOrders("t_juliolv", 1);
            data.Clear();
            foreach (var item in ordenes)
            {
                data.Add(item);
            }
            
            adapter.NotifyDataSetChanged();
            refresh.Refreshing = false;
            adapter.NotifyItemRemoved(adapter.ItemCount);
            IsLoadingMore = false;
        }
    }
}