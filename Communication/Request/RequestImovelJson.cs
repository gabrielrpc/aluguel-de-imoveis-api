using aluguel_de_imoveis.Utils.Enums;

namespace aluguel_de_imoveis.Communication.Request
{
    public class RequestImovelJson
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal ValorAluguel { get; set; }
        public bool Disponivel { get; set; }
        public TipoImovel Tipo { get; set; }
        public Guid UsuarioId { get; set; }

        public RequestEnderecoJson Endereco { get; set; } = null!;
    }
}
