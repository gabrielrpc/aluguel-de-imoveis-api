﻿using aluguel_de_imoveis.Infraestructure.DataAccess;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Utils.Enums;
using Microsoft.EntityFrameworkCore;

namespace aluguel_de_imoveis.Repository
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly Context _context;

        public LocacaoRepository(Context context)
        {
            _context = context;
        }

        public async Task<Locacao> RegistrarLocacao(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();
            return locacao;
        }

        public async Task<Locacao?> ObterLocacaoPorImovelIdEUsuarioId(Guid ImovelId, Guid UsuarioId)
        {
            return await _context.Locacoes.FirstOrDefaultAsync(locacao => locacao.ImovelId == ImovelId && 
            locacao.UsuarioId == UsuarioId && 
            locacao.Status == StatusLocacao.Locado);
        }
    }
}
