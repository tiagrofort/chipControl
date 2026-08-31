using ChipControl.Domain;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using Xunit;

namespace ChipControl.Tests;

public class UsuarioSistemaTests
{
    [Fact]
    public void Create_CamposBasicosAtribuidosCorretamente()
    {
        var usuario = UsuarioSistema.Create(
            nome: "Joao",
            login: "joao",
            senhaHash: "hash123",
            nivelAcesso: NivelAcesso.Administrador,
            email: "joao@test.com");

        Assert.Equal("Joao", usuario.Nome);
        Assert.Equal("joao", usuario.Login);
        Assert.Equal("hash123", usuario.SenhaHash);
        Assert.Equal(NivelAcesso.Administrador, usuario.NivelAcesso);
        Assert.True(usuario.Ativo);
        Assert.Equal("joao@test.com", usuario.Email);
    }

    [Theory]
    [InlineData("", "login", "hash", NivelAcesso.Usuario)]
    [InlineData("Nome", "", "hash", NivelAcesso.Administrador)]
    [InlineData("Nome", "login", "", NivelAcesso.Usuario)]
    public void Create_ComCampoObrigatorioVazio_LancaExcecao(string nome, string login, string senha, NivelAcesso nivel)
    {
        Assert.Throws<ArgumentException>(() =>
            UsuarioSistema.Create(nome, login, senha, nivel));
    }

    [Fact]
    public void DefinirInativo_MudaStatusParaInativo()
    {
        var usuario = UsuarioSistema.Create("Teste", "teste", "hash123", NivelAcesso.Usuario);

        Assert.True(usuario.Ativo);
        usuario.DefinirInativo();
        Assert.False(usuario.Ativo);
    }

    [Fact]
    public void DefinirAtivo_ReativaUsuarioInativo()
    {
        var usuario = UsuarioSistema.Create("Teste", "teste", "hash123", NivelAcesso.Usuario);
        usuario.DefinirInativo();
        usuario.DefinirAtivo();

        Assert.True(usuario.Ativo);
    }

    [Fact]
    public void PodeAutenticar_RetornaTrue_ParaUsuarioAtivo()
    {
        var usuario = UsuarioSistema.Create("Teste", "teste", "hash123", NivelAcesso.Usuario);

        Assert.True(usuario.PodeAutenticar());
    }

    [Fact]
    public void PodeAutenticar_RetornaFalse_ParaUsuarioInativo()
    {
        var usuario = UsuarioSistema.Create("Teste", "teste", "hash123", NivelAcesso.Usuario);
        usuario.DefinirInativo();

        Assert.False(usuario.PodeAutenticar());
    }

    [Fact]
    public void AlterarSenha_AtualizaSenhaHash()
    {
        var usuario = UsuarioSistema.Create("Teste", "teste", "hash_antiga", NivelAcesso.Usuario);
        usuario.AlterarSenha("nova_hash");

        Assert.Equal("nova_hash", usuario.SenhaHash);
    }
}
