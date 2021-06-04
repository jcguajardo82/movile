using Android.App;
using Android.Content;
using Android.OS;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Setc
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        private PermissionStatus permissionNetwork = PermissionStatus.Unknown;
        private PermissionStatus permissionLocation = PermissionStatus.Unknown;
        private PermissionStatus permissionCamera = PermissionStatus.Unknown;
        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { Startup(); });
            startupWork.Start();
        }
        public override void OnBackPressed() { }
        private async void Startup()
        {
            permissionLocation = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            permissionNetwork = await Permissions.CheckStatusAsync<Permissions.NetworkState>();
            permissionCamera = await Permissions.CheckStatusAsync<Permissions.Camera>();
            if (permissionNetwork == PermissionStatus.Granted && permissionLocation == PermissionStatus.Granted && permissionCamera == PermissionStatus.Granted)
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }
            else
            {
                StartActivity(new Intent(Application.Context, typeof(PermisosActivity)));
            }
        }
    }
}