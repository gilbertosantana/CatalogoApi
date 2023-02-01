using CatalogoApi.Context;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        public readonly CatalogoApiContext _context;

        public ProdutosController(CatalogoApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos!.ToListAsync();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        [HttpGet("{id:int}", Name ="ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int? id) 
        {
            var produto = await _context.Produtos!.FirstOrDefaultAsync(x => x.ProdutoId == id);

            if(produto == null)
            {
                return NotFound("Produto não encontrado...");
            }

            return produto;
        }
        [HttpPost]
        public async Task<ActionResult> Post(Produto produto)
        {
            if(produto is null)
            {
                return BadRequest();
            }
            _context.Produtos?.Add(produto);
            await _context.SaveChangesAsync();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId, produto});
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }
            _context.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _context.Produtos!.FirstOrDefaultAsync(x => x.ProdutoId == id);
            if(produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            _context.Produtos!.Remove(produto);
            await _context.SaveChangesAsync();

            return Ok(produto);
        }
    }
}
