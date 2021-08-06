using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.AppBar;
using Google.Android.Material.FloatingActionButton;
using Setc.Adapters;
using Setc.Api;
using Setc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xamarin.Essentials;
using AlertDialog = AndroidX.AppCompat.App.AlertDialog;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;
namespace Setc
{
    [Activity(Theme = "@style/AppTheme.NoActionBar")]
    public class DetalleActivity : AppCompatActivity
    {
        private OrdenModel data;
        private ListView _ordenesListView;
        private List<DetalleModel> _ordenes;
        private string usuario = string.Empty;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_detalle);

            var toolbar = FindViewById(Resource.Id.toolbar);
            var bar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(bar);
            string json = Intent.GetStringExtra("detalle");
            usuario = Intent.GetStringExtra("usuario");
            data = JsonSerializer.Deserialize<OrdenModel>(json);
            FindViewById<CollapsingToolbarLayout>(Resource.Id.toolbar_layout).Title = data.customerName.ToUpperInvariant();
            _ordenesListView = FindViewById<ListView>(Resource.Id.OrdenesListViewControl);
            _ordenes = data.detalle.OrderByDescending(o=>o.surtido).ToList();
            _ordenesListView.Adapter = new ProductosListAdapter(this, _ordenes);
            _ordenesListView.NestedScrollingEnabled = true;
            var terminar = FindViewById<Button>(Resource.Id.btnFinalizar);
            var enProceso = FindViewById<Button>(Resource.Id.btnEnProceso);
            var mapa = FindViewById<FloatingActionButton>(Resource.Id.mapa);
            var ordenText = FindViewById<TextView>(Resource.Id.textViewOrden);
            var direccionText = FindViewById<TextView>(Resource.Id.textViewDireccion);
            var pagoText = FindViewById<TextView>(Resource.Id.textViewTipoPago);
            var itemsText = FindViewById<TextView>(Resource.Id.textViewProductos);

            pagoText.Text = $"Método de pago {data.methodPayment}";
            ordenText.Text = $"Orden Número {data.orderNo}";
            direccionText.Text = $"Dirección {data.address1} {data.address2}, {data.city}. {data.stateCode}";
            itemsText.Text = $"Productos Surtidos {data.detalle.Where(s=>s.surtido == 1).Count()} / No Surtidos {data.detalle.Where(s => s.surtido == 0).Count()}";
            mapa.Click += async (sender, e) =>
            {
                string direccion = $"{data.address1} {data.address2}, {data.city}. {data.stateCode}";
                await Launcher.OpenAsync($"http://maps.google.com/?daddr={direccion}");
            };
            terminar.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(FinalizarActivity));
                string detalle = JsonSerializer.Serialize(data);
                intent.PutExtra("detalle", detalle);
                intent.PutExtra("usuario", usuario);
                StartActivity(intent);
            };



            enProceso.Click += (sender, e) =>
           {
               AlertDialog.Builder alert = new AlertDialog.Builder(this);
               alert.SetTitle("Atención");
               alert.SetMessage("Se procede a entrega");

               alert.SetPositiveButton("Aceptar", async (senderAlert, args) =>
               {
                   Apis api = new Apis();
                   await api.ChangeEstatusOrder(data.orderNo, "EN PROGRESO", 3);
                   enProceso.SetBackgroundColor(Color.LightGray);
                   enProceso.Enabled = false;
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