namespace Biblioteca.Domain.Entities;

public class Libro
{
    public Guid Id { get; private set; }

    public string Titulo { get; private set; } = null!;

    public string Autor { get; private set; } = null!;

    public int AnioPublicacion { get; private set; }

    public bool Disponible { get; private set; }

    private Libro()
    {
    }

    public Libro(
        string titulo,
        string autor,
        int anioPublicacion,
        bool disponible = true)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título es obligatorio.", nameof(titulo));

        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("El autor es obligatorio.", nameof(autor));

        if (anioPublicacion <= 0)
            throw new ArgumentException("El año de publicación debe ser válido.", nameof(anioPublicacion));

        Id = Guid.NewGuid();
        Titulo = titulo;
        Autor = autor;
        AnioPublicacion = anioPublicacion;
        Disponible = disponible;
    }

    public void Actualizar(
        string titulo,
        string autor,
        int anioPublicacion,
        bool disponible)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("El título es obligatorio.", nameof(titulo));

        if (string.IsNullOrWhiteSpace(autor))
            throw new ArgumentException("El autor es obligatorio.", nameof(autor));

        if (anioPublicacion <= 0)
            throw new ArgumentException("El año de publicación debe ser válido.", nameof(anioPublicacion));

        Titulo = titulo;
        Autor = autor;
        AnioPublicacion = anioPublicacion;
        Disponible = disponible;
    }
}