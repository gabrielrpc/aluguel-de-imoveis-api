using aluguel_de_imoveis.Communication.Dto;
using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Exceptions;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Services.Interfaces;
using aluguel_de_imoveis.Services.Validations;
using aluguel_de_imoveis.Utils.Enums;

namespace aluguel_de_imoveis.Services
{
    public class LocacaoService : ILocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly IImovelRepository _imovelRepository;

        public LocacaoService(ILocacaoRepository locacaoRepository, IImovelRepository imovelRepository)
        {
            _locacaoRepository = locacaoRepository;
            _imovelRepository = imovelRepository;

        }

        public async Task<Locacao> RegistrarLocacao(RequestRegistrarLocacaoJson request)
        {
            var validator = new LocacaoValidations();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {

                if (result.Errors.Count > 1)
                {
                    var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                    throw new ErrorOnValidationException(errorMessages);
                }

                var errorMessage = result.Errors.First().ErrorMessage;
                throw new BadRequestException(errorMessage);
            }

            var imovel = await _imovelRepository.ObterImovelPorId(request.ImovelId);

            if (imovel == null) {
                throw new NotFoundException("Imóvel não encontrado.");
            }

            if(imovel.UsuarioId == request.UsuarioId)
            {
                throw new BadRequestException("O usuário não pode alugar o próprio imóvel.");
            }

            var existeLocacao = await _locacaoRepository.ObterLocacaoPorImovelIdEUsuarioId(request.ImovelId, request.UsuarioId);

            if (existeLocacao != null)
            {
                throw new ConflictException("O imóvel já está alugado.");
            }

            var novaLocacao = new Locacao
            {
                DataInicio = request.DataInicio,
                DataFim = request.DataFim,
                ValorFinal = imovel.ValorAluguel,
                Status = StatusLocacao.Locado,
                UsuarioId = request.UsuarioId,
                ImovelId = request.ImovelId
            };

            var locacaoRegistrada = await _locacaoRepository.RegistrarLocacao(novaLocacao);

            if (locacaoRegistrada == null)
            {
                throw new BadRequestException("Erro ao registrar a locação.");
            }

            imovel.Disponivel = false;
            var imovelAtualizado = await _imovelRepository.AtualizarImovel(imovel);

            return locacaoRegistrada;
        }

        public async Task<ResponseListarLocacoesAtivas> ListarLocacoesAtivas(RequestListarLocacoesAtivas request)
        {
            var locacoesAtivas = await _locacaoRepository.ListarLocacoesPorUsuarioId(request.UsuarioId, StatusLocacao.Locado);

            var hoje = DateTime.UtcNow.Date;

            var resposta = new ResponseListarLocacoesAtivas
            {
                Locacoes = locacoesAtivas.Select(locacao => new LocacaoAtivaDto
                {
                    Id = locacao.Id,
                    ValorFinal = locacao.ValorFinal,
                    DiasEmAndamento = (hoje - locacao.DataInicio.Date).Days,
                    DiasRestantes = (locacao.DataFim.Date - hoje).Days,
                    TituloImovel = locacao.Imovel.Titulo,
                    TipoImovel = locacao.Imovel.Tipo
                }).ToList()
            };

            return resposta;
        }
    }
}
