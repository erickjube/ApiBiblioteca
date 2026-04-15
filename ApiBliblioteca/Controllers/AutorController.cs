using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosAutor;
using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutorController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AutorResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata = await _autorService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }   

    [HttpGet("{autorId}/livros")]
    public async Task<ActionResult<IEnumerable<LivroResponseDto>>> GetAutorComLivros(long autorId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _autorService.GetComLivros(autorId, parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{autorId}", Name = "ObterAutor")]
    public async Task<ActionResult<AutorResponseDto>> GetById(long autorId)
    {
        var dto = await _autorService.GetId(autorId);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<AutorResponseDto>> Create(AutorDto dto)
    {
        var autorCriado = await _autorService.Create(dto);
        return CreatedAtRoute("ObterAutor", new { id = autorCriado.Id }, autorCriado);
    }

    [HttpPut("{autorId}")]
    public async Task<ActionResult<AutorResponseDto>> Update(long autorId, AutorDto dto)
    {
        var dtoAtualizado = await _autorService.Update(autorId, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{autorId}")]
    public async Task<ActionResult> Delete(long autorId)
    {
        await _autorService.Delete(autorId);
        return NoContent();
    }
}
