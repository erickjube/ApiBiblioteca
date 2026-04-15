using ApiBiblioteca.Application.DTOs.DtosAutor;
using ApiBiblioteca.Application.DTOs.DtosExemplar;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class LivroService : ILivroService
{
    private readonly IUnitOfWork _UOW;
    private readonly ILivroRepository _livroRepository;
    private readonly IMapper _mapper;

    public LivroService(ILivroRepository livroRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _livroRepository = livroRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<PagedList<LivroResponseDto>> Get(QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _livroRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar livros.");
        var totalPages = (int)Math.Ceiling((double)result.TotalCount / parameters.PageSize);
        if (parameters.PageNumber > totalPages && totalPages > 0) throw new BadRequestException("Página solicitada não existe.");

        return new PagedList<LivroResponseDto>
        {
            Data = _mapper.Map<IEnumerable<LivroResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<ExemplarResponseDto>> GetComExemplares(long livroId, QueryParameters parameters)
    {
        if (livroId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _livroRepository.GetExemplaresByLivroAsync(livroId, skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar livros.");
        var totalPages = (int)Math.Ceiling((double)result.TotalCount / parameters.PageSize);
        if (parameters.PageNumber > totalPages && totalPages > 0) throw new BadRequestException("Página solicitada não existe.");

        return new PagedList<ExemplarResponseDto>
        {
            Data = _mapper.Map<IEnumerable<ExemplarResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<LivroResponseDto> GetId(long livroId)
    {
        if (livroId <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetByIdAsync(livroId) ?? throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<LivroResponseDto>(livro);
    }

    public async Task<LivroResponseDto> Create(CreateLivroDto dto)
    {
        if (dto is null) throw new BadRequestException("Livro inválido!");
        var livro = _mapper.Map<Livro>(dto);
        var isbn = livro.Isbn;

        if (await _livroRepository.ExistsByIsbn(isbn))
            throw new BadRequestException("Já existe um livro com esse ISBN.");

        _livroRepository.Create(livro);
        await _UOW.SaveAsync();
        return _mapper.Map<LivroResponseDto>(livro);
    }

    public async Task<LivroResponseDto> Update(long livroId, UpdateLivroDto dto)
    {
        if (dto == null) throw new BadRequestException("Livro inválido!");
        var livro = await _livroRepository.GetByIdAsync(livroId) ?? throw new NotFoundException("Livro não encontrado!");
        livro.AtualizarInformacoes(dto.Titulo, dto.NumeroDePaginas, dto.DataPublicacao, dto.CategoriaId);
        await _UOW.SaveAsync();
        return _mapper.Map<LivroResponseDto>(livro);
    }

    public async Task Delete(long livroId)
    {
        if (livroId <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetByIdAsync(livroId) ?? throw new NotFoundException("Livro não encontrado!");
        livro.ValidarExclusao();
         _livroRepository.Remove(livro);
        await _UOW.SaveAsync();
    }
}
