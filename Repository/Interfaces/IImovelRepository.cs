using aluguel_de_imoveis.Models;

namespace aluguel_de_imoveis.Repository.Interfaces
{
    public interface IImovelRepository
    {
        Task<Imovel> CadastrarImovel(Imovel imovel);
    }
}
