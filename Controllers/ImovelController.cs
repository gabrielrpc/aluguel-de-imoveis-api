using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Services;
using aluguel_de_imoveis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aluguel_de_imoveis.Controllers
{
    [Route("imovel")]
    [ApiController]
    [Authorize]
    public class ImovelController : ControllerBase
    {
        private readonly IImovelService _imovelService;

        public ImovelController(IImovelService imovelService)
        {
            _imovelService = imovelService;
        }

        [HttpPost("cadastrar")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessegesJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarImovel(RequestImovelJson request)
        {
            await _imovelService.CadastrarImovel(request);

            return Created();
        }
    }
}
