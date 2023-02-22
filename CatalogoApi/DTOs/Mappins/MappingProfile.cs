using AutoMapper;
using CatalogoApi.Models;

namespace CatalogoApi.DTOs.Mappins
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();

        }
    }
}
