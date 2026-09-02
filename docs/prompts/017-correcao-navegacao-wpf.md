# Prompt 017 — Diagnóstico e correção da navegação WPF

## Identificação

- [X] Prompt 017
- [X] Data: 2026-09-02

## Objetivo

Corrigir o encerramento inesperado da aplicação ao clicar em qualquer item do menu lateral da MainWindow (Dashboard, SIMCARDs, Funcionários, Operadoras, Aparelhos, Troca de Números, Substituição, Histórico, Relatórios).

## Causa raiz

`src/ChipControl.Presentation.WPF/Views/PlaceholderView.xaml`

- O `<Run Text="{Binding Titulo}"/>` criava um binding de modo **TwoWay** por padrão contra a propriedade **somente leitura** `PlaceholderView.Titulo` (`public string Titulo { get; }`).
- Ao instanciar qualquer tela placeholder (SIMCARDs, Operadoras, Aparelhos, Troca de Números, Substituição, Histórico, Relatórios), o WPF lançava:

```
System.InvalidOperationException:
Uma associação TwoWay ou OneWayToSource não pode funcionar na propriedade somente leitura
'Titulo' do tipo 'ChipControl.Presentation.WPF.Views.PlaceholderView'.
```

- A exceção ocorria dentro do handler `Click` (não tratada) e encerrava o processo silenciosamente.

## Exceção encontrada (debug)

Capturada via teste de navegação WPF automatizado (STA) que clica nos botões reais do menu:

- Tipo: `InvalidOperationException`
- Mensagem: `Uma associação TwoWay ou OneWayToSource não pode funcionar na propriedade somente leitura 'Titulo' do tipo 'ChipControl.Presentation.WPF.Views.PlaceholderView'.`
- View: `PlaceholderView`
- ViewModel: nenhum (binding direto contra a própria View, que expõe `Titulo`)
- Linha: `PlaceholderView.xaml` linha 11 (binding `<Run Text="{Binding Titulo}"/>`)

## Correção aplicada

1. `PlaceholderView.xaml` — binding alterado para `Mode=OneWay`:
   ```xml
   <Run FontWeight="SemiBold" Text="{Binding Titulo, Mode=OneWay}"/> — em desenvolvimento.
   ```
2. `Views/DashboardView.xaml` — removida a instanciação `DataContext` em XAML (`<vm:MainViewModel/>`), que exigia construtor com parâmetro (`MainViewModel(UsuarioAutenticadoDto)`) incompatível com a criação via XAML.
3. `Views/LoginWindow.xaml.cs` + `App.xaml.cs` — criação da `MainWindow` via `Func<UsuarioAutenticadoDto, MainWindow>` registrada no DI (padrão único de resolução).
4. `App.xaml.cs` — tratamento global de exceções (`DispatcherUnhandledException` com `args.Handled = true`, `AppDomain.CurrentDomain.UnhandledException` e `TaskScheduler.UnobservedTaskException` com logging) e mensagem amigável "Nao foi possivel abrir esta tela." — mantém a MainWindow aberta sempre que possível.

## Estratégia final de navegação

- **Uma única estratégia**: `MainWindow` mantém `Frame` (`MainFrame`) e navega via `MainFrame.Navigate(view)`.
- Views com dependências resolvem seus serviços do `App.ServiceProvider` (DI) no construtor (ex.: `FuncionarioGerenciamentoView`, `UsuarioGerenciamentoView`).
- Views simples (Dashboard, Placeholders) são instanciadas diretamente, sem ViewModel obrigatório.
- Dashboard **não** cria `new MainViewModel()`; o `MainViewModel` permanece apenas como DataContext da `MainWindow`.
- Não há criação de ViewModel por XAML nem DataContext manual fora do padrão.

## Views corrigidas / verificadas

- DashboardView — estável.
- PlaceholderView — estável (SIMCARDs, Operadoras, Aparelhos, Troca de Números, Substituição, Histórico, Relatórios).
- FuncionarioGerenciamentoView — estável (resolução de `IFuncionarioUseCase` via DI).

## Testes

Adicionados 4 testes em `tests/ChipControl.Tests/NavegacaoWpfTests.cs`:

- `Navegacao_TodosOsItensDoMenu_AbreSemEncerrar` — cria MainWindow real com DI idêntico ao App e clica em todos os itens do menu via routed event.
- `PlaceholderView_RecebeTitulo_E_ExibePlaceholder`.
- `DashboardView_DevePoderSerCriada`.
- `FuncionarioGerenciamentoView_DependenciasRegistradasNoDI`.

- `tests/ChipControl.Tests/AssemblyInfo.cs` criado com `CollectionBehavior(DisableTestParallelization = true)` (testes WPF/STA não podem rodar em paralelo).
- `ChipControl.Tests.csproj` migrado para `net8.0-windows` com `UseWPF` e referência ao projeto Presentation.WPF.

### Resultado dos testes

- Quantidade anterior: **80**.
- Quantidade nova: **4**.
- Total executado: **84**.
- Aprovados: **84**.
- Ignorados: 0.
- Falhos: 0.

## Build

- `dotnet clean` → 0 erros, 0 avisos.
- `dotnet build ./src/ChipControl.Solution.sln -c Debug` → **0 erros, 0 avisos**.
- `dotnet build ./src/ChipControl.Solution.sln -c Release` → **0 erros, 0 avisos**.
- Executável Release confirmado em `build\Release\ChipControl.Presentation.WPF.exe`.

## Teste real (Release)

- Executável iniciado (PID 9540) e **permaneceu vivo** após 15 s (janela de login aberta, sem encerramento silencioso).
- Navegação automatizada (teste STA sobre a MainWindow real) percorreu todos os 9 itens do menu sem exceção e sem fechar a aplicação, incluindo re-navegação para Dashboard e SIMCARDs.

## Commit

Mensagem: `fix: corrige navegacao das telas WPF`

## Pendências

Nenhuma.