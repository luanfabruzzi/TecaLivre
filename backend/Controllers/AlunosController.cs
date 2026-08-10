using TecaLivre.Api.Contracts;
using TecaLivre.Api.Data;
using TecaLivre.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TecaLivre.Api.Controllers;

[ApiController, Route("api/alunos")]
public class AlunosController(BibliotecaContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Listar([FromQuery] string? busca) => Ok(await db.Alunos.AsNoTracking()
        .Where(x => busca == null || x.Nome.Contains(busca) || x.Matricula.Contains(busca))
        .OrderBy(x => x.Nome).ToListAsync());

    [HttpPost]
    public async Task<ActionResult> Criar(CriarAlunoRequest request)
    {
        if (await db.Alunos.AnyAsync(x => x.Matricula == request.Matricula)) return Conflict("Matrícula já cadastrada.");
        var aluno = new Aluno { Nome = request.Nome.Trim(), Matricula = request.Matricula.Trim(), Turma = request.Turma.Trim(), Turno = request.Turno, TelefoneResponsavel = request.TelefoneResponsavel };
        db.Add(aluno); await db.SaveChangesAsync();
        return CreatedAtAction(nameof(Listar), new { id = aluno.Id }, aluno);
    }
}
