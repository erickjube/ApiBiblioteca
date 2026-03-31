using ApiBiblioteca.DTOs;
using ApiBiblioteca.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class ExemplarController : ControllerBase
{
    private readonly IExemplarService _exemplarService;

    public ExemplarController(IExemplarService exemplarService)
    {
        _exemplarService = exemplarService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoResponseExemplar>>> Get()
    {
        var exemplares = await _exemplarService.Get();
        return Ok(exemplares);
    }

    [HttpGet("id", Name = "ObterExemplar")]
    public async Task<ActionResult<DtoResponseExemplar>> GetId(int id)
    {
        var exemplar = await _exemplarService.GetId(id);
        return Ok(exemplar);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<DtoResponseExemplar>>> GetByName([FromQuery] string nome)
    {
        var exemplar = await _exemplarService.GetByName(nome);
        return Ok(exemplar);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseExemplar>> Create(DtoCriarExemplar dto)
    {
        var exemplarCriado = await _exemplarService.Create(dto);
        return CreatedAtRoute("ObterExemplar", new { id = exemplarCriado.Id }, exemplarCriado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DtoResponseExemplar>> Update(int id, DtoAtualizarExemplar dto)
    {
        var exemplarAtualizado = await _exemplarService.Update(id, dto);
        return Ok(exemplarAtualizado);
    }    

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _exemplarService.Delete(id);
        return NoContent();
    }

    [HttpPost("{id}/danificar")]
    public async Task<ActionResult> DanificarExemplar(int id)
    {
        await _exemplarService.DanificarExemplar(id);
        return NoContent();
    }

    [HttpPost("{id}/perder")]
    public async Task<ActionResult> PerderExemplar(int id)
    {
        await _exemplarService.PerderExemplar(id);
        return NoContent();
    }
}
