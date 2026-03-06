using ApiBibliotecaSimples.Domain.Entities;
using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Exceptions;
using ApiBlibliotecaSimples.Interfaces;
using AutoMapper;

namespace ApiBlibliotecaSimples.Services;

public class AutorService : IAutorService
{
    private readonly IAutorRepository _autorRepository;
    private readonly IMapper _mapper;

    public AutorService(IAutorRepository autorRepository, IMapper mapper)
    {
        _autorRepository = autorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DtoResponseAutor>> Get()
    {
        var autores = await _autorRepository.GetAllAsync() ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<IEnumerable<DtoResponseAutor>>(autores);
    }

    public async Task<DtoAutorComLivros> GetComLivros(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autores = await _autorRepository.GetAutorComLivrosAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<DtoAutorComLivros>(autores);
    }

    public async Task<DtoResponseAutor> GetId(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<DtoResponseAutor>(autor);
    }

    public async Task<IEnumerable<DtoAutorComLivros>> GetByNameComLivros(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var autores = await _autorRepository.GetByNameComLivrosAsync(nome) ?? throw new NotFoundException("Autor não encontrado!");
        if (!autores.Any()) throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<IEnumerable<DtoAutorComLivros>>(autores);
    }

    public async Task<DtoResponseAutor> Create(DtoAutor dto)
    {
        if (dto is null) throw new BadRequestException("Autor inválido!");
        var autor = _mapper.Map<Autor>(dto);
        _autorRepository.Create(autor);
        await _autorRepository.SaveAsync();
        return _mapper.Map<DtoResponseAutor>(autor);
    }

    public async Task<DtoResponseAutor> Update(long id, DtoAutor dto)
    {
        if (dto is null) throw new BadRequestException("Autor inválido!");
        var autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        autor.AtualizarInformacoes(dto.Nome, dto.DataNascimento, dto.Nacionalidade);
        await _autorRepository.SaveAsync();
        return _mapper.Map<DtoResponseAutor>(autor);
    }

    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autor = _autorRepository.GetByIdAsync(id).Result ?? throw new NotFoundException("Autor não encontrado!");
        autor.ValidarExclusao();
        _autorRepository.Remove(autor);
        await _autorRepository.SaveAsync();
    }
}
