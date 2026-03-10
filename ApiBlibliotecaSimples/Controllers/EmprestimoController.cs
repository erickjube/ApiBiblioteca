using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlibliotecaSimples.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
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
    public async Task<ActionResult<DtoResponseEmprestimoComItens>> GetById(int id)
    {
        var emprestimo = await _emprestimoService.GetEmprestimoById(id);
        return Ok(emprestimo);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseEmprestimo>> Create(DtoCriarEmprestimo dto)
    {
        var emprestimoCriado = await _emprestimoService.CreateEmprestimo(dto);
        return CreatedAtRoute("ObterEmprestimo", new { id = emprestimoCriado.Id }, emprestimoCriado);
    }

    [HttpPost("{emprestimoId}/Item")]
    public async Task<ActionResult<DtoResponseEmprestimo>> AdicionarItem(int emprestimoId, int exemplarId)
    {
        var emprestimoAtualizado = await _emprestimoService.AdicionarItem(emprestimoId, exemplarId);
        return Ok(emprestimoAtualizado);
    }

    [HttpPost("{emprestimoId}/DevolverItem")]
    public async Task<ActionResult> DevolverItem(int emprestimoId, int itemId)
    {
        await _emprestimoService.DevolverItem(emprestimoId, itemId);
        return NoContent();
    }

    [HttpPost("{emprestimoId}/marcar-item-perdido")]
    public async Task<ActionResult> MarcarItemComoPerdido(int emprestimoId, int itemId)
    {
        await _emprestimoService.MarcarItemComoPerdido(emprestimoId, itemId);
        return NoContent();
    }

    [HttpPost("{emprestimoId}/marcar-item-danificado")]
    public async Task<ActionResult> MarcarItemComoDanificado(int emprestimoId, int itemId)
    {
        await _emprestimoService.MarcarItemComoDanificado(emprestimoId, itemId);
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
