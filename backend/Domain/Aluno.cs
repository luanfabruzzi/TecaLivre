using System.ComponentModel.DataAnnotations;

namespace TecaLivre.Api.Domain;

public class Aluno
{
    public int Id { get; set; }
    [Required, MaxLength(150)] public string Nome { get; set; } = string.Empty;
    [Required, MaxLength(30)] public string Matricula { get; set; } = string.Empty;
    [Required, MaxLength(80)] public string Turma { get; set; } = string.Empty;
    [MaxLength(30)] public string? Turno { get; set; }
    [MaxLength(30)] public string? TelefoneResponsavel { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public ICollection<Emprestimo> Emprestimos { get; set; } = [];
}
