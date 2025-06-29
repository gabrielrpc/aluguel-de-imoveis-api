using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Exceptions;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Services.Interfaces;
using aluguel_de_imoveis.Services.Validations;
using aluguel_de_imoveis.Utils.Enums;

namespace aluguel_de_imoveis.Services
{
    public class ImovelService : IImovelService
    {
        private readonly IImovelRepository _imoveloRepository;

        public ImovelService(IImovelRepository imovelRepository)
        {
            _imoveloRepository = imovelRepository;
        }

        public async Task<Imovel> CadastrarImovel(RequestImovelJson request)
        {
            request.Endereco.Cep = new string(request.Endereco.Cep.Where(char.IsDigit).ToArray());

            var validator = new ImovelValidations();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

            var novoImovel = new Imovel
            {
                Titulo = request.Titulo,
                Descricao = request.Descricao,
                ValorAluguel = request.ValorAluguel,
                Disponivel = request.Disponivel,
                Tipo = (TipoImovel)request.Tipo,
                Endereco = new Endereco
                {
                    Logradouro = request.Endereco.Logradouro,
                    Numero = request.Endereco.Numero,
                    Bairro = request.Endereco.Bairro,
                    Cidade = request.Endereco.Cidade,
                    Uf = request.Endereco.Uf,
                    Cep = request.Endereco.Cep
                },
                UsuarioId = request.UsuarioId
            };


            return await _imoveloRepository.CadastrarImovel(novoImovel);
        }

        public async Task<List<Imovel>> ListarImoveisDisponiveis(RequestListarImoveisDisponiveis request)
        {
            var imoveis = await _imoveloRepository.ListarImoveisDisponiveis(request);

            if (imoveis == null || imoveis.Count == 0)
            {
                throw new NotFoundException("Nenhum imóvel foi encontrado para os critérios informados.");
            }

            return imoveis;
        }
    }
}
