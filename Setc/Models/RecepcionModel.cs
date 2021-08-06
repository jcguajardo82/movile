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
    public class RecepcionModel
    {
        public string nombre { get; set; }
        public DateTime horaRecepcion { get; set; }
        public UbicacionModel ubicacion { get; set; }
    }
}