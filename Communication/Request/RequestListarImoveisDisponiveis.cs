using aluguel_de_imoveis.Utils.Enums;

namespace aluguel_de_imoveis.Communication.Request
{
    public class RequestListarImoveisDisponiveis
    {
        public int Pagina { get; set; }
        public decimal? ValorMin { get; set; }
        public decimal? ValorMax { get; set; }
        public TipoImovel? Tipo { get; set; }
    }
}
