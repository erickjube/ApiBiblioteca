using ApiBiblioteca.DTOs.DtosAutor;

namespace ApiBiblioteca.Interfaces;

public interface IAutorService
{
    public Task<IEnumerable<AutorResponseDto>> Get();
    public Task<AutorComLivrosDto> GetComLivros(long id);
    public Task<AutorResponseDto> GetId(long id);
    public Task<IEnumerable<AutorComLivrosDto>> GetByNameComLivros(string nome);
    public Task<AutorResponseDto> Create(AutorDto dto);
    public Task<AutorResponseDto> Update(long id, AutorDto dto);
    public Task Delete(long id);
}
