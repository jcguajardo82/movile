using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Setc.Api;
using Setc.Models;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setc
{
    [Activity(Label = "Finalizar Orden")]
    public class FinalizarActivity : AppCompatActivity
    {
        private OrdenModel data;
        Apis api = new Apis();
        List<CuestionarioModel> cuestionario;
        private LinearLayout preguntas;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_finalizar);

            string json = Intent.GetStringExtra("detalle");
            data = JsonSerializer.Deserialize<OrdenModel>(json);

            preguntas = FindViewById<LinearLayout>(Resource.Id.preguntas);
            var terminar = FindViewById<Button>(Resource.Id.btnFinalizar);
            _ = Preguntas();

            terminar.Click += async (sender, e) =>
            {

               


                Apis api = new Apis();
                await api.ChangeEstatusOrder(data.orderNo, "FINALIZADA", 10);
                Intent intent = new Intent(this, typeof(OrdenesActivity));
                StartActivity(intent);
            };

        }

        private async Task Preguntas()
        {
            cuestionario = await api.GetCuestionario("t_juliolv");
            foreach (var item in cuestionario)
            {
                TextView pregunta = new TextView(this)
                {
                    Text = $"{item.pregunta}",
                    TextSize = 15
                };

                pregunta.SetPadding(0, 5, 0, 5);
                preguntas.AddView(pregunta);

                Spinner spinner = new Spinner(this);
                ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, item.respuestas);
                spinner.Adapter = adaptador;
                spinner.Id = item.id;
                spinner.ItemSelected += Spinner_ItemSelected;
                preguntas.AddView(spinner);


            }

        }
        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selected = (Spinner)sender;
            var id = selected.Id;
            var value = selected.SelectedItem;
        }
    }
}