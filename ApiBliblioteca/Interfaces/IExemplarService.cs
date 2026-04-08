using ApiBiblioteca.DTOs.DtosExemplar;

namespace ApiBiblioteca.Interfaces;

public interface IExemplarService
{
    public Task<IEnumerable<ExemplarResponseDto>> Get();
    public Task<ExemplarResponseDto> GetId(int id);
    public Task<IEnumerable<ExemplarResponseDto>> GetByName(string nome);
    public Task<ExemplarResponseDto> Create(CreateExemplarDto dto);
    public Task<ExemplarResponseDto> Update(int id, UpdateExemplarDto dto);
    public Task Delete(int id);
    public Task PerderExemplar(int id);
    public Task DanificarExemplar(int id);
}
