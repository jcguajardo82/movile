using Refit;
using Setc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Setc.Api
{
    public interface ISetcApi
    {
        [Post("/api/ChangeEstatusOrder?NoOrder={orden}&Status={estatus}&UEOrder={id}")]
        Task<EstatusOrder> ChangeEstatusOrder(int orden, string estatus, int id);

        [Get("/api/GetCuestionario?Id_User={usuario}")]
        Task<List<CuestionarioModel>> GetCuestionario(string usuario);

        [Post("/api/GetOrders?Id_User={usuario}&Id_Page={pagina}")]
        Task<List<OrdenModel>> GetOrders(string usuario, int pagina);

        [Post("/api/SendCuestionario")]
        Task<string> SendCuestionario([Body] List<RespuestaModel> respuestas);

        [Post("/api/SendRecepcion")]
        Task<string> SendRecepcion([Body] RecepcionModel recepcion);
    }
}