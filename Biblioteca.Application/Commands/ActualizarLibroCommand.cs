using Biblioteca.Application.Abstractions.Repositories;
using Biblioteca.Application.Common;
using MediatR;

namespace Biblioteca.Application.Commands;

public record ActualizarLibroCommand(
    Guid Id,
    string Titulo,
    string Autor,
    int AnioPublicacion,
    bool Disponible
) : IRequest<Result<bool>>;

public class ActualizarLibroCommandHandler
    : IRequestHandler<ActualizarLibroCommand, Result<bool>>
{
    private readonly ILibroRepository _repository;

    public ActualizarLibroCommandHandler(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(
        ActualizarLibroCommand request,
        CancellationToken cancellationToken)
    {
        var libro = await _repository.ObtenerPorIdAsync(
            request.Id,
            cancellationToken);

        if (libro is null)
            return Result<bool>.Failure("Libro no encontrado.");

        if (string.IsNullOrWhiteSpace(request.Titulo))
            return Result<bool>.Failure("El título es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Autor))
            return Result<bool>.Failure("El autor es obligatorio.");

        libro.Actualizar(
            request.Titulo,
            request.Autor,
            request.AnioPublicacion,
            request.Disponible);

        await _repository.ActualizarAsync(libro, cancellationToken);

        return Result<bool>.Success(true);
    }
}