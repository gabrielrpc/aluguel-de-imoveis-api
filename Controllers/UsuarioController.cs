using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aluguel_de_imoveis.Controllers
{
    [Route("[controller]/CadastrarUsuario")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseCadastrarUsuarioJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessegesJson), StatusCodes.Status400BadRequest)]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(RequestUsuarioJson request)
        {
            var response = await _usuarioService.CadastrarUsuario(request);

            return Created(string.Empty, response);
        }
    }
}
