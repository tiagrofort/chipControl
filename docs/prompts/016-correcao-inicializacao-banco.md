# Prompt 016 — Correção da inicialização, primeira execução e ciclo de vida do banco

## Identificação
- [X] Prompt 016
- [X] Data: 2026-09-01

## Objetivo
Corrigir o processo de inicialização do ChipControl, especialmente primeira execução, criação do banco SQLite, arquivo de configuração, aplicação de migrations e criação do administrador inicial. Eliminar o erro `SQLite Error 1: 'table "UsuariosSistema" already exists'` que ocorria na segunda execução Release.

## Problema encontrado (causa raiz)
`src/ChipControl.Presentation.WPF/Services/DatabaseInitializer.cs` (versão anterior) continha, na linha 26:

```csharp
await context.Database.EnsureCreatedAsync();
```

seguido de:

```csharp
var pending = await context.Database.GetPendingMigrationsAsync();
if (pending.Any())
    await context.Database.MigrateAsync();
```

Esse padrão é **mutuamente excludente**: `EnsureCreated()` cria as tabelas diretamente, sem registrar nada em `__EFMigrationsHistory`. Na segunda execução, `Migrate()` via `GetPendingMigrationsAsync()` retornava as 3 migrations como pendentes e tentava executar a `InitialCreate`, que continha `CREATE TABLE UsuariosSistema` — colidindo com a tabela já criada.

Adicionalmente, `DatabaseProviderFactory.EnsureCreated(...)` era um método paralelo disponível no projeto (não chamado pelo fluxo principal, mas presente).

## Análise do fluxo anterior
1. `App.xaml.cs::OnStartup` lia `database.json` (criava padrão se ausente).
2. Registrava `ChipControlDbContext` via `DatabaseProviderFactory.ConfigureDbContext`.
3. Resolvia `DatabaseInitializer` e chamava `EnsureSeedAsync()`.
4. `EnsureSeedAsync` executava `EnsureCreated()` → depois `MigrateAsync()` se houvesse pendentes.
5. Se `repo.CountAsync() == 0`, criava usuário `admin / admin123`.

Onde o SQLite ficava: `%APPDATA%\ChipControl\chipcontrol.db`. `database.json` em `%APPDATA%\ChipControl\database.json`.

## Solução implementada

### DatabaseInitializer reescrito
- **Removido `EnsureCreated` completamente.**
- Uso exclusivo de `Database.MigrateAsync()`.
- Antes do `Migrate`, verifica se o banco é legado (tabelas existem mas `__EFMigrationsHistory` não); se for, registra as migrations conhecidas como aplicadas.
- Concorrência: `SemaphoreSlim` + flag estática `Volatile` para garantir inicialização única por processo.
- Logs via `Debug.WriteLine` para diagnóstico.

### DatabaseProviderFactory
- Removido o método `EnsureCreated(ChipControlDbContext)`.

### App.xaml.cs
- Caminho do banco agora: `%APPDATA%\ChipControl\chipcontrol.db` (mantido lowercase para compatibilidade com o já criado).
- Mensagem amigável em caso de erro: "Nao foi possivel inicializar o banco de dados do ChipControl." (com detalhe técnico no box).
- `InitializeAsync()` substituindo `EnsureSeedAsync()`.

### Migrations
- Nenhuma migration foi alterada.
- `__EFMigrationsHistory` é consultada e atualizada exclusivamente pelo EF.

