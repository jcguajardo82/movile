﻿using Android.Views;
using Google.Android.Material.Snackbar;

namespace AppAndroid.Helpers
{
    public static class SnackbarMaker
    {
        public static void Make(string mensaje, View view)
        {
            Snackbar.Make(view, mensaje, Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();
        }
    }
}