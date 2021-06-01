using Setc.Helpers;
using Setc.Models;
using Refit;
using System;
using System.Threading.Tasks;

namespace Setc.Api
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
            string exito;
            try
            {
                var encriptado = await gestorAPI.PasswordEncriptadoApi(userLogin);
                var password = encriptado.Replace("\"", "").Replace("\\", "");
                userLogin.Pass = password;
                exito = await gestorAPI.ActiveDirectoryApi(userLogin);
            }
            catch (Exception e)
            {
                exito = e.Message;
            }
            return exito;
        }


    }
}