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
        CreateMap<Livro, LivroResponseDto>().ReverseMap();
        CreateMap<Livro, UpdateLivroDto>().ReverseMap();
        CreateMap<Livro, CreateLivroDto>().ReverseMap();
        CreateMap<Livro, LivroResumoDto>().ReverseMap();
        CreateMap<Livro, LivroComExemplaresDto>().ReverseMap();

        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Categoria, CategoriaComLivrosDto>().ReverseMap();
        CreateMap<Categoria, CategoriaResponseDto>().ReverseMap();

        CreateMap<Autor, AutorDto>().ReverseMap();
        CreateMap<Autor, AutorResponseDto>().ReverseMap();
        CreateMap<Autor, AutorComLivrosDto>().ReverseMap();

        CreateMap<ExemplarLivro, UpdateExemplarDto>().ReverseMap();
        CreateMap<ExemplarLivro, CreateExemplarDto>().ReverseMap();
        CreateMap<ExemplarLivro, ExemplarResponseDto>().ReverseMap();
        CreateMap<ExemplarLivro, ExemplarResumoDto>().ReverseMap();

        CreateMap<Emprestimo, EmprestimoResumoDto>().ReverseMap();
        CreateMap<Emprestimo, EmprestimoResponseDto>().ReverseMap();
        CreateMap<Emprestimo, EmprestimoComItensDto>().ReverseMap();   
        CreateMap<Emprestimo, EmprestimoComMultasDto>().ReverseMap();

        CreateMap<ItemEmprestimo, ItemEmprestimoResponseDto>().ReverseMap();
        CreateMap<ItemEmprestimo, DevolverItemEmprestimoDto>().ReverseMap();

        CreateMap<Venda, VendaResumoDto>().ReverseMap();
        CreateMap<Venda, CreateVendaDto>().ReverseMap();
        CreateMap<Venda, VendaResponseDto>().ReverseMap();
        CreateMap<Venda, VendaComItensDto>().ReverseMap();

        CreateMap<ItemVenda, ItemVendaResponseDto>().ReverseMap();

        CreateMap<Cliente, CreateClienteDto>().ReverseMap();
        CreateMap<Cliente, UpdateClienteDto>().ReverseMap();
        CreateMap<Cliente, ClienteResponseDto>().ReverseMap();
        CreateMap<Cliente, ClienteComEmprestimosDto>().ReverseMap();
        CreateMap<Cliente, ClienteComVendasDto>().ReverseMap();

        CreateMap<Multa, MultaResponseDto>().ReverseMap();
    }
}
