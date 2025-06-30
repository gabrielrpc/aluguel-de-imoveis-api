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

        for (int i = 1; i <= 10; i++)
        {
            var usuarioId = Guid.NewGuid();
            var imovelId = Guid.NewGuid();
            var enderecoId = Guid.NewGuid();
            var locacaoId = Guid.NewGuid();

            usuarios.Add(new Usuario
            {
                Id = usuarioId,
                Nome = $"Usuário {i}",
                Email = $"usuario{i}@email.com",
                Cpf = $"12345678{i:000}",
                Telefone = $"21980001{i:000}",
                Senha = BCrypt.Net.BCrypt.HashPassword("123456")
            });

            imoveis.Add(new Imovel
            {
                Id = imovelId,
                Titulo = $"Imóvel {i}",
                Descricao = $"Descrição do imóvel {i}",
                ValorAluguel = 1000 + (i * 100),
                Disponivel = i % 2 == 0,
                UsuarioId = usuarioId,
                Tipo = (TipoImovel)(i % 3)
            });

            enderecos.Add(new Endereco
            {
                Id = enderecoId,
                Logradouro = $"Rua {i}",
                Numero = 100 + i,
                Bairro = $"Bairro {i}",
                Cidade = $"Cidade {i}",
                Uf = "SP",
                Cep = $"01010{i:000}",
                ImovelId = imovelId
            });

            locacoes.Add(new Locacao
            {
                Id = locacaoId,
                ImovelId = imovelId,
                UsuarioId = usuarioId,
                ValorFinal = 1000 + (i * 100),
                DataInicio = DateTime.UtcNow.AddDays(-i),
                DataFim = DateTime.UtcNow.AddYears(1).AddDays(-i),
                Status = (StatusLocacao)(i % 3)
            });
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
