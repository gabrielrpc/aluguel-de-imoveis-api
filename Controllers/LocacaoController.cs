﻿using aluguel_de_imoveis.Communication.Request;
using aluguel_de_imoveis.Communication.Response;
using aluguel_de_imoveis.Services;
using aluguel_de_imoveis.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aluguel_de_imoveis.Controllers
{
    [Route("locacao")]
    [ApiController]
    [Authorize]
    public class LocacaoController : ControllerBase
    {
        private readonly ILocacaoService _locacaoService;

        public LocacaoController(ILocacaoService locacaoService)
        {
            _locacaoService = locacaoService;
        }

        [HttpPost("registrar-locacao")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessegeJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorMessegeJson), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> RegistrarLocacao(RequestRegistrarLocacaoJson request)
        {
            await _locacaoService.RegistrarLocacao(request);
            return Ok();
        }

        [HttpGet("listar-locacoes-ativas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ResponseErrorMessegeJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ListarLocacoesAtivas([FromQuery] RequestListarLocacoesAtivas request)
        {
            var response = await _locacaoService.ListarLocacoesAtivas(request);
            return Ok(response);
        }
    }
}
