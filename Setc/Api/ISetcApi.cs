using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;
using Setc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setc.Api
{
    public interface ISetcApi
    {
        [Post("/api/ChangeEstatusOrder?NoOrder={orden}&Status={estatus}")]
        Task<EstatusOrder> ChangeEstatusOrder(int orden, string estatus);

        //[Post("/api/GetCuestionario?Id_User={usuario}")]
        //Task<string> GetCuestionario(string usuario);

        [Post("/api/GetOrders?Id_User={usuario}&Id_Page={pagina}")]
        Task<List<OrdenModel>> GetOrders(string usuario, int pagina);

        //[Post("/api/SendCuestionario")]
        //Task<string> SendCuestionario();
    }
}