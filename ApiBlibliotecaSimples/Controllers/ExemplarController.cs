using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlibliotecaSimples.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
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

    [HttpGet("nome")]
    public async Task<ActionResult<IEnumerable<DtoResponseExemplar>>> GetByName(string nome)
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

    [HttpPost("{id}/danificar")]
    public async Task<ActionResult> Danificar(int id)
    {
        await _exemplarService.Danificar(id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _exemplarService.Delete(id);
        return NoContent();
    }
}
