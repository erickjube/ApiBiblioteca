using ApiBiblioteca.Application.DTOs.DtosItemVenda;
using ApiBiblioteca.Application.DTOs.DtosVenda;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;

    public VendaController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpGet]
    public async Task<ActionResult<VendaResponseDto>> GetVendas([FromQuery] QueryParameters parameters)
    {
        var metadata = await _vendaService.ObterVendas(parameters);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(new
        {
            metadata.TotalCount,
            metadata.PageSize,
            metadata.PageNumber,
            metadata.TotalPages,
            metadata.HasNext,
            metadata.HasPrevious
        }));

        return Ok(metadata.Data);
    }

    [HttpGet("id", Name = "ObterVenda")]
    public async Task<ActionResult<VendaResponseDto>> GetVendaPorId(int vendaId)
    {
        var venda = await _vendaService.ObterVendaPorId(vendaId);
        return Ok(venda);
    }

    [HttpGet("{vendaId}/Itens")]
    public async Task<ActionResult<ItemVendaResponseDto>> GetVendaComItens(int vendaId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _vendaService.ObterVendaComItens(vendaId, parameters);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(new
        {
            metadata.TotalCount,
            metadata.PageSize,
            metadata.PageNumber,
            metadata.TotalPages,
            metadata.HasNext,
            metadata.HasPrevious
        }));

        return Ok(metadata.Data);
    }

    [HttpPost]
    public async Task<ActionResult<VendaResponseDto>> Create(CreateVendaDto dto)
    {
        var vendaCriada = await _vendaService.Create(dto);
        return CreatedAtRoute("ObterVenda", new { id = vendaCriada.Id }, vendaCriada);
    }

    [HttpPost("{vendaId}/Cancelar")]
    public async Task<ActionResult> CancelarVenda(int vendaId)
    {
        await _vendaService.CancelarVenda(vendaId);
        return NoContent();
    }

    [HttpPost("{vendaId}/Finalizar")]
    public async Task<ActionResult> FinalizarVenda(int vendaId)
    {
        await _vendaService.FinalizarVenda(vendaId);
        return NoContent();
    }

    [HttpPost("{vendaId}/AdicionarItem")]
    public async Task<ActionResult> AdicionarItem(int vendaId, [FromQuery] int exemplarId)
    {
        await _vendaService.AdicionarItem(vendaId, exemplarId);
        return NoContent();
    }

    [HttpPost("{vendaId}/ExcluirItem")]
    public async Task<ActionResult> ExcluirItem(int vendaId, [FromQuery] int itemId)
    {
        await _vendaService.ExcluirItem(vendaId, itemId);
        return NoContent();
    }
}
