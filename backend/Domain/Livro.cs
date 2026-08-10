using System.ComponentModel.DataAnnotations;

namespace TecaLivre.Api.Domain;

public class Livro
{
    public int Id { get; set; }
    [Required, MaxLength(200)] public string Titulo { get; set; } = string.Empty;
    [Required, MaxLength(150)] public string Autor { get; set; } = string.Empty;
    [MaxLength(20)] public string? Isbn { get; set; }
    [MaxLength(120)] public string? Editora { get; set; }
    public int? AnoPublicacao { get; set; }
    [MaxLength(80)] public string? Categoria { get; set; }
    [MaxLength(1000)] public string? Descricao { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public ICollection<Exemplar> Exemplares { get; set; } = [];
}
