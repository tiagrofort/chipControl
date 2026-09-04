# Prompt 019 — Cadastro de SIMCARDs

## Objetivo

Implementar o cadastro funcional de SIMCARDs conforme regras definidas no Prompt 004 e Prompt 005.

## Documentação consultada

- `docs/prompts/004-cadastros-e-campos.md` — Definição inicial dos campos de SIMCARD
- `docs/prompts/005-consolidacao-cadastros.md` — Consolidação dos campos
- `docs/prompts/007-modelo-de-dados.md` — Modelo de dados SIMCARDs
- `docs/02-REGRAS-DE-NEGOCIO.md` — Regras de negócio
- `docs/03-MODELO-DE-DADOS.md` — Modelo de dados completo
- `docs/prompts/018-adequacao-ux-ui-stitch.md` — Design System

## Regras do Prompt 004 identificadas

### Campos do SIMCARD

- ID (automático)
- Identificação do chip
- ICCID (único)
- Operadora (relacionamento)
- Plano/tipo de linha
- DDD (máximo 3 caracteres)
- Data de aquisição
- Data de ativação
- Observações
- Status
- Ativo/Inativo

### Status do SIMCARD

Conforme Prompt 004 e regras de negócio:

- `EmEstoque` — status padrão após cadastro
- `EmUsoParticular` — em uso pelo funcionário
- `WhatsApp` — uso exclusivo WhatsApp
- `Danificado` — chip com defeito
- `Perdido` — chip extraviado
- `NaoDevolvido` — não devolvido após desligamento
- `Descartado` — descartado
- `Inativo` — inativado

### Regras de duplicidade

- ICCID deve ser único (constraint no banco)
- Identificação do chip deve ser única por operadora (constraint composta)

## Auditoria inicial

### Entidades existentes

- `Simcard` — entidade já existente com validações de domínio
- `SimcardStatus` — enum já definido
- `Operadora` — entidade relacionada

### Infraestrutura existente

- `ISimcardRepository` — interface já definida
- `SimcardRepository` — implementação já existente
- `SimcardUseCase` — implementação já existente
- DTOs (`CriarSimcardDto`, `EditarSimcardDto`, `SimcardDto`) — já existentes

### ViewModels existentes

- `SimcardGerenciamentoViewModel` — já implementado
- `SimcardModalViewModel` — já implementado

### Telas faltantes (criadas neste prompt)

- `SimcardGerenciamentoView.xaml` — criada
- `SimcardModalView.xaml` — criada
- `SimcardModalView.xaml.cs` — criada

## Entidade

`Simcard` — entidade de domínio com:

- Factory method `Create` com validações
- Método `AtualizarDados` para edição
- Métodos de comportamento: `Ativar`, `Desativar`, `AlterarStatus`
- Validações de domínio nos métodos de fábrica e comportamento

## Repository

`ISimcardRepository` / `SimcardRepository`:

- `ListarAsync` — lista todos com include de Operadora
- `BuscarPorIdAsync` — busca por ID com include
- `PesquisarAsync` — pesquisa por termo (identificação, ICCID, operadora)
- `CriarAsync` — insere novo SIMCARD
- `AtualizarAsync` — atualiza existente
- `ExisteIccidAsync` — verifica duplicidade de ICCID
- `ExisteIdentificacaoNaOperadoraAsync` — verifica duplicidade composta
- `OperadoraExisteAsync` — valida operadora

## DbContext

Mapping Fluent no `ChipControlDbContext`:

- Tabela `Simcards`
- Chave primária `Id`
- Índice único em `Iccid`
- Índice composto único em `OperadoraId` + `IdentificacaoChip`
- Relacionamento com `Operadora` (Required, Restrict delete)

## Migration

`20260903133708_AddSimcardGerenciamento` — migration criada e aplicada ao banco.

## DTOs

- `CriarSimcardDto` — para criação
- `EditarSimcardDto` — para edição
- `SimcardDto` — para listagem e retorno

## UseCase

`SimcardUseCase` — implementa `ISimcardUseCase`:

- `ListarAsync` — lista todos os SIMCARDs
- `BuscarPorIdAsync` — busca por ID
- `PesquisarAsync` — pesquisa por termo
- `CriarAsync` — cria novo SIMCARD com validações
- `EditarAsync` — atualiza dados editáveis
- `AlternarAtivoAsync` — alterna ativo/inativo
- `AlterarStatusAsync` — altera status do SIMCARD
- `ListarOperadorasAsync` — lista operadoras para combo

## ViewModels

### SimcardGerenciamentoViewModel

- `Simcards` — `ObservableCollection<SimcardDto>`
- `TermoBusca` — propriedade de pesquisa
- `CarregarAsync` — carrega lista
- `PesquisarAsync` — executa pesquisa

### SimcardModalViewModel

