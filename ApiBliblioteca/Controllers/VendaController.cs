using ApiBiblioteca.DTOs.DtosVenda;
using ApiBiblioteca.Interfaces;
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
    public async Task<ActionResult<DtoResponseVenda>> GetVendas()
    {
        var vendas = await _vendaService.ObterVendas();
        return Ok(vendas);
    }

    [HttpGet("id", Name = "ObterVenda")]
    public async Task<ActionResult<DtoResponseVenda>> GetVendaPorId(int vendaId)
    {
        var venda = await _vendaService.ObterVendaPorId(vendaId);
        return Ok(venda);
    }

    [HttpGet("{vendaId}/Itens")]
    public async Task<ActionResult<DtoResponseVendaComItens>> GetVendaComItens(int vendaId)
    {
        var venda = await _vendaService.ObterVendaComItens(vendaId);
        return Ok(venda);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseVenda>> Create(DtoCriarVenda dto)
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
