using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Setc.Adapters;
using Setc.Models;
using System.Collections.Generic;
using System.Text.Json;
using Xamarin.Essentials;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;

namespace Setc
{
    [Activity(Label = "Detalle de Ordenes")]
    public class DetalleActivity : AppCompatActivity
    {
        private OrdenModel data;
        private ListView _ordenesListView;
        private List<DetalleModel> _ordenes;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_detalle);
            string json = Intent.GetStringExtra("detalle");
            data = JsonSerializer.Deserialize<OrdenModel>(json);
            _ordenesListView = FindViewById<ListView>(Resource.Id.OrdenesListViewControl);
            _ordenes = data.detalle;
            _ordenesListView.Adapter = new ProductosListAdapter(this, _ordenes);

            var terminar = FindViewById<Button>(Resource.Id.btnFinalizar);
            var maps = FindViewById<ImageButton>(Resource.Id.btnMap);
            var enProceso = FindViewById<Button>(Resource.Id.btnEnProceso);
            var direccionText = FindViewById<TextView>(Resource.Id.textViewDireccion);

            terminar.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(FinalizarActivity));
                StartActivity(intent);
            };

            maps.Click += async (sender, e) =>
            {
                string direccion = "paseo+del+rio+no.+31+geovillas+castillotla+puebla+72498";
                await Launcher.OpenAsync($"http://maps.google.com/?daddr={direccion}");

            };

            enProceso.Click += delegate
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Atención");
                alert.SetMessage("Se procede a entrega");

                alert.SetPositiveButton("Aceptar", (senderAlert, args) =>
                {
                    enProceso.SetBackgroundColor(Color.LightGray);

                });

                alert.SetNegativeButton("Cancelar", (senderAlert, args) =>
                {

                });

                Dialog dialog = alert.Create();
                dialog.Show();
            };

        }
    }
}