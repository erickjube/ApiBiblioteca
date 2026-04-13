using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces;
using ApiBiblioteca.Application.Interfaces.IRepository;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Exceptions;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class LivroService : ILivroService
{
    private readonly IUnitOfWork _UOW;
    private readonly ILivroRepository _livroRepository;
    private readonly IMapper _mapper;

    public LivroService(ILivroRepository livroRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _livroRepository = livroRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<IEnumerable<LivroResponseDto>> Get()
    {
        var livros = await _livroRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LivroResponseDto>>(livros);
    }

    public async Task<LivroResponseDto> GetId(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetByIdAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<LivroResponseDto>(livro);
    }

    public async Task<LivroComExemplaresDto> GetLivroComExemplares(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetLivroComExemplaresAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<LivroComExemplaresDto>(livro);
    }

    public async Task<IEnumerable<LivroComExemplaresDto>> GetByNameComExemplares(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new BadRequestException("Titulo inválido!");
        var livros = await _livroRepository.GetByNameComExemplaresAsync(titulo) ?? throw new NotFoundException("Livro não encontrado!");
        if (!livros.Any()) throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<IEnumerable<LivroComExemplaresDto>>(livros);
    }

    public async Task<LivroResponseDto> Create(CreateLivroDto dto)
    {
        if (dto is null) throw new BadRequestException("Livro inválido!");
        var livro = _mapper.Map<Livro>(dto);
        var isbn = livro.Isbn;

        if (await _livroRepository.ExistsByIsbn(isbn))
            throw new BadRequestException("Já existe um livro com esse ISBN.");

        _livroRepository.Create(livro);
        await _UOW.SaveAsync();
        return _mapper.Map<LivroResponseDto>(livro);
    }

    public async Task<LivroResponseDto> Update(long id, UpdateLivroDto dto)
    {
        if (dto == null) throw new BadRequestException("Livro inválido!");
        var livro = await _livroRepository.GetByIdAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        livro.AtualizarInformacoes(dto.Titulo, dto.NumeroDePaginas, dto.DataPublicacao, dto.CategoriaId);
        await _UOW.SaveAsync();
        return _mapper.Map<LivroResponseDto>(livro);
    }

    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetByIdAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        livro.ValidarExclusao();
         _livroRepository.Remove(livro);
        await _UOW.SaveAsync();
    }
}
