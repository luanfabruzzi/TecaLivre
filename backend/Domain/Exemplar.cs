using System.ComponentModel.DataAnnotations;

namespace TecaLivre.Api.Domain;

public class Exemplar
{
    public int Id { get; set; }
    public int LivroId { get; set; }
    [Required, MaxLength(50)] public string Codigo { get; set; } = string.Empty;
    public EstadoConservacao EstadoConservacao { get; set; } = EstadoConservacao.Bom;
    public SituacaoExemplar Situacao { get; set; } = SituacaoExemplar.Disponivel;
    [MaxLength(500)] public string? Observacao { get; set; }
    public Livro Livro { get; set; } = null!;
    public ICollection<Emprestimo> Emprestimos { get; set; } = [];
}
