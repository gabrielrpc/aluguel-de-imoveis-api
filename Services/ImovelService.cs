using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Exceptions;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
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

                if (result.Errors.Count > 1)
                {
                    var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                    throw new ErrorOnValidationException(errorMessages);
                }

                var errorMessage = result.Errors.First().ErrorMessage;
                throw new BadRequestException(errorMessage);
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

        public async Task<List<ResponseImovelJson>> ListarImoveisDisponiveis(RequestListarImoveisDisponiveis request)
        {
            var imoveis = await _imoveloRepository.ListarImoveisDisponiveis(request);

            if ((imoveis == null || imoveis.Count == 0) && (request.ValorMax.HasValue || request.ValorMin.HasValue || request.Tipo.HasValue))
            {
                throw new NotFoundException("Nenhum imóvel foi encontrado para os critérios informados.");
            }

            return imoveis.Select(imovel => new ResponseImovelJson
            {
                Id = imovel.Id,
                Titulo = imovel.Titulo,
                Descricao = imovel.Descricao,
                ValorAluguel = imovel.ValorAluguel,
                Tipo = imovel.Tipo,
                Disponivel = imovel.Disponivel,
                UsuarioId = imovel.Usuario.Id,
                Endereco = new ResponseEnderecoJson
                {
                    Logradouro = imovel.Endereco.Logradouro,
                    Numero = imovel.Endereco.Numero,
                    Bairro = imovel.Endereco.Bairro,
                    Cidade = imovel.Endereco.Cidade,
                    Uf = imovel.Endereco.Uf,
                    Cep = imovel.Endereco.Cep
                },
                Usuario = new ResponseUsuarioJson
                {
                    Nome = imovel.Usuario.Nome,
                    Email = imovel.Usuario.Email,
                    Telefone = imovel.Usuario.Telefone,
                }
            }).ToList();
        }

        public async Task<ResponseImovelJson> ObterImovelPorId(RequestObterImovelJson request)
        {
            var imovel = await _imoveloRepository.ObterImovelPorId(request.ImovelId);

            if (imovel == null)
            {
                throw new NotFoundException("Imóvel não foi encontrado.");
            }

            var response = new ResponseImovelJson
            {
                Id = imovel.Id,
                Titulo = imovel.Titulo,
                Descricao = imovel.Descricao,
                ValorAluguel = imovel.ValorAluguel,
                Tipo = imovel.Tipo,
                Disponivel = imovel.Disponivel,
                Endereco = new ResponseEnderecoJson
                {
                    Logradouro = imovel.Endereco.Logradouro,
                    Numero = imovel.Endereco.Numero,
                    Bairro = imovel.Endereco.Bairro,
                    Cidade = imovel.Endereco.Cidade,
                    Uf = imovel.Endereco.Uf,
                    Cep = imovel.Endereco.Cep
                }
            };

            return response;
        }

        public async Task<bool> AtualizarImovel(Guid ImovelId, RequestImovelJson request)
        {
            request.Endereco.Cep = new string(request.Endereco.Cep.Where(char.IsDigit).ToArray());

            var validator = new ImovelValidations();

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

            var imovel = await _imoveloRepository.ObterImovelPorId(ImovelId);

            if (imovel == null)
            {
                throw new NotFoundException("Imóvel não foi encontrado.");
            }

            imovel.Titulo = request.Titulo;
            imovel.Descricao = request.Descricao;
            imovel.ValorAluguel = request.ValorAluguel;
            imovel.Tipo = request.Tipo;
            if (request.Endereco != null)
            {
                imovel.Endereco.Logradouro = request.Endereco.Logradouro;
                imovel.Endereco.Numero = request.Endereco.Numero;
                imovel.Endereco.Bairro = request.Endereco.Bairro;
                imovel.Endereco.Cidade = request.Endereco.Cidade;
                imovel.Endereco.Uf = request.Endereco.Uf;
                imovel.Endereco.Cep = request.Endereco.Cep;
            }

            return await _imoveloRepository.AtualizarImovel(imovel);
        }

        public async Task<bool> DeletarImovel(Guid imovelId)
        {
            var imovel = await _imoveloRepository.ObterImovelPorId(imovelId);
            if (imovel == null)
            {
                throw new NotFoundException("Imóvel não foi encontrado.");
            }
            return await _imoveloRepository.DeletarImovel(imovel.Id);
        }
    }
}
