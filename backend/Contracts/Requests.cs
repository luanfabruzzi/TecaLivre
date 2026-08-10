using TecaLivre.Api.Domain;

namespace TecaLivre.Api.Contracts;

public record CriarAlunoRequest(string Nome, string Matricula, string Turma, string? Turno, string? TelefoneResponsavel);
public record CriarLivroRequest(string Titulo, string Autor, string? Isbn, string? Editora, int? AnoPublicacao, string? Categoria, string? Descricao);
public record CriarExemplarRequest(string Codigo, EstadoConservacao EstadoConservacao, string? Observacao);
public record CriarEmprestimoRequest(int AlunoId, int ExemplarId, string? Observacao);
public record DevolverEmprestimoRequest(string? Observacao);
