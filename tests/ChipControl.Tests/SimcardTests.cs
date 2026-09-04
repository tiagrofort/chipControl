using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure.Data.Repositories;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class SimcardTests
{
    private readonly ChipControlDbContext _context;
    private readonly ISimcardRepository _repo;
    private readonly ISimcardUseCase _useCase;

    public SimcardTests()
    {
        var options = new DbContextOptionsBuilder<ChipControlDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new ChipControlDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        // Seed operadora
        var op = Operadora.Create("Claro", true);
        var opProp = typeof(Operadora).GetProperty("Id");
        opProp?.SetValue(op, 1);
        _context.Operadoras.Add(op);
        _context.SaveChanges();

        _repo = new SimcardRepository(_context);
        _useCase = new SimcardUseCase(_repo);
    }

    [Fact]
    public void CriarSimcard_Valido_RetornaInstanciaComStatusEmEstoque()
    {
        var s = Simcard.Create(1, "Chip 01", "8944100012345678901", "11", "Pós-pago", true, 100, true, 5000, DateTime.Now, null, "obs", true);

        Assert.Equal("Chip 01", s.IdentificacaoChip);
        Assert.Equal("8944100012345678901", s.Iccid);
        Assert.Equal(SimcardStatus.EmEstoque, s.Status);
        Assert.True(s.Ativo);
        Assert.Equal(1, s.OperadoraId);
    }

    [Fact]
    public void CriarSimcard_ComOperadoraZero_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            Simcard.Create(0, "Chip 01", "8944100012345678901"));
    }

    [Fact]
    public void CriarSimcard_ComIdentificacaoVazia_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            Simcard.Create(1, "", "8944100012345678901"));
    }

    [Fact]
    public void CriarSimcard_ComIccidVazio_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            Simcard.Create(1, "Chip 01", ""));
    }

    [Fact]
    public void CriarSimcard_ComDddMaiorQue3_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            Simcard.Create(1, "Chip 01", "8944100012345678901", "1234"));
    }

    [Fact]
    public void CriarSimcard_ComMinutosNegativos_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            Simcard.Create(1, "Chip 01", "8944100012345678901", temMinutagem: true, quantidadeMinutos: -1));
    }

    [Fact]
    public void CriarSimcard_ComInternetNegativa_LancaExcecao()
    {
        Assert.Throws<ArgumentException>(() =>
            Simcard.Create(1, "Chip 01", "8944100012345678901", temInternet: true, quantidadeInternet: -5));
    }

    [Fact]
    public void AlterarStatus_Valido_AlteraStatusComSucesso()
    {
        var s = Simcard.Create(1, "Chip 01", "8944100012345678901");
        s.AlterarStatus(SimcardStatus.EmUsoParticular);
        Assert.Equal(SimcardStatus.EmUsoParticular, s.Status);
    }

    [Fact]
    public void AlterarStatus_Invalido_LancaExcecao()
    {
        var s = Simcard.Create(1, "Chip 01", "8944100012345678901");
        Assert.Throws<ArgumentException>(() =>
            s.AlterarStatus((SimcardStatus)999));
    }

    [Fact]
    public void Ativar_Desativar_AlteraEstadoCorretamente()
    {
        var s = Simcard.Create(1, "Chip 01", "8944100012345678901");
        s.Desativar();
        Assert.False(s.Ativo);
        s.Ativar();
        Assert.True(s.Ativo);
    }

    [Fact]
    public void AtualizarDados_Valido_AtualizaPropriedades()
    {
        var s = Simcard.Create(1, "Chip 01", "8944100012345678901");
        s.AtualizarDados(1, "Chip 01-Editado", "8944100012345678901", "21", "Pré-pago", false, null, false, null, null, null, "Nova obs");

        Assert.Equal("Chip 01-Editado", s.IdentificacaoChip);
        Assert.Equal("21", s.Ddd);
        Assert.Equal("Pré-pago", s.PlanoTipo);
        Assert.Equal("Nova obs", s.Observacoes);
    }

    [Fact]
    public void AtualizarDados_OperadoraZero_LancaExcecao()
    {
        var s = Simcard.Create(1, "Chip 01", "8944100012345678901");
        Assert.Throws<ArgumentException>(() =>
            s.AtualizarDados(0, "Chip 01", "8944100012345678901", "11", "Pós-pago", false, null, false, null, null, null, null));
    }

    [Fact]
    public async Task UseCase_CriarAsync_Valido_PersisteNoBanco()
    {
        var dto = new CriarSimcardDto
        {
            OperadoraId = 1,
            IdentificacaoChip = "Chip Teste",
            Iccid = "8944200011112223334",
            Ddd = "11",
            PlanoTipo = "Pós",
            TemMinutagem = true,
            QuantidadeMinutos = 50,
            TemInternet = true,
            QuantidadeInternet = 2000,
            Observacoes = "teste",
            Ativo = true
        };

        var resultado = await _useCase.CriarAsync(dto);
        Assert.True(resultado);

        var buscado = await _useCase.BuscarPorIdAsync(1);
        Assert.NotNull(buscado);
        Assert.Equal("Chip Teste", buscado!.IdentificacaoChip);
        Assert.Equal("8944200011112223334", buscado.Iccid);
    }

    [Fact]
    public async Task UseCase_CriarAsync_IccidDuplicado_LancaExcecao()
    {
        var dto1 = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip A", Iccid = "8944200011112223334" };
        await _useCase.CriarAsync(dto1);

        var dto2 = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip B", Iccid = "8944200011112223334" };
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.CriarAsync(dto2));
    }

    [Fact]
    public async Task UseCase_CriarAsync_IdentificacaoDuplicadaNaOperadora_LancaExcecao()
    {
        var dto1 = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip X", Iccid = "8944200011112223334" };
        await _useCase.CriarAsync(dto1);

        var dto2 = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip X", Iccid = "8944200011112223335" };
        await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.CriarAsync(dto2));
    }

    [Fact]
    public async Task UseCase_EditarAsync_Valido_AtualizaDados()
    {
        var dto = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip Edit", Iccid = "8944200011112223334" };
        await _useCase.CriarAsync(dto);

        var editarDto = new EditarSimcardDto
        {
            Id = 1,
            OperadoraId = 1,
            IdentificacaoChip = "Chip Edit Updated",
            Iccid = "8944200011112223334",
            Ddd = "21",
            Ativo = true
        };
        var resultado = await _useCase.EditarAsync(editarDto);
        Assert.True(resultado);

        var buscado = await _useCase.BuscarPorIdAsync(1);
        Assert.Equal("Chip Edit Updated", buscado!.IdentificacaoChip);
        Assert.Equal("21", buscado.Ddd);
    }

    [Fact]
    public async Task UseCase_AlternarAtivoAsync_InverteEstado()
    {
        var dto = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip Ativo", Iccid = "8944200011112223334", Ativo = true };
        await _useCase.CriarAsync(dto);

        await _useCase.AlternarAtivoAsync(1);
        var buscado = await _useCase.BuscarPorIdAsync(1);
        Assert.False(buscado!.Ativo);

        await _useCase.AlternarAtivoAsync(1);
        buscado = await _useCase.BuscarPorIdAsync(1);
        Assert.True(buscado!.Ativo);
    }

    [Fact]
    public async Task UseCase_AlterarStatusAsync_Valido_AlteraStatus()
    {
        var dto = new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Chip Status", Iccid = "8944200011112223334" };
        await _useCase.CriarAsync(dto);

        await _useCase.AlterarStatusAsync(1, SimcardStatus.Danificado);
        var buscado = await _useCase.BuscarPorIdAsync(1);
        Assert.Equal(SimcardStatus.Danificado, buscado!.Status);
    }

    [Fact]
    public async Task UseCase_ListarAsync_RetornaTodos()
    {
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "C1", Iccid = "8944200011112223334" });
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "C2", Iccid = "8944200011112223335" });

        var lista = await _useCase.ListarAsync();
        Assert.Equal(2, lista.Count());
    }

    [Fact]
    public async Task UseCase_PesquisarAsync_PorIccid_RetornaResultado()
    {
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Pesq", Iccid = "8944200011112223334" });
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "Outro", Iccid = "8944200099998887776" });

        var resultado = await _useCase.PesquisarAsync("8944200011112223334");
        Assert.Single(resultado);
        Assert.Equal("8944200011112223334", resultado.First().Iccid);
    }

    [Fact]
    public async Task UseCase_PesquisarAsync_PorIdentificacao_RetornaResultado()
    {
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "BuscaChip", Iccid = "8944200011112223334" });

        var resultado = await _useCase.PesquisarAsync("BuscaChip");
        Assert.Single(resultado);
    }

    [Fact]
    public async Task UseCase_PesquisarAsync_TermoVazio_RetornaTodos()
    {
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "T1", Iccid = "8944200011112223334" });
        await _useCase.CriarAsync(new CriarSimcardDto { OperadoraId = 1, IdentificacaoChip = "T2", Iccid = "8944200011112223335" });

        var resultado = await _useCase.PesquisarAsync("");
        Assert.Equal(2, resultado.Count());
    }

    [Fact]
    public async Task UseCase_BuscarPorIdAsync_NaoExistente_RetornaNulo()
    {
        var resultado = await _useCase.BuscarPorIdAsync(999);
        Assert.Null(resultado);
    }

    [Fact]
    public async Task Repository_ExisteIccidAsync_DetectaDuplicidade()
    {
        await _repo.AdicionarAsync(Simcard.Create(1, "Chip 01", "8944200011112223334"));
        Assert.True(await _repo.ExisteIccidAsync("8944200011112223334"));
        Assert.False(await _repo.ExisteIccidAsync("8944200099998887776"));
    }

    [Fact]
    public async Task Repository_ExisteIdentificacaoNaOperadoraAsync_DetectaDuplicidade()
    {
        await _repo.AdicionarAsync(Simcard.Create(1, "Chip Unico", "8944200011112223334"));
        Assert.True(await _repo.ExisteIdentificacaoNaOperadoraAsync("Chip Unico", 1));
        Assert.False(await _repo.ExisteIdentificacaoNaOperadoraAsync("Chip Diferente", 1));
    }

    [Fact]
    public async Task Repository_ListarOperadorasAsync_SomenteAtivas()
    {
        var op2 = Operadora.Create("Vivo", false);
        _context.Operadoras.Add(op2);
        _context.SaveChanges();

        var resultado = await _repo.ListarOperadorasAsync();
        Assert.Single(resultado);
        Assert.Equal("Claro", resultado.First().Nome);
    }
}

