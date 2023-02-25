using AutoMapper;
using CatalogoApi.DTOs;
using CatalogoApi.Models;
using CatalogoApi.Pagination;
using CatalogoApi.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;


        public ProdutosController(IUnitOfWork context, IMapper mapper)
        {
            _uow = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery]ProdutosParameters produtosParameters)
        {
            var produtos = _uow.ProdutoRepository.GetProdutos(produtosParameters).ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtosDto;
        }
        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPrecos()
        {
            var produtos =_uow.ProdutoRepository.GetProdutosPorPreco().ToList();
            var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDto;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<ProdutoDTO> Get(int? id)
        {
            var produto = _uow
                .ProdutoRepository
                .GetById(p => p.ProdutoId == id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado...");
            }
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return produtoDTO;
        }
        [HttpPost]
        public ActionResult Post([FromBody]ProdutoDTO produtoDto)
        {
            if (produtoDto is null)
            {
                return BadRequest();
            }
            var produto = _mapper.Map<Produto>(produtoDto);
            _uow.ProdutoRepository.Add(produto);
            _uow.Commit();

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId, produtoDTO });
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
            {
                return BadRequest();
            }
            var produto = _mapper.Map<Produto>(produtoDto);
            _uow.ProdutoRepository.Update(produto);
            _uow.Commit();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();
            
            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }
    }
}
