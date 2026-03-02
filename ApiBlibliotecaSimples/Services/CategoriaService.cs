using ApiBibliotecaSimples.Domain.Entities;
using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Exceptions;
using ApiBlibliotecaSimples.Interfaces;
using AutoMapper;

namespace ApiBlibliotecaSimples.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
        _categoriaRepository = categoriaRepository;
        _mapper = mapper;
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
        await _categoriaRepository.SaveAsync();
        return _mapper.Map<DtoResponseCategoria>(categoria);
    }

    public async Task<DtoResponseCategoria> Update(long id, DtoCategoria dto)
    {
        if (dto is null) throw new BadRequestException("Categoria inválida!");
        var categoria = await _categoriaRepository.GetByIdAsync(id) ?? throw new NotFoundException("Categoria não encontrada!");
        categoria.AtualizarNome(dto.Nome);
        await _categoriaRepository.SaveAsync();
        return _mapper.Map<DtoResponseCategoria>(categoria);
    }
    
    public async Task Delete(long id)
    {
        if (id <= 0) throw new BadRequestException("Id inválido!");
        var categoria = _categoriaRepository.GetByIdAsync(id).Result ?? throw new NotFoundException("Categoria não encontrada!");
        categoria.ValidarExclusao();
        _categoriaRepository.Remove(categoria);
        await _categoriaRepository.SaveAsync();
    }
}
