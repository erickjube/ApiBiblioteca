using ApiBlibliotecaSimples.DTOs;
using ApiBlibliotecaSimples.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlibliotecaSimples.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoResponseLivro>>> GetAll()
    {
        var dtoLivros = await _livroService.Get();
        return Ok(dtoLivros);
    }

    [HttpGet("{id}", Name = "ObterLivro")]
    public async Task<ActionResult<DtoResponseLivro>> GetById(long id)
    {
         var dto = await _livroService.GetId(id);
        return Ok(dto);
    }

    [HttpGet("{titulo}")]
    public async Task<ActionResult<IEnumerable<DtoResponseLivro>>> GetByName(string titulo)
    {
        var dtos = await _livroService.GetByName(titulo);
        return Ok(dtos);
    }

    [HttpPost]
    public async Task<ActionResult<DtoResponseLivro>> Create(DtoCriarLivro dto)
    {
        var livroCriado = await _livroService.Create(dto);
        return CreatedAtRoute("ObterLivro", new { id = livroCriado.Id }, livroCriado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<DtoResponseLivro>> Update(long id, DtoAtualizarLivro dto)
    {
        var dtoAtualizado = await _livroService.Update(id, dto);
        return Ok(dtoAtualizado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(long id)
    {
        await _livroService.Delete(id);
        return NoContent();
    }
}
