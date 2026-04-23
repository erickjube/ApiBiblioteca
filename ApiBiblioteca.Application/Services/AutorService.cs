using ApiBiblioteca.Application.DTOs.DtosAutor;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Helpers;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Application.Services;

public class AutorService : IAutorService
{
    private readonly IUnitOfWork _UOW;
    private readonly IAutorRepository _autorRepository;
    private readonly IMapper _mapper;

    public AutorService(IAutorRepository autorRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _autorRepository = autorRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<PagedList<AutorResponseDto>> Get(QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _autorRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar autores.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<AutorResponseDto>
        {
            Data = _mapper.Map<IEnumerable<AutorResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<LivroResponseDto>> GetComLivros(long autorId, QueryParameters parameters)
    {
        if (autorId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _autorRepository.GetLivrosByAutorIdAsync(autorId, skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar livros.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<LivroResponseDto>
        {
            Data = _mapper.Map<IEnumerable<LivroResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<AutorResponseDto> GetId(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<AutorResponseDto>(autor);
    }

    public async Task<AutorResponseDto> Create(AutorDto dto)
    {
        if (dto is null) throw new BadRequestException("Autor inválido!");
        var autor = _mapper.Map<Autor>(dto);
        _autorRepository.Create(autor);
        await _UOW.SaveAsync();
        return _mapper.Map<AutorResponseDto>(autor);
    }

    public async Task<AutorResponseDto> Update(long autorId, AutorDto dto)
    {
        if (autorId <= 0) throw new BadRequestException("Id inválido!");
        if (dto is null) throw new BadRequestException("Autor inválido!");
        var autor = await _autorRepository.GetByIdAsync(autorId) ?? throw new NotFoundException("Autor não encontrado!");
        autor.AtualizarInformacoes(dto.Nome, dto.DataNascimento, dto.Nacionalidade);
        await _UOW.SaveAsync();
        return _mapper.Map<AutorResponseDto>(autor);
    }

    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autor = _autorRepository.GetByIdAsync(id).Result ?? throw new NotFoundException("Autor não encontrado!");
        autor.ValidarExclusao();
        _autorRepository.Remove(autor);
        await _UOW.SaveAsync();
    }
}
