namespace ChipControl.Domain.Enums;

/// <summary>
/// Status válidos de um SIMCARD, conforme Prompt 005 / modelo de dados (seção 4).
/// Não criar transições ou status adicionais fora da documentação.
/// </summary>
public enum SimcardStatus
{
    /// <summary>Em estoque</summary>
    EmEstoque = 1,

    /// <summary>Em uso particular</summary>
    EmUsoParticular = 2,

    /// <summary>WhatsApp</summary>
    WhatsApp = 3,

    /// <summary>Danificado</summary>
    Danificado = 4,

    /// <summary>Perdido</summary>
    Perdido = 5,

    /// <summary>Não devolvido</summary>
    NaoDevolvido = 6,

    /// <summary>Descartado</summary>
    Descartado = 7,

    /// <summary>Inativo</summary>
    Inativo = 8
}
