using TecaLivre.Api.Contracts;
using TecaLivre.Api.Data;
using TecaLivre.Api.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TecaLivre.Api.Controllers;

[ApiController, Route("api/livros")]
public class LivrosController(BibliotecaContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> Listar([FromQuery] string? busca) => Ok(await db.Livros.AsNoTracking()
        .Include(x => x.Exemplares).Where(x => busca == null || x.Titulo.Contains(busca) || x.Autor.Contains(busca))
        .OrderBy(x => x.Titulo).ToListAsync());

    [HttpPost]
    public async Task<ActionResult> Criar(CriarLivroRequest request)
    {
        var livro = new Livro { Titulo = request.Titulo.Trim(), Autor = request.Autor.Trim(), Isbn = request.Isbn, Editora = request.Editora, AnoPublicacao = request.AnoPublicacao, Categoria = request.Categoria, Descricao = request.Descricao };
        db.Add(livro); await db.SaveChangesAsync(); return Created($"api/livros/{livro.Id}", livro);
    }

    [HttpPost("{livroId:int}/exemplares")]
    public async Task<ActionResult> CriarExemplar(int livroId, CriarExemplarRequest request)
    {
        if (!await db.Livros.AnyAsync(x => x.Id == livroId)) return NotFound("Livro não encontrado.");
        if (await db.Exemplares.AnyAsync(x => x.Codigo == request.Codigo)) return Conflict("Código já cadastrado.");
        var exemplar = new Exemplar { LivroId = livroId, Codigo = request.Codigo.Trim(), EstadoConservacao = request.EstadoConservacao, Observacao = request.Observacao };
        db.Add(exemplar); await db.SaveChangesAsync(); return Created($"api/livros/{livroId}/exemplares/{exemplar.Id}", exemplar);
    }
}
