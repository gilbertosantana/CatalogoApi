using Microsoft.EntityFrameworkCore;
using CatalogoApi.Models;
namespace CatalogoApi.Context;

public class CatalogoApiContext : DbContext
{
    public CatalogoApiContext(DbContextOptions<CatalogoApiContext> options)
        : base(options) 
    {

    }

    public DbSet<Produto>? Produtos { get; set; }
    public DbSet<Categoria>? Categorias { get; set; }
}
