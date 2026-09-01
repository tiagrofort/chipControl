using ChipControl.Application.DTOs;
using ChipControl.Application.UseCases;
using ChipControl.Domain.Enums;
using ChipControl.Infrastructure.Data.Repositories;
using ChipControl.Infrastructure.Security;
using ChipControl.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class UsuarioUseCaseTests
{
    private readonly ChipControlDbContext _context;
    private readonly UsuarioUseCase _useCase;
    private readonly HashService _hashService;
    private readonly UsuarioRepository _repo;

    public UsuarioUseCaseTests()
    {
        var options = new DbContextOptionsBuilder<ChipControlDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        _context = new ChipControlDbContext(options);
        _context.Database.OpenConnection();
        _context.Database.EnsureCreated();

        _repo = new UsuarioRepository(_context);
        _hashService = new HashService();
        _useCase = new UsuarioUseCase(_repo, _hashService);
    }

    [Fact]
    public async Task CriarUsuario_Valido_RetornaTrue()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Maria Silva",
            Login = "maria",
            Senha = "senha123",
            Email = "maria@test.com",
            NivelAcesso = NivelAcesso.Usuario,
            Ativo = true,
            Observacoes = "Teste"
        };

        var result = await _useCase.CriarAsync(dto);

        Assert.True(result);
        var usuarios = await _repo.ListarAsync();
        Assert.Contains(usuarios, u => u.Login == "maria");
    }

    [Fact]
    public async Task CriarUsuario_ComNomeVazio_LancaExcecao()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "",
            Login = "maria",
            Senha = "senha123"
        };

        await Assert.ThrowsAsync<System.ArgumentException>(() => _useCase.CriarAsync(dto));
    }

    [Fact]
    public async Task CriarUsuario_ComLoginVazio_LancaExcecao()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Maria",
            Login = "",
            Senha = "senha123"
        };

        await Assert.ThrowsAsync<System.ArgumentException>(() => _useCase.CriarAsync(dto));
    }

    [Fact]
    public async Task CriarUsuario_ComLoginDuplicado_LancaExcecao()
    {
        var dto1 = new CriarUsuarioDto { Nome = "Maria", Login = "maria", Senha = "123" };
        await _useCase.CriarAsync(dto1);

        var dto2 = new CriarUsuarioDto { Nome = "Maria 2", Login = "maria", Senha = "456" };

        await Assert.ThrowsAsync<System.InvalidOperationException>(() => _useCase.CriarAsync(dto2));
    }

    [Fact]
    public async Task CriarUsuario_SenhaArmazenadaComoHash()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Joao",
            Login = "joao",
            Senha = "minhasenha"
        };

        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("joao");
        Assert.NotNull(usuario);
        Assert.NotEqual("minhasenha", usuario.SenhaHash);
        Assert.True(_hashService.Verificar("minhasenha", usuario.SenhaHash));
    }

    [Fact]
    public async Task CriarUsuario_ComSenhaCorreta_PermiteAutenticacao()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Teste",
            Login = "teste",
            Senha = "senha123"
        };

        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("teste");
        Assert.NotNull(usuario);
        Assert.True(_hashService.Verificar("senha123", usuario!.SenhaHash));
    }

    [Fact]
    public async Task CriarUsuario_ComSenhaIncorreta_NaoPermiteAutenticacao()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Teste",
            Login = "teste",
            Senha = "senha123"
        };

        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("teste");
        Assert.NotNull(usuario);
        Assert.False(_hashService.Verificar("senha_errada", usuario!.SenhaHash));
    }

    [Fact]
    public async Task AlternarAtivo_DesativaUsuario()
    {
        var dto = new CriarUsuarioDto { Nome = "Teste", Login = "t1", Senha = "123" };
        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("t1");
        Assert.NotNull(usuario);
        await _useCase.AlternarAtivoAsync(usuario!.Id);

        var atualizado = await _repo.BuscarPorIdAsync(usuario.Id);
        Assert.NotNull(atualizado);
        Assert.False(atualizado!.Ativo);
    }

    [Fact]
    public async Task AlternarAtivo_AtivaUsuarioInativo()
    {
        var dto = new CriarUsuarioDto { Nome = "Teste", Login = "t2", Senha = "123" };
        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("t2");
        Assert.NotNull(usuario);
        await _useCase.AlternarAtivoAsync(usuario!.Id);
        await _useCase.AlternarAtivoAsync(usuario.Id);

        var atualizado = await _repo.BuscarPorIdAsync(usuario.Id);
        Assert.NotNull(atualizado);
        Assert.True(atualizado!.Ativo);
    }

    [Fact]
    public async Task EditarUsuario_AlteraDados_SemAlterarSenha()
    {
        var dto = new CriarUsuarioDto { Nome = "Original", Login = "orig", Senha = "123456" };
        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("orig");
        Assert.NotNull(usuario);
        var senhaAntes = usuario!.SenhaHash;

        await _useCase.EditarAsync(new EditarUsuarioDto
        {
            Id = usuario.Id,
            Nome = "Alterado",
            Login = "novologin",
            Senha = null,
            Email = "novo@test.com",
            NivelAcesso = NivelAcesso.Usuario,
            Ativo = true,
            Observacoes = ""
        });

        var atualizado = await _repo.BuscarPorIdAsync(usuario.Id);
        Assert.NotNull(atualizado);
        Assert.Equal("Alterado", atualizado!.Nome);
        Assert.Equal("novologin", atualizado.Login);
        Assert.Equal("novo@test.com", atualizado.Email);
        Assert.Equal(senhaAntes, atualizado.SenhaHash);
    }

    [Fact]
    public async Task EditarUsuario_ComNovaSenha_GeraNovoHash()
    {
        var dto = new CriarUsuarioDto { Nome = "Teste", Login = "t3", Senha = "senha123" };
        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("t3");
        Assert.NotNull(usuario);
        var hashAntes = usuario!.SenhaHash;

        await _useCase.EditarAsync(new EditarUsuarioDto
        {
            Id = usuario.Id,
            Nome = "Teste",
            Login = "t3",
            Senha = "nova_senha",
            Email = null,
            NivelAcesso = NivelAcesso.Usuario,
            Ativo = true,
            Observacoes = ""
        });

        var atualizado = await _repo.BuscarPorIdAsync(usuario.Id);
        Assert.NotNull(atualizado);
        Assert.NotEqual(hashAntes, atualizado!.SenhaHash);
        Assert.True(_hashService.Verificar("nova_senha", atualizado.SenhaHash));
    }

    [Fact]
    public async Task Pesquisar_PorParteDoNome_RetornaResultadosCorretos()
    {
        await _useCase.CriarAsync(new CriarUsuarioDto { Nome = "Ana Silva", Login = "ana", Senha = "123" });
        await _useCase.CriarAsync(new CriarUsuarioDto { Nome = "Carlos Souza", Login = "carlos", Senha = "123" });

        var resultados = (await _useCase.PesquisarAsync("Ana")).ToList();

        Assert.Single(resultados);
        Assert.Equal("ana", resultados[0].Login);
    }

    [Fact]
    public async Task Pesquisar_PorNivelAcesso_RetornaResultados()
    {
        await _useCase.CriarAsync(new CriarUsuarioDto { Nome = "Admin", Login = "a1", Senha = "123", NivelAcesso = NivelAcesso.Administrador });
        await _useCase.CriarAsync(new CriarUsuarioDto { Nome = "User", Login = "u1", Senha = "123", NivelAcesso = NivelAcesso.Usuario });

        var resultados = (await _useCase.PesquisarAsync("Administrador")).ToList();

        Assert.Single(resultados);
        Assert.Equal("a1", resultados[0].Login);
    }

    [Fact]
    public async Task AdministradorECriadoComNivelCorreto()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Admin",
            Login = "admin2",
            Senha = "123456",
            NivelAcesso = NivelAcesso.Administrador
        };

        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("admin2");
        Assert.NotNull(usuario);
        Assert.Equal(NivelAcesso.Administrador, usuario!.NivelAcesso);
    }

    [Fact]
    public async Task UsuarioComumCriadoComNivelCorreto()
    {
        var dto = new CriarUsuarioDto
        {
            Nome = "Usuario",
            Login = "user2",
            Senha = "123456",
            NivelAcesso = NivelAcesso.Usuario
        };

        await _useCase.CriarAsync(dto);

        var usuario = await _repo.BuscarPorLoginAsync("user2");
        Assert.NotNull(usuario);
        Assert.Equal(NivelAcesso.Usuario, usuario!.NivelAcesso);
    }
}
