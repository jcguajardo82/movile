using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Setc.Adapters;
using Setc.Api;
using Setc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Android.Widget.AbsListView;

namespace Setc
{
    [Activity(Label = "Mis Ordenes")]
    public class OrdenesActivity : AppCompatActivity, IOnScrollListener
    {
        private readonly Apis api = new Apis();
        private ListView _ordenesListView;
        private List<OrdenModel> _ordenes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_ordenes);

            _ordenesListView = FindViewById<ListView>(Resource.Id.OrdenesListViewControl);
            _ = GetOrdenes();
            _ordenesListView.ItemClick += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(DetalleActivity));
                StartActivity(intent);               
            };

        }
        public override void OnBackPressed() { }
        private async Task<bool> GetOrdenes()
        {
            _ordenes = await api.GetOrders("asd",1);
            MainThread.BeginInvokeOnMainThread(() =>
            {
            _ordenesListView.Adapter = new OrdenesListAdapter(this, _ordenes);
            });
            return true;
        }

        public void OnScroll(AbsListView view, int firstVisibleItem, int visibleItemCount, int totalItemCount)
        {
            throw new System.NotImplementedException();
        }

        public void OnScrollStateChanged(AbsListView view, [GeneratedEnum] ScrollState scrollState)
        {
            throw new System.NotImplementedException();
        }
    }
}