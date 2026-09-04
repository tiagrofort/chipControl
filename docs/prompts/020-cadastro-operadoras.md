# Prompt 020 — Cadastro de Operadoras

## Objetivo

Implementar o cadastro funcional de Operadoras no ChipControl.

## Documentacao consultada

- docs/prompts/004-cadastros-e-campos.md
- docs/prompts/005-consolidacao-cadastros.md
- docs/prompts/007-modelo-de-dados.md
- docs/02-REGRAS-DE-NEGOCIO.md
- docs/03-MODELO-DE-DADOS.md
- docs/prompts/018-adequacao-ux-ui-stitch.md
- docs/prompts/019-cadastro-simcards.md

## Auditoria inicial

- Operadora: entidade existente mas incompleta (faltavam validacoes, DataAlteracao, AtualizarDados)
- OperadoraDto: existente dentro de SimcardDto.cs (apenas Id/Nome) — removido e substituido
- ChipControlDbContext: ja possuia DbSet<Operadora> e configuracao Fluent
- ISimcardRepository.ListarOperadorasAsync(): ja existente

## Arquivos criados

- src/ChipControl.Domain/Interfaces/IOperadoraRepository.cs
- src/ChipControl.Infrastructure/Data/Repositories/OperadoraRepository.cs
- src/ChipControl.Application/DTOs/OperadoraDto.cs
- src/ChipControl.Application/UseCases/OperadoraUseCase.cs
- src/ChipControl.Presentation.WPF/ViewModels/OperadoraGerenciamentoViewModel.cs
- src/ChipControl.Presentation.WPF/ViewModels/OperadoraModalViewModel.cs
- src/ChipControl.Presentation.WPF/Views/OperadoraGerenciamentoView.xaml
- src/ChipControl.Presentation.WPF/Views/OperadoraGerenciamentoView.xaml.cs
- src/ChipControl.Presentation.WPF/Views/OperadoraModalView.xaml
- src/ChipControl.Presentation.WPF/Views/OperadoraModalView.xaml.cs
- tests/ChipControl.Tests/OperadoraTests.cs
- src/ChipControl.Persistence/Migrations/20260904125204_AddOperadoraGerenciamento.cs

## Arquivos alterados

- src/ChipControl.Domain/Entities/Operadora.cs — entidade completada
- src/ChipControl.Application/ApplicationServiceExtensions.cs — registro do UseCase
- src/ChipControl.Infrastructure/DependencyInjection/ServiceCollectionExtensions.cs — registro do Repository
- src/ChipControl.Infrastructure/Data/DatabaseInitializer.cs — lista de migrations legadas
- src/ChipControl.Application/DTOs/SimcardDto.cs — removido OperadoraDto duplicado
- src/ChipControl.Presentation.WPF/Views/MainWindow.xaml.cs — navegacao para OperadoraGerenciamentoView
- tests/ChipControl.Tests/SimcardTests.cs — ajustes nos parametros
- tests/ChipControl.Tests/NavegacaoWpfTests.cs — verificacao de Operadora
- tests/ChipControl.Tests/DatabaseInitializerTests.cs — contagem de migrations

## Regras de negocio

- Nome obrigatorio, codigo/CNPJ/telefone/email/observacoes opcionais
- Ativo obrigatorio, nome unico, CNPJ unico quando informado
- Sem exclusao fisica — desligamento pelo campo Ativo

## Migration

20260904125204_AddOperadoraGerenciamento — adiciona DataAlteracao a tabela Operadoras

## Testes

142 total (107 anteriores + 35 novos), todos aprovados

## Build

Debug: 0 erros, 0 warnings
Release: 0 erros, 0 warnings
Executavel: build\Release\ChipControl.Presentation.WPF.exe

## Problemas encontrados

1. Conflito OperadoraDto duplicado — resolvido
2. Assinatura Operadora.Create alterada — ajuste nos testes
3. EF Core Design package — versao 8.0.0 adicionada
4. Migration legada — DatabaseInitializer atualizado
5. Contagem de migrations — teste atualizado

## Resultado final

Prompt 020 CONCLUIDO com sistema compilado, testado e integrado.