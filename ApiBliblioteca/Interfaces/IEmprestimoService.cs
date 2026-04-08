using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.DtosEmprestimo;
using ApiBiblioteca.ENUMs;

namespace ApiBiblioteca.Interfaces;

public interface IEmprestimoService
{
    Task<IEnumerable<EmprestimoResponseDto>> GetAllEmprestimos();
    Task<EmprestimoResponseDto> GetEmprestimoById(int emprestimoId);
    Task<EmprestimoComItensDto?> GetEmprestimoComItens(int emprestimoId);
    Task<EmprestimoComMultasDto> GetMultas(int emprestimoId);
    Task<EmprestimoResponseDto> CreateEmprestimo(int clienteId);
    public Task<EmprestimoResponseDto> AdicionarItem(int emprestimoId, int exemplarId);
    public Task DevolverItem(int emprestimoId, int itemId, CondicaoItem condicao);
    public Task FinalizarEmprestimo(int emprestimoId);
    public Task CancelarEmprestimo(int emprestimoId);
    public Task EstenderPrazoDevolucao(int emprestimoId, DateOnly novoPrazoDevolucao);
}
