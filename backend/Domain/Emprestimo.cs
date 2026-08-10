using System.ComponentModel.DataAnnotations;

namespace TecaLivre.Api.Domain;

public class Emprestimo
{
    public int Id { get; set; }
    public int AlunoId { get; set; }
    public int ExemplarId { get; set; }
    public int? RegistradoPorUsuarioId { get; set; }
    public int? DevolvidoPorUsuarioId { get; set; }
    public DateTime EmprestadoEm { get; set; }
    public DateTime PrevistoPara { get; set; }
    public DateTime? DevolvidoEm { get; set; }
    [MaxLength(500)] public string? Observacao { get; set; }
    public Aluno Aluno { get; set; } = null!;
    public Exemplar Exemplar { get; set; } = null!;
    public Usuario? RegistradoPorUsuario { get; set; }
    public Usuario? DevolvidoPorUsuario { get; set; }
    public bool EstaAtrasado(DateTime agora) => DevolvidoEm is null && PrevistoPara.Date < agora.Date;
}
