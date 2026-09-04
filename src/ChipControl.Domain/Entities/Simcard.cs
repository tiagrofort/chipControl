namespace ChipControl.Domain.Entities;

using ChipControl.Domain.Enums;

/// <summary>
/// SIMCARD (modelo de dados, seção 4).
/// Regras documentadas (docs/03-MODELO-DE-DADOS.md seção 4 / Prompt 004):
/// - ICCID único;
/// - identificacao_chip único por operadora (índice único composto);
/// - status restrito aos 8 valores definidos no Prompt 005;
/// - sem exclusão física (regra de integridade) — desligamento pelo campo Ativo.
/// </summary>
public class Simcard
{
    public int Id { get; private set; }

    /// <summary>FK para Operadoras (obrigatório).</summary>
    public int OperadoraId { get; private set; }

    public Operadora Operadora { get; private set; } = null!;

    /// <summary>Identificação física do chip (ex.: "Chip 01"). Obrigatório, máx. 100.</summary>
    public string IdentificacaoChip { get; private set; } = string.Empty;

    /// <summary>ICCID do SIMCARD. Obrigatório, máx. 22, único.</summary>
    public string Iccid { get; private set; } = string.Empty;

    /// <summary>DDD (3 caracteres).</summary>
    public string? Ddd { get; private set; }

    /// <summary>Nome/tipo do plano (máx. 100).</summary>
    public string? PlanoTipo { get; private set; }

    /// <summary>Indica se possui minutagem.</summary>
    public bool TemMinutagem { get; private set; }

    /// <summary>Quantidade de minutos (quando aplicável).</summary>
    public int? QuantidadeMinutos { get; private set; }

    /// <summary>Indica se possui franquia de internet.</summary>
    public bool TemInternet { get; private set; }

    /// <summary>Quantidade de internet MB/GB (quando aplicável).</summary>
    public int? QuantidadeInternet { get; private set; }

    /// <summary>Data de aquisição.</summary>
    public DateTime? DataAquisicao { get; private set; }

    /// <summary>Data de ativação.</summary>
    public DateTime? DataAtivacao { get; private set; }

    /// <summary>Status atual (Prompt 005 — 8 valores válidos).</summary>
    public SimcardStatus Status { get; private set; }

    public string? Observacoes { get; private set; }

    /// <summary>Ativo/inativo (sem exclusão física).</summary>
    public bool Ativo { get; private set; }

    public DateTime DataCadastro { get; private set; }

    public DateTime? DataAlteracao { get; private set; }

    private Simcard()
    {
    }

    /// <summary>
    /// Cria um novo SIMCARD com status inicial EmEstoque (primeiro estado
    /// do ciclo de vida documentado no Prompt 005).
    /// </summary>
    public static Simcard Create(
        int operadoraId,
        string identificacaoChip,
        string iccid,
        string? ddd = null,
        string? planoTipo = null,
        bool temMinutagem = false,
        int? quantidadeMinutos = null,
        bool temInternet = false,
        int? quantidadeInternet = null,
        DateTime? dataAquisicao = null,
        DateTime? dataAtivacao = null,
        string? observacoes = null,
        bool ativo = true)
    {
        if (operadoraId <= 0)
            throw new ArgumentException("Operadora é obrigatória.", nameof(operadoraId));

        if (string.IsNullOrWhiteSpace(identificacaoChip))
            throw new ArgumentException("Identificação do chip é obrigatória.", nameof(identificacaoChip));

        if (identificacaoChip.Trim().Length > 100)
            throw new ArgumentException("Identificação do chip deve ter no máximo 100 caracteres.", nameof(identificacaoChip));

        if (string.IsNullOrWhiteSpace(iccid))
            throw new ArgumentException("ICCID é obrigatório.", nameof(iccid));

        var iccidTrim = iccid.Trim();
        if (iccidTrim.Length > 22)
            throw new ArgumentException("ICCID deve ter no máximo 22 caracteres.", nameof(iccid));

        if (!string.IsNullOrWhiteSpace(ddd) && ddd.Trim().Length > 3)
            throw new ArgumentException("DDD deve ter no máximo 3 caracteres.", nameof(ddd));

        if (!string.IsNullOrWhiteSpace(planoTipo) && planoTipo.Trim().Length > 100)
            throw new ArgumentException("Tipo de plano deve ter no máximo 100 caracteres.", nameof(planoTipo));

        if (quantidadeMinutos is < 0)
            throw new ArgumentException("Quantidade de minutos não pode ser negativa.", nameof(quantidadeMinutos));

        if (quantidadeInternet is < 0)
            throw new ArgumentException("Quantidade de internet não pode ser negativa.", nameof(quantidadeInternet));

        return new Simcard
        {
            OperadoraId = operadoraId,
            IdentificacaoChip = identificacaoChip.Trim(),
            Iccid = iccidTrim,
            Ddd = string.IsNullOrWhiteSpace(ddd) ? null : ddd.Trim(),
            PlanoTipo = string.IsNullOrWhiteSpace(planoTipo) ? null : planoTipo.Trim(),
            TemMinutagem = temMinutagem,
            QuantidadeMinutos = temMinutagem ? quantidadeMinutos : null,
            TemInternet = temInternet,
            QuantidadeInternet = temInternet ? quantidadeInternet : null,
            DataAquisicao = dataAquisicao,
            DataAtivacao = dataAtivacao,
            Status = SimcardStatus.EmEstoque,
            Observacoes = string.IsNullOrWhiteSpace(observacoes) ? null : observacoes.Trim(),
            Ativo = ativo,
            DataCadastro = DateTime.Now
        };
    }

