using aluguel_de_imoveis.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace aluguel_de_imoveis.Infraestructure.DataAccess
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Imovel>()
                .HasOne(i => i.Usuario)
                .WithMany()
                .HasForeignKey(i => i.UsuarioId);

            modelBuilder.Entity<Imovel>()
                .HasOne(i => i.Endereco)
                .WithOne(e => e.Imovel)
                .HasForeignKey<Endereco>(e => e.ImovelId);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Usuario)
                .WithMany()
                .HasForeignKey(l => l.UsuarioId);

            modelBuilder.Entity<Locacao>()
                .HasOne(l => l.Imovel)
                .WithMany(i => i.Locacoes)
                .HasForeignKey(l => l.ImovelId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }

        public override int SaveChanges()
        {
            AtualizarTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AtualizarTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AtualizarTimestamps()
        {
            var agora = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<Imovel>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DataCriacao = agora;
                    entry.Entity.DataAtualizacao = agora;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DataAtualizacao = agora;
                }
            }
        }
    }
}
