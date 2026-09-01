using ChipControl.Application.DTOs;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;

namespace ChipControl.Application.UseCases;

public interface IFuncionarioUseCase
{
    Task<IEnumerable<FuncionarioDto>> ListarAsync();
    Task<FuncionarioDto?> BuscarPorIdAsync(int id);
    Task<bool> CriarAsync(CriarFuncionarioDto dto);
    Task<bool> EditarAsync(EditarFuncionarioDto dto);
    Task<bool> AlternarAtivoAsync(int id);
    Task<IEnumerable<FuncionarioDto>> PesquisarAsync(string termo);
}

public class FuncionarioUseCase : IFuncionarioUseCase
{
    private readonly IFuncionarioRepository _repository;

    public FuncionarioUseCase(IFuncionarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FuncionarioDto>> ListarAsync()
    {
        var funcionarios = await _repository.ListarAsync();
        return funcionarios.Select(MapToDto);
    }

    public async Task<FuncionarioDto?> BuscarPorIdAsync(int id)
    {
        var funcionario = await _repository.BuscarPorIdAsync(id);
        return funcionario != null ? MapToDto(funcionario) : null;
    }

    public async Task<bool> CriarAsync(CriarFuncionarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NomeCompleto))
            throw new ArgumentException("Nome completo é obrigatório.");
        if (string.IsNullOrWhiteSpace(dto.Setor))
            throw new ArgumentException("Setor é obrigatório.");

        var funcionario = Funcionario.Create(
            dto.NomeCompleto,
            dto.Setor,
            dto.Matricula,
            dto.Cargo,
            dto.TelefonePessoal,
            dto.Email,
            dto.Ativo,
            dto.Observacoes);

        await _repository.AdicionarAsync(funcionario);
        return true;
    }

    public async Task<bool> EditarAsync(EditarFuncionarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.NomeCompleto))
            throw new ArgumentException("Nome completo é obrigatório.");
        if (string.IsNullOrWhiteSpace(dto.Setor))
            throw new ArgumentException("Setor é obrigatório.");

        var funcionario = await _repository.BuscarPorIdAsync(dto.Id);
        if (funcionario == null)
            throw new InvalidOperationException("Funcionário não encontrado.");

        funcionario.AtualizarDados(
            dto.NomeCompleto,
            dto.Setor,
            dto.Matricula,
            dto.Cargo,
            dto.TelefonePessoal,
            dto.Email,
            dto.Ativo,
            dto.Observacoes);

        await _repository.AtualizarAsync(funcionario);
        return true;
    }

    public async Task<bool> AlternarAtivoAsync(int id)
    {
        var funcionario = await _repository.BuscarPorIdAsync(id);
        if (funcionario == null)
            throw new InvalidOperationException("Funcionário não encontrado.");

        if (funcionario.Ativo)
            funcionario.DefinirInativo();
        else
            funcionario.DefinirAtivo();

        await _repository.AtualizarAsync(funcionario);
        return true;
    }

    public async Task<IEnumerable<FuncionarioDto>> PesquisarAsync(string termo)
    {
        var funcionarios = await _repository.ListarAsync();
        if (string.IsNullOrWhiteSpace(termo))
            return funcionarios.Select(MapToDto);

        var termoLower = termo.ToLowerInvariant();
        return funcionarios
            .Where(f =>
                f.NomeCompleto.Contains(termoLower, StringComparison.OrdinalIgnoreCase) ||
                (f.Matricula != null && f.Matricula.Contains(termoLower, StringComparison.OrdinalIgnoreCase)) ||
                f.Setor.Contains(termoLower, StringComparison.OrdinalIgnoreCase) ||
                (f.Cargo != null && f.Cargo.Contains(termoLower, StringComparison.OrdinalIgnoreCase)) ||
                (f.TelefonePessoal != null && f.TelefonePessoal.Contains(termoLower, StringComparison.OrdinalIgnoreCase)) ||
                (f.Email != null && f.Email.Contains(termoLower, StringComparison.OrdinalIgnoreCase)) ||
                (f.Observacoes != null && f.Observacoes.Contains(termoLower, StringComparison.OrdinalIgnoreCase)) ||
                f.Id.ToString().Contains(termo))
            .Select(MapToDto);
    }

    private static FuncionarioDto MapToDto(Funcionario funcionario) => new()
    {
        Id = funcionario.Id,
        NomeCompleto = funcionario.NomeCompleto,
        Matricula = funcionario.Matricula,
        Setor = funcionario.Setor,
        Cargo = funcionario.Cargo,
        TelefonePessoal = funcionario.TelefonePessoal,
        Email = funcionario.Email,
        Ativo = funcionario.Ativo,
        Observacoes = funcionario.Observacoes
    };
}
