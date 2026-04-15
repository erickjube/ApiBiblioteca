using ApiBiblioteca.API.Header;
using ApiBiblioteca.Application.DTOs.DtosCliente;
using ApiBiblioteca.Application.DTOs.DtosEmprestimo;
using ApiBiblioteca.Application.DTOs.DtosVenda;
using ApiBiblioteca.Application.Interfaces.IServices;
using ApiBiblioteca.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBiblioteca.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "Funcionarios")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetAll([FromQuery] QueryParameters parameters)
    {
        var metadata = await _clienteService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{clienteId}/Emprestimos")]
    public async Task<ActionResult<IEnumerable<EmprestimoResponseDto>>> GetComEmprestimos(int clienteId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _clienteService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("{clienteId}/Vendas")]
    public async Task<ActionResult<IEnumerable<VendaResponseDto>>> GetComVendas(int clienteId, [FromQuery] QueryParameters parameters)
    {
        var metadata = await _clienteService.Get(parameters);
        Response.AppendPaginationHeader(metadata);
        return Ok(metadata.Data);
    }

    [HttpGet("clienteId", Name = "ObterCliente")]
    public async Task<ActionResult<ClienteResponseDto>> GetId(int clienteId)
    {
        var dtoCliente = await _clienteService.GetId(clienteId);
        return Ok(dtoCliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Create(CreateClienteDto dto)
    {
        var cliente = await _clienteService.Create(dto);
        return CreatedAtRoute("ObterCliente", new { id = cliente.Id }, cliente);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateClienteDto>> Update(int clienteId,  UpdateClienteDto dto)
    {
        var cliente = await _clienteService.Update(clienteId, dto);
        return Ok(cliente);
    }

    [HttpDelete("clienteId")]
    public async Task<ActionResult> Delete(int clienteId)
    {
        await _clienteService.Delete(clienteId);
        return NoContent();
    }
}