## Comportamento da primeira execução
1. Não existe `%APPDATA%\ChipControl\`.
2. `App.OnStartup` chama `DatabaseConfigPaths.GetConfigPath()` — cria diretório.
3. `database.json` ausente → cria com provider=SQLite, ConnectionString=`Data Source=...chipcontrol.db`.
4. Constrói o `IServiceProvider` com `ChipControlDbContext`, repos, use cases, etc.
5. `DatabaseInitializer.InitializeAsync()`:
   - Verifica legado: `__EFMigrationsHistory` ausente, `UsuariosSistema` ausente → não há legado.
   - `MigrateAsync()` cria todas as tabelas e popula `__EFMigrationsHistory`.
   - `repo.ExisteLoginAsync("admin") == false` → cria admin com `admin123`.
6. Abre `LoginWindow`.

## Localização do database.json
`%APPDATA%\ChipControl\database.json`

## Localização do SQLite
`%APPDATA%\ChipControl\chipcontrol.db`

## Estratégia de migrations
- `Database.MigrateAsync()` como mecanismo único e oficial.
- `__EFMigrationsHistory` é a fonte da verdade.
- Não há fallback para `EnsureCreated`.

## Estratégia do administrador inicial
- Criado em `DatabaseInitializer.EnsureInitialAdminAsync`.
- Verificação de existência via `repo.ExisteLoginAsync("admin")` — idempotente.
- Senha: `admin123` (apenas para o usuário admin inicial).
- Nível: `Administrador`.
- Hash via `IHashService` (BCrypt).

## Tratamento de banco existente
- Detecta `__EFMigrationsHistory` presente → pula recovery.
- Detecta tabelas presentes sem `__EFMigrationsHistory` (legado do `EnsureCreated`) → registra as 3 migrations como aplicadas e prossegue normalmente.
- `MigrateAsync()` aplica apenas pendentes.

## Tratamento de banco inexistente
- `MigrateAsync()` cria todas as tabelas e o histórico de migrations.

## Testes
11 novos testes em `DatabaseInitializerTests` (TESTE 1 a TESTE 10 + TESTE BONUS), cobrindo:
1. Primeira execução completa.
2. Segunda execução sem duplicação.
3. Banco já atualizado.
4. Migration pendente aplicada.
5. Admin já existente não duplica.
6. `database.json` existe, banco não.
7. Configuração padrão usada quando ausente.
8. Erro de inicialização não destrói dados (testado com `Mode=ReadOnly`).
9. Grep do `EnsureCreated` no código (não pode haver).
10. Duas inicializações consecutivas no mesmo banco.
- BÔNUS: banco legado (tabelas sem `__EFMigrationsHistory`) é recuperado.

### Resultado dos testes
- Quantidade anterior: 69.
- Quantidade nova: 11.
- Total executado: **80**.
- Aprovados: 80.
- Ignorados: 0.
- Falhos: 0.

## Build Debug
`dotnet build .\src\ChipControl.Solution.sln -c Debug` → **0 erros, 0 warnings**.

## Build Release
`dotnet clean` + `dotnet build .\src\ChipControl.Solution.sln -c Release` → **0 erros, 0 warnings**.
Executável: `build\Release\ChipControl.Presentation.WPF.exe`.

## Teste real da primeira execução (cenário de aceitação)
1. Backup de `%APPDATA%\ChipControl` → `ChipControl.bak`.
2. Remoção completa de `%APPDATA%\ChipControl`.
3. Execução de `build\Release\ChipControl.Presentation.WPF.exe`.
   - **Resultado**: processo iniciou (PID 20280) sem erro.
   - Arquivos criados: `database.json` (272 bytes), `chipcontrol.db` (28672 bytes).
4. Encerramento do processo.

## Teste real da segunda execução (mesmo banco)
5. Execução de `build\Release\ChipControl.Presentation.WPF.exe` novamente.
   - **Resultado**: processo iniciou (PID 22400) sem erro. Arquivos mantidos inalterados.
6. Terceira execução também sem erro.
7. Restauração do backup.

**Erro original "table UsuariosSistema already exists" eliminado.**

## Acesso master
- Preservado. Verificado em `AutenticarUsuarioUseCase.cs:25` dentro de `#if DEBUG`.
- No Release, o bloco é excluído, portanto acesso master desabilitado.

## Arquivos criados
- `tests/ChipControl.Tests/DatabaseInitializerTests.cs`
- `docs/prompts/016-correcao-inicializacao-banco.md` (este)

## Arquivos alterados
- `src/ChipControl.Infrastructure/Data/DatabaseInitializer.cs` (movido do WPF para Infrastructure, reescrito)
- `src/ChipControl.Infrastructure/Data/Providers/DatabaseProviderFactory.cs` (removido `EnsureCreated`)
- `src/ChipControl.Presentation.WPF/App.xaml.cs` (mensagem amigável, namespace)
- (Removido) `src/ChipControl.Presentation.WPF/Services/DatabaseInitializer.cs`

## Commit
Mensagem: `fix: corrige inicializacao e primeira execucao do banco`

## Pendências
Nenhuma.
