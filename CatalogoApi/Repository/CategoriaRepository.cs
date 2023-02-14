using CatalogoApi.Context;
using CatalogoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CatalogoApiContext context) : base(context)
        {
        }        
        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(p => p.Produtos);
        }
    }
}
