using aluguel_de_imoveis.Communication.Request;
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
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

            var imovel = await _imovelRepository.ObterImovelPorId(request.ImovelId);

            if (imovel == null) {
                throw new NotFoundException("Imóvel não encontrado.");
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

            return await _locacaoRepository.RegistrarLocacao(novaLocacao);
        }
    }
}
