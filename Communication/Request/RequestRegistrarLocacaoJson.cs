using aluguel_de_imoveis.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Communication.Request
{
    public class RequestRegistrarLocacaoJson
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public Guid UsuarioId { get; set; }
        public Guid ImovelId { get; set; }
    }
}
