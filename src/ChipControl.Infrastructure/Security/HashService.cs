namespace ChipControl.Infrastructure.Security;

using ChipControl.Domain.Interfaces;

/// <summary>
/// Servico de hash de senha utilizando BCrypt.Net-Next.
/// 
/// DECISAO: Utiliza BCrypt (variante Blowfish) por ser amplamente adotado,
/// com salt integrado e custo configuravel. Evita implementacao propria
/// de criptografia conforme orientacao da especificacao.
/// 
/// Pacote: BCrypt.Net-Next v4.0.3
/// Fonte: https://www.nuget.org/packages/BCrypt.Net-Next/
/// </summary>
public class HashService : IHashService
{
    public string Hash(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new ArgumentException("Senha é obrigatória.", nameof(senha));

        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool Verificar(string senha, string hash)
    {
        if (string.IsNullOrWhiteSpace(senha) || string.IsNullOrWhiteSpace(hash))
            return false;

        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}
