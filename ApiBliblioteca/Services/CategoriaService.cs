using ApiBiblioteca.Domain.Entities;
using ApiBiblioteca.DTOs.DtosCategoria;
using ApiBiblioteca.Exceptions;
using ApiBiblioteca.Interfaces;
using AutoMapper;
using System.Globalization;

namespace ApiBiblioteca.Services;

public class CategoriaService : ICategoriaService
{
    private readonly IUnitOfWork _UOW;
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, IUnitOfWork uOW)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
        _UOW = uOW;
    }

    public async Task<IEnumerable<DtoResponseCategoria>> Get()
    {
        var categorias = await _categoriaRepository.GetAllAsync() ?? throw new NotFoundException("Categoria não encontrada!");
        return _mapper.Map<IEnumerable<DtoResponseCategoria>>(categorias);
    }

    public async Task<DtoCategoriaComLivros> GetComLivros(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var categorias = await _categoriaRepository.GetCategoriaComLivrosAsync(id) ?? throw new NotFoundException("Categoria não encontrada!");
        return _mapper.Map<DtoCategoriaComLivros>(categorias);
    }

    public async Task<IEnumerable<DtoCategoriaComLivros>> GetByNameComLivros(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) throw new BadRequestException("Nome inválido!");
        var categorias = await _categoriaRepository.GetByNameComLivrosAsync(nome) ?? throw new NotFoundException("Categoria não encontrada!");
        if (!categorias.Any()) throw new NotFoundException("Categoria não encontrada!");
        return _mapper.Map<IEnumerable<DtoCategoriaComLivros>>(categorias);
    }

    public async Task<DtoResponseCategoria> GetId(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var categoria = await _categoriaRepository.GetByIdAsync(id) ?? throw new NotFoundException("Categoria não encontrada!");
        return _mapper.Map<DtoResponseCategoria>(categoria);
    }

    public async Task<DtoResponseCategoria> Create(DtoCategoria dto)
    {
        if (dto is null) throw new BadRequestException("Categoria inválida!");
        var categoria = _mapper.Map<Categoria>(dto);
        _categoriaRepository.Create(categoria);
        await _UOW.SaveAsync();
        return _mapper.Map<DtoResponseCategoria>(categoria);
    }

    public async Task<DtoResponseCategoria> Update(long id, DtoCategoria dto)
    {
        if (dto is null) throw new BadRequestException("Categoria inválida!");
        var categoria = await _categoriaRepository.GetByIdAsync(id) ?? throw new NotFoundException("Categoria não encontrada!");
        categoria.AtualizarNome(dto.Nome);
        await _UOW.SaveAsync();
        return _mapper.Map<DtoResponseCategoria>(categoria);
    }
    
    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var categoria = _categoriaRepository.GetByIdAsync(id).Result ?? throw new NotFoundException("Categoria não encontrada!");
        categoria.ValidarExclusao();
        _categoriaRepository.Remove(categoria);
        await _UOW.SaveAsync();
    }
}
