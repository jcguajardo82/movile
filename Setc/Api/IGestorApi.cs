using Refit;
using Setc.Models;
using System.Threading.Tasks;

namespace Setc.Api
{
    public interface IGestorApi
    {
        [Post("/PasswordEncriptado")]
        Task<string> PasswordEncriptadoApi([Body] LoginModel userLogin);
        [Post("/UsuarioValidoAD")]
        Task<string> ActiveDirectoryApi([Body] LoginModel userLogin);
    }
}