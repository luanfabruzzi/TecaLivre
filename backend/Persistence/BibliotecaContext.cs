using TecaLivre.Api.Domain;
using Microsoft.EntityFrameworkCore;

namespace TecaLivre.Api.Data;

public class BibliotecaContext(DbContextOptions<BibliotecaContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Livro> Livros => Set<Livro>();
    public DbSet<Exemplar> Exemplares => Set<Exemplar>();
    public DbSet<Emprestimo> Emprestimos => Set<Emprestimo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().HasIndex(x => x.Login).IsUnique();
        modelBuilder.Entity<Aluno>().HasIndex(x => x.Matricula).IsUnique();
        modelBuilder.Entity<Exemplar>().HasIndex(x => x.Codigo).IsUnique();
        modelBuilder.Entity<Livro>().HasIndex(x => x.Isbn);
        modelBuilder.Entity<Emprestimo>().HasIndex(x => x.ExemplarId)
            .IsUnique().HasFilter("DevolvidoEm IS NULL");
        modelBuilder.Entity<Emprestimo>().HasOne(x => x.RegistradoPorUsuario)
            .WithMany().HasForeignKey(x => x.RegistradoPorUsuarioId).OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Emprestimo>().HasOne(x => x.DevolvidoPorUsuario)
            .WithMany().HasForeignKey(x => x.DevolvidoPorUsuarioId).OnDelete(DeleteBehavior.Restrict);
    }
}
