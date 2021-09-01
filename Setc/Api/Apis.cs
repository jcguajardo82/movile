using Microsoft.AppCenter.Crashes;
using Refit;
using Setc.Helpers;
using Setc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
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
            var response = new List<CuestionarioModel>();
            try
            {
                response = await setcApi.GetCuestionario(usuario);
                return response;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string> {
                        { "Api", "GetCuestionario" },
                    };
                Crashes.TrackError(exception, properties);
                return response;
            }
        }

        public async Task<List<OrdenModel>> GetOrders(string usuario, int pagina)
        {
            var ordenes = new List<OrdenModel>();
            try
            {
                ordenes = await setcApi.GetOrders(usuario, pagina);
                return ordenes.OrderBy(o => o.deliveryDate).ToList();

            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string> {
                        { "Api", "GetOrders" },
                    };
                Crashes.TrackError(exception, properties);
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
            catch (HttpRequestException exception)
            {
                var properties = new Dictionary<string, string> {
                        { "Api", "Login" },
                    };
                Crashes.TrackError(exception, properties);
                exito = exception.Message;
            }
            return exito;
        }

        public async Task<string> SendCuestionario(List<RespuestaModel> respuestas)
        {
            string response;
            try
            {
                response = await setcApi.SendCuestionario(respuestas);
            }
            catch (HttpRequestException e)
            {
                var properties = new Dictionary<string, string> {
                        { "Api", "SendCuestionario" },
                    };
                Crashes.TrackError(e, properties);
                response = e.Message;
            }
            return response;
        }

        public async Task<string> SendRecepcion(RecepcionModel recepcion)
        {
            string response;
            try
            {
                string detalle = JsonSerializer.Serialize(recepcion);
                response = await setcApi.SendRecepcion(recepcion);
            }
            catch (HttpRequestException e)
            {
                var properties = new Dictionary<string, string> {
                        { "Api", "SendRecepcion" },
                    };
                Crashes.TrackError(e, properties);
                response = e.Message;
            }
            return response;
        }
    }
}