    /// <summary>
    /// Atualiza os dados cadastrais editáveis do SIMCARD.
    /// Status é alterado apenas por <see cref="AlterarStatus"/>.
    /// </summary>
    public void AtualizarDados(
        int operadoraId,
        string identificacaoChip,
        string iccid,
        string? ddd,
        string? planoTipo,
        bool temMinutagem,
        int? quantidadeMinutos,
        bool temInternet,
        int? quantidadeInternet,
        DateTime? dataAquisicao,
        DateTime? dataAtivacao,
        string? observacoes)
    {
        if (operadoraId <= 0)
            throw new ArgumentException("Operadora é obrigatória.", nameof(operadoraId));

        if (string.IsNullOrWhiteSpace(identificacaoChip))
            throw new ArgumentException("Identificação do chip é obrigatória.", nameof(identificacaoChip));

        if (identificacaoChip.Trim().Length > 100)
            throw new ArgumentException("Identificação do chip deve ter no máximo 100 caracteres.", nameof(identificacaoChip));

        if (string.IsNullOrWhiteSpace(iccid))
            throw new ArgumentException("ICCID é obrigatório.", nameof(iccid));

        var iccidTrim = iccid.Trim();
        if (iccidTrim.Length > 22)
            throw new ArgumentException("ICCID deve ter no máximo 22 caracteres.", nameof(iccid));

        if (!string.IsNullOrWhiteSpace(ddd) && ddd.Trim().Length > 3)
            throw new ArgumentException("DDD deve ter no máximo 3 caracteres.", nameof(ddd));

        if (!string.IsNullOrWhiteSpace(planoTipo) && planoTipo.Trim().Length > 100)
            throw new ArgumentException("Tipo de plano deve ter no máximo 100 caracteres.", nameof(planoTipo));

        if (quantidadeMinutos is < 0)
            throw new ArgumentException("Quantidade de minutos não pode ser negativa.", nameof(quantidadeMinutos));

        if (quantidadeInternet is < 0)
            throw new ArgumentException("Quantidade de internet não pode ser negativa.", nameof(quantidadeInternet));

        OperadoraId = operadoraId;
        IdentificacaoChip = identificacaoChip.Trim();
        Iccid = iccidTrim;
        Ddd = string.IsNullOrWhiteSpace(ddd) ? null : ddd.Trim();
        PlanoTipo = string.IsNullOrWhiteSpace(planoTipo) ? null : planoTipo.Trim();
        TemMinutagem = temMinutagem;
        QuantidadeMinutos = temMinutagem ? quantidadeMinutos : null;
        TemInternet = temInternet;
        QuantidadeInternet = temInternet ? quantidadeInternet : null;
        DataAquisicao = dataAquisicao;
        DataAtivacao = dataAtivacao;
        Observacoes = string.IsNullOrWhiteSpace(observacoes) ? null : observacoes.Trim();
        DataAlteracao = DateTime.Now;
    }

    /// <summary>
    /// Altera o status do SIMCARD. Apenas os 8 valores definidos no
    /// Prompt 005 são aceitos (nenhuma transição adicional é criada).
    /// </summary>
    public void AlterarStatus(SimcardStatus novoStatus)
    {
        if (!Enum.IsDefined(typeof(SimcardStatus), novoStatus))
            throw new ArgumentException("Status inválido para SIMCARD.", nameof(novoStatus));

        if (Status == novoStatus)
            return;

        Status = novoStatus;
        DataAlteracao = DateTime.Now;
    }

    /// <summary>Ativa o SIMCARD (sem exclusão física, o chip permanece cadastrado).</summary>
    public void Ativar()
    {
        Ativo = true;
        DataAlteracao = DateTime.Now;
    }

    /// <summary>Desativa o SIMCARD preservando o registro (regra de integridade).</summary>
    public void Desativar()
    {
        Ativo = false;
        DataAlteracao = DateTime.Now;
    }
}