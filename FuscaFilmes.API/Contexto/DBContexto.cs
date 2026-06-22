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

            modelBuilder.Entity<Diretor>().HasData(                
                new Diretor { Id = 2, Nome = "Christopher Nolan" },
                new Diretor { Id = 3, Nome = "Quentin Tarantino" },
                new Diretor { Id = 4, Nome = "Martin Scorsese" },
                new Diretor { Id = 5, Nome = "Alfred Hitchcock" }
            );

            modelBuilder.Entity<Filme>().HasData(
                
                new Filme { Id = 3, Titulo = "Pulp Fiction", Ano = 1994, DiretorId = 3 },
                new Filme { Id = 4, Titulo = "The Departed", Ano = 2006, DiretorId = 4 },
                new Filme { Id = 5, Titulo = "Psycho", Ano = 1960, DiretorId = 5 },
                new Filme { Id = 6, Titulo = "E.T. the Extra-Terrestrial", Ano = 1982, DiretorId = 1 },
                new Filme { Id = 7, Titulo = "Dunkirk", Ano = 2017, DiretorId = 2 },
                new Filme { Id = 8, Titulo = "Kill Bill: Vol. 1", Ano = 2003, DiretorId = 3 },
                new Filme { Id = 9, Titulo = "Goodfellas", Ano = 1990, DiretorId = 4 },
                new Filme { Id = 10, Titulo = "Rear Window", Ano = 1954, DiretorId = 5 }
            );
        }        
    }
}