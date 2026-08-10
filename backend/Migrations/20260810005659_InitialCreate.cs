using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TecaLivre.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Matricula = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Turma = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Turno = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    TelefoneResponsavel = table.Column<string>(type: "TEXT", maxLength: 30, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Titulo = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Autor = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    Editora = table.Column<string>(type: "TEXT", maxLength: 120, nullable: true),
                    AnoPublicacao = table.Column<int>(type: "INTEGER", nullable: true),
                    Categoria = table.Column<string>(type: "TEXT", maxLength: 80, nullable: true),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    Login = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    SenhaHash = table.Column<string>(type: "TEXT", nullable: false),
                    Perfil = table.Column<int>(type: "INTEGER", nullable: false),
                    Ativo = table.Column<bool>(type: "INTEGER", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exemplares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LivroId = table.Column<int>(type: "INTEGER", nullable: false),
                    Codigo = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    EstadoConservacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Situacao = table.Column<int>(type: "INTEGER", nullable: false),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exemplares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exemplares_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Emprestimos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AlunoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ExemplarId = table.Column<int>(type: "INTEGER", nullable: false),
                    RegistradoPorUsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    DevolvidoPorUsuarioId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmprestadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PrevistoPara = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DevolvidoEm = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Observacao = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Exemplares_ExemplarId",
                        column: x => x.ExemplarId,
                        principalTable: "Exemplares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Usuarios_DevolvidoPorUsuarioId",
                        column: x => x.DevolvidoPorUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Emprestimos_Usuarios_RegistradoPorUsuarioId",
                        column: x => x.RegistradoPorUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alunos_Matricula",
                table: "Alunos",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_AlunoId",
                table: "Emprestimos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_DevolvidoPorUsuarioId",
                table: "Emprestimos",
                column: "DevolvidoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_ExemplarId",
                table: "Emprestimos",
                column: "ExemplarId",
                unique: true,
                filter: "DevolvidoEm IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimos_RegistradoPorUsuarioId",
                table: "Emprestimos",
                column: "RegistradoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Exemplares_Codigo",
                table: "Exemplares",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exemplares_LivroId",
                table: "Exemplares",
                column: "LivroId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_Isbn",
                table: "Livros",
                column: "Isbn");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Login",
                table: "Usuarios",
                column: "Login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimos");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Exemplares");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
