using ApiBiblioteca.Application.DTOs.DtosCategoria;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _UOW;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<PagedList<CategoriaResponseDto>> Get(QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _categoriaRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar Categorias.");
        var totalPages = (int)Math.Ceiling((double)result.TotalCount / parameters.PageSize);
        if (parameters.PageNumber > totalPages && totalPages > 0) throw new BadRequestException("Página solicitada não existe.");

        return new PagedList<CategoriaResponseDto>
        {
            Data = _mapper.Map<IEnumerable<CategoriaResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<LivroResponseDto>> GetComLivros(long categoriaId, QueryParameters parameters)
    {
        if (categoriaId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _categoriaRepository.GetLivrosByCategoriaAsync(categoriaId, skip, parameters.PageSize);
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

    public async Task<CategoriaResponseDto> GetId(long categoriaId)
    {
        if (categoriaId <= 0) throw new BadRequestException("Id inválido!");
        var categoria = await _categoriaRepository.GetByIdAsync(categoriaId) ?? throw new NotFoundException("Categoria não encontrada!");
        return _mapper.Map<CategoriaResponseDto>(categoria);
    }

    public async Task<CategoriaResponseDto> Create(CategoriaDto dto)
    {
        if (dto is null) throw new BadRequestException("Categoria inválida!");
        var categoria = _mapper.Map<Categoria>(dto);
        _categoriaRepository.Create(categoria);
        await _UOW.SaveAsync();
        return _mapper.Map<CategoriaResponseDto>(categoria);
    }

    public async Task<CategoriaResponseDto> Update(long categoriaId, CategoriaDto dto)
    {
        if (categoriaId <= 0) throw new BadRequestException("Id inválido!");
        if (dto is null) throw new BadRequestException("Categoria inválida!");
        var categoria = await _categoriaRepository.GetByIdAsync(categoriaId) ?? throw new NotFoundException("Categoria não encontrada!");
        categoria.AtualizarNome(dto.Nome);
        await _UOW.SaveAsync();
        return _mapper.Map<CategoriaResponseDto>(categoria);
    }
    
    public async Task Delete(long categoriaId)
    {
        if (categoriaId <= 0) throw new BadRequestException("Id inválido!");
        var categoria = _categoriaRepository.GetByIdAsync(categoriaId).Result ?? throw new NotFoundException("Categoria não encontrada!");
        categoria.ValidarExclusao();
        _categoriaRepository.Remove(categoria);
        await _UOW.SaveAsync();
    }
}
