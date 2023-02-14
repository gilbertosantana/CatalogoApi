using CatalogoApi.Context;
using CatalogoApi.Models;
using CatalogoApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        public readonly IUnitOfWork _uow;

        public ProdutosController(IUnitOfWork context)
        {
            _uow = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _uow.ProdutoRepository.Get().ToList();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPrecos()
        {
            return _uow.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int? id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado...");
            }

            return produto;
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            _uow.ProdutoRepository.Add(produto);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId, produto });
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }
            _uow.ProdutoRepository.Update(produto);
            _uow.Commit();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();

            return Ok(produto);
        }
    }
}
