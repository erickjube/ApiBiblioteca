using ApiBibliotecaSimples.Domain.Entities;
using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Exceptions;
using ApiBlibliotecaSimples.Interfaces;
using AutoMapper;

namespace ApiBlibliotecaSimples.Services;

public class ExemplarService : IExemplarService
{
    private readonly IExemplarRepository _exemplarRepository;
    private readonly IMapper _mapper;

    public ExemplarService(IExemplarRepository exemplarRepository, IMapper mapper)
    {
        _exemplarRepository = exemplarRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DtoResponseExemplar>> Get()
    {
        var exemplares = await _exemplarRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DtoResponseExemplar>>(exemplares);
    }

    public async Task<DtoResponseExemplar> GetId(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        return _mapper.Map<DtoResponseExemplar>(exemplar);
    }

    public async Task<IEnumerable<DtoResponseExemplar>> GetByName(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var exemplar = await _exemplarRepository.GetByNameAsync(nome) ?? throw new NotFoundException("Exemplar não encontrado!");
        return _mapper.Map<IEnumerable<DtoResponseExemplar>>(exemplar);
    }

    public async Task<DtoResponseExemplar> Create(DtoCriarExemplar dto)
    {
        var exemplar = _mapper.Map<ExemplarLivro>(dto);
        if (await _exemplarRepository.ExisteCodigoBarras(exemplar.CodigoDeBarras))
            throw new BadRequestException("Código de barras já existe!");
        _exemplarRepository.Create(exemplar);
        await _exemplarRepository.SaveAsync();
        return _mapper.Map<DtoResponseExemplar>(exemplar);
    }

    public async Task<DtoResponseExemplar> Update(int id, DtoAtualizarExemplar dto)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        exemplar.AtualizarInformacoes(dto.Nome, dto.Preco);
        await _exemplarRepository.SaveAsync();
        return _mapper.Map<DtoResponseExemplar>(exemplar);
    }

    public async Task Danificar(int id)
    {
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        exemplar.Danificar();
        await _exemplarRepository.SaveAsync();
    }

    public async Task Delete(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var exemplar = await _exemplarRepository.GetByIdAsync(id) ?? throw new NotFoundException("Exemplar não encontrado!");
        _exemplarRepository.Remove(exemplar);
        await _exemplarRepository.SaveAsync();
    }
}
