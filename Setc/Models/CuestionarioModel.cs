using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Setc.Models
{
    public class CuestionarioModel
    {
        public int id { get; set; }
        public string pregunta { get; set; }
        public List<string> respuestas { get; set; }

    }
}