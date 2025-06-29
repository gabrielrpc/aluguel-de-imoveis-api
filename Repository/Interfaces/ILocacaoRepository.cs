using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Repository.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao> RegistrarLocacao(Locacao locacao);

        Task<Locacao?> ObterLocacaoPorImovelIdEUsuarioId(Guid ImovelId, Guid UsuarioId);
    }
}
