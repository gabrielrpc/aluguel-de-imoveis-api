using aluguel_de_imoveis.Infraestructure.DataAccess;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;

namespace aluguel_de_imoveis.Repository
{
    public class ImovelRepository : IImovelRepository
    {
        private readonly Context _context;

        public ImovelRepository(Context context)
        {
            _context = context;
        }

        public async Task<Imovel> CadastrarImovel(Imovel imovel)
        {
            _context.Imoveis.Add(imovel);
            await _context.SaveChangesAsync();
            return imovel;
        }
    }
}
