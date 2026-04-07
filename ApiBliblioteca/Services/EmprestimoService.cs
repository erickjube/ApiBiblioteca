using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.DtosEmprestimo;
using ApiBiblioteca.ENUMs;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class EmprestimoService : IEmprestimoService
{
    private readonly IUnitOfWork _UOW;
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly IExemplarRepository _exemplarRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMultaRepository _multaRepository;
    private readonly IMapper _mapper;

    public EmprestimoService(IEmprestimoRepository emprestimoRepository, 
                             IExemplarRepository exemplarRepository, 
                             IClienteRepository clienteRepository, 
                             IMultaRepository multaRepository,
                             IMapper mapper,
                             IUnitOfWork uOF)
    {
        _emprestimoRepository = emprestimoRepository;
        _exemplarRepository = exemplarRepository;
        _clienteRepository = clienteRepository;
        _multaRepository = multaRepository;
        _mapper = mapper;
        _UOW = uOF;
    }

    public async Task<IEnumerable<DtoResponseEmprestimo>> GetAllEmprestimos()
    {
        var emprestimos = await _emprestimoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DtoResponseEmprestimo>>(emprestimos);
    }

    public async Task<DtoResponseEmprestimo> GetEmprestimoById(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo is null) throw new NotFoundException("Empréstimo não encontrado");
        return _mapper.Map<DtoResponseEmprestimo>(emprestimo);
    }

    public async Task<DtoResponseEmprestimoComItens?> GetEmprestimoComItens(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        return _mapper.Map<DtoResponseEmprestimoComItens>(emprestimo);
    }

    public async Task<DtoResponseEmprestimoComMultas> GetMultas(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetEmprestimoComMultas(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        return _mapper.Map<DtoResponseEmprestimoComMultas>(emprestimo);
    }

    public async Task<DtoResponseEmprestimo> CreateEmprestimo(int clienteId)
    {
        var cliente = await _clienteRepository.GetByIdAsync(clienteId);
        if (cliente == null) throw new NotFoundException("Cliente não encontrado");
        var possuiEmprestimo = await _emprestimoRepository.ClienteTemEmprestimoAtivo(clienteId);
        if (possuiEmprestimo) throw new BadRequestException("Cliente já possui um empréstimo em aberto");
        var emprestimo = new Emprestimo(clienteId);
        await _emprestimoRepository.AddAsync(emprestimo);
        await _UOW.SaveAsync();
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
        await _UOW.SaveAsync();
        return _mapper.Map<DtoResponseEmprestimo>(emprestimo);
    }

    public async Task DevolverItem(int emprestimoId, int itemId, CondicaoItem condicao)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        var multa = emprestimo.DevolverItem(itemId, condicao);
        if (multa != null) await _multaRepository.AddAsync(multa);
        await _UOW.SaveAsync();
    }

    public async Task FinalizarEmprestimo(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetEmprestimoComMultas(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        decimal count = 0;

        foreach (var multa in emprestimo.Multas)
        {
            var total = multa.Valor;
            count += total;
        }
        emprestimo.DefinirValorMultaTotal(count);
        emprestimo.Finalizar();
        await _UOW.SaveAsync();
    }

    public async Task CancelarEmprestimo(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.Cancelar();
        await _UOW.SaveAsync();
    }

    public async Task EstenderPrazoDevolucao(int emprestimoId, DateOnly novoPrazoDevolucao)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        if (novoPrazoDevolucao <= DateOnly.FromDateTime(DateTime.UtcNow)) throw new BadRequestException("Nova data de devolução deve ser futura");
        emprestimo.AtualizarPrevisaoDevolucao(novoPrazoDevolucao);
        await _UOW.SaveAsync();
    }
}
