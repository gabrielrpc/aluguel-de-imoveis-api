using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Infraestructure.DataAccess;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Utils.Enums;
using Microsoft.EntityFrameworkCore;

namespace aluguel_de_imoveis.Repository
{
    public class ImovelRepository : IImovelRepository
    {
        private readonly Context _context;

        private const int PAGE_SIZE = 10;

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

        public async Task<List<Imovel>> ListarImoveisDisponiveis(RequestListarImoveisDisponiveis request)
        {
            var skip = request.Pagina > 0 ? (request.Pagina - 1) * PAGE_SIZE : 0;

            var query = _context.Imoveis.Include(imovel => imovel.Endereco).Include(imovel => imovel.Usuario).OrderByDescending(imovel => imovel.DataCriacao).AsQueryable();

            if (request.ValorMin.HasValue && request.ValorMax.HasValue)
            {
                query = query.Where(imovel => imovel.ValorAluguel >= request.ValorMin.Value && imovel.ValorAluguel <= request.ValorMax.Value);
            }
            else if (request.ValorMin.HasValue)
            {
                query = query.Where(imovel => imovel.ValorAluguel >= request.ValorMin.Value);
            }
            else if (request.ValorMax.HasValue)
            {
                query = query.Where(imovel => imovel.ValorAluguel <= request.ValorMax.Value);
            }

            if (request.Tipo.HasValue)
            {
                query = query.Where(imovel => imovel.Tipo == request.Tipo.Value);
            }

            return await query.Where(imovel => imovel.Disponivel).Skip(skip).Take(PAGE_SIZE).ToListAsync();
        }

        public async Task<Imovel?> ObterImovelPorId(Guid imovelId)
        {
            return await _context.Imoveis.Include(imovel => imovel.Endereco).FirstOrDefaultAsync(imovel => imovel.Id == imovelId);
        }

        public async Task<bool> AtualizarImovel(Imovel imovel)
        {
            _context.Imoveis.Update(imovel);
            var registrosAfetados = await _context.SaveChangesAsync();
            return registrosAfetados > 0;
        }

        public async Task<bool> DeletarImovel(Guid imovelId)
        {
            var registrosAfetados = await _context.Imoveis.Where(imovel => imovel.Id == imovelId).ExecuteDeleteAsync();
            return registrosAfetados > 0;
        }
    }
}
