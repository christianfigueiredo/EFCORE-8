using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.Contexto
{
    public class DBContexto(DbContextOptions<DBContexto> options) : DbContext(options)
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diretor>()
                .HasMany(d => d.Filmes)
                .WithOne(f => f.Diretor)
                .HasForeignKey(f => f.DiretorId)    
                .IsRequired();
        }        
    }
}