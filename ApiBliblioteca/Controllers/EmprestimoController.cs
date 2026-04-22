using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosItemEmprestimo;
using ApiBiblioteca.Application.Interfaces.Services;
using ApiBiblioteca.Application.Pagination;
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
    public async Task<ActionResult<IEnumerable<EmprestimoResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata = await _emprestimoService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{emprestimoId}/Itens")]
    public async Task<ActionResult<IEnumerable<EmprestimoResponseDto>>> GetEmprestimoComItens(int emprestimoId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _emprestimoService.GetComItens(emprestimoId, parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{emprestimoId}/multas")]
    public async Task<ActionResult<EmprestimoComMultasDto>> GetMultas(int emprestimoId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _emprestimoService.GetComMultas(emprestimoId, parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("id", Name = "ObterEmprestimo")]
    public async Task<ActionResult<EmprestimoResponseDto>> GetById(int emprestimoId)
    {
        var emprestimo = await _emprestimoService.GetEmprestimoById(emprestimoId);
        return Ok(emprestimo);
    }

    [HttpPost]
    public async Task<ActionResult<EmprestimoResponseDto>> Create(CreateEmprestimoDto dto)
    {
        var emprestimoCriado = await _emprestimoService.CreateEmprestimo(dto);
        return CreatedAtRoute("ObterEmprestimo", new { id = emprestimoCriado.Id }, emprestimoCriado);
    }

    [HttpPost("{emprestimoId}/Item")]
    public async Task<ActionResult<EmprestimoResponseDto>> AdicionarItem(int emprestimoId, int exemplarId)
    {
        var emprestimoAtualizado = await _emprestimoService.AdicionarItem(emprestimoId, exemplarId);
        return Ok(emprestimoAtualizado);
    }

    [HttpPatch("{emprestimoId}/itens/devolucao")]
    public async Task<ActionResult> DevolverItem( int emprestimoId, [FromQuery] DevolverItemEmprestimoDto dto)
    {
        await _emprestimoService.DevolverItem(emprestimoId, dto);
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
    public async Task<ActionResult> EstenderPrazoDevolucao(EstenderDevolucaoDto dto)
    {
        await _emprestimoService.EstenderPrazoDevolucao(dto);
        return NoContent();
    }
}
