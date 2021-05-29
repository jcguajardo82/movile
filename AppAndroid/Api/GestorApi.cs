using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppAndroid.Helpers;
using AppAndroid.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AppAndroid.Api
{
   public class GestorApi : IGestorApi
    {
        private readonly IApis gestorAPI;
        public GestorApi()
        {
            gestorAPI = RestService.For<IApis>(Constants.GestorUrl);
        }
        public async Task<string> Login(LoginModel userLogin)
        {
            var encriptado = await gestorAPI.PasswordEncriptadoApi(userLogin);
            var password = encriptado.Replace("\"", "").Replace("\\", "");      
            userLogin.Pass = password;
            var exito = await gestorAPI.ActiveDirectoryApi(userLogin);
            return exito;
        }


    }
}