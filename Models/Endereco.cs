using System.ComponentModel.DataAnnotations.Schema;

namespace aluguel_de_imoveis.Models
{
    public class Endereco
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column(TypeName = "varchar(100)")]
        public string Logradouro { get; set; } = string.Empty;
        public int Numero { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Bairro { get; set; } = string.Empty;

        [Column(TypeName = "varchar(50)")]
        public string Cidade { get; set; } = string.Empty;

        [Column(TypeName = "varchar(2)")]
        public string Uf { get; set; } = string.Empty;

        [Column(TypeName = "varchar(8)")]
        public string Cep { get; set; } = string.Empty;

        public Guid ImovelId { get; set; }
        public Imovel Imovel { get; set; } = null!;
    }
}
