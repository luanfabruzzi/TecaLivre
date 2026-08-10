using TecaLivre.Api.Contracts;
using TecaLivre.Api.Data;
using TecaLivre.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TecaLivre.Api.Controllers;

[ApiController, Route("api/emprestimos")]
public class EmprestimosController(BibliotecaContext db, IConfiguration configuration) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Listar([FromQuery] bool? ativos)
    {
        var query = db.Emprestimos.AsNoTracking().Include(x => x.Aluno).Include(x => x.Exemplar).ThenInclude(x => x.Livro).AsQueryable();
        if (ativos == true) query = query.Where(x => x.DevolvidoEm == null);
        var agora = DateTime.UtcNow;
        return Ok((await query.OrderByDescending(x => x.EmprestadoEm).ToListAsync()).Select(x => new { x.Id, x.Aluno, x.Exemplar, x.EmprestadoEm, x.PrevistoPara, x.DevolvidoEm, x.Observacao, atrasado = x.EstaAtrasado(agora) }));
    }

    [HttpPost]
    public async Task<ActionResult> Criar(CriarEmprestimoRequest request)
    {
        var aluno = await db.Alunos.FindAsync(request.AlunoId);
        var exemplar = await db.Exemplares.FindAsync(request.ExemplarId);
        if (aluno is null || !aluno.Ativo) return BadRequest("Aluno inválido ou inativo.");
        if (exemplar is null || exemplar.Situacao != SituacaoExemplar.Disponivel) return BadRequest("Exemplar indisponível.");
        var agora = DateTime.UtcNow;
        var emprestimo = new Emprestimo { AlunoId = aluno.Id, ExemplarId = exemplar.Id, EmprestadoEm = agora, PrevistoPara = agora.AddDays(configuration.GetValue("PrazoEmprestimoDias", 30)), Observacao = request.Observacao };
        exemplar.Situacao = SituacaoExemplar.Emprestado;
        db.Add(emprestimo); await db.SaveChangesAsync(); return Created($"api/emprestimos/{emprestimo.Id}", emprestimo);
    }

    [HttpPost("{id:int}/devolucao")]
    public async Task<ActionResult> Devolver(int id, DevolverEmprestimoRequest request)
    {
        var emprestimo = await db.Emprestimos.Include(x => x.Exemplar).SingleOrDefaultAsync(x => x.Id == id);
        if (emprestimo is null) return NotFound();
        if (emprestimo.DevolvidoEm is not null) return Conflict("Empréstimo já devolvido.");
        emprestimo.DevolvidoEm = DateTime.UtcNow;
        emprestimo.Observacao = request.Observacao ?? emprestimo.Observacao;
        emprestimo.Exemplar.Situacao = SituacaoExemplar.Disponivel;
        await db.SaveChangesAsync(); return NoContent();
    }
}
