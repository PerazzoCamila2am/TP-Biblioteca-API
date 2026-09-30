using Biblioteca.Application.Commands;
using Biblioteca.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LibrosController : ControllerBase
{
    private readonly IMediator _mediator;

    public LibrosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Crear(
        [FromBody] CrearLibroCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return CreatedAtAction(
            nameof(ObtenerPorId),
            new { id = result.Value },
            new { id = result.Value });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new ObtenerLibroPorIdQuery(id),
            cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return Ok(result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(
        Guid id,
        [FromBody] ActualizarLibroRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ActualizarLibroCommand(
            id,
            request.Titulo,
            request.Autor,
            request.AnioPublicacion,
            request.Disponible);

        var result = await _mediator.Send(
            command,
            cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(
            new EliminarLibroCommand(id),
            cancellationToken);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return NoContent();
    }
}

public record ActualizarLibroRequest(
    string Titulo,
    string Autor,
    int AnioPublicacion,
    bool Disponible
);