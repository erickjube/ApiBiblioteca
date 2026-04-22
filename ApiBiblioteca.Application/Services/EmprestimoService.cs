using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosItemEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosMulta;
using ApiBiblioteca.Application.Helpers;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.Services;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.ENUMs;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Application.Services;

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

    public async Task<PagedList<EmprestimoResponseDto>> Get(QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _emprestimoRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar emprestimos.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<EmprestimoResponseDto>
        {
            Data = _mapper.Map<IEnumerable<EmprestimoResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<MultaResponseDto>> GetComMultas(int emprestimoId, QueryParameters parameters)
    {
        if (emprestimoId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _emprestimoRepository.GetMultasByEmprestimo(emprestimoId, skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar multas.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<MultaResponseDto>
        {
            Data = _mapper.Map<IEnumerable<MultaResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<ItemEmprestimoResponseDto>> GetComItens(int emprestimoId, QueryParameters parameters)
    {
        if (emprestimoId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _emprestimoRepository.GetItensByEmprestimo(emprestimoId, skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar itens.");
        ValidatePagination.Validate(parameters.PageNumber, parameters.PageSize, result.TotalCount);

        return new PagedList<ItemEmprestimoResponseDto>
        {
            Data = _mapper.Map<IEnumerable<ItemEmprestimoResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<EmprestimoResponseDto> GetEmprestimoById(int emprestimoId)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo is null) throw new NotFoundException("Empréstimo não encontrado");
        return _mapper.Map<EmprestimoResponseDto>(emprestimo);
    }


    public async Task<EmprestimoResponseDto> CreateEmprestimo(CreateEmprestimoDto dto)
    {
        var cliente = await _clienteRepository.GetByIdAsync(dto.ClienteId);
        if (cliente == null) throw new NotFoundException("Cliente não encontrado");

        var possuiEmprestimo = await _emprestimoRepository.ClienteTemEmprestimoAtivo(dto.ClienteId);
        if (possuiEmprestimo) throw new BadRequestException("Cliente já possui um empréstimo em aberto");
        
        var emprestimo = new Emprestimo(dto.ClienteId);
        await _emprestimoRepository.AddAsync(emprestimo);
        await _UOW.SaveAsync();
        return _mapper.Map<EmprestimoResponseDto>(emprestimo);
    }

    public async Task<EmprestimoResponseDto> AdicionarItem(int emprestimoId, int exemplarId)
    {
        if (emprestimoId <= 0 || exemplarId <= 0) throw new BadRequestException("Ids inválidos");
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        var exemplar = await _exemplarRepository.GetByIdAsync(exemplarId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        if (exemplar == null) throw new NotFoundException("Exemplar não encontrado");
        emprestimo.AdicionarItem(exemplarId);
        exemplar.Emprestar();
        await _UOW.SaveAsync();
        return _mapper.Map<EmprestimoResponseDto>(emprestimo);
    }

    public async Task DevolverItem(int emprestimoId, DevolverItemEmprestimoDto dto)
    {
        if (emprestimoId <= 0) throw new BadRequestException("Id inválido");
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        var item = await _emprestimoRepository.GetItemByIdAsync(dto.ItemId);
        if (item is null) throw new NotFoundException("Item não encontrado");
        var exemplar = await _exemplarRepository.GetByIdAsync(item.ExemplarId);
        var multa = emprestimo.DevolverItem(item, exemplar, dto.Condicao);
        if (multa != null) await _multaRepository.AddAsync(multa);
        await _UOW.SaveAsync();
    }

    public async Task FinalizarEmprestimo(int emprestimoId)
    {
        if (emprestimoId <= 0) throw new BadRequestException("Id inválido");
        var emprestimo = await _emprestimoRepository.GetMultas(emprestimoId);
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
        if (emprestimoId <= 0) throw new BadRequestException("Id inválido");
        var emprestimo = await _emprestimoRepository.GetByIdAsync(emprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.Cancelar();
        foreach (var item in emprestimo.Itens)
        {
            var exemplar = await _exemplarRepository.GetByIdAsync(item.ExemplarId);
            exemplar.Devolver();
        }
        await _UOW.SaveAsync();
    }

    public async Task EstenderPrazoDevolucao(EstenderDevolucaoDto dto)
    {
        var emprestimo = await _emprestimoRepository.GetByIdAsync(dto.EmprestimoId);
        if (emprestimo == null) throw new NotFoundException("Empréstimo não encontrado");
        emprestimo.AtualizarPrevisaoDevolucao(dto.NovoPrazoDevolucao);
        await _UOW.SaveAsync();
    }
}
