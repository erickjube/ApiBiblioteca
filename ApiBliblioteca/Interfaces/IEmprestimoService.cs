using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs;
using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.Interfaces;

public interface IEmprestimoService
{
    Task<IEnumerable<DtoResponseEmprestimo>> GetAllEmprestimos();
    Task<DtoResponseEmprestimoComItens?> GetEmprestimoById(int emprestimoId);
    Task<DtoResponseEmprestimo> CreateEmprestimo(int clienteId);
    public Task<DtoResponseEmprestimo> AdicionarItem(int emprestimoId, int exemplarId);
    public Task DevolverItem(int emprestimoId, int itemId, CondicaoItem condicao);
    public Task FinalizarEmprestimo(int emprestimoId);
    public Task CancelarEmprestimo(int emprestimoId);
    public Task EstenderPrazoDevolucao(int emprestimoId, DateOnly novoPrazoDevolucao);
}
