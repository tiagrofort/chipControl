# Prompt 018 — Adequação da UX/UI WPF ao padrão visual do Stitch

## Objetivo

Alinhar a camada de apresentação WPF (Login, MainWindow, Dashboard, Placeholder,
UsuarioGerenciamento, UsuarioModal, FuncionarioGerenciamento, FuncionarioModal) ao
padrão visual definido no Google Stitch, sem alterar regras de negócio, banco,
migrations, autenticação ou navegação.

## Referências Stitch analisadas

- `docs/ux_design/login_controle_de_chips/code.html` (login, card centralizado, botão primário)
- `docs/ux_design/dashboard_controle_de_chips/code.html` (sidebar, header, saudação, cards de indicadores)
- `docs/ux_design/controle_de_chips_visual_identity/DESIGN.md` (identidade visual oficial)
- `docs/ux_design/gerenciamento_de_simcards/code.html` (listagem: toolbar, busca, tabela, status pill, ações)
- `docs/ux_design/troca_de_n_meros/code.html` (formulários, grids)
- `docs/ux_design/substitui_o_de_simcard/code.html` (modais)
- `docs/ux_design/hist_rico_do_simcard_modal/code.html` (modal)
- `docs/ux_design/rea_de_relat_rios/code.html` (área de conteúdo, cards)

## Diagnóstico (auditoria Stitch × WPF atual)

