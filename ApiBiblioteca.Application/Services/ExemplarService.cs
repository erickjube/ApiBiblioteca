using ApiBiblioteca.Application.DTOs.DtosExemplar;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Services;

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

    public async Task<IEnumerable<ExemplarResponseDto>> Get()
    {
        var exemplares = await _exemplarRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ExemplarResponseDto>>(exemplares);
    }

    public async Task<ExemplarResponseDto> GetId(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        return _mapper.Map<ExemplarResponseDto>(exemplar);
    }

    public async Task<IEnumerable<ExemplarResponseDto>> GetByName(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var exemplar = await _exemplarRepository.GetByNameAsync(nome) ?? throw new NotFoundException("Exemplar não encontrado!");
        if (!exemplar.Any()) throw new NotFoundException("Exemplar não encontrado!");
        return _mapper.Map<IEnumerable<ExemplarResponseDto>>(exemplar);
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
