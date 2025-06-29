using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Utils.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Communication.Response
{
    public class ResponseImovelJson
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal ValorAluguel { get; set; }
        public bool Disponivel { get; set; } = true;
        public TipoImovel Tipo { get; set; }
        public ResponseEnderecoJson Endereco { get; set; } = null!;
    }
}