- Propriedades para todos os campos do SIMCARD
- `Operadoras` e `OperadoraSelecionada` — combo de operadoras
- `SalvarAsync` — cria ou edita conforme modo
- Validações com `INotifyDataErrorInfo`
- `TituloModal` — "Novo SIMCARD" ou "Editar SIMCARD"

## Views

### SimcardGerenciamentoView

- Action Bar com pesquisa e botões "Atualizar" e "+ Novo"
- DataGrid com colunas: ID, Identificação Chip, ICCID, Operadora, Status, Ativo, Ações
- Colunas de Status e Ativo com badges coloridos (StatusPill)
- Ações: Editar, Status (menu contextual), Ativar/Desativar

### SimcardModalView

- Modal sem janela (border arredondada com sombra)
- Campos: Operadora (ComboBox), Identificação Chip, ICCID, DDD, Plano/Tipo
- Checkboxes: Possui minutagem, Possui internet (com campos condicionais)
- DatePickers: Data de Aquisição, Data de Ativação
- Checkbox: Ativo
- Campo: Observações
- Footer com botões "Cancelar" e "Salvar"

## Integração com menu

`MainWindow.xaml.cs` — método `Simcards_Click` atualizado para navegar para `SimcardGerenciamentoView` em vez de `PlaceholderView`.

## Design System utilizado

- Cores: `BrushPrimaryContainer`, `BrushSuccessBg`, `BrushErrorContainer`, etc.
- Tipografia: Work Sans
- Estilos: `ButtonPrimary`, `ButtonGhost`, `ButtonAction`, `TextBoxSearch`, `TextBoxInput`, `ComboBoxInput`, `DataGridChipControl`, `CardBorder`, `StatusPill`, `FormLabelStyle`, `TypoHeadline`, `TypoLabelSm`
- Ícones: `IconSearch`
- Espaçamentos: `SpacePage`, `SpaceLg`

## Funcionalidades implementadas

- [x] Listar SIMCARDs
- [x] Pesquisar SIMCARDs
- [x] Cadastrar SIMCARD
- [x] Editar SIMCARD
- [x] Alterar status (menu contextual)
- [x] Alternar ativo/inativo
- [x] Visualizar informações (grid e modal)
- [x] Validação de duplicidade (ICCID, Identificação+Operadora)
- [x] Modal para criação/edição
- [x] Pesquisa funcional

## Regras de negócio implementadas

- [x] ICCID único (validação + constraint)
- [x] Identificação do chip única por operadora (validação + constraint)
- [x] Status padrão "EmEstoque" ao criar
- [x] DDD máximo 3 caracteres
- [x] Status alterável apenas por método específico
- [x] Ativo/Inativo independente do status

## Testes criados

`tests/ChipControl.Tests/SimcardTests.cs` — 28 testes de unidade:

- Criação válida
- Campos obrigatórios (Identificação Chip, ICCID)
- Validações (tamanho máximo, DDD, duplicidade)
- Edição
- Pesquisa
- Busca por ID
- Status e transições
- Ativar/Desativar
- Comportamento de SIMCARD inativo

## Resultados

- **Testes**: 110 total (82 anteriores + 28 novos), todos aprovados
- **Build Debug**: 0 erros, 0 warnings
- **Build Release**: 0 erros, 0 warnings
- **Migration**: Aplicada com sucesso
- **Executável Release**: `build\Release\ChipControl.Presentation.WPF.exe`

## Problemas encontrados

1. **Validação DDD no UseCase** — Documentação (Prompt 004) define máximo 3 caracteres, mas UseCase validava 4. Corrigido.
2. **Indentação código** — Pequenos problemas de indentação corrigidos via PowerShell.

## Arquivos criados

- `src/ChipControl.Presentation.WPF/Views/SimcardGerenciamentoView.xaml`
- `src/ChipControl.Presentation.WPF/Views/SimcardModalView.xaml`
- `src/ChipControl.Presentation.WPF/Views/SimcardModalView.xaml.cs`
- `tests/ChipControl.Tests/SimcardTests.cs`
- `docs/prompts/019-cadastro-simcards.md`

## Arquivos alterados

- `src/ChipControl.Presentation.WPF/Views/MainWindow.xaml.cs` — Simcards navega para SimcardGerenciamentoView
- `src/ChipControl.Presentation.WPF/Views/SimcardGerenciamentoView.xaml.cs` — usings atualizados
- `src/ChipControl.Presentation.WPF/Resources/DesignSystem.xaml` — Adicionado estilo DatePickerInput
- `src/ChipControl.Application/UseCases/SimcardUseCase.cs` — Correção validação DDD
- `tests/ChipControl.Tests/NavegacaoWpfTests.cs` — Atualizado para SimcardGerenciamentoView
- `docs/08-CHECKPOINTS.md` — Checkpoint do Prompt 019 adicionado

## Pendências

Nenhuma pendência identificada para este prompt. Funcionalidades futuras (Troca de Números, Substituição, Histórico completo, Relatórios, Importação Excel) serão tratadas em prompts próprios.
