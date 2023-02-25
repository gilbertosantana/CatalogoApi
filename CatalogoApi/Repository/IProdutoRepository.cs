using CatalogoApi.Models;
using CatalogoApi.Pagination;

namespace CatalogoApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
