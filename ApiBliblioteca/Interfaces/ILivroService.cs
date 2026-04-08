using ApiBiblioteca.DTOs.DtosLivro;

namespace ApiBiblioteca.Interfaces;

public interface ILivroService
{
    public Task<IEnumerable<LivroResponseDto>> Get();
    public Task<LivroResponseDto> GetId(long id);
    public Task<LivroComExemplaresDto> GetLivroComExemplares(long id);
    public Task<IEnumerable<LivroComExemplaresDto>> GetByNameComExemplares(string titulo);
    public Task<LivroResponseDto> Create(CreateLivroDto dto);
    public Task<LivroResponseDto> Update(long id, UpdateLivroDto dto);
    public Task Delete(long id);
}
