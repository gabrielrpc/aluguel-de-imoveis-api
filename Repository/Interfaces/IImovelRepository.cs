using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Repository.Interfaces
{
    public interface IImovelRepository
    {
        Task<Imovel> CadastrarImovel(Imovel imovel);

        Task<List<Imovel>> ListarImoveisDisponiveis(RequestListarImoveisDisponiveis request);

        Task<Imovel?> ObterImovelPorId(Guid imovelId);

        Task<bool> AtualizarImovel(Imovel imovel);

        Task<bool> DeletarImovel(Guid imovelId);
    }
}
