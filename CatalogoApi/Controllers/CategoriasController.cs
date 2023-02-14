using CatalogoApi.Context;
using CatalogoApi.Models;
using CatalogoApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        public readonly IUnitOfWork _uow;

        public CategoriasController(IUnitOfWork context)
        {
            _uow = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos()
        {
            try
            {
                return _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
                //return _context.Categorias!.Include(p => p.Produtos).Where(c => c.CategoriaId < 5).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                var categorias = _uow.CategoriaRepository.Get().ToList();

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
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);

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
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados inválidos");
            }

            _uow.CategoriaRepository.Add(categoria);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }

            _uow.CategoriaRepository.Update(categoria);
            _uow.Commit();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria == null)
            {
                return NotFound($"Categoria com o id={id} não encontrada...");
            }
            _uow.CategoriaRepository.Delete(categoria);
            _uow.Commit();

            return Ok(categoria);
        }

    }
}
