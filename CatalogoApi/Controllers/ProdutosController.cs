﻿using CatalogoApi.Context;
using CatalogoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos?.ToList();
            if (produtos is null)
            {
                return NotFound("Produtos não encontrados...");
            }
            return produtos;
        }

        [HttpGet("{id:int}")]
        public ActionResult<Produto> Get(int? id) 
        {
            var produto = _context.Produtos?.FirstOrDefault(x => x.ProdutoId == id);

            if(produto == null)
            {
                return NotFound("Produto não encontrado...");
            }

            return produto;
        }
    }
}
