using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure.Data.Repositories;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class FuncionarioTests
{
    private readonly ChipControlDbContext _context;
    private readonly IFuncionarioRepository _repo;
    private readonly IFuncionarioUseCase _useCase;

    public FuncionarioTests()
    {
        var options = new DbContextOptionsBuilder<ChipControlDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new ChipControlDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repo = new FuncionarioRepository(_context);
        _useCase = new FuncionarioUseCase(_repo);
    }

    [Fact]
    public void CriarFuncionario_Valido_RetornaInstancia()
    {
        var f = Funcionario.Create("João Silva", "TI", "123", "Analista", "1199999-0000", "joao@test.com", true, "obs");

        Assert.Equal("João Silva", f.NomeCompleto);
        Assert.Equal("TI", f.Setor);
        Assert.Equal("123", f.Matricula);
        Assert.Equal("Analista", f.Cargo);
        Assert.Equal("1199999-0000", f.TelefonePessoal);
        Assert.Equal("joao@test.com", f.Email);
        Assert.True(f.Ativo);
        Assert.Equal("obs", f.Observacoes);
    }

    [Fact]
    public void CriarFuncionario_ComNomeVazio_LancaExcecao()
    {
        Assert.Throws<System.ArgumentException>(() =>
            Funcionario.Create("", "TI"));
    }

    [Fact]
    public void CriarFuncionario_ComSetorVazio_LancaExcecao()
    {
        Assert.Throws<System.ArgumentException>(() =>
            Funcionario.Create("Maria", ""));
    }

    [Fact]
    public void CriarFuncionario_SemCamposOpcionais_RetornaInstancia()
    {
        var f = Funcionario.Create("Carlos", "RH");

        Assert.Null(f.Matricula);
        Assert.Null(f.Cargo);
        Assert.Null(f.TelefonePessoal);
        Assert.Null(f.Email);
        Assert.Null(f.Observacoes);
        Assert.True(f.Ativo);
    }

    [Fact]
    public void CriarFuncionario_InativoPorParametro_Respeitado()
    {
        var f = Funcionario.Create("Pedro", "Ops", ativo: false);
        Assert.False(f.Ativo);
    }

    [Fact]
    public void DefinirInativoEReativar_AlteraEstado()
    {
        var f = Funcionario.Create("Ana", "TI");
        Assert.True(f.Ativo);

        f.DefinirInativo();
        Assert.False(f.Ativo);

        f.DefinirAtivo();
        Assert.True(f.Ativo);
    }

    [Fact]
    public void AtualizarDados_NomeVazio_LancaExcecao()
    {
        var f = Funcionario.Create("Maria", "TI");
        Assert.Throws<System.ArgumentException>(() =>
            f.AtualizarDados("", "TI", null, null, null, null, true, null));
    }

    [Fact]
    public void AtualizarDados_SetorVazio_LancaExcecao()
    {
        var f = Funcionario.Create("Maria", "TI");
        Assert.Throws<System.ArgumentException>(() =>
            f.AtualizarDados("Maria", "", null, null, null, null, true, null));
    }

    [Fact]
    public void AtualizarDados_DadosValidos_PersisteAlteracoes()
    {
        var f = Funcionario.Create("Maria", "TI", "001");
        f.AtualizarDados("Maria Souza", "RH", "002", "Gerente", "11111-1111", "maria@x.com", false, "atualizado");

        Assert.Equal("Maria Souza", f.NomeCompleto);
        Assert.Equal("RH", f.Setor);
        Assert.Equal("002", f.Matricula);
        Assert.Equal("Gerente", f.Cargo);
        Assert.Equal("11111-1111", f.TelefonePessoal);
        Assert.Equal("maria@x.com", f.Email);
        Assert.False(f.Ativo);
        Assert.Equal("atualizado", f.Observacoes);
    }

    [Fact]
    public async Task UseCase_CriarFuncionario_Valido_RetornaTrueEPersiste()
    {
        var dto = new CriarFuncionarioDto
        {
            NomeCompleto = "Maria",
            Setor = "TI",
            Matricula = "001",
            Cargo = "Dev",
            Email = "m@test.com"
        };

        var ok = await _useCase.CriarAsync(dto);
        Assert.True(ok);

        var lista = await _repo.ListarAsync();
        Assert.Single(lista);
        Assert.Equal("Maria", lista.First().NomeCompleto);
    }

    [Fact]
    public async Task UseCase_CriarFuncionario_NomeVazio_LancaExcecao()
    {
        var dto = new CriarFuncionarioDto { NomeCompleto = "", Setor = "TI" };
        await Assert.ThrowsAsync<System.ArgumentException>(() => _useCase.CriarAsync(dto));
    }

    [Fact]
    public async Task UseCase_CriarFuncionario_SetorVazio_LancaExcecao()
    {
        var dto = new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "" };
        await Assert.ThrowsAsync<System.ArgumentException>(() => _useCase.CriarAsync(dto));
    }

    [Fact]
    public async Task UseCase_EditarFuncionario_AtualizaDados()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto
        {
            NomeCompleto = "Maria",
            Setor = "TI"
        });

        var lista = await _repo.ListarAsync();
        var id = lista.First().Id;

        await _useCase.EditarAsync(new EditarFuncionarioDto
        {
            Id = id,
            NomeCompleto = "Maria Souza",
            Setor = "RH",
            Matricula = "M1",
            Cargo = "Coord",
            Ativo = true
        });

        var atualizado = await _repo.BuscarPorIdAsync(id);
        Assert.NotNull(atualizado);
        Assert.Equal("Maria Souza", atualizado.NomeCompleto);
        Assert.Equal("RH", atualizado.Setor);
        Assert.Equal("M1", atualizado.Matricula);
        Assert.Equal("Coord", atualizado.Cargo);
    }

    [Fact]
    public async Task UseCase_EditarFuncionario_NomeVazio_LancaExcecao()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "TI" });
        var id = (await _repo.ListarAsync()).First().Id;

        await Assert.ThrowsAsync<System.ArgumentException>(() => _useCase.EditarAsync(new EditarFuncionarioDto
        {
            Id = id,
            NomeCompleto = "",
            Setor = "TI"
        }));
    }

    [Fact]
    public async Task UseCase_EditarFuncionario_SetorVazio_LancaExcecao()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "TI" });
        var id = (await _repo.ListarAsync()).First().Id;

        await Assert.ThrowsAsync<System.ArgumentException>(() => _useCase.EditarAsync(new EditarFuncionarioDto
        {
            Id = id,
            NomeCompleto = "Maria",
            Setor = ""
        }));
    }

    [Fact]
    public async Task UseCase_EditarFuncionario_Inexistente_LancaExcecao()
    {
        await Assert.ThrowsAsync<System.InvalidOperationException>(() => _useCase.EditarAsync(new EditarFuncionarioDto
        {
            Id = 9999,
            NomeCompleto = "X",
            Setor = "Y"
        }));
    }

    [Fact]
    public async Task UseCase_BuscarPorId_Existente_RetornaDto()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "TI" });
        var id = (await _repo.ListarAsync()).First().Id;

        var dto = await _useCase.BuscarPorIdAsync(id);
        Assert.NotNull(dto);
        Assert.Equal("Maria", dto.NomeCompleto);
    }

    [Fact]
    public async Task UseCase_BuscarPorId_Inexistente_RetornaNull()
    {
        var dto = await _useCase.BuscarPorIdAsync(9999);
        Assert.Null(dto);
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_DesativaFuncionario()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "TI" });
        var id = (await _repo.ListarAsync()).First().Id;

        await _useCase.AlternarAtivoAsync(id);

        var f = await _repo.BuscarPorIdAsync(id);
        Assert.NotNull(f);
        Assert.False(f.Ativo);
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_ReativaFuncionario()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "TI" });
        var id = (await _repo.ListarAsync()).First().Id;

        await _useCase.AlternarAtivoAsync(id);
        await _useCase.AlternarAtivoAsync(id);

        var f = await _repo.BuscarPorIdAsync(id);
        Assert.NotNull(f);
        Assert.True(f.Ativo);
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_Inexistente_LancaExcecao()
    {
        await Assert.ThrowsAsync<System.InvalidOperationException>(() => _useCase.AlternarAtivoAsync(9999));
    }

    [Fact]
    public async Task UseCase_AlternarAtivo_FuncionarioInativoPermaneceNoBanco()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Maria", Setor = "TI" });
        var id = (await _repo.ListarAsync()).First().Id;
        await _useCase.AlternarAtivoAsync(id);

        var todos = await _repo.ListarAsync();
        Assert.Single(todos);
    }

    [Fact]
    public async Task UseCase_Pesquisar_PorNome_RetornaResultados()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Ana Silva", Setor = "TI" });
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Carlos Souza", Setor = "RH" });

        var resultados = (await _useCase.PesquisarAsync("Ana")).ToList();

        Assert.Single(resultados);
        Assert.Equal("Ana Silva", resultados[0].NomeCompleto);
    }

    [Fact]
    public async Task UseCase_Pesquisar_PorSetor_RetornaResultados()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Ana", Setor = "TI" });
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Carlos", Setor = "RH" });

        var resultados = (await _useCase.PesquisarAsync("RH")).ToList();

        Assert.Single(resultados);
        Assert.Equal("Carlos", resultados[0].NomeCompleto);
    }

    [Fact]
    public async Task UseCase_Pesquisar_PorMatricula_RetornaResultados()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Ana", Setor = "TI", Matricula = "ABC123" });
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Carlos", Setor = "RH" });

        var resultados = (await _useCase.PesquisarAsync("ABC123")).ToList();

        Assert.Single(resultados);
        Assert.Equal("Ana", resultados[0].NomeCompleto);
    }

    [Fact]
    public async Task UseCase_Pesquisar_TermoVazio_RetornaTodos()
    {
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Ana", Setor = "TI" });
        await _useCase.CriarAsync(new CriarFuncionarioDto { NomeCompleto = "Carlos", Setor = "RH" });

        var resultados = (await _useCase.PesquisarAsync("")).ToList();

        Assert.Equal(2, resultados.Count);
    }

    [Fact]
    public async Task Repository_Listar_OrdenadoPorNome()
    {
        await _repo.AdicionarAsync(Funcionario.Create("Zelia", "TI"));
        await _repo.AdicionarAsync(Funcionario.Create("Ana", "TI"));
        await _repo.AdicionarAsync(Funcionario.Create("Marcos", "TI"));

        var lista = (await _repo.ListarAsync()).ToList();

        Assert.Equal("Ana", lista[0].NomeCompleto);
        Assert.Equal("Marcos", lista[1].NomeCompleto);
        Assert.Equal("Zelia", lista[2].NomeCompleto);
    }

    [Fact]
    public async Task Repository_Pesquisar_PorCargo_RetornaResultados()
    {
        await _repo.AdicionarAsync(Funcionario.Create("Ana", "TI", cargo: "Analista"));
        await _repo.AdicionarAsync(Funcionario.Create("Carlos", "RH", cargo: "Gerente"));

        var resultados = (await _repo.PesquisarAsync("Gerente")).ToList();

        Assert.Single(resultados);
        Assert.Equal("Carlos", resultados[0].NomeCompleto);
    }
}
