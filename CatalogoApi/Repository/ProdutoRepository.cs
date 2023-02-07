using CatalogoApi.Context;
using CatalogoApi.Models;

namespace CatalogoApi.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CatalogoApiContext context) : base(context)
        {
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }
    }
}
