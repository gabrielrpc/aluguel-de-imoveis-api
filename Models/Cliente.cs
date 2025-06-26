using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Models
{
    public class Cliente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UsuarioId { get; set; }

        [Column(TypeName = "varchar(11)")]
        public string Cpf { get; set; } = string.Empty;

        [Column(TypeName = "varchar(11)")]
        public string Telefone { get; set; } = string.Empty;
        public Usuario Usuario { get; set; } = null!;
    }
}
