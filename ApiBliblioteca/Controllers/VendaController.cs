using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosItemVenda;
using ApiBiblioteca.Application.DTOs.DtosVenda;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<IEnumerable<VendaResponseDto>>> GetVendas([FromQuery] QueryParameters parameters)
    {
        var metadata = await _vendaService.GetAll(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("id", Name = "ObterVenda")]
    public async Task<ActionResult<VendaResponseDto>> GetVendaPorId(int vendaId)
    {
        var venda = await _vendaService.GetId(vendaId);
        return Ok(venda);
    }

    [HttpGet("{vendaId}/Itens")]
    public async Task<ActionResult<IEnumerable<ItemVendaResponseDto>>> GetVendaComItens(int vendaId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _vendaService.GetComItens(vendaId, parameters);
        Response.AppendPaginationHeader(metadata);
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
