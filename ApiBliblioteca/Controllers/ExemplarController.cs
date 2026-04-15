using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosExemplar;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
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
    public async Task<ActionResult<IEnumerable<ExemplarResponseDto>>> Get([FromQuery] QueryParameters parameters)
    {
        var metadata = await _exemplarService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("exemplarId", Name = "ObterExemplar")]
    public async Task<ActionResult<ExemplarResponseDto>> GetId(int exemplarId)
    {
        var exemplar = await _exemplarService.GetId(exemplarId);
        return Ok(exemplar);
    }

    [HttpPost]
    public async Task<ActionResult<ExemplarResponseDto>> Create(CreateExemplarDto dto)
    {
        var exemplarCriado = await _exemplarService.Create(dto);
        return CreatedAtRoute("ObterExemplar", new { id = exemplarCriado.Id }, exemplarCriado);
    }

    [HttpPut("{exemplarId}")]
    public async Task<ActionResult<ExemplarResponseDto>> Update(int exemplarId, UpdateExemplarDto dto)
    {
        var exemplarAtualizado = await _exemplarService.Update(exemplarId, dto);
        return Ok(exemplarAtualizado);
    }    

    [HttpDelete("{exemplarId}")]
    public async Task<ActionResult> Delete(int exemplarId)
    {
        await _exemplarService.Delete(exemplarId);
        return NoContent();
    }

    [HttpPost("{exemplarId}/danificar")]
    public async Task<ActionResult> DanificarExemplar(int exemplarId)
    {
        await _exemplarService.DanificarExemplar(exemplarId);
        return NoContent();
    }

    [HttpPost("{exemplarId}/perder")]
    public async Task<ActionResult> PerderExemplar(int exemplarId)
    {
        await _exemplarService.PerderExemplar(exemplarId);
        return NoContent();
    }
}
