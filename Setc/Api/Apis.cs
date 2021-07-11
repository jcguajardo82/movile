using Refit;
using Setc.Helpers;
using Setc.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Setc.Api
{
    public class Apis : IApis
    {
        private readonly IGestorApi gestorAPI;
        private readonly ISetcApi setcApi;
        public Apis()
        {
            gestorAPI = RestService.For<IGestorApi>(Constants.GestorUrl);
            setcApi = RestService.For<ISetcApi>(Constants.SectUrl);
        }

        public async Task<EstatusOrder> ChangeEstatusOrder(int orden, string estatus, int id)
        {
            var response = await setcApi.ChangeEstatusOrder(orden, estatus, id);
            return response;
        }

        public async Task<List<CuestionarioModel>> GetCuestionario(string usuario)
        {
            var response = await setcApi.GetCuestionario(usuario);
            return response;
        }

        public async Task<List<OrdenModel>> GetOrders(string usuario, int pagina)
        {
            var ordenes = new List<OrdenModel>();
            try
            {
                ordenes = await setcApi.GetOrders(usuario, pagina);
                return ordenes;
            }
            catch
            {
                return ordenes;
            }
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
            catch (HttpRequestException e)
            {
                exito = e.Message;
            }
            return exito;
        }

        //public async Task<string> SendCuestionario()
        //{
        //    var response = await setcApi.SendCuestionario();
        //    return response;
        //}
    }
}