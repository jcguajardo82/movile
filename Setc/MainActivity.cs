using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
using Xamarin.Essentials;
using static Android.Views.View;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Setc
{
    [Activity(Label = "@string/app_name", NoHistory = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly Apis api = new Apis();
        private TextInputLayout MensajeUsuario;
        private TextInputEditText Usuario;
        private TextInputLayout MensajePassword;
        private TextInputEditText Password;
        private Button Login;
        private ProgressBar Cargando;
        private bool running = false;
        private bool internet = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
           MensajeUsuario = FindViewById<TextInputLayout>(Resource.Id.mensajeUsuario);
            Usuario = FindViewById<TextInputEditText>(Resource.Id.usuario);
            MensajePassword = FindViewById<TextInputLayout>(Resource.Id.mensajePassword);
            Password = FindViewById<TextInputEditText>(Resource.Id.password);
            Login = FindViewById<Button>(Resource.Id.btnLogin);
            Cargando = FindViewById<ProgressBar>(Resource.Id.progressBar);

            Usuario.FocusChange += FocusChanged;
            Usuario.TextChanged += TextChanged;

            Password.FocusChange += FocusChanged;
            Password.TextChanged += TextChanged;

            Login.Click += LoginClick;
            NetworkState();

            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                internet = false;
            }

            AppCenter.Start("f3a7ebef-4b4f-4f92-9dd3-9dc066f6b5c3",typeof(Analytics), typeof(Crashes));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == Usuario)
            {
                ValidarUsuario();
            }
            if (sender == Password)
            {
                ValidarPassword();
            }
        }
        private void FocusChanged(object sender, FocusChangeEventArgs e)
        {
            if (sender == Usuario)
            {
                if (!Usuario.HasFocus)
                    ValidarUsuario();
            }
            if (sender == Password)
            {
                if (!Password.HasFocus)
                    ValidarPassword();
            }
        }

        private async void LoginClick(object sender, EventArgs e)
        {
            if (internet == false)
            {
                Analytics.TrackEvent("Sin Internet");
                SnackbarMaker.Make("No tiene acceso a Internet", Login);
                return;
            }

            if (running == true)
                return;

            running = true;

            Cargando.Visibility = ViewStates.Visible;
            ValidarPassword();
            ValidarUsuario();
            if (MensajePassword.ErrorEnabled == true || MensajeUsuario.ErrorEnabled == true)
            {
                SnackbarMaker.Make("Ingrese la información necesaria", Login);
                Cargando.Visibility = ViewStates.Invisible;
                running = false;
                return;
            }
            Habilitar(false);

            var exito = await api.Login(new LoginModel
            {
                Pass = Password.Text,
                User = Usuario.Text
            });
            if (exito == "true")
            {
                Preferences.Set(Usuario.Text, "usuario");
                Intent intent = new Intent(this, typeof(OrdenesActivity));
                intent.PutExtra("usuario", Usuario.Text);
                StartActivity(intent);
                SetResult(Result.Ok, intent);
                Finish();
            }
            else
            {
                Habilitar(true);
                Cargando.Visibility = ViewStates.Invisible;
                SnackbarMaker.Make("Usuario y/o contraseña incorrecto", Login);
            }
            running = false;
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
                Analytics.TrackEvent("Sin Internet");
                internet = false;
                SnackbarMaker.Make("No tiene acceso a Internet", Login);
            }
        }
        private void ValidarUsuario()
        {
            MensajeUsuario.Error = ValidarControl.ValidarInput(Usuario.Text, nameof(Usuario));
            if (MensajeUsuario.Error == null)
                MensajeUsuario.ErrorEnabled = false;
            else
                MensajeUsuario.ErrorEnabled = true;
        }
        private void ValidarPassword()
        {
            MensajePassword.Error = ValidarControl.ValidarInput(Password.Text, nameof(Password));
            if (MensajePassword.Error == null)
                MensajePassword.ErrorEnabled = false;
            else
                MensajePassword.ErrorEnabled = true;
        }
        private void Habilitar(bool estado)
        {
            Password.Enabled = estado;
            Usuario.Enabled = estado;
            Login.Enabled = estado;
        }
        public override void OnBackPressed() { }
    }
}
