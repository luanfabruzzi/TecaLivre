using System.ComponentModel.DataAnnotations;

namespace TecaLivre.Api.Domain;

public class Usuario
{
    public int Id { get; set; }
    [Required, MaxLength(120)] public string Nome { get; set; } = string.Empty;
    [Required, MaxLength(80)] public string Login { get; set; } = string.Empty;
    [Required] public string SenhaHash { get; set; } = string.Empty;
    public PerfilUsuario Perfil { get; set; } = PerfilUsuario.Atendente;
    public bool Ativo { get; set; } = true;
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
}
