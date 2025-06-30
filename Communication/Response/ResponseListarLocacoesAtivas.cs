using aluguel_de_imoveis.Communication.Dto;

namespace aluguel_de_imoveis.Communication.Response
{
    public class ResponseListarLocacoesAtivas
    {
        public List<LocacaoAtivaDto> Locacoes { get; set; } = new();
    }
}
