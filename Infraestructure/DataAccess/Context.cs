using aluguel_de_imoveis.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace aluguel_de_imoveis.Infraestructure.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.UsuarioId);

            modelBuilder.Entity<Imovel>()
                .HasOne(i => i.Proprietario)
                .WithMany()
                .HasForeignKey(i => i.ProprietarioId);

            modelBuilder.Entity<Imovel>()
                .HasOne(i => i.Endereco)
                .WithOne(e => e.Imovel)
                .HasForeignKey<Endereco>(e => e.ImovelId);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Cliente)
                .WithMany()
                .HasForeignKey(l => l.ClienteId);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Imovel)
                .WithMany(i => i.Locacoes)
                .HasForeignKey(l => l.ImovelId)
                .OnDelete(DeleteBehavior.NoAction); ;

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
