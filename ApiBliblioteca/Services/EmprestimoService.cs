using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs;
using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class EmprestimoService : IEmprestimoService
{
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly IExemplarRepository _exemplarRepository;
    private readonly IMapper _mapper;

    public EmprestimoService(IEmprestimoRepository emprestimoRepository, IExemplarRepository exemplarRepository, IMapper mapper)
    {
        _emprestimoRepository = emprestimoRepository;
        _exemplarRepository = exemplarRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DtoResponseEmprestimo>> GetAllEmprestimos()
    {
        var emprestimos = await _emprestimoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DtoResponseEmprestimo>>(emprestimos);
    }

    public async Task<DtoResponseEmprestimoComItens?> GetEmprestimoById(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        return _mapper.Map<DtoResponseEmprestimoComItens>(emprestimo);
    }

    public async Task<DtoResponseEmprestimo> CreateEmprestimo(DtoCriarEmprestimo dto)
    {
        if (dto == null) throw new BadRequestException("Empréstimo inválido");
        var emprestimo = _mapper.Map<Emprestimo>(dto);
        await _emprestimoRepository.AddAsync(emprestimo);
        await _emprestimoRepository.SaveChanges();
        return _mapper.Map<DtoResponseEmprestimo>(emprestimo);
    }

    public async Task<DtoResponseEmprestimo> AdicionarItem(int emprestimoId, int exemplarId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        var exemplar = await _exemplarRepository.GetByIdAsync(exemplarId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        if (exemplar == null) throw new NotFoundException("Exemplar não encontrado");
        emprestimo.AdicionarItem(exemplarId);
        exemplar.Emprestar();
        await _emprestimoRepository.SaveChanges();
        return _mapper.Map<DtoResponseEmprestimo>(emprestimo);
    }

    public async Task DevolverItem(int emprestimoId, int itemId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.DevolverItem(itemId);
        await _emprestimoRepository.SaveChanges();
    }

    public async Task MarcarItemComoPerdido(int emprestimoId, int itemId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.MarcarItemComoPerdido(itemId);
        await _emprestimoRepository.SaveChanges();
    }

    public async Task MarcarItemComoDanificado(int emprestimoId, int itemId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.MarcarItemComoDanificado(itemId);
        await _emprestimoRepository.SaveChanges();
    }

    public async Task FinalizarEmprestimo(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.Finalizar();
        await _emprestimoRepository.SaveChanges();
    }

    public async Task CancelarEmprestimo(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.Cancelar();
        await _emprestimoRepository.SaveChanges();
    }

    public async Task EstenderPrazoDevolucao(int emprestimoId, DateOnly novoPrazoDevolucao)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        if (novoPrazoDevolucao <= DateOnly.FromDateTime(DateTime.UtcNow)) throw new BadRequestException("Nova data de devolução deve ser futura");
        emprestimo.AtualizarPrevisaoDevolucao(novoPrazoDevolucao);
        await _emprestimoRepository.SaveChanges();
    }
}
