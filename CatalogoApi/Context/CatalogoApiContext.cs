using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CatalogoApi.Models;
namespace CatalogoApi.Context;

public class CatalogoApiContext : IdentityDbContext
{
    public CatalogoApiContext(DbContextOptions<CatalogoApiContext> options)
        : base(options) 
    {

    }

    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Produto>? Produtos { get; set; }
}
