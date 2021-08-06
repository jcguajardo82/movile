using Setc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setc.Api
{
    public interface IApis
    {
        Task<string> Login(LoginModel userLogin);
        Task<EstatusOrder> ChangeEstatusOrder(int orden, string estatus, int id);
        Task<List<CuestionarioModel>> GetCuestionario(string usuario);
        Task<List<OrdenModel>> GetOrders(string usuario, int pagina);
        Task<string> SendCuestionario(List<RespuestaModel> respuestas);
        Task<string> SendRecepcion(RecepcionModel recepcion);
    }
}