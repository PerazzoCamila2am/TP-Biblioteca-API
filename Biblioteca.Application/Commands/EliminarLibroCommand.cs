using Biblioteca.Application.Abstractions.Repositories;
using Biblioteca.Application.Common;
using MediatR;

namespace Biblioteca.Application.Commands;

public record EliminarLibroCommand(Guid Id)
    : IRequest<Result<bool>>;

public class EliminarLibroCommandHandler
    : IRequestHandler<EliminarLibroCommand, Result<bool>>
{
    private readonly ILibroRepository _repository;

    public EliminarLibroCommandHandler(ILibroRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(
        EliminarLibroCommand request,
        CancellationToken cancellationToken)
    {
        var libro = await _repository.ObtenerPorIdAsync(
            request.Id,
            cancellationToken);

        if (libro is null)
            return Result<bool>.Failure("Libro no encontrado.");

        await _repository.EliminarAsync(libro, cancellationToken);

        return Result<bool>.Success(true);
    }
}