using ApiBibliotecaSimples.Domain.Entities;
using AutoMapper;

namespace ApiBlibliotecaSimples.DTOs;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Livro, DtoResponseLivro>().ReverseMap();
        CreateMap<Livro, DtoAtualizarLivro>().ReverseMap();
        CreateMap<Livro, DtoCriarLivro>().ReverseMap();
        CreateMap<Livro, DtoLivroResumo>().ReverseMap();
        CreateMap<Categoria, DtoCategoria>().ReverseMap();
        CreateMap<Categoria, DtoCategoriaComLivros>().ReverseMap();
        CreateMap<Categoria, DtoResponseCategoria>().ReverseMap();
        CreateMap<Autor, DtoAutor>().ReverseMap();
        CreateMap<Autor, DtoResponseAutor>().ReverseMap();
        CreateMap<Autor, DtoAutorComLivros>().ReverseMap();
    }
}
