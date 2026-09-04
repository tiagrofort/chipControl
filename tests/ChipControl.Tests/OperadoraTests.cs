using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure.Data.Repositories;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class OperadoraTests
{
    private readonly ChipControlDbContext _context;
    private readonly IOperadoraRepository _repo;
    private readonly IOperadoraUseCase _useCase;

    public OperadoraTests()
    {
        var options = new DbContextOptionsBuilder<ChipControlDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new ChipControlDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repo = new OperadoraRepository(_context);
        _useCase = new OperadoraUseCase(_repo);
    }

    [Fact]
    public void CriarOperadora_Valido_RetornaInstancia()
    {
        var op = Operadora.Create("Claro", "CL", "12.345.678/0001-90", "114002-8922", "contato@claro.com", "Obs teste", true);

        Assert.Equal("Claro", op.Nome);
        Assert.Equal("CL", op.Codigo);
        Assert.Equal("12.345.678/0001-90", op.Cnpj);
        Assert.Equal("114002-8922", op.Telefone);
        Assert.Equal("contato@claro.com", op.Email);
        Assert.Equal("Obs teste", op.Observacoes);
        Assert.True(op.Ativo);
    }

    [Fact]
    public void CriarOperadora_ComNomeVazio_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => Operadora.Create(""));
    }

    [Fact]
    public void CriarOperadora_ComNomeNulo_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() => Operadora.Create(null!));
    }

    [Fact]
    public void CriarOperadora_ComNomeMuitoLongo_LancaExcecao()
    {
        var nome = new string('A', 101);
        Assert.Throws<ArgumentException>(() => Operadora.Create(nome));
    }

    [Fact]
    public void CriarOperadora_ComCodigoMuitoLongo_LancaExcecao()
    {
        var codigo = new string('B', 21);
        Assert.Throws<ArgumentException>(() => Operadora.Create("Vivo", codigo: codigo));
    }

    [Fact]
    public void CriarOperadora_ComCnpjMuitoLongo_LancaExcecao()
    {
        var cnpj = new string('C', 21);
        Assert.Throws<ArgumentException>(() => Operadora.Create("Vivo", cnpj: cnpj));
    }

    [Fact]
    public void CriarOperadora_ComTelefoneMuitoLongo_LancaExcecao()
    {
        var telefone = new string('D', 31);
        Assert.Throws<ArgumentException>(() => Operadora.Create("Vivo", telefone: telefone));
    }

    [Fact]
    public void CriarOperadora_ComEmailMuitoLongo_LancaExcecao()
    {
        var email = new string('E', 256);
        Assert.Throws<ArgumentException>(() => Operadora.Create("Vivo", email: email));
    }

    [Fact]
    public void CriarOperadora_SemCamposOpcionais_RetornaInstancia()
    {
        var op = Operadora.Create("Tim");
        Assert.Equal("Tim", op.Nome);
        Assert.Null(op.Codigo);
        Assert.Null(op.Cnpj);
        Assert.Null(op.Telefone);
        Assert.Null(op.Email);
        Assert.Null(op.Observacoes);
        Assert.True(op.Ativo);
    }

    [Fact]
    public void CriarOperadora_InativoPorParametro_Respeitado()
    {
        var op = Operadora.Create("Nextel", ativo: false);
        Assert.False(op.Ativo);
    }

    [Fact]
    public void DefinirInativoEReativar_AlteraEstado()
    {
        var op = Operadora.Create("Oi");
        Assert.True(op.Ativo);
        op.DefinirInativo();
        Assert.False(op.Ativo);
        op.DefinirAtivo();
        Assert.True(op.Ativo);
    }

    [Fact]
    public void AtualizarDados_NomeVazio_LancaExcecao()
    {
        var op = Operadora.Create("Oi");
        Assert.Throws<ArgumentException>(() => op.AtualizarDados("", null, null, null, null, null, true));
    }

    [Fact]
    public void AtualizarDados_DadosValidos_PersisteAlteracoes()
    {
        var op = Operadora.Create("Oi");
        op.AtualizarDados("Oi Nova", "ON", "99.888.777/0001-66", null, null, "Atualizado", false);
        Assert.Equal("Oi Nova", op.Nome);
        Assert.Equal("ON", op.Codigo);
        Assert.Equal("99.888.777/0001-66", op.Cnpj);
        Assert.Equal("Atualizado", op.Observacoes);
        Assert.False(op.Ativo);
        Assert.NotNull(op.DataAlteracao);
    }

    [Fact]
    public async Task UseCase_Criar_ValidaNomeObrigatorio()
    {
        await Assert.ThrowsAsync<ArgumentException>(() =>
            _useCase.CriarAsync(new CriarOperadoraDto { Nome = "" }));
    }

    [Fact]
    public async Task UseCase_Criar_Duplicado_LancaExcecao()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Claro" });
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Claro" }));
    }

    [Fact]
    public async Task UseCase_Criar_CnpjDuplicado_LancaExcecao()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "OA", Cnpj = "11.222.333/0001-81" });
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _useCase.CriarAsync(new CriarOperadoraDto { Nome = "OB", Cnpj = "11.222.333/0001-81" }));
    }

    [Fact]
    public async Task UseCase_Criar_Valido_PersisteNoBanco()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Vivo", Codigo = "VIV" });
        var lista = await _repo.ListarAsync();
        Assert.Single(lista);
        Assert.Equal("Vivo", lista.First().Nome);
    }

    [Fact]
    public async Task UseCase_Editar_Inexistente_LancaExcecao()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _useCase.EditarAsync(new EditarOperadoraDto { Id = 9999, Nome = "Teste" }));
    }

    [Fact]
    public async Task UseCase_Editar_Duplicado_LancaExcecao()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Operadora X" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Operadora Y" });
        var ops = (await _repo.ListarAsync()).ToList();
        var opY = ops.First(o => o.Nome == "Operadora Y");
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _useCase.EditarAsync(new EditarOperadoraDto { Id = opY.Id, Nome = "Operadora X" }));
    }

    [Fact]
    public async Task UseCase_Editar_Valido_AtualizaDados()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Nome Antigo" });
        var id = (await _repo.ListarAsync()).First().Id;
        await _useCase.EditarAsync(new EditarOperadoraDto { Id = id, Nome = "Nome Novo", Codigo = "NN" });
        var buscado = await _useCase.BuscarPorIdAsync(id);
        Assert.Equal("Nome Novo", buscado!.Nome);
        Assert.Equal("NN", buscado.Codigo);
    }

    [Fact]
    public async Task UseCase_BuscarPorId_EncontraRegistro()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Busca" });
        var id = (await _repo.ListarAsync()).First().Id;
        var resultado = await _useCase.BuscarPorIdAsync(id);
        Assert.NotNull(resultado);
        Assert.Equal("Busca", resultado.Nome);
    }

    [Fact]
    public async Task UseCase_BuscarPorId_NaoExistente_RetornaNulo()
    {
        var resultado = await _useCase.BuscarPorIdAsync(999);
        Assert.Null(resultado);
    }

    [Fact]
    public async Task UseCase_Pesquisar_PorNome_RetornaResultados()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Claro Net" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Vivo Telecom" });
        var resultados = (await _useCase.PesquisarAsync("Claro")).ToList();
        Assert.Single(resultados);
        Assert.Equal("Claro Net", resultados[0].Nome);
    }

    [Fact]
    public async Task UseCase_Pesquisar_PorCnpj_RetornaResultados()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Op A", Cnpj = "11.222.333/0001-81" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Op B", Cnpj = "22.333.444/0001-91" });
        var resultados = (await _useCase.PesquisarAsync("11.222.333")).ToList();
        Assert.Single(resultados);
    }

    [Fact]
    public async Task UseCase_Pesquisar_TermoVazio_RetornaTodos()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "A" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "B" });
        var resultados = (await _useCase.PesquisarAsync("")).ToList();
        Assert.Equal(2, resultados.Count);
    }

    [Fact]
    public async Task UseCase_ListarAsync_RetornaTodosOrdenados()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Zelia" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Ana" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Marcos" });
        var lista = (await _useCase.ListarAsync()).ToList();
        Assert.Equal(3, lista.Count);
        Assert.Equal("Ana", lista[0].Nome);
        Assert.Equal("Marcos", lista[1].Nome);
        Assert.Equal("Zelia", lista[2].Nome);
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_AlteraEstado()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Toggle" });
        var id = (await _repo.ListarAsync()).First().Id;
        await _useCase.AlternarAtivoAsync(id);
        var op = await _useCase.BuscarPorIdAsync(id);
        Assert.False(op!.Ativo);
        await _useCase.AlternarAtivoAsync(id);
        op = await _useCase.BuscarPorIdAsync(id);
        Assert.True(op!.Ativo);
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_Inexistente_LancaExcecao()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.AlternarAtivoAsync(9999));
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_InativoPermaneceNoBanco()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Permanente" });
        var id = (await _repo.ListarAsync()).First().Id;
        await _useCase.AlternarAtivoAsync(id);
        var todos = await _repo.ListarAsync();
        Assert.Single(todos);
    }

    [Fact]
    public async Task UseCase_ListarAtivasAsync_SomenteAtivas()
    {
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Ativa 1" });
        await _useCase.CriarAsync(new CriarOperadoraDto { Nome = "Ativa 2" });
        var idInativa = (await _repo.ListarAsync()).First().Id;
        await _useCase.AlternarAtivoAsync(idInativa);
        var ativas = (await _useCase.ListarAtivasAsync()).ToList();
        Assert.All(ativas, o => Assert.True(o.Ativo));
    }

    [Fact]
    public async Task Repository_Listar_OrdenadoPorNome()
    {
        await _repo.AdicionarAsync(Operadora.Create("Zelia"));
        await _repo.AdicionarAsync(Operadora.Create("Ana"));
        await _repo.AdicionarAsync(Operadora.Create("Marcos"));
        var lista = (await _repo.ListarAsync()).ToList();
        Assert.Equal("Ana", lista[0].Nome);
        Assert.Equal("Marcos", lista[1].Nome);
        Assert.Equal("Zelia", lista[2].Nome);
    }

    [Fact]
    public async Task Repository_Pesquisar_PorCodigo_RetornaResultados()
    {
        await _repo.AdicionarAsync(Operadora.Create("Op Codigo", codigo: "COD123"));
        await _repo.AdicionarAsync(Operadora.Create("Op Sem Codigo"));
        var resultados = (await _repo.PesquisarAsync("COD123")).ToList();
        Assert.Single(resultados);
    }
}