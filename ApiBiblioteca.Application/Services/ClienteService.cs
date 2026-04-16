using ApiBiblioteca.Application.DTOs.DtosCliente;
using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosVenda;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using ApiBiblioteca.Domain.Common;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Application.Services;

public class ClienteService : IClienteService
{
    private readonly IUnitOfWork _UOW;
    private readonly IClienteRepository _clienteRepository;
    private readonly IMapper _mapper;

    public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _clienteRepository = clienteRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<PagedList<ClienteResponseDto>> Get(QueryParameters parameters)
    {
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _clienteRepository.GetAllAsync(skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar clientes.");
        var totalPages = (int)Math.Ceiling((double)result.TotalCount / parameters.PageSize);
        if (parameters.PageNumber > totalPages && totalPages > 0) throw new BadRequestException("Página solicitada não existe.");

        return new PagedList<ClienteResponseDto>
        {
            Data = _mapper.Map<IEnumerable<ClienteResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<EmprestimoResponseDto>> GetComEmprestimos(int clienteId, QueryParameters parameters)
    {
        if (clienteId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _clienteRepository.GetEmprestimosByClienteAsync(clienteId, skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar emprestimos.");
        var totalPages = (int)Math.Ceiling((double)result.TotalCount / parameters.PageSize);
        if (parameters.PageNumber > totalPages && totalPages > 0) throw new BadRequestException("Página solicitada não existe.");

        return new PagedList<EmprestimoResponseDto>
        {
            Data = _mapper.Map<IEnumerable<EmprestimoResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<PagedList<VendaResponseDto>> GetComVendas(int clienteId, QueryParameters parameters)
    {
        if (clienteId <= 0) throw new BadRequestException("Id inválido!");
        var skip = (parameters.PageNumber - 1) * parameters.PageSize;
        var result = await _clienteRepository.GetVendasByClienteAsync(clienteId, skip, parameters.PageSize);
        if (result == null) throw new NotFoundException("Erro ao buscar vendas.");
        var totalPages = (int)Math.Ceiling((double)result.TotalCount / parameters.PageSize);
        if (parameters.PageNumber > totalPages && totalPages > 0) throw new BadRequestException("Página solicitada não existe.");

        return new PagedList<VendaResponseDto>
        {
            Data = _mapper.Map<IEnumerable<VendaResponseDto>>(result.Data),
            TotalCount = result.TotalCount,
            PageNumber = parameters.PageNumber,
            PageSize = parameters.PageSize
        };
    }

    public async Task<ClienteResponseDto> GetId(int clienteId)
    {
        if (clienteId <= 0) throw new BadRequestException("Id inválido");
        var cliente = await _clienteRepository.GetByIdAsync(clienteId) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task<ClienteResponseDto> Create(CreateClienteDto dto)
    {
        if (dto is null) throw new BadRequestException("Cliente inválido!");
        var cliente = _mapper.Map<Cliente>(dto);
        if (await _clienteRepository.Existe(cliente.Cpf, cliente.Email, cliente.Telefone)) throw new BadRequestException("CPF, Email ou Telefone já cadastrado!");
        _clienteRepository.Create(cliente);
        await _UOW.SaveAsync();
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task<ClienteResponseDto> Update(int clienteId, UpdateClienteDto dto)
    {
        if (clienteId <= 0) throw new BadRequestException("Id inválido!");
        var cliente = await _clienteRepository.GetByIdAsync(clienteId) ?? throw new NotFoundException("Cliente não encontrado!");
        cliente.AtualizarInformacoes(dto.Nome, dto.Email, dto.Telefone);
        await _UOW.SaveAsync();
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task Delete(int clienteId)
    {
        if (clienteId <= 0) throw new BadRequestException("Id inválido!");
        var cliente = await _clienteRepository.GetByIdAsync(clienteId) ?? throw new NotFoundException("Cliente não encontrado!");
        _clienteRepository.Remove(cliente);
        await _UOW.SaveAsync();
    }
}
