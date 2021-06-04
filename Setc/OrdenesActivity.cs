using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Setc.Adapters;
using Setc.Models;
using System.Collections.Generic;
using static Android.Widget.AbsListView;

namespace Setc
{
    [Activity(Label = "Mis Ordenes")]
    public class OrdenesActivity : AppCompatActivity, IOnScrollListener
    {
        private ListView _ordenesListView;
        private List<OrdenModel> _ordenes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_ordenes);

            _ordenesListView = FindViewById<ListView>(Resource.Id.OrdenesListViewControl);
            _ordenes = GetOrdenes();
            _ordenesListView.Adapter = new OrdenesListAdapter(this, _ordenes);
            _ordenesListView.ItemClick += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(DetalleActivity));
                StartActivity(intent);               
            };

        }
        public override void OnBackPressed() { }
        private List<OrdenModel> GetOrdenes()
        {
            var result = new List<OrdenModel>()
            {
                new OrdenModel()
                {
                    Descripcion = "Descripción Orden",
                    Numero = 1,
                    Estado = "En Proceso"
                },
                new OrdenModel()
                {
                    Descripcion = "Descripción Orden",
                    Numero = 2,
                    Estado= "En Espera"
                },
                new OrdenModel()
                {
                    Descripcion = "Descripción Orden",
                    Numero = 3,
                    Estado = "Entregado"
                }
            };
            return result;
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