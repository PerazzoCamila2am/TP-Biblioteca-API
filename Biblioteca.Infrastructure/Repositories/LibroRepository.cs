using Biblioteca.Application.Abstractions.Repositories;
using Biblioteca.Domain.Entities;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories;

public class LibroRepository : ILibroRepository
{
    private readonly AppDbContext _context;

    public LibroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Libro?> ObtenerPorIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Libros
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task AgregarAsync(
        Libro libro,
        CancellationToken cancellationToken)
    {
        await _context.Libros.AddAsync(libro, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(
        Libro libro,
        CancellationToken cancellationToken)
    {
        _context.Libros.Update(libro);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EliminarAsync(
        Libro libro,
        CancellationToken cancellationToken)
    {
        _context.Libros.Remove(libro);
        await _context.SaveChangesAsync(cancellationToken);
    }
}