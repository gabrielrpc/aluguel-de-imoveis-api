using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Exceptions;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Repository.Interfaces;
using aluguel_de_imoveis.Security;
using aluguel_de_imoveis.Services.Interfaces;
using aluguel_de_imoveis.Services.Validations;

namespace aluguel_de_imoveis.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly JwtTokenGenerator _tokenGenerator;

        public UsuarioService(IUsuarioRepository usuarioRepository, JwtTokenGenerator tokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<Usuario> CadastrarUsuario(RequestUsuarioJson request)
        {
            request.Telefone = new string(request.Telefone.Where(char.IsDigit).ToArray());
            request.Cpf = new string(request.Cpf.Where(char.IsDigit).ToArray());

            var validator = new UsuarioValidations();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }

            var emailExistente = await _usuarioRepository.VerificarEmailExistente(request.Email);

            if (emailExistente)
            {
                throw new ErrorOnValidationException(new List<string> { "E-mail já registrado na plataforma." });
            }

            var novoUsuario = new Usuario
            {
                Nome = request.Nome,
                Email = request.Email,
                Cpf = request.Cpf,
                Telefone = request.Telefone,
                Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            };

            return await _usuarioRepository.CadastrarUsuario(novoUsuario);
        }


        public async Task<ResponseLoginJson> Login(RequestLoginJson request)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorEmail(request.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            {
                throw new ErrorOnValidationException(new List<string> { "E-mail ou senha inválidos." });
            }

            var token = _tokenGenerator.GerarToken(usuario);

            return new ResponseLoginJson
            {
                Token = token,
                Nome = usuario.Nome,
            };
        }
    }
}
