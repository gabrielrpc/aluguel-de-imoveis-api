using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Services.Interfaces
{
    public interface IImovelService
    {
        Task<Imovel> CadastrarImovel(RequestImovelJson request);

        Task<List<ResponseImovelJson>> ListarImoveisDisponiveis(RequestListarImoveisDisponiveis request);

        Task<ResponseImovelJson> ObterImovelPorId(RequestObterImovelJson request);
    }
}
