using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.Domain.Interfaces;
using ApiBiblioteca.DTOs;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
using AutoMapper;

namespace ApiBiblioteca.Services;

public class LivroService : ILivroService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IMapper _mapper;

    public LivroService(ILivroRepository livroRepository, IMapper mapper)
    {
        _livroRepository = livroRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DtoResponseLivro>> Get()
    {
        var livros = await _livroRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<DtoResponseLivro>>(livros);
    }

    public async Task<DtoResponseLivro> GetId(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetByIdAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<DtoResponseLivro>(livro);
    }

    public async Task<DtoLivroComExemplares> GetLivroComExemplares(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetLivroComExemplaresAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<DtoLivroComExemplares>(livro);
    }

    public async Task<IEnumerable<DtoLivroComExemplares>> GetByNameComExemplares(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo)) throw new BadRequestException("Titulo inválido!");
        var livros = await _livroRepository.GetByNameComExemplaresAsync(titulo) ?? throw new NotFoundException("Livro não encontrado!");
        if (!livros.Any()) throw new NotFoundException("Livro não encontrado!");
        return _mapper.Map<IEnumerable<DtoLivroComExemplares>>(livros);
    }

    public async Task<DtoResponseLivro> Create(DtoCriarLivro dto)
    {
        if (dto is null) throw new BadRequestException("Livro inválido!");
        var livro = _mapper.Map<Livro>(dto);
        var isbn = livro.Isbn;

        if (await _livroRepository.ExistsByIsbn(isbn))
            throw new BadRequestException("Já existe um livro com esse ISBN.");

        _livroRepository.Create(livro);
        await _livroRepository.SaveAsync();
        return _mapper.Map<DtoResponseLivro>(livro);
    }

    public async Task<DtoResponseLivro> Update(long id, DtoAtualizarLivro dto)
    {
        if (dto == null) throw new BadRequestException("Livro inválido!");
        var livro = await _livroRepository.GetByIdAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        livro.AtualizarInformacoes(dto.Titulo, dto.NumeroDePaginas, dto.DataPublicacao, dto.CategoriaId);
        await _livroRepository.SaveAsync();
        return _mapper.Map<DtoResponseLivro>(livro);
    }

    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var livro = await _livroRepository.GetByIdAsync(id) ?? throw new NotFoundException("Livro não encontrado!");
        livro.ValidarExclusao();
         _livroRepository.Remove(livro);
        await _livroRepository.SaveAsync();
    }
}
