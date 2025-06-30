using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aluguel_de_imoveis.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastrar")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessegesJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarUsuario(RequestUsuarioJson request)
        {
            var response = await _usuarioService.CadastrarUsuario(request);

            return Created();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessegesJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(RequestLoginJson request)
        {
            var response = await _usuarioService.Login(request);

            return Ok(response);
        }
    }
}
