using CatalogoApi.Context;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public readonly CatalogoApiContext _context;

        public CategoriasController(CatalogoApiContext context)
        {
            _context = context;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoriaProdutos()
        {
            return await _context.Categorias!.Include(p => p.Produtos).AsNoTracking().ToListAsync();
            //return _context.Categorias!.Include(p => p.Produtos).Where(c => c.CategoriaId < 5).AsNoTracking().ToList();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> Get()
        {
            try
            {
                var categorias = await _context.Categorias!.AsNoTracking().ToListAsync();

                if (categorias is null)
                {
                    return NotFound();
                }

                return categorias;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<Categoria>> Get(int id)
        {
            try
            {
                var categoria = await _context.Categorias!.AsNoTracking().FirstOrDefaultAsync(c => c.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com id={id} não encontrada...");
                }

                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados inválidos");
            }

            _context.Categorias!.Add(categoria);
            await _context.SaveChangesAsync();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

            _context.Entry(categoria).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Categoria>> Delete(int id)
        {
            var categoria = await _context.Categorias!.FirstOrDefaultAsync(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"Categoria com o id={id} não encontrada...");
            }
            _context.Categorias!.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

    }
}
