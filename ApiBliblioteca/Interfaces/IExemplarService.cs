using ApiBiblioteca.DTOs;

namespace ApiBiblioteca.Interfaces;

public interface IExemplarService
{
    public Task<IEnumerable<DtoResponseExemplar>> Get();
    public Task<DtoResponseExemplar> GetId(int id);
    public Task<IEnumerable<DtoResponseExemplar>> GetByName(string nome);
    public Task<DtoResponseExemplar> Create(DtoCriarExemplar dto);
    public Task<DtoResponseExemplar> Update(int id, DtoAtualizarExemplar dto);
    public Task Delete(int id);
    public Task PerderExemplar(int id);
    public Task DanificarExemplar(int id);
}
