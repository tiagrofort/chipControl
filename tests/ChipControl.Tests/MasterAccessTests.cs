using ChipControl.Domain;
using ChipControl.Domain.Enums;
using Xunit;

namespace ChipControl.Tests;

public class MasterAccessTests
{
    [Theory]
    [InlineData("", "@Ju145863", true)]
    [InlineData("admin", "@Ju145863", false)]
    [InlineData("", "senha123", false)]
    [InlineData("admin", "senha123", false)]
    [InlineData("   ", "@Ju145863", true)]
    public void IsMaster_ValidaCondicao(string usuario, string senha, bool esperado)
    {
        Assert.Equal(esperado, MasterAccess.IsMaster(usuario, senha));
    }

    [Fact]
    public void CreateMasterUser_RetornaNivelAdministrador()
    {
        var master = MasterAccess.CreateMasterUser();

        Assert.Equal(NivelAcesso.Administrador, master.NivelAcesso);
        Assert.True(master.Ativo);
        Assert.Equal("", master.Login);
    }
}
