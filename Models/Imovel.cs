using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Models
{
    public class Imovel
    { 
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "varchar(100)")]
        public string Titulo { get; set; } = string.Empty;

        [Column(TypeName = "text")]
        public string Descricao { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorAluguel { get; set; }
        public bool Disponivel { get; set; } = true;

        public Endereco Endereco { get; set; } = null!;

        public Guid ProprietarioId { get; set; }
        public Usuario Proprietario { get; set; } = null!;

        public ICollection<Locacao> Locacoes { get; set; } = new List<Locacao>();
    }
}
