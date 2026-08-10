# TecaLivre

**A sua biblioteca open source.**

Sistema livre e local para escolas administrarem acervo, alunos, empréstimos e devoluções.

> Projeto em reconstrução. A API, o novo banco e a fundação do frontend já estão em desenvolvimento; ainda não use esta versão com dados reais em produção.

## Objetivo

Oferecer uma solução gratuita, simples e open source para bibliotecas escolares que não dependam de serviços em nuvem. A instalação poderá funcionar em um único computador ou em um servidor dentro da rede local da escola.

## Funcionalidades planejadas

- cadastro de alunos por nome, matrícula e turma;
- catálogo de livros com múltiplos exemplares;
- empréstimos com devolução prevista automaticamente em 30 dias;
- registro de devoluções e histórico permanente;
- acompanhamento de atrasos;
- usuários administradores e atendentes;
- busca, relatórios, importação e exportação;
- backup e restauração do banco local.

## Tecnologias

- Backend: ASP.NET Core Web API, .NET 10 e Entity Framework Core;
- Banco: SQLite;
- Frontend: React 19, TypeScript, Vite e TanStack Query.

## Estrutura

```text
backend/    API, domínio, migrations e acesso ao SQLite
frontend/   aplicação React
```

## Requisitos

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) 22 ou mais recente
- npm 10 ou mais recente

## Executando localmente

Em um terminal:

```bash
cd backend
dotnet restore
dotnet run
```

Em outro terminal:

```bash
cd frontend
npm install
npm run dev
```

Abra `http://localhost:5173`. A API será executada em `http://localhost:5214`.

Na primeira inicialização, as migrations criam automaticamente `backend/data/tecalivre.db`.

## Regra de empréstimo

Cada exemplar só pode possuir um empréstimo ativo. A data prevista é calculada pela API usando o prazo configurado em `PrazoEmprestimoDias`, cujo padrão é 30. Um empréstimo fica atrasado quando não foi devolvido e sua data prevista já passou.

## Dados e privacidade

Arquivos `.db` não são versionados. Nunca publique bancos utilizados por escolas: eles podem conter dados pessoais de alunos e funcionários. Dados demonstrativos futuros serão totalmente fictícios.

## Estado atual

- [x] novo modelo de domínio;
- [x] API inicial para alunos, livros, exemplares e empréstimos;
- [x] base visual e navegação React;
- [ ] autenticação e perfis de acesso;
- [ ] formulários completos no frontend;
- [ ] testes automatizados;
- [ ] backup, restauração e importação CSV;
- [ ] documentação de instalação para escolas.

## Licença

A licença open source ainda será definida antes da primeira versão pública. A sugestão inicial é MIT, por ser simples e permissiva.
