using CatalogoApi.Models;
using CatalogoApi.Pagination;

namespace CatalogoApi.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParameters);
        
        IEnumerable<Produto> GetProdutosPorPreco();
    }
}
