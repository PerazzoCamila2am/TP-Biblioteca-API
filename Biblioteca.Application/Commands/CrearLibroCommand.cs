using Biblioteca.Application.Abstractions.Repositories;
using Biblioteca.Application.Common;
using Biblioteca.Domain.Entities;
using MediatR;

namespace Biblioteca.Application.Commands;

public record CrearLibroCommand(
    string Titulo,
    string Autor,
    int AnioPublicacion,
    bool Disponible
) : IRequest<Result<Guid>>;

public class CrearLibroCommandHandler
    : IRequestHandler<CrearLibroCommand, Result<Guid>>
{
    private readonly ILibroRepository _repository;

    public CrearLibroCommandHandler(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Guid>> Handle(
        CrearLibroCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Titulo))
        {
            return Result<Guid>.Failure(
                "El título es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(request.Autor))
        {
            return Result<Guid>.Failure(
                "El autor es obligatorio.");
        }

        if (request.AnioPublicacion <= 0)
        {
            return Result<Guid>.Failure(
                "El año de publicación no es válido.");
        }

        var libro = new Libro(
            request.Titulo,
            request.Autor,
            request.AnioPublicacion,
            request.Disponible);

        await _repository.AgregarAsync(
            libro,
            cancellationToken);

        return Result<Guid>.Success(libro.Id);
    }
}