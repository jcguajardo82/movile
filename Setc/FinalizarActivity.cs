using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.TextField;
using Setc.Api;
using Setc.Controls;
using Setc.Helpers;
using Setc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Setc
{
    [Activity(Label = "Finalizar Orden")]
    public class FinalizarActivity : AppCompatActivity
    {
        private OrdenModel data;
        Apis api = new Apis();
        private List<CuestionarioModel> cuestionario;
        private List<RespuestaModel> respuestas;
        private TextInputLayout MensajeRecibe;
        private TextInputEditText Recibe;
        private ProgressBar Cargando;
        private LinearLayout preguntas;
        private Location ubicacion = null;
        private string usuario = string.Empty;
        private bool running = false;
        private bool internet = true;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_finalizar);
            usuario = Intent.GetStringExtra("usuario");
            string json = Intent.GetStringExtra("detalle");
            data = JsonSerializer.Deserialize<OrdenModel>(json);
            respuestas = new List<RespuestaModel>();
            preguntas = FindViewById<LinearLayout>(Resource.Id.preguntas);
            MensajeRecibe = FindViewById<TextInputLayout>(Resource.Id.mensajeRecibe);
            Recibe = FindViewById<TextInputEditText>(Resource.Id.recibe);
            Cargando = FindViewById<ProgressBar>(Resource.Id.progressBar);
            var terminar = FindViewById<Button>(Resource.Id.btnFinalizar);
            _ = Preguntas();

            Recibe.FocusChange += FocusChanged;
            Recibe.TextChanged += TextChanged;

            terminar.Click += async (sender, e) =>
            {

                if (internet == false)
                {
                    SnackbarMaker.Make("No tiene acceso a Internet", Recibe);
                    return;
                }

                if (running == true)
                    return;


                ValidarRecibe();
                Cargando.Visibility = ViewStates.Visible;
                running = true;


                if (ubicacion == null)
                    ubicacion = await GetCurrentLocationAsync();


                if (MensajeRecibe.ErrorEnabled == false)
                {
                    var resp = new RecepcionModel
                    {
                        horaRecepcion = DateTime.Now,
                        nombre = Recibe.Text,
                        ubicacion = new UbicacionModel
                        {
                            latitud = ubicacion.Latitude.ToString(),
                            longitud = ubicacion.Longitude.ToString()
                        }
                    };

                    Apis api = new Apis();
                    await api.SendRecepcion(resp);
                    await api.SendCuestionario(respuestas);
                    await api.ChangeEstatusOrder(data.orderNo, "FINALIZADA", 10);
                    Cargando.Visibility = ViewStates.Invisible;
                    Intent intent = new Intent(this, typeof(OrdenesActivity));
                    intent.PutExtra("usuario", usuario);                   
                    StartActivity(intent);
                }
                Cargando.Visibility = ViewStates.Invisible;
                running = false;
            };

            NetworkState();

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                internet = false;
            }
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidarRecibe();
        }

        private void FocusChanged(object sender, View.FocusChangeEventArgs e)
        {
            if (!Recibe.HasFocus)
                ValidarRecibe();
        }

        private void ValidarRecibe()
        {
            MensajeRecibe.Error = ValidarControl.ValidarInput(Recibe.Text, nameof(Recibe));
            if (MensajeRecibe.Error == null)
                MensajeRecibe.ErrorEnabled = false;
            else
                MensajeRecibe.ErrorEnabled = true;
        }
        private async Task Preguntas()
        {
            ubicacion = await GetCurrentLocationAsync();
            cuestionario = await api.GetCuestionario(usuario);
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
                item.respuestas.Add(string.Empty);
                ArrayAdapter<string> adaptador = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, item.respuestas.OrderBy(x => x).ToList());
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
            var respuesta = selected.SelectedItem.ToString();
            if (!string.IsNullOrWhiteSpace(respuesta))
            {
                var pregunta = new RespuestaModel
                {
                    id = id,
                    respuesta = respuesta
                };

                if (respuestas.Where(i => i.id == id).Count() == 0)
                {
                    respuestas.Add(pregunta);
                }
                else
                {
                    respuestas.Remove(respuestas.Where(i => i.id == id).FirstOrDefault());
                    respuestas.Add(pregunta);
                }
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
                SnackbarMaker.Make("No tiene acceso a Internet", Recibe);
            }
        }
        private async Task<Location> GetCurrentLocationAsync()
        {
            string errorMessage = string.Empty;
            Location locationResult = null;
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best);
                locationResult = await Geolocation.GetLocationAsync(request);
            }
            catch (FeatureNotSupportedException ex)
            {
                errorMessage = ("{0}, : {1}", ex.ToString(), ex.StackTrace).ToString();
            }
            catch (FeatureNotEnabledException ex)
            {
                errorMessage = ("{0}, : {1}", ex.ToString(), ex.StackTrace).ToString();
            }
            catch (PermissionException ex)
            {
                errorMessage = ("{0}, : {1}", ex.ToString(), ex.StackTrace).ToString();
            }
            catch (Exception ex)
            {
                errorMessage = ("{0}, : {1}", ex.ToString(), ex.StackTrace).ToString();
            }
            return locationResult;
        }
    }
}