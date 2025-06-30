using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Utils.Enums;

namespace aluguel_de_imoveis.Repository.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao> RegistrarLocacao(Locacao locacao);

        Task<Locacao?> ObterLocacaoPorImovelIdEUsuarioId(Guid ImovelId, Guid UsuarioId);

        Task<List<Locacao>> ListarLocacoesPorUsuarioId(Guid usuarioId, StatusLocacao status);
    }
}
