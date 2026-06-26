using System.Text.Json.Serialization;
using FuscaFilmes.API.Contexto;
using FuscaFilmes.API.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DBContexto>(options =>
    options.UseSqlite(builder.Configuration["ConnectionStrings:FuscaFilmesStr"])
           .LogTo(Console.WriteLine)
);

/* using (var context = new Contexto())
{
    context.Database.EnsureCreated();
} */

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/CreateDB", (DBContexto contexto) =>
{    
   contexto.Database.EnsureCreated();   
})
.WithOpenApi();

app.UseHttpsRedirection();

app.MapGet("/diretores", (DBContexto contexto) =>
{    
    return contexto.Diretores.Include(d => d.Filmes)
    .ToList();
   
})
.WithOpenApi();

app.MapGet("/diretores/{id}", (int id, DBContexto contexto) =>
{    
    return contexto.Diretores
    .Where(d => d.Id == id)
    .Include(d => d.Filmes)
    .ToList();
   
})
.WithOpenApi();

app.MapPost("/diretores", (DBContexto contexto,Diretor diretor) =>
{    
    contexto.Diretores.Add(diretor);
    contexto.SaveChanges();
})
.WithOpenApi();

app.MapPut("/diretores/{diretorId}", (DBContexto contexto,int diretorId, Diretor diretorNovo) =>
{
    var diretor = contexto.Diretores.Find(diretorId);
    if (diretor != null){
        diretor.Nome = diretorNovo.Nome;
        if (diretorNovo.Filmes.Count > 0 )
        {
            diretor.Filmes.Clear();
            foreach (var filme in diretorNovo.Filmes)
            {
                diretor.Filmes.Add(filme);
            }
        } 
    }            
   contexto.SaveChanges();   
})
.WithOpenApi();

app.MapDelete("/diretores/{diretorId}", (DBContexto contexto, int diretorId) =>
{
    
    var diretor = contexto.Diretores.Find(diretorId);
    if (diretor != null)
        contexto.Diretores.Remove(diretor);
    
    contexto.SaveChanges();        
})
.WithOpenApi();


app.MapGet("/filmes", (DBContexto contexto) =>
{    
    return contexto.Filmes.Include(f => f.Diretor)
    .ToList();
   
})
.WithOpenApi();

app.MapGet("/filmes/{id}", (int id, DBContexto contexto) =>
{    
    return contexto.Filmes
    .Where(f => f.Id == id)
    .Include(f => f.Diretor)
    .ToList();
   
})
.WithOpenApi();

app.MapGet("/filmesbyAno", (DBContexto contexto) =>
{    
    return contexto.Filmes
            .Include(f => f.Diretor)
            .OrderByDescending(f => f.Ano)
            .ThenBy(f => f.Titulo)
            .ToList();   
})
.WithOpenApi();


app.MapGet("/filmesEFFunctions/byName/{titulo}", (string titulo, DBContexto contexto) =>
{    
    return contexto.Filmes
         .Where(f => 
            EF.Functions.Like(f.Titulo, $"%{titulo}%"))
            .Include(f => f.Diretor)
            .ToList();   
})
.WithOpenApi();

app.MapGet("/filmesLinQ/byName/{titulo}", (string titulo, DBContexto contexto) =>
{    
    return contexto.Filmes
    .Where(f => f.Titulo.Contains(titulo))
    .Include(f => f.Diretor)
    .ToList();       
})
.WithOpenApi();

app.Run();


