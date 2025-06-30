using aluguel_de_imoveis.Utils.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Models
{
    public class Locacao
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorFinal { get; set; }

        [Column(TypeName = "int")]
        public StatusLocacao Status { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public Guid ImovelId { get; set; }
        public Imovel Imovel { get; set; } = null!;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
