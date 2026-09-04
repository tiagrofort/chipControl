using ChipControl.Application.DTOs;
using ChipControl.Domain.Entities;
using ChipControl.Domain.Interfaces;

namespace ChipControl.Application.UseCases;

public interface IOperadoraUseCase
{
    Task<IEnumerable<OperadoraDto>> ListarAsync();
    Task<OperadoraDto?> BuscarPorIdAsync(int id);
    Task<bool> CriarAsync(CriarOperadoraDto dto);
    Task<bool> EditarAsync(EditarOperadoraDto dto);
    Task<bool> AlternarAtivoAsync(int id);
    Task<IEnumerable<OperadoraDto>> PesquisarAsync(string termo);
    Task<IEnumerable<OperadoraDto>> ListarAtivasAsync();
}

public class OperadoraUseCase : IOperadoraUseCase
{
    private readonly IOperadoraRepository _repository;

    public OperadoraUseCase(IOperadoraRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OperadoraDto>> ListarAsync()
    {
        var operadoras = await _repository.ListarAsync();
        return operadoras.Select(MapToDto);
    }

    public async Task<OperadoraDto?> BuscarPorIdAsync(int id)
    {
        var operadora = await _repository.BuscarPorIdAsync(id);
        return operadora != null ? MapToDto(operadora) : null;
    }

    public async Task<bool> CriarAsync(CriarOperadoraDto dto)
    {
        ValidarDto(dto.Nome, dto.Codigo, dto.Cnpj, dto.Telefone, dto.Email);

        var nome = dto.Nome.Trim();
        if (await _repository.ExisteOperadoraAsync(nome))
            throw new InvalidOperationException("Já existe uma operadora cadastrada com este nome.");

        if (!string.IsNullOrWhiteSpace(dto.Cnpj))
        {
            var cnpj = dto.Cnpj.Trim();
            if (await _repository.ExisteCnpjAsync(cnpj))
                throw new InvalidOperationException("Já existe uma operadora cadastrada com este CNPJ.");
        }

        var operadora = Operadora.Create(
            nome, dto.Codigo, dto.Cnpj, dto.Telefone, dto.Email, dto.Observacoes, dto.Ativo);

        await _repository.AdicionarAsync(operadora);
        return true;
    }

    public async Task<bool> EditarAsync(EditarOperadoraDto dto)
    {
        if (dto.Id <= 0)
            throw new ArgumentException("Operadora inválida para edição.");

        ValidarDto(dto.Nome, dto.Codigo, dto.Cnpj, dto.Telefone, dto.Email);

        var operadora = await _repository.BuscarPorIdAsync(dto.Id);
        if (operadora == null)
            throw new InvalidOperationException("Operadora não encontrada.");

        var nome = dto.Nome.Trim();
        if (await _repository.ExisteOperadoraAsync(nome, dto.Id))
            throw new InvalidOperationException("Já existe uma operadora cadastrada com este nome.");

        if (!string.IsNullOrWhiteSpace(dto.Cnpj))
        {
            var cnpj = dto.Cnpj.Trim();
            if (await _repository.ExisteCnpjAsync(cnpj, dto.Id))
                throw new InvalidOperationException("Já existe uma operadora cadastrada com este CNPJ.");
        }

        operadora.AtualizarDados(
            nome, dto.Codigo, dto.Cnpj, dto.Telefone, dto.Email, dto.Observacoes, dto.Ativo);

        await _repository.AtualizarAsync(operadora);
        return true;
    }

    public async Task<bool> AlternarAtivoAsync(int id)
    {
        var operadora = await _repository.BuscarPorIdAsync(id);
        if (operadora == null)
            throw new InvalidOperationException("Operadora não encontrada.");

        if (operadora.Ativo)
            operadora.DefinirInativo();
        else
            operadora.DefinirAtivo();

        await _repository.AtualizarAsync(operadora);
        return true;
    }

    public async Task<IEnumerable<OperadoraDto>> PesquisarAsync(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
            return await ListarAsync();

        var resultados = await _repository.PesquisarAsync(termo.Trim());
        return resultados.Select(MapToDto);
    }

    public async Task<IEnumerable<OperadoraDto>> ListarAtivasAsync()
    {
        var operadoras = await _repository.ListarAtivasAsync();
        return operadoras.Select(MapToDto);
    }

    private static void ValidarDto(string nome, string? codigo, string? cnpj, string? telefone, string? email)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome da operadora é obrigatório.");

        if (nome.Trim().Length > 100)
            throw new ArgumentException("Nome da operadora deve ter no máximo 100 caracteres.");

        if (!string.IsNullOrWhiteSpace(codigo) && codigo.Trim().Length > 20)
            throw new ArgumentException("Código deve ter no máximo 20 caracteres.");

        if (!string.IsNullOrWhiteSpace(cnpj) && cnpj.Trim().Length > 20)
            throw new ArgumentException("CNPJ deve ter no máximo 20 caracteres.");

        if (!string.IsNullOrWhiteSpace(telefone) && telefone.Trim().Length > 30)
            throw new ArgumentException("Telefone deve ter no máximo 30 caracteres.");

        if (!string.IsNullOrWhiteSpace(email) && email.Trim().Length > 255)
            throw new ArgumentException("E-mail deve ter no máximo 255 caracteres.");
    }

    private static OperadoraDto MapToDto(Operadora operadora) => new()
    {
        Id = operadora.Id,
        Nome = operadora.Nome,
        Codigo = operadora.Codigo,
        Cnpj = operadora.Cnpj,
        Telefone = operadora.Telefone,
        Email = operadora.Email,
        Ativo = operadora.Ativo,
        Observacoes = operadora.Observacoes,
        DataCadastro = operadora.DataCadastro,
        DataAlteracao = operadora.DataAlteracao
    };
}