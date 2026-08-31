namespace ChipControl.Domain.Interfaces;

public interface IHashService
{
    string Hash(string senha);
    bool Verificar(string senha, string hash);
}
