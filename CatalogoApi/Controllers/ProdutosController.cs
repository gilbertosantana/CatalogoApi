using CatalogoApi.Context;
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
    }
}
