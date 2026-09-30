using Biblioteca.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Libro> Libros => Set<Libro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Libro>(entity =>
        {
            entity.ToTable("Libros");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.Autor)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(x => x.AnioPublicacion)
                .IsRequired();

            entity.Property(x => x.Disponible)
                .IsRequired();
        });
    }
}