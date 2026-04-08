using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.DtosAutor;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class AutorService : IAutorService
{
    private readonly IUnitOfWork _UOW;
    private readonly IAutorRepository _autorRepository;
    private readonly IMapper _mapper;

    public AutorService(IAutorRepository autorRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _autorRepository = autorRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<IEnumerable<AutorResponseDto>> Get()
    {
        var autores = await _autorRepository.GetAllAsync() ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<IEnumerable<AutorResponseDto>>(autores);
    }

    public async Task<AutorComLivrosDto> GetComLivros(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autores = await _autorRepository.GetAutorComLivrosAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<AutorComLivrosDto>(autores);
    }

    public async Task<AutorResponseDto> GetId(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<AutorResponseDto>(autor);
    }

    public async Task<IEnumerable<AutorComLivrosDto>> GetByNameComLivros(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var autores = await _autorRepository.GetByNameComLivrosAsync(nome) ?? throw new NotFoundException("Autor não encontrado!");
        if (!autores.Any()) throw new NotFoundException("Autor não encontrado!");
        return _mapper.Map<IEnumerable<AutorComLivrosDto>>(autores);
    }

    public async Task<AutorResponseDto> Create(AutorDto dto)
    {
        if (dto is null) throw new BadRequestException("Autor inválido!");
        var autor = _mapper.Map<Autor>(dto);
        _autorRepository.Create(autor);
        await _UOW.SaveAsync();
        return _mapper.Map<AutorResponseDto>(autor);
    }

    public async Task<AutorResponseDto> Update(long id, AutorDto dto)
    {
        if (dto is null) throw new BadRequestException("Autor inválido!");
        var autor = await _autorRepository.GetByIdAsync(id) ?? throw new NotFoundException("Autor não encontrado!");
        autor.AtualizarInformacoes(dto.Nome, dto.DataNascimento, dto.Nacionalidade);
        await _UOW.SaveAsync();
        return _mapper.Map<AutorResponseDto>(autor);
    }

    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var autor = _autorRepository.GetByIdAsync(id).Result ?? throw new NotFoundException("Autor não encontrado!");
        autor.ValidarExclusao();
        _autorRepository.Remove(autor);
        await _UOW.SaveAsync();
    }
}
