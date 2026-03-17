using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs;

namespace ApiBiblioteca.Interfaces;

public interface IEmprestimoService
{
    Task<IEnumerable<DtoResponseEmprestimo>> GetAllEmprestimos();
    Task<DtoResponseEmprestimoComItens?> GetEmprestimoById(int emprestimoId);
    Task<DtoResponseEmprestimo> CreateEmprestimo(DtoCriarEmprestimo dto);
    public Task<DtoResponseEmprestimo> AdicionarItem(int emprestimoId, int exemplarId);
    public Task DevolverItem(int emprestimoId, int itemId);
    public Task MarcarItemComoPerdido(int emprestimoId, int itemId);
    public Task MarcarItemComoDanificado(int emprestimoId, int itemId);
    public Task FinalizarEmprestimo(int emprestimoId);
    public Task CancelarEmprestimo(int emprestimoId);
    public Task EstenderPrazoDevolucao(int emprestimoId, DateOnly novoPrazoDevolucao);
}
