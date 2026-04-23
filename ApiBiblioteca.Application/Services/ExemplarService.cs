using ApiBiblioteca.Application.DTOs.DtosAutor;
using ApiBiblioteca.Application.DTOs.DtosExemplar;
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

public class ExemplarService : IExemplarService
{
    private readonly IUnitOfWork _UOW;
    private readonly IExemplarRepository _exemplarRepository;
    private readonly IMapper _mapper;

    public ExemplarService(IExemplarRepository exemplarRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _exemplarRepository = exemplarRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<PagedList<ExemplarResponseDto>> Get(QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _exemplarRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar exemplares.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<ExemplarResponseDto>
        {
            Data = _mapper.Map<IEnumerable<ExemplarResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<ExemplarResponseDto> GetId(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        return _mapper.Map<ExemplarResponseDto>(exemplar);
    }

    public async Task<ExemplarResponseDto> Create(CreateExemplarDto dto)
    {
        if (dto is null) throw new BadRequestException("Exemplar inválido!");
        var exemplar = _mapper.Map<ExemplarLivro>(dto);
        if (await _exemplarRepository.ExisteCodigoBarras(exemplar.CodigoDeBarras))
            throw new BadRequestException("Código de barras já existe!");
        _exemplarRepository.Create(exemplar);
        await _UOW.SaveAsync();
        return _mapper.Map<ExemplarResponseDto>(exemplar);
    }

    public async Task<ExemplarResponseDto> Update(int id, UpdateExemplarDto dto)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        exemplar.AtualizarInformacoes(dto.Nome, dto.Preco);
        await _UOW.SaveAsync();
        return _mapper.Map<ExemplarResponseDto>(exemplar);
    }

    public async Task Delete(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        _exemplarRepository.Remove(exemplar);
        await _UOW.SaveAsync();
    }

    public async Task PerderExemplar(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        exemplar.Perder();
        await _UOW.SaveAsync();
    }

    public async Task DanificarExemplar(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        exemplar.Danificar();
        await _UOW.SaveAsync();
    }
}
