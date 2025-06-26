using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Models
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "varchar(100)")]
        public string Nome { get; set; } = string.Empty;

        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; } = string.Empty;

        [Column(TypeName = "varchar(255)")]
        public string Senha { get; set; } = string.Empty;

        public int Tipo { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
