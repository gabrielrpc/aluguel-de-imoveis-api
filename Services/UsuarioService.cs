using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Exceptions;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Services.Interfaces;
using aluguel_de_imoveis.Services.Validations;
using FluentValidation.Results;

namespace aluguel_de_imoveis.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> CadastrarUsuario(RequestUsuarioJson request)
        {
            var validator = new UsuarioValidations();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

            var emailExistente = await _usuarioRepository.VerificarEmailExistente(request.Email);

            if(emailExistente)
            {
                throw new ErrorOnValidationException(new List<string> { "E-mail já registrado na plataforma." });
            }

            var novoUsuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
                Tipo = request.Tipo
            };

            return await _usuarioRepository.CadastrarUsuario(novoUsuario);
        }
    }
}
