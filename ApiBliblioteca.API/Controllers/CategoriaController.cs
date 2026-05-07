using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosCategoria;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata = await _categoriaService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{categoriaId}/livros")]
    public async Task<ActionResult<LivroResponseDto>> GetCategoriaComLivros(long categoriaId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _categoriaService.GetComLivros(categoriaId, parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }


    [HttpGet("{categoriaId}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaResponseDto>> GetById(long categoriaId)
    {
        var dto = await _categoriaService.GetId(categoriaId);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaResponseDto>> Create(CategoriaDto dto)
    {
        var categoriaCriada = await _categoriaService.Create(dto);
        return CreatedAtRoute("ObterCategoria", new { id = categoriaCriada.Id }, categoriaCriada);
    }

    [HttpPut("{categoriaId}")]
    public async Task<ActionResult<CategoriaResponseDto>> Update(long categoriaId, CategoriaDto dto)
    {
        var dtoAtualizado = await _categoriaService.Update(categoriaId, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{categoriaId}")]
    public async Task<ActionResult> Delete(long categoriaId)
    {
        await _categoriaService.Delete(categoriaId);
        return NoContent();
    }
}
