using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace AppAndroid
{
    [Activity(Label = "Finalizar Orden")]
    public class FinalizarActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.content_finalizar);

            var login = FindViewById<Button>(Resource.Id.btnFinalizar);

            login.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(OrdenesActivity));
                StartActivity(intent);
            };

        }
    }
}