- Sidebar genérica de WPF: sem ícones, sem accent de seleção, densidade e alturas divergentes.
- Header sem hierarquia clara e com textos próximos.
- Dashboard praticamente vazio, sem cards/saudação do Stitch.
- Placeholder com aparência genérica.
- Cores hardcoded (`#005A9E`, `#C1C7D2`, `#E1E2E8`...) espalhadas nos XAMLs em vez do
  esquema M3 do DESIGN.md (primary #005A9E/#00427D, containers, outline, status).
- DataGrid com aparência padrão (sem pill de status, cabeçalho divergente).
- Botões/inputs estilizados inline em cada tela, sem reuso.
- Work Sans apenas citada por literal `FontFamily="Work Sans"` (sem fallback declarado).

## Design System WPF criado

`src/ChipControl.Presentation.WPF/Resources/DesignSystem.xaml` (única fonte de verdade,
mesclado no `App.xaml`):

- **Cores** (Color + Brush): `Primary/PrimaryContainer/OnPrimary/OnPrimaryContainer/PrimaryFixed`,
  `Surface/SurfaceLowest/SurfaceContainer(Low|High)/SurfaceVariant`, `OnSurface(Variant)`,
  `Outline/OutlineVariant`, `Error/ErrorContainer/OnErrorContainer`, `SuccessBg/SuccessFg`,
  `BorderRow`, `Hover`.
- **Tipografia**: `FontFamilyWorkSans` (Work Sans, fallback Segoe UI), `FontFamilyMono`;
  estilos `TypoDisplay` (28/600), `TypoHeadline` (20/600), `TypoTitleMd` (16/600),
  `TypoBodyMd` (13/400), `TypoLabelSm` (12/500), `FormLabelStyle`.
- **Espaços/raios**: `SpaceXs/Sm/Md/Lg/Page` (escala 4px), `CornerRadiusSm/Default/Lg/Full`.
- **Ícones**: geometrias vetoriais Material Design (Apache 2.0) sem dependência externa
  (dashboard, simcard, pessoas, empresa, telefone, troca, substituição, histórico,
  relatórios, busca, novo, editar, atualizar, sair, etc.).
- **Botões**: `ButtonPrimary`, `ButtonGhost`, `ButtonAction`, `NavItemButton`,
  `NavItemButtonSelected` (accent bar + fundo + hover conforme Stitch).
- **Campos**: `TextBoxInput`, `TextBoxSearch`, `PasswordBoxInput`, `ComboBoxInput`
  (borda 1px, foco 2px primário, raio 4, altura 36).
- **DataGrid**: `DataGridChipControl` (cabeçalho cinza-azulado, linhas 40px, grade sutil,
  hover, seleção `PrimaryFixed`, células sem borda dupla).
- **Componentes**: `CardBorder`, `StatusPill` (verde/vermelho por estado).

## Alterações por tela

- **MainWindow**: sidebar 260px com logo, itens com ícone (Path) + label, estado selecionado
  com accent bar primária; header com bloco de saudação (título da tela + usuário logado),
  usando `TypoDisplay/TypoLabelSm`; separador de seções. Mesmos 9 itens de menu preservados.
- **MainWindow.xaml.cs**: novo handler `OnNavItem` com `NavigateTo`/`SetSelectedNav`
  (marca visual do item ativo) — navegação por `Frame` preservada.
- **DashboardView**: saudação "Bom dia/Boa tarde/Boa noite" + nome do usuário (herdado do
  DataContext da MainWindow), linha de cards de indicadores (visual do Stitch, sem dados
  inventados — valores "—" até os módulos existirem).
- **PlaceholderView**: card centralizado com ícone do módulo, título e mensagem
  "Módulo em desenvolvimento." — binding `{Binding Titulo, Mode=OneWay}` **preservado**
  (regressão do Prompt 017 não reintroduzida).
- **UsuarioGerenciamentoView / FuncionarioGerenciamentoView**: action bar com busca
  (`TextBoxSearch` + ícone), botões `Atualizar` (ghost) e `+ Novo` (primário); grid com
  `DataGridChipControl`, status em pill e ações `Editar` / `Ativar/Desativar` com
  `ButtonAction`. Funcionários agora idêntico a Usuários (não há mais estilo próprio).
- **UsuarioModalView / FuncionarioModalView**: fundo com overlay, cartão central com
  sombra, título `TypoTitleMd`, labels `FormLabelStyle`, campos `TextBoxInput`,
  rodapé com `Cancelar` (ghost) e `Salvar` (primário). Corrigido `StackPanel` órfão
  no FuncionarioModalView.
- **LoginWindow**: já estava aderente ao Stitch (Prompt 012); conferido campo a campo
  contra `code.html` — nenhuma divergência relevante encontrada.
- **App.xaml**: recursos duplicados antigos (`PrimaryBrush`, `InputStyle`, `ButtonStyle`...)
  removidos; apenas o `DesignSystem.xaml` permanece mesclado.

## Decisões técnicas

1. **Work Sans**: distribuída como `FontFamily` com fallback `Segoe UI` (fonte do SO).
   Se a fonte não estiver instalada na máquina, o fallback garante consistência sem
   quebrar a aplicação; não foi embutida como recurso por não haver arquivo de fonte
   licenciado no repositório.
2. **Ícones**: paths vetoriais Material Design inline no ResourceDictionary (sem pacote
   NuGet externo, sem emojis).
3. **Estados de seleção do menu**: implementados trocando o `Style` do botão
   (`NavItemButton` ↔ `NavItemButtonSelected`) no code-behind — abordagem simples para
   um componente puramente visual, sem MVVM adicional.
4. **Dados do Dashboard**: nenhum dado inventado; cards exibem "—" (estrutura visual
   pronta para os módulos futuros).

## Arquivos

Criados:
- `src/ChipControl.Presentation.WPF/Resources/DesignSystem.xaml`

Alterados:
- `src/ChipControl.Presentation.WPF/App.xaml`
- `src/ChipControl.Presentation.WPF/Views/MainWindow.xaml` (+ `.cs`)
- `src/ChipControl.Presentation.WPF/Views/DashboardView.xaml`
- `src/ChipControl.Presentation.WPF/Views/PlaceholderView.xaml`
- `src/ChipControl.Presentation.WPF/Views/UsuarioGerenciamentoView.xaml`
- `src/ChipControl.Presentation.WPF/Views/UsuarioModalView.xaml`
- `src/ChipControl.Presentation.WPF/Views/FuncionarioGerenciamentoView.xaml`
- `src/ChipControl.Presentation.WPF/Views/FuncionarioModalView.xaml`
- `tests/ChipControl.Tests/NavegacaoWpfTests.cs` (o harness agora extrai o rótulo do
  item de menu da visual tree, pois os itens passaram a ter ícone + texto)
- `docs/08-CHECKPOINTS.md` (checkpoints 017 e 018)

Removidos: nenhum arquivo de código.

## Validação

- `dotnet test`: **84/84 aprovados, 0 falhas, 0 ignorados** (inclui o teste de navegação
  real que clica nos 9 itens do menu).
- Build Debug: **0 erros, 0 warnings** (`bin\Debug\net8.0-windows\`).
- Build Release: **0 erros, 0 warnings** — exe em `build\Release\ChipControl.Presentation.WPF.exe`.
- Execução real da Release: processo inicia, permanece estável (login).
- Regressão 017 verificada: `Mode=OneWay` presente; nenhum `<vm:MainViewModel/>` em XAML;
  `MainWindow` continua sendo criada pela factory DI `Func<UsuarioAutenticadoDto, MainWindow>`.

## Pendências / limitações

- Screenshots automatizados não foram possíveis neste ambiente (sessão headless);
  a validação visual detalhada deve ser feita manualmente pelo usuário na Release.
- Os cards do Dashboard exibem placeholder "—" até que os módulos (SIMCARDs, Operadoras,
  Aparelhos etc.) sejam implementados.

