using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<LivroResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata = await _livroService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{livroId}/Exemplares")]
    public async Task<ActionResult<LivroComExemplaresDto>> GetLivroComExemplares(long livroId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _livroService.GetComExemplares(livroId, parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{livroId}", Name = "ObterLivro")]
    public async Task<ActionResult<LivroResponseDto>> GetById(long livroId)
    {
        var dto = await _livroService.GetId(livroId);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<LivroResponseDto>> Create(CreateLivroDto dto)
    {
        var livroCriado = await _livroService.Create(dto);
        return CreatedAtRoute("ObterLivro", new { id = livroCriado.Id }, livroCriado);
    }

    [HttpPut("{livroId}")]
    public async Task<ActionResult<LivroResponseDto>> Update(long livroId, UpdateLivroDto dto)
    {
        var dtoAtualizado = await _livroService.Update(livroId, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{livroId}")]
    public async Task<ActionResult> Delete(long livroId)
    {
        await _livroService.Delete(livroId);
        return NoContent();
    }
}
