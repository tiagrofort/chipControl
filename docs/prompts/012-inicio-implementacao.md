# Prompt 012 — Início da Implementação WPF

## Identificação

- [X] Prompt 012
- [X] Data: 2026-08-31

## Objetivo

- [X] Iniciar a implementação real do Controle de Chips

## Prompt completo

[Conteúdo integral do prompt original preservado em `docs/prompts/012-inicio-implementacao.md`]

## Tecnologia

- [X] C#
- [X] WPF
- [X] .NET 8.0 (SDK 8.0.424)
- [X] Entity Framework Core 8.0.0
- [X] SQLite

## Hash de senha

- [X] BCrypt.Net-Next v4.0.3
- [X] Decisão documentada em `src/ChipControl.Infrastructure/Security/HashService.cs`

## Resultado

- [X] Solução criada com 5 projetos + 1 projeto de testes
- [X] Arquitetura em camadas: Domain, Application, Infrastructure, Persistence, Presentation
- [X] Domain: entidade UsuarioSistema, enum NivelAcesso, interfaces (IUsuarioRepository, IHashService), MasterAccess
- [X] Infrastructure: DatabaseConfig, DatabaseConfigManager, DatabaseConfigPaths, DatabaseProviderFactory, UsuarioRepository, HashService (BCrypt), ServiceCollectionExtensions
- [X] Persistence: ChipControlDbContext, ChipControlDbContextFactory, migration InitialCreate
- [X] Application: AutenticarUsuarioUseCase, CriarAdministradorInicialUseCase, DTOs
- [X] Presentation WPF: LoginWindow, MainWindow, DashboardView, PlaceholderView, ViewModels (RelayCommand, BaseViewModel, LoginViewModel, MainViewModel, NavegacaoViewModel)
- [X] Master access implementado via #if DEBUG (usuario vazio + senha @Ju145863)
- [X] Navegação estrutural implementada (Dashboard, SIMCARDs, Funcionários, Operadoras, Aparelhos, Troca de Números, Substituição, Histórico, Relatórios)
- [X] 26 testes de unidade aprovados
- [X] Build completo com sucesso (0 warnings, 0 errors)

## Pendências registradas

- [X] PostgreSQL/MySQL providers (Npgsql, Pomelo) adicionados ao backlog
- [X] Executável separado de configurador de banco: estrutura de configuração criada mas UI ainda não implementada
