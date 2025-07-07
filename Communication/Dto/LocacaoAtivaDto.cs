using aluguel_de_imoveis.Utils.Enums;

namespace aluguel_de_imoveis.Communication.Dto
{
    public class LocacaoAtivaDto
    {
        public Guid Id { get; set; }
        public decimal ValorFinal { get; set; }
        public int DiasEmAndamento { get; set; }
        public int DiasRestantes { get; set; }

        public string TituloImovel { get; set; } = string.Empty;
        public TipoImovel TipoImovel { get; set; }

        public string NomeProprietario { get; set; } = string.Empty;
        public string EmailProprietario { get; set; } = string.Empty;
    }
}
