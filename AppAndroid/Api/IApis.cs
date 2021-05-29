using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppAndroid.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAndroid.Api
{
    public interface IApis
    {
        [Post("/PasswordEncriptado")]
        Task<string> PasswordEncriptadoApi([Body] LoginModel userLogin);
        [Post("/UsuarioValidoAD")]
        Task<string> ActiveDirectoryApi([Body] LoginModel userLogin);
    }
}