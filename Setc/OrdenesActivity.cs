using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.RecyclerView.Widget;
using AndroidX.SwipeRefreshLayout.Widget;
using Microsoft.AppCenter.Analytics;
using Setc.Adapters;
using Setc.Api;
using Setc.Controls;
using Setc.Models;
using System.Collections.Generic;
using System.Text.Json;
using Xamarin.Essentials;

namespace Setc
{
    [Activity(Label = "Mis Órdenes")]
    public class OrdenesActivity : AppCompatActivity
    {
        private readonly Apis api = new Apis();
        private Handler handler;
        private List<OrdenModel> data = new List<OrdenModel>();
        private SwipeRefreshLayout refresh;
        private RecyclerView recyclerView;
        private OrdenesListAdapter adapter;
        private bool IsLoadingMore = false;
        private bool internet = true;
        private int Page = 1;
        private string usuario = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            if (savedInstanceState != null)
            {
                Page = savedInstanceState.GetInt("pagina");
                string json = savedInstanceState.GetString("datos");
                data = JsonSerializer.Deserialize<List<OrdenModel>>(json);
            }
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_ordenes);
            usuario = Intent.GetStringExtra("usuario");
            if (string.IsNullOrEmpty(usuario))
                usuario = Preferences.Get("usuario", string.Empty);

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
                string detalle = JsonSerializer.Serialize(data[position]);
                intent.PutExtra("detalle", detalle);
                intent.PutExtra("usuario", usuario);
                StartActivityForResult(intent, 100);
            };
            LinearLayoutManager linearLayoutManager = (LinearLayoutManager)recyclerView.GetLayoutManager();
            RecyclerView.OnScrollListener scroll = new RecyclerViewOnScrollListtener(refresh, handler, linearLayoutManager, adapter, AddData, IsLoadingMore);

            NetworkState();

            recyclerView.AddOnScrollListener(scroll);
            if (data.Count == 0)
            {
                Page = 1;
                GetOrdenes();
            }

        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.action_close)
            {
               AndroidX.AppCompat.App.AlertDialog.Builder alert = new AndroidX.AppCompat.App.AlertDialog.Builder(this);
                alert.SetTitle("Atención");
                alert.SetMessage("¿Desea cerrar su sesión?");

                alert.SetPositiveButton("Aceptar", async (senderAlert, args) =>
                {
                    Intent intent = new Intent(this, typeof(MainActivity));
                    FinishAfterTransition();
                    StartActivity(intent);
                });

                alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
                {

                });

                Dialog dialog = alert.Create();
                dialog.Show();
                
            }


            return base.OnOptionsItemSelected(item);
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutInt("pagina", Page);
            string json = JsonSerializer.Serialize(data);
            outState.PutString("datos", json);
            base.OnSaveInstanceState(outState);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            Page = 1;
            GetOrdenes();
        }
        private void SwipeRefreshLayout_Refresh()
        {
            Page = 1;
            handler.PostDelayed(() =>
            {
                data.Clear();
                RefreshData();
            }, 1000);
        }

        public override void OnBackPressed() { }
        private async void GetOrdenes()
        {
            if (internet == false)
            {
                Analytics.TrackEvent("Sin Internet");
                SnackbarMaker.Make("No tiene acceso a Internet", recyclerView);
                return;
            }
            else
            {
                IsLoadingMore = true;
                data.Clear();
                var ordenes = await api.GetOrders(usuario, Page);
                data.AddRange(ordenes);
                adapter.NotifyDataSetChanged();
                refresh.Refreshing = false;
                IsLoadingMore = false;
                adapter.NotifyItemRemoved(adapter.ItemCount);
                if (data.Count == 0)
                    SnackbarMaker.Make("Sin Ordenes de entrega", recyclerView);
                else
                    Page++;
            }
        }
        private async void AddData()
        {
            if (internet == false)
            {
                SnackbarMaker.Make("No tiene acceso a Internet", recyclerView);
                return;
            }
            else
            {
                IsLoadingMore = true;
                var ordenes = await api.GetOrders(usuario, Page);
                data.AddRange(ordenes);
                adapter.NotifyDataSetChanged();
                IsLoadingMore = false;
                refresh.Refreshing = false;
                adapter.NotifyItemRemoved(adapter.ItemCount);
                if (ordenes.Count > 0)
                    Page++;
                else
                    SnackbarMaker.Make("Sin más Ordenes de entrega", recyclerView);

            }
        }
        private async void RefreshData()
        {
            if (internet == false)
            {
                SnackbarMaker.Make("No tiene acceso a Internet", recyclerView);
                return;
            }
            else
            {
                IsLoadingMore = true;
                var ordenes = await api.GetOrders(usuario, Page);
                data.Clear();
                data.AddRange(ordenes);
                adapter.NotifyDataSetChanged();
                refresh.Refreshing = false;
                IsLoadingMore = false;
                adapter.NotifyItemRemoved(adapter.ItemCount);
                if (data.Count == 0)
                    SnackbarMaker.Make("Sin Ordenes de entrega", recyclerView);
                else
                    Page++;
            }
        }
        private void NetworkState()
        {
            Connectivity.ConnectivityChanged += GetNetworkChange;
        }
        private void GetNetworkChange(object sender, ConnectivityChangedEventArgs e)
        {
            var access = e.NetworkAccess;
            if (access == NetworkAccess.Internet)
            {
                internet = true;
            }
            else
            {
                internet = false;
                SnackbarMaker.Make("No tiene acceso a Internet", recyclerView);
            }
        }
    }
}