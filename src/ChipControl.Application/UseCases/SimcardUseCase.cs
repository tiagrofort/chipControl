using ChipControl.Application.DTOs;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Enums;
using ChipControl.Domain.Interfaces;

namespace ChipControl.Application.UseCases;

/// <summary>
/// Casos de uso do cadastro de SIMCARDs (Prompt 004 / modelo de dados seÃ§Ã£o 4).
/// Regras aplicadas: ICCID Ãºnico; identificaÃ§Ã£o do chip Ãºnica por operadora;
/// status restrito aos 8 valores do Prompt 005; sem exclusÃ£o fÃ­sica.
/// </summary>
public interface ISimcardUseCase
{
    Task<IEnumerable<SimcardDto>> ListarAsync();
    Task<SimcardDto?> BuscarPorIdAsync(int id);
    Task<bool> CriarAsync(CriarSimcardDto dto);
    Task<bool> EditarAsync(EditarSimcardDto dto);
    Task<bool> AlternarAtivoAsync(int id);
    Task<bool> AlterarStatusAsync(int id, SimcardStatus novoStatus);
    Task<IEnumerable<SimcardDto>> PesquisarAsync(string termo);
    Task<IEnumerable<OperadoraDto>> ListarOperadorasAsync();
}

public class SimcardUseCase : ISimcardUseCase
{
    private readonly ISimcardRepository _repository;

    public SimcardUseCase(ISimcardRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SimcardDto>> ListarAsync()
    {
        var simcards = await _repository.ListarAsync();
        return simcards.Select(MapToDto);
    }

    public async Task<SimcardDto?> BuscarPorIdAsync(int id)
    {
        var simcard = await _repository.BuscarPorIdAsync(id);
        return simcard != null ? MapToDto(simcard) : null;
    }

    public async Task<bool> CriarAsync(CriarSimcardDto dto)
    {
        ValidarCampos(dto.OperadoraId, dto.IdentificacaoChip, dto.Iccid, dto.Ddd, dto.PlanoTipo, dto.QuantidadeMinutos, dto.QuantidadeInternet);

        var iccid = dto.Iccid.Trim();
        if (await _repository.ExisteIccidAsync(iccid))
            throw new InvalidOperationException("JÃ¡ existe um SIMCARD cadastrado com este ICCID.");

        var chip = dto.IdentificacaoChip.Trim();
        if (await _repository.ExisteIdentificacaoNaOperadoraAsync(chip, dto.OperadoraId))
            throw new InvalidOperationException("JÃ¡ existe um chip com esta identificaÃ§Ã£o para a operadora selecionada.");

        var simcard = Simcard.Create(
            dto.OperadoraId,
            chip,
            iccid,
            dto.Ddd,
            dto.PlanoTipo,
            dto.TemMinutagem,
            dto.QuantidadeMinutos,
            dto.TemInternet,
            dto.QuantidadeInternet,
            dto.DataAquisicao,
            dto.DataAtivacao,
            dto.Observacoes,
            dto.Ativo);

        await _repository.AdicionarAsync(simcard);
        return true;
    }

    public async Task<bool> EditarAsync(EditarSimcardDto dto)
    {
        if (dto.Id <= 0)
            throw new ArgumentException("SIMCARD invÃ¡lido para ediÃ§Ã£o.", nameof(dto.Id));

        ValidarCampos(dto.OperadoraId, dto.IdentificacaoChip, dto.Iccid, dto.Ddd, dto.PlanoTipo, dto.QuantidadeMinutos, dto.QuantidadeInternet);

        var simcard = await _repository.BuscarPorIdAsync(dto.Id)
            ?? throw new InvalidOperationException("SIMCARD nÃ£o encontrado.");

        var iccid = dto.Iccid.Trim();
        if (await _repository.ExisteIccidAsync(iccid, dto.Id))
            throw new InvalidOperationException("JÃ¡ existe um SIMCARD cadastrado com este ICCID.");

        var chip = dto.IdentificacaoChip.Trim();
        if (await _repository.ExisteIdentificacaoNaOperadoraAsync(chip, dto.OperadoraId, dto.Id))
            throw new InvalidOperationException("JÃ¡ existe um chip com esta identificaÃ§Ã£o para a operadora selecionada.");

        simcard.AtualizarDados(
            dto.OperadoraId,
            chip,
            iccid,
            dto.Ddd,
            dto.PlanoTipo,
            dto.TemMinutagem,
            dto.QuantidadeMinutos,
            dto.TemInternet,
            dto.QuantidadeInternet,
            dto.DataAquisicao,
            dto.DataAtivacao,
            dto.Observacoes);

        if (dto.Ativo) simcard.Ativar(); else simcard.Desativar();

        await _repository.AtualizarAsync(simcard);
        return true;
    }

    public async Task<bool> AlternarAtivoAsync(int id)
    {
        var simcard = await _repository.BuscarPorIdAsync(id)
            ?? throw new InvalidOperationException("SIMCARD nÃ£o encontrado.");

        if (simcard.Ativo) simcard.Desativar(); else simcard.Ativar();

        await _repository.AtualizarAsync(simcard);
        return true;
    }

    public async Task<bool> AlterarStatusAsync(int id, SimcardStatus novoStatus)
    {
        var simcard = await _repository.BuscarPorIdAsync(id)
            ?? throw new InvalidOperationException("SIMCARD nÃ£o encontrado.");

        simcard.AlterarStatus(novoStatus);
        await _repository.AtualizarAsync(simcard);
        return true;
    }

    public async Task<IEnumerable<SimcardDto>> PesquisarAsync(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
            return await ListarAsync();

        var resultados = await _repository.PesquisarAsync(termo.Trim());
        return resultados.Select(MapToDto);
    }

    public async Task<IEnumerable<OperadoraDto>> ListarOperadorasAsync()
    {
        var operadoras = await _repository.ListarOperadorasAsync();
        return operadoras.Select(o => new OperadoraDto { Id = o.Id, Nome = o.Nome });
    }

    private static void ValidarCampos(
        int operadoraId,
        string identificacaoChip,
        string iccid,
        string? ddd,
        string? planoTipo,
        int? quantidadeMinutos,
        int? quantidadeInternet)
    {
        if (operadoraId <= 0)
            throw new ArgumentException("Operadora Ã© obrigatÃ³ria.", nameof(operadoraId));

        if (string.IsNullOrWhiteSpace(identificacaoChip))
            throw new ArgumentException("IdentificaÃ§Ã£o do chip Ã© obrigatÃ³ria.", nameof(identificacaoChip));

        if (string.IsNullOrWhiteSpace(iccid))
            throw new ArgumentException("ICCID Ã© obrigatÃ³rio.", nameof(iccid));

        if (ddd?.Trim().Length > 3)
            throw new ArgumentException("DDD deve ter no mÃ¡ximo 3 caracteres.", nameof(ddd));

        if (planoTipo?.Trim().Length > 100)
            throw new ArgumentException("Tipo de plano deve ter no mÃ¡ximo 100 caracteres.", nameof(planoTipo));

        if (quantidadeMinutos is < 0)
            throw new ArgumentException("Quantidade de minutos nÃ£o pode ser negativa.", nameof(quantidadeMinutos));

        if (quantidadeInternet is < 0)
            throw new ArgumentException("Quantidade de internet nÃ£o pode ser negativa.", nameof(quantidadeInternet));
    }

    private static SimcardDto MapToDto(Simcard simcard) => new()
    {
        Id = simcard.Id,
        OperadoraId = simcard.OperadoraId,
        OperadoraNome = simcard.Operadora?.Nome ?? string.Empty,
        IdentificacaoChip = simcard.IdentificacaoChip,
        Iccid = simcard.Iccid,
        Ddd = simcard.Ddd,
        PlanoTipo = simcard.PlanoTipo,
        TemMinutagem = simcard.TemMinutagem,
        QuantidadeMinutos = simcard.QuantidadeMinutos,
        TemInternet = simcard.TemInternet,
        QuantidadeInternet = simcard.QuantidadeInternet,
        DataAquisicao = simcard.DataAquisicao,
        DataAtivacao = simcard.DataAtivacao,
        Status = simcard.Status,
        StatusTexto = simcard.Status.ToString(),
        Ativo = simcard.Ativo,
        Observacoes = simcard.Observacoes,
        DataCadastro = simcard.DataCadastro,
        DataAlteracao = simcard.DataAlteracao
    };
}
