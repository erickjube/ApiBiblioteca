using ApiBiblioteca.Application.DTOs.DtosLivro;
using ApiBiblioteca.Application.Interfaces.IServices;
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
    public async Task<ActionResult<IEnumerable<LivroResponseDto>>> GetAll()
    {
        var dtoLivros = await _livroService.Get();
        return Ok(dtoLivros);
    }

    [HttpGet("{id}", Name = "ObterLivro")]
    public async Task<ActionResult<LivroResponseDto>> GetById(long id)
    {
         var dto = await _livroService.GetId(id);
        return Ok(dto);
    }

    [HttpGet("{id}/Exemplares")]
    public async Task<ActionResult<LivroComExemplaresDto>> GetLivroComExemplares(long id)
    {
        var dto = await _livroService.GetLivroComExemplares(id);
        return Ok(dto);
    }

    [HttpGet("buscar")]
    public async Task<IEnumerable<LivroComExemplaresDto>> GetByNameComExemplares([FromQuery] string titulo)
    {
        var dtos = await _livroService.GetByNameComExemplares(titulo);
        return dtos;
    }

    [HttpPost]
    public async Task<ActionResult<LivroResponseDto>> Create(CreateLivroDto dto)
    {
        var livroCriado = await _livroService.Create(dto);
        return CreatedAtRoute("ObterLivro", new { id = livroCriado.Id }, livroCriado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<LivroResponseDto>> Update(long id, UpdateLivroDto dto)
    {
        var dtoAtualizado = await _livroService.Update(id, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _livroService.Delete(id);
        return NoContent();
    }
}
