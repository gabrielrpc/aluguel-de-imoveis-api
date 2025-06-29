using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Services.Interfaces
{
    public interface ILocacaoService
    {
        Task<Locacao> RegistrarLocacao(RequestRegistrarLocacaoJson request);
    }
}
