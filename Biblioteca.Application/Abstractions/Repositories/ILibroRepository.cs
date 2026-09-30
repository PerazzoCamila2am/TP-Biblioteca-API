using Biblioteca.Domain.Entities;

namespace Biblioteca.Application.Abstractions.Repositories;

public interface ILibroRepository
{
    Task<Libro?> ObtenerPorIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task AgregarAsync(
        Libro libro,
        CancellationToken cancellationToken);

    Task ActualizarAsync(
        Libro libro,
        CancellationToken cancellationToken);

    Task EliminarAsync(
        Libro libro,
        CancellationToken cancellationToken);
}