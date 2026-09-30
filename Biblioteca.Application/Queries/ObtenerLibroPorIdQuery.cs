using Biblioteca.Application.Abstractions.Repositories;
using Biblioteca.Application.Common;
using Biblioteca.Domain.Entities;
using MediatR;

namespace Biblioteca.Application.Queries;

public record ObtenerLibroPorIdQuery(Guid Id)
    : IRequest<Result<Libro>>;

public class ObtenerLibroPorIdQueryHandler
    : IRequestHandler<ObtenerLibroPorIdQuery, Result<Libro>>
{
    private readonly ILibroRepository _repository;

    public ObtenerLibroPorIdQueryHandler(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<Libro>> Handle(
        ObtenerLibroPorIdQuery request,
        CancellationToken cancellationToken)
    {
        var libro = await _repository.ObtenerPorIdAsync(
            request.Id,
            cancellationToken
        );

        if (libro is null)
            return Result<Libro>.Failure("Libro no encontrado.");

        return Result<Libro>.Success(libro);
    }
}

