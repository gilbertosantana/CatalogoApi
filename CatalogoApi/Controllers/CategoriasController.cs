using AutoMapper;
using CatalogoApi.Context;
using CatalogoApi.DTOs;
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
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;


        public CategoriasController(IUnitOfWork context, IMapper mapper)
        {
            _uow = context;
            _mapper = mapper;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriaProdutos()
        {
            try
            {
                var categorias = _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

                return categoriasDto;
                //return _context.Categorias!.Include(p => p.Produtos).Where(c => c.CategoriaId < 5).AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            try
            {
                var categorias = _uow.CategoriaRepository.Get().ToList();

                if (categorias is null)
                {
                    return NotFound();
                }
                var categoriasDto = _mapper.Map<List<CategoriaDTO>>(categorias);

                return categoriasDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com id={id} não encontrada...");
                }
                var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

                return Ok(categoriaDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] CategoriaDTO categoriaDto)
        {
            if (categoriaDto is null)
            {
                return BadRequest("Dados inválidos");
            }
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uow.CategoriaRepository.Add(categoria);
            _uow.Commit();

            return new CreatedAtRouteResult("ObterCategoria",
                new { id = categoria.CategoriaId }, categoria);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest("Dados inválidos");
            }
            var categoria = _mapper.Map<Categoria>(categoriaDto);

            _uow.CategoriaRepository.Update(categoria);
            _uow.Commit();

            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uow.CategoriaRepository.GetById(c => c.CategoriaId == id);

            if (categoria == null)
            {
                return NotFound($"Categoria com o id={id} não encontrada...");
            }
            _uow.CategoriaRepository.Delete(categoria);
            _uow.Commit();

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(categoriaDto);
        }

    }
}
