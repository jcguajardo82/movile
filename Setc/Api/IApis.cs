﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Setc.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setc.Api
{
    public interface IApis
    {
        //Task<string> Login(LoginModel userLogin);
        //Task<string> ChangeEstatusOrder(int orden, int estatus);
        //Task<string> GetCuestionario(string usuario);
        Task<List<OrdenModel>> GetOrders(string usuario, int pagina);
        //Task<string> SendCuestionario();
    }
}