using ApiBiblioteca.DTOs.DtosCliente;
using ApiBiblioteca.Interfaces;
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
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> Get()
    {
        var dtoClientes = await _clienteService.Get();
        return Ok(dtoClientes);
    }

    [HttpGet("id", Name = "ObterCliente")]
    public async Task<ActionResult<ClienteResponseDto>> GetId(int id)
    {
        var dtoCliente = await _clienteService.GetId(id);
        return Ok(dtoCliente);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<ClienteResponseDto>>> GetByName([FromQuery] string nome)
    {
        var dtoCliente = await _clienteService.GetByName(nome);
        return Ok(dtoCliente);
    }

    [HttpGet("{id}/Emprestimos")]
    public async Task<ActionResult<ClienteComEmprestimosDto>> GetComEmprestimos(int id)
    {
        var dto = await _clienteService.GetClienteComEmprestimos(id);
        return Ok(dto);
    }

    [HttpGet("{id}/Vendas")]
    public async Task<ActionResult<ClienteComVendasDto>> GetComVendas(int id)
    {
        var dto = await _clienteService.GetClienteComVendas(id);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> Create(CreateClienteDto dto)
    {
        var cliente = await _clienteService.Create(dto);
        return CreatedAtRoute("ObterCliente", new { id = cliente.Id }, cliente);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateClienteDto>> Update(int id,  UpdateClienteDto dto)
    {
        var cliente = await _clienteService.Update(id, dto);
        return Ok(cliente);
    }

    [HttpDelete("id")]
    public async Task<ActionResult> Delete(int id)
    {
        await _clienteService.Delete(id);
        return NoContent();
    }
}
