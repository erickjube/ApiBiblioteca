using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlibliotecaSimples.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class AutorController : ControllerBase
{
    private readonly IAutorService _autorService;

    public AutorController(IAutorService autorService)
    {
        _autorService = autorService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoResponseAutor>>> GetAll()
    {
        var dtoAutores = await _autorService.Get();
        return Ok(dtoAutores);
    }

    [HttpGet("{id}/livros")]
    public async Task<ActionResult<DtoAutorComLivros>> GetAutorComLivros(long id)
    {
        var dto = await _autorService.GetComLivros(id);
        return Ok(dto);
    }

    [HttpGet("{id}", Name = "ObterAutor")]
    public async Task<ActionResult<DtoResponseAutor>> GetById(long id)
    {
        var dto = await _autorService.GetId(id);
        return Ok(dto);
    }

    [HttpGet("buscar")]
    public async Task<ActionResult<IEnumerable<DtoAutorComLivros>>> GetByNameComLivros([FromQuery] string nome)
    {
        var dtoAutores = await _autorService.GetByNameComLivros(nome);
        return Ok(dtoAutores);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseAutor>> Create(DtoAutor dto)
    {
        var autorCriado = await _autorService.Create(dto);
        return CreatedAtRoute("ObterAutor", new { id = autorCriado.Id }, autorCriado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DtoResponseAutor>> Update(long id, DtoAutor dto)
    {
        var dtoAtualizado = await _autorService.Update(id, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _autorService.Delete(id);
        return NoContent();
    }
}
