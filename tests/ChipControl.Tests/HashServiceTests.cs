using ChipControl.Domain;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;
using ChipControl.Infrastructure.Security;
using System.Threading.Tasks;
using Xunit;

namespace ChipControl.Tests;

public class HashServiceTests
{
    private readonly HashService _hashService = new();

    [Fact]
    public void Hash_GeraHashComSalt_DiferenteDeSenhaOriginal()
    {
        var senha = "minhasenha123";
        var hash = _hashService.Hash(senha);

        Assert.NotEqual(senha, hash);
        Assert.NotEmpty(hash);
        Assert.True(hash.Length > 20);
    }

    [Fact]
    public void Hash_GeraHashDiferenteParasSenhasIguais_DevidoAoSalt()
    {
        var senha = "minhasenha123";
        var hash1 = _hashService.Hash(senha);
        var hash2 = _hashService.Hash(senha);

        Assert.NotEqual(hash1, hash2);
    }

    [Fact]
    public void Verificar_SenhaCorreta_RetornaTrue()
    {
        var senha = "senha123";
        var hash = _hashService.Hash(senha);

        var result = _hashService.Verificar(senha, hash);

        Assert.True(result);
    }

    [Fact]
    public void Verificar_SenhaIncorreta_RetornaFalse()
    {
        var hash = _hashService.Hash("senha123");

        var result = _hashService.Verificar("senha_errada", hash);

        Assert.False(result);
    }

    [Fact]
    public void Verificar_SenhaNula_RetornaFalse()
    {
        var hash = _hashService.Hash("senha123");

        var result = _hashService.Verificar("", hash);

        Assert.False(result);
    }
}
