using CatalogoApi.Context;
using CatalogoApi.Models;
using CatalogoApi.Pagination;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(CatalogoApiContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
        {
            return PagedList<Categoria>.ToPagedList(Get().OrderBy(on => on.Nome),
                                categoriasParameters.PageNumber,
                                categoriasParameters.PageSize);
        }

        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(p => p.Produtos);
        }
    }
}
