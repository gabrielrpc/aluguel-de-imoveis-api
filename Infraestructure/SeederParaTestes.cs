using aluguel_de_imoveis.Infraestructure.DataAccess;
using aluguel_de_imoveis.Models;
using aluguel_de_imoveis.Utils.Enums;

public static class SeederParaTestes
{
    public static async Task SeedAsync(Context context)
    {
        if (context.Usuarios.Any()) return;

        var usuarios = new List<Usuario>();
        var imoveis = new List<Imovel>();
        var enderecos = new List<Endereco>();
        var locacoes = new List<Locacao>();

        for (int i = 1; i <= 40; i++)
        {
            usuarios.Add(new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = $"Usuário {i}",
                Email = $"usuario{i}@email.com",
                Cpf = $"12345678{i:000}",
                Telefone = $"21980001{i:000}",
                Senha = BCrypt.Net.BCrypt.HashPassword("123456")
            });
        }

        for (int i = 0; i < 40; i++)
        {
            var usuarioDono = usuarios[i];
            var imovelId = Guid.NewGuid();
            var enderecoId = Guid.NewGuid();

            imoveis.Add(new Imovel
            {
                Id = imovelId,
                Titulo = $"Imóvel {i + 1}",
                Descricao = $"Casa {i + 1} aconchegante com 3 quartos, sala espaçosa, cozinha equipada e quintal amplo. Localizada em um bairro tranquilo, próxima a comércios e escolas. Ideal para quem busca conforto e praticidade no dia a dia.",
                ValorAluguel = 1000 + ((i + 1) * 100),
                Disponivel = (i + 1) % 2 == 0,
                UsuarioId = usuarioDono.Id,
                Tipo = (TipoImovel)((i + 1) % 3)
            });

            enderecos.Add(new Endereco
            {
                Id = enderecoId,
                Logradouro = $"Rua {i + 1}",
                Numero = 100 + i + 1,
                Bairro = $"Bairro {i + 1}",
                Cidade = $"Cidade {i + 1}",
                Uf = "SP",
                Cep = $"01010{i + 1:000}",
                ImovelId = imovelId
            });

            var usuarioLocatario = usuarios.FirstOrDefault(u => u.Id != usuarioDono.Id);
            if (usuarioLocatario != null)
            {
                locacoes.Add(new Locacao
                {
                    Id = Guid.NewGuid(),
                    ImovelId = imovelId,
                    UsuarioId = usuarioLocatario.Id,
                    ValorFinal = 1000 + ((i + 1) * 100),
                    DataInicio = DateTime.UtcNow.AddDays(-(i + 1)),
                    DataFim = DateTime.UtcNow.AddYears(1).AddDays(-(i + 1)),
                    Status = (StatusLocacao)((i + 1) % 3)
                });
            }
        }

        await context.Usuarios.AddRangeAsync(usuarios);
        await context.Imoveis.AddRangeAsync(imoveis);
        await context.Enderecos.AddRangeAsync(enderecos);
        await context.Locacoes.AddRangeAsync(locacoes);

        await context.SaveChangesAsync();
    }

    public static async Task RemoverAsync(Context context)
    {
        context.Locacoes.RemoveRange(context.Locacoes);
        context.Enderecos.RemoveRange(context.Enderecos);
        context.Imoveis.RemoveRange(context.Imoveis);
        context.Usuarios.RemoveRange(context.Usuarios);
        await context.SaveChangesAsync();
    }
}
