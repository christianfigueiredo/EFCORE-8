using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.Contexto
{
    public class DBContexto(DbContextOptions<DBContexto> options) : DbContext(options)
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }      
        
    }
}