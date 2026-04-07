using ApiBiblioteca.DTOs.Cliente;
using ApiBiblioteca.DTOs.DtosCliente;

namespace ApiBiblioteca.Interfaces;

public interface IClienteService
{
    public Task<IEnumerable<DtoResponseCliente>> Get();
    public Task<DtoResponseCliente> GetId(int id);
    public Task<IEnumerable<DtoResponseCliente>> GetByName(string nome);
    public Task<DtoClienteComEmprestimos> GetClienteComEmprestimos(int id);
    public Task<DtoClienteComVendas> GetClienteComVendas(int id);
    public Task<DtoResponseCliente> Create(DtoCriarCliente dto);
    public Task<DtoResponseCliente> Update(int id, DtoAtualizarCliente dto);
    public Task Delete(int id);
}
