using ApiBiblioteca.Application.DTOs.DtosCliente;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Services;

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

    public async Task<IEnumerable<ClienteResponseDto>> Get()
    {
        var clientes = await _clienteRepository.GetAllAsync() ?? throw new NotFoundException("Cliente não encontrado!"); ;
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }

    public async Task<ClienteResponseDto> GetId(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido");
        var cliente = await _clienteRepository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task<IEnumerable<ClienteResponseDto>> GetByName(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var clientes = await _clienteRepository.GetByNameAsync(nome) ?? throw new NotFoundException("Cliente não encontrado!");
        if (!clientes.Any()) throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);
    }

    public async Task<ClienteComEmprestimosDto> GetClienteComEmprestimos(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido");
        var autor = await _clienteRepository.GetClienteComEmprestimosAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<ClienteComEmprestimosDto>(autor);
    }

    public async Task<ClienteComVendasDto> GetClienteComVendas(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido");
        var autor = await _clienteRepository.GetClienteComVendasAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<ClienteComVendasDto>(autor);
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

    public async Task<ClienteResponseDto> Update(int id, UpdateClienteDto dto)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var cliente = await _clienteRepository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        cliente.AtualizarInformacoes(dto.Nome, dto.Email, dto.Telefone);
        await _UOW.SaveAsync();
        return _mapper.Map<ClienteResponseDto>(cliente);
    }

    public async Task Delete(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var cliente = await _clienteRepository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        _clienteRepository.Remove(cliente);
        await _UOW.SaveAsync();
    }
}
