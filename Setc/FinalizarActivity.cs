using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Setc.Api;
using Setc.Models;
using System.Text.Json;

namespace Setc
{
    [Activity(Label = "Finalizar Orden")]
    public class FinalizarActivity : AppCompatActivity
    {
        private OrdenModel data;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_finalizar);

            string json = Intent.GetStringExtra("detalle");
            data = JsonSerializer.Deserialize<OrdenModel>(json);


            var terminar = FindViewById<Button>(Resource.Id.btnFinalizar);

            terminar.Click += async (sender, e) =>
            {
                Apis api = new Apis();
                await api.ChangeEstatusOrder(data.orderNo, "FINALIZADA", 10);
                Intent intent = new Intent(this, typeof(OrdenesActivity));
                StartActivity(intent);
            };

        }
    }
}