using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> CadastrarUsuario(RequestUsuarioJson request);

        Task<ResponseLoginJson> Login(RequestLoginJson request);
    }
}
