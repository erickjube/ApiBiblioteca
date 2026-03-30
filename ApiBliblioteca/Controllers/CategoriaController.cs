using ApiBiblioteca.DTOs;
using ApiBiblioteca.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IEnumerable<DtoResponseCategoria>>> GetAll()
    {
        var dtoCategorias = await _categoriaService.Get();
        return Ok(dtoCategorias);
    }

    [HttpGet("{id}/livros")]
    public async Task<ActionResult<DtoCategoriaComLivros>> GetCategoriaComLivros(long id)
    {
        var dto = await _categoriaService.GetComLivros(id);
        return Ok(dto);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<DtoCategoriaComLivros>>> GetByNameComLivros([FromQuery] string nome)
    {
        var dtoCategorias = await _categoriaService.GetByNameComLivros(nome);
        return Ok(dtoCategorias);
    }

    [HttpGet("{id}", Name = "ObterCategoria")]
    public async Task<ActionResult<DtoResponseCategoria>> GetById(long id)
    {
        var dto = await _categoriaService.GetId(id);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseCategoria>> Create(DtoCategoria dto)
    {
        var categoriaCriada = await _categoriaService.Create(dto);
        return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.Id }, categoriaCriada);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DtoResponseCategoria>> Update(long id, DtoCategoria dto)
    {
        var dtoAtualizado = await _categoriaService.Update(id, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _categoriaService.Delete(id);
        return NoContent();
    }
}
