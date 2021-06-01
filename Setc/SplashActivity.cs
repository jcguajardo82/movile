using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setc
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme.Splash", MainLauncher = true)]
    public class SplashActivity : AppCompatActivity
    {
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
        async void Startup()
        {
            await Task.Delay(1); 
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}