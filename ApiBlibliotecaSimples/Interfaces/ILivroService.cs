using ApiBlibliotecaSimples.DTOs;

namespace ApiBlibliotecaSimples.Interfaces;

public interface ILivroService
{
    public Task<IEnumerable<DtoResponseLivro>> Get();
    public Task<DtoResponseLivro> GetId(long id);
    public Task<IEnumerable<DtoResponseLivro>> GetByName(string titulo);
    public Task<DtoResponseLivro> Create(DtoCriarLivro dto);
    public Task<DtoResponseLivro> Update(long id, DtoAtualizarLivro dto);
    public Task Delete(long id);
}
