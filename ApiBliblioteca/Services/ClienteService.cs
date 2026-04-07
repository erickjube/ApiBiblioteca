using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.Cliente;
using ApiBiblioteca.DTOs.DtosCliente;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
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

    public async Task<IEnumerable<DtoResponseCliente>> Get()
    {
        var clientes = await _clienteRepository.GetAllAsync() ?? throw new NotFoundException("Cliente não encontrado!"); ;
        return _mapper.Map<IEnumerable<DtoResponseCliente>>(clientes);
    }

    public async Task<DtoResponseCliente> GetId(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido");
        var cliente = await _clienteRepository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<DtoResponseCliente>(cliente);
    }

    public async Task<IEnumerable<DtoResponseCliente>> GetByName(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var clientes = await _clienteRepository.GetByNameAsync(nome) ?? throw new NotFoundException("Cliente não encontrado!");
        if (!clientes.Any()) throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<IEnumerable<DtoResponseCliente>>(clientes);
    }

    public async Task<DtoClienteComEmprestimos> GetClienteComEmprestimos(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido");
        var autor = await _clienteRepository.GetClienteComEmprestimosAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<DtoClienteComEmprestimos>(autor);
    }

    public async Task<DtoClienteComVendas> GetClienteComVendas(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido");
        var autor = await _clienteRepository.GetClienteComVendasAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        return _mapper.Map<DtoClienteComVendas>(autor);
    }

    public async Task<DtoResponseCliente> Create(DtoCriarCliente dto)
    {
        if (dto is null) throw new BadRequestException("Cliente inválido!");
        var cliente = _mapper.Map<Cliente>(dto);
        if (await _clienteRepository.Existe(cliente.Cpf, cliente.Email, cliente.Telefone)) throw new BadRequestException("CPF, Email ou Telefone já cadastrado!");
        _clienteRepository.Create(cliente);
        await _UOW.SaveAsync();
        return _mapper.Map<DtoResponseCliente>(cliente);
    }

    public async Task<DtoResponseCliente> Update(int id, DtoAtualizarCliente dto)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var cliente = await _clienteRepository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        cliente.AtualizarInformacoes(dto.Nome, dto.Email, dto.Telefone);
        await _UOW.SaveAsync();
        return _mapper.Map<DtoResponseCliente>(cliente);
    }

    public async Task Delete(int id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var cliente = await _clienteRepository.GetByIdAsync(id) ?? throw new NotFoundException("Cliente não encontrado!");
        _clienteRepository.Remove(cliente);
        await _UOW.SaveAsync();
    }
}
