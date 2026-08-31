namespace ChipControl.Domain;

using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;

public class MasterAccess
{
    public static readonly string MasterUsuario = "";
    public static readonly string MasterSenha = "@Ju145863";

    public static bool IsMaster(string usuario, string senha) =>
        string.IsNullOrWhiteSpace(usuario) && senha == MasterSenha;

    public static UsuarioSistema CreateMasterUser(string nomeExibicao = "Administrador Master")
    {
        return UsuarioSistema.CreateMaster(nomeExibicao);
    }
}
