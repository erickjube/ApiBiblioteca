using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.DtosAutor;
using ApiBiblioteca.DTOs.DtosCategoria;
using ApiBiblioteca.DTOs.DtosCliente;
using ApiBiblioteca.DTOs.DtosEmprestimo;
using ApiBiblioteca.DTOs.DtosExemplar;
using ApiBiblioteca.DTOs.DtosItemEmprestimo;
using ApiBiblioteca.DTOs.DtosItemVenda;
using ApiBiblioteca.DTOs.DtosLivro;
using ApiBiblioteca.DTOs.DtosMulta;
using ApiBiblioteca.DTOs.DtosVenda;
using ApiBiblioteca.Entities;
using AutoMapper;

namespace ApiBiblioteca.DTOs;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<Livro, DtoResponseLivro>().ReverseMap();
        CreateMap<Livro, DtoAtualizarLivro>().ReverseMap();
        CreateMap<Livro, DtoCriarLivro>().ReverseMap();
        CreateMap<Livro, DtoLivroResumo>().ReverseMap();
        CreateMap<Livro, DtoLivroComExemplares>().ReverseMap();

        CreateMap<Categoria, DtoCategoria>().ReverseMap();
        CreateMap<Categoria, DtoCategoriaComLivros>().ReverseMap();
        CreateMap<Categoria, DtoResponseCategoria>().ReverseMap();

        CreateMap<Autor, DtoAutor>().ReverseMap();
        CreateMap<Autor, DtoResponseAutor>().ReverseMap();
        CreateMap<Autor, DtoAutorComLivros>().ReverseMap();

        CreateMap<ExemplarLivro, DtoAtualizarExemplar>().ReverseMap();
        CreateMap<ExemplarLivro, DtoCriarExemplar>().ReverseMap();
        CreateMap<ExemplarLivro, DtoResponseExemplar>().ReverseMap();
        CreateMap<ExemplarLivro, DtoExemplarResumo>().ReverseMap();

        CreateMap<Emprestimo, DtoEmprestimoResumo>().ReverseMap();
        CreateMap<Emprestimo, DtoResponseEmprestimo>().ReverseMap();
        CreateMap<Emprestimo, DtoResponseEmprestimoComItens>().ReverseMap();   
        CreateMap<Emprestimo, DtoResponseEmprestimoComMultas>().ReverseMap();

        CreateMap<ItemEmprestimo, DtoResponseItemEmprestimo>().ReverseMap();
        CreateMap<ItemEmprestimo, DtoDevolverItemRequest>().ReverseMap();

        CreateMap<Venda, DtoVendaResumo>().ReverseMap();
        CreateMap<Venda, DtoCriarVenda>().ReverseMap();
        CreateMap<Venda, DtoResponseVenda>().ReverseMap();
        CreateMap<Venda, DtoResponseVendaComItens>().ReverseMap();

        CreateMap<ItemVenda, DtoResponseItemVenda>().ReverseMap();

        CreateMap<Cliente, DtoCriarCliente>().ReverseMap();
        CreateMap<Cliente, DtoAtualizarCliente>().ReverseMap();
        CreateMap<Cliente, DtoResponseCliente>().ReverseMap();
        CreateMap<Cliente, DtoClienteComEmprestimos>().ReverseMap();
        CreateMap<Cliente, DtoClienteComVendas>().ReverseMap();

        CreateMap<Multa, DtoResponseMulta>().ReverseMap();
    }
}
