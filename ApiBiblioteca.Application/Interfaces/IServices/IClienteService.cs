using ApiBiblioteca.Application.DTOs.DtosCliente;

namespace ApiBiblioteca.Application.Interfaces.IServices;

public interface IClienteService
{
    public Task<IEnumerable<ClienteResponseDto>> Get();
    public Task<ClienteResponseDto> GetId(int id);
    public Task<IEnumerable<ClienteResponseDto>> GetByName(string nome);
    public Task<ClienteComEmprestimosDto> GetClienteComEmprestimos(int id);
    public Task<ClienteComVendasDto> GetClienteComVendas(int id);
    public Task<ClienteResponseDto> Create(CreateClienteDto dto);
    public Task<ClienteResponseDto> Update(int id, UpdateClienteDto dto);
    public Task Delete(int id);
}
