using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Exceptions;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Security;
using aluguel_de_imoveis.Services.Interfaces;
using aluguel_de_imoveis.Services.Validations;

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
                Tipo = request.Tipo,
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
    }
}
