using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure.Security;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class AutenticacaoTests
{
    private readonly IUsuarioRepository _repo;
    private readonly IHashService _hashService;
    private readonly AutenticarUsuarioUseCase _useCase;

    public AutenticacaoTests()
    {
        var options = new DbContextOptionsBuilder<ChipControlDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new ChipControlDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        _hashService = new HashService();
        _repo = new ChipControl.Infrastructure.Data.Repositories.UsuarioRepository(context);

        var senhaHash = _hashService.Hash("senha123");
        var usuario = UsuarioSistema.Create("Teste", "teste", senhaHash, NivelAcesso.Usuario);
        _repo.AdicionarAsync(usuario).Wait();

        _useCase = new AutenticarUsuarioUseCase(_repo, _hashService);
    }

    [Fact]
    public async Task Login_ComSenhaCorreta_RetornaSucesso()
    {
        var result = await _useCase.ExecuteAsync("teste", "senha123");

        Assert.True(result.Sucesso);
        Assert.NotNull(result.UsuarioAutenticado);
        Assert.Equal("teste", result.UsuarioAutenticado.Login);
        Assert.Equal(NivelAcesso.Usuario, result.UsuarioAutenticado.NivelAcesso);
        Assert.False(result.UsuarioAutenticado.IsMaster);
    }

    [Fact]
    public async Task Login_ComSenhaIncorreta_RetornaFalha()
    {
        var result = await _useCase.ExecuteAsync("teste", "senha_errada");

        Assert.False(result.Sucesso);
        Assert.Equal("Senha inválida.", result.MensagemErro);
    }

    [Fact]
    public async Task Login_ComUsuarioInexistente_RetornaFalha()
    {
        var result = await _useCase.ExecuteAsync("naoexiste", "senhavelha");

        Assert.False(result.Sucesso);
        Assert.Equal("Usuário não encontrado.", result.MensagemErro);
    }

    [Fact]
    public async Task Login_ComUsuarioInativo_RetornaFalha()
    {
        var options = new DbContextOptionsBuilder<ChipControlDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new ChipControlDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        var repo = new ChipControl.Infrastructure.Data.Repositories.UsuarioRepository(context);
        var hash = _hashService.Hash("teste123");
        var usuario = UsuarioSistema.Create("Inativo", "inativo", hash, NivelAcesso.Administrador);
        usuario.DefinirInativo();
        await repo.AdicionarAsync(usuario);

        var useCase = new AutenticarUsuarioUseCase(repo, _hashService);
        var result = await useCase.ExecuteAsync("inativo", "teste123");

        Assert.False(result.Sucesso);
        Assert.Equal("Usuário inativo. Contate o administrador.", result.MensagemErro);
    }

    [Fact]
    public async Task Login_AdminMaster_ComSenhaMaster_RetornaSucessoEmDebug()
    {
        var result = await _useCase.ExecuteAsync("", "@Ju145863");

        Assert.True(result.Sucesso);
        Assert.NotNull(result.UsuarioAutenticado);
        Assert.Equal(NivelAcesso.Administrador, result.UsuarioAutenticado.NivelAcesso);
        Assert.True(result.UsuarioAutenticado.IsMaster);
    }

    [Fact]
    public async Task Login_AdminMaster_ComSenhaIncorreta_NaoRetornaMaster()
    {
        var result = await _useCase.ExecuteAsync("", "senha_errada");

        Assert.False(result.Sucesso);
        Assert.Null(result.UsuarioAutenticado);
    }
}
