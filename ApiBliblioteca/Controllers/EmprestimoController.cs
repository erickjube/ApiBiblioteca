using ApiBiblioteca.DTOs;
using ApiBiblioteca.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class EmprestimoController : ControllerBase
{
    private readonly IEmprestimoService _emprestimoService;

    public EmprestimoController(IEmprestimoService emprestimoService)
    {
        _emprestimoService = emprestimoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoResponseEmprestimo>>> GetAll()
    {
        var emprestimos = await _emprestimoService.GetAllEmprestimos();
        return Ok(emprestimos);
    }

    [HttpGet("id", Name = "ObterEmprestimo")]
    public async Task<ActionResult<DtoResponseEmprestimo>> GetById(int emprestimoId)
    {
        var emprestimo = await _emprestimoService.GetEmprestimoById(emprestimoId);
        return Ok(emprestimo);
    }

    [HttpGet("{emprestimoId}/Itens")]
    public async Task<ActionResult<DtoResponseEmprestimo>> GetEmprestimoComItens(int emprestimoId)
    {
        var emprestimo = await _emprestimoService.GetEmprestimoComItens(emprestimoId);
        return Ok(emprestimo);
    }

    [HttpGet("{emprestimoId}/multas")]
    public async Task<ActionResult<DtoResponseEmprestimoComMultas>> GetMultas(int emprestimoId)
    {
        var emprestimo = await _emprestimoService.GetMultas(emprestimoId);
        return Ok(emprestimo);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseEmprestimo>> Create(int clienteId)
    {
        var emprestimoCriado = await _emprestimoService.CreateEmprestimo(clienteId);
        return CreatedAtRoute("ObterEmprestimo", new { id = emprestimoCriado.Id }, emprestimoCriado);
    }

    [HttpPost("{emprestimoId}/Item")]
    public async Task<ActionResult<DtoResponseEmprestimo>> AdicionarItem(int emprestimoId, int exemplarId)
    {
        var emprestimoAtualizado = await _emprestimoService.AdicionarItem(emprestimoId, exemplarId);
        return Ok(emprestimoAtualizado);
    }

    [HttpPatch("{emprestimoId}/itens/devolucao")]
    public async Task<ActionResult> DevolverItem( int emprestimoId, [FromBody] DtoDevolverItemRequest request)
    {
        await _emprestimoService.DevolverItem(emprestimoId, request.ItemId, request.Condicao);
        return NoContent();
    }

    [HttpPost("{emprestimoId}/finalizar")]
    public async Task<ActionResult> FinalizarEmprestimo(int emprestimoId)
    {
        await _emprestimoService.FinalizarEmprestimo(emprestimoId);
        return NoContent();
    }

    [HttpPost("{emprestimoId}/cancelar")]
    public async Task<ActionResult> CancelarEmprestimo(int emprestimoId)
    {
        await _emprestimoService.CancelarEmprestimo(emprestimoId);
        return NoContent();
    }

    [HttpPost("{emprestimoId}/estender-prazo")]
    public async Task<ActionResult> EstenderPrazoDevolucao(int emprestimoId, DateOnly novoPrazoDevolucao)
    {
        await _emprestimoService.EstenderPrazoDevolucao(emprestimoId, novoPrazoDevolucao);
        return NoContent();
    }
}
