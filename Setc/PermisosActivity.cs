using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using Xamarin.Essentials;

namespace Setc
{
    [Activity(Label = "Permisos", Theme = "@style/AppTheme.NoActionBar", NoHistory = true)]
    public class PermisosActivity : AppCompatActivity
    {
        private PermissionStatus permissionNetwork = PermissionStatus.Unknown;
        private PermissionStatus permissionLocation = PermissionStatus.Unknown;
        private PermissionStatus permissionCamera = PermissionStatus.Unknown;
        private Button Permisos;
        private bool running = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_permisos);

            Permisos = FindViewById<Button>(Resource.Id.btnPermisos);


            Permisos.Click += PermisosClick;

            var current = Connectivity.NetworkAccess;


        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }



        private void PermisosClick(object sender, EventArgs e)
        {

            if (running == true)
                return;

            running = true;

            GetPermissions();
            running = false;
        }

        public override void OnBackPressed() { }
        private async void GetPermissions()
        {
            if (permissionNetwork == PermissionStatus.Granted && permissionLocation == PermissionStatus.Granted && permissionCamera == PermissionStatus.Granted)
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
            else
            {
                permissionLocation = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                permissionNetwork = await Permissions.CheckStatusAsync<Permissions.NetworkState>();
                permissionCamera = await Permissions.CheckStatusAsync<Permissions.Camera>();

                _ = permissionLocation != PermissionStatus.Granted ? await Permissions.RequestAsync<Permissions.LocationWhenInUse>() : permissionLocation;
                _ = permissionNetwork != PermissionStatus.Granted ? await Permissions.RequestAsync<Permissions.NetworkState>() : permissionNetwork;
                _ = permissionCamera != PermissionStatus.Granted ? await Permissions.RequestAsync<Permissions.Camera>() : permissionCamera;

                GetPermissions();
            }
        }
    }
}
