# Checkpoints

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Os checkpoints devem ser acordados com a equipe conforme o projeto avança.

## Histórico de alterações

* [x] **2026-08-27** — Estrutura inicial de checkpoints criada no Prompt 001.
* [x] **2026-08-27** — Prompt 003 executado. Formalizada a regra de significado de `[X]` (definido/aprovado) e `[ ]` (pendente). Corrigida a classificação dos checklists sem apagar o histórico do Prompt 002. O arquivo `docs/prompts/002-objetivo-e-escopo.md` permanece preservado por motivos de rastreabilidade.
* [x] **2026-08-27** — Prompt 004 executado. Definição inicial dos cadastros e seus campos: decisões já confirmadas registradas por cadastro; as listas de campos de cada cadastro e os status do SIMCARD permanecem pendentes. Criado o backlog permanente `docs/09-BACKLOG-FUTURO.md` (fora do escopo atual).
* [x] **2026-08-28** — Prompt 005 executado. Consolidou os campos dos cinco cadastros principais (Funcionários, Operadoras, SIMCARDs, Aparelhos, Usuários do Sistema) com todas as decisões já definidas.
* [x] **2026-08-28** — Prompt 006 executado. Documentadas as regras fundamentais de histórico e movimentação: princípio de preservação do histórico, ciclo de vida do SIMCARD, regras de números telefônicos, troca de números por importação, substituição de SIMCARD, troca simultânea de SIMCARD e número, histórico de funcionários, histórico de aparelhos, regras de WhatsApp, cadastro rápido, operações que preservam histórico. Itens pendentes preservados.
* [x] **2026-08-28** — Prompt 007 executado. Definido o modelo conceitual de dados com 8 tabelas, campos, chaves, relacionamentos, histórico, integridade e observações. Compatível com SQLite (inicial) e preparado para PostgreSQL/MySQL futuramente.
* [x] **2026-08-28** — Prompt 008 executado. Definida a arquitetura técnica completa: WPF + C#, arquitetura em camadas (Presentation/Application/Domain/Infrastructure/Persistence), SQLite inicial com suporte futuro para PostgreSQL/MySQL, executável separado de configuração do banco, autenticação posicionada, segurança de senha, logging, tratamento de erros.
* [x] **2026-08-28** — Prompt 009 executado. UX Design já existente do Google Stitch registrado como referência visual para implementação WPF. Design incorporado sem recriação. Interface documentada com layout de menu lateral, área de ações, grids, modais, pesquisa e cadastro rápido.
* [x] **2026-08-28** — Prompt 010 executado. Revisão final e congelamento da especificação. Decisões finais sobre modelo de histórico, relação SIMCARD/números, fluxo de troca de números, fluxo de substituição de SIMCARD, utilização/estoque, cadastro rápido, autenticação, acesso master de testes, estratégia de banco, relatórios essenciais e interface. Especificação considerada CONGELADA para início da codificação.
* [x] **2026-08-28** — Prompt 011 executado. Fechamento final pré-codificação: revisão estrutural final, UX Design (docs/ux_design/ e docs/ux_design.zip) versionado no Git, modelo de histórico e relação SIMCARD/número fechados, fluxo de troca de números e substituição fechados, formato de importação EXCEL (.xlsx), autenticação e acesso master documentados, banco inicial/futuro definido, relatórios da primeira versão confirmados e projeto congelado para início da codificação. Artefato residual `docs/prompts/002_objetivo_e_escopo.ps1` removido.
* [x] **2026-08-31** — Prompt 012 executado. Início da implementação real. Solução WPF criada com arquitetura em camadas (Domain, Application, Infrastructure, Persistence, Presentation). SQLite + EF Core 8.0.30. Autenticação com hash BCrypt.Net-Next. Acesso master via #if DEBUG. 26 testes de unidade aprovados. Build completo com sucesso.
* [x] **2026-09-01** — Prompt 014 executado. Cadastro funcional de Funcionários implementado em todas as camadas (Domain, Application, Infrastructure, Persistence, Presentation.WPF) seguindo o padrão visual do cadastro de Usuários. Migration `20260901121911_AddFuncionarioGerenciamento` criada e aplicada. 24 testes novos adicionados (total 69 testes, todos passando). Build com 0 erros e 0 warnings. Push realizado para `main`.
* [x] **2026-09-01** — Prompt 015 executado. Saneamento da base (removido diretório órfão `src/Views/`) e configuração da build Release do projeto WPF para `build\Release\`. `.gitignore` atualizado para ignorar a pasta `build/`. Incluído no commit o código válido do Prompt 013 que estava pendente. 69 testes continuam passando. Build Debug e Release com 0 erros e 0 warnings.
* [x] **2026-09-01** — Prompt 016 executado. Corrigida a inicialização do banco. Removido `EnsureCreated` (incompatível com Migrations). `DatabaseInitializer` migrado para `Infrastructure` e reescrito usando exclusivamente `Database.MigrateAsync()`. Adicionada recuperação para banco legado (tabelas sem `__EFMigrationsHistory`). Adicionada proteção contra inicialização concorrente. 11 novos testes (total 80 passando). Validação real de primeira/segunda execução na Release. Mensagem amigável em caso de erro. Build Debug e Release com 0 erros e 0 warnings.
* [x] **2026-09-02** — Prompt 017 executado. Corrigido o encerramento silencioso ao navegar pelo menu: causa raiz era binding TwoWay em propriedade somente leitura (`PlaceholderView.Titulo`). Navegação via `Frame` padronizada; `MainWindow` criada por factory DI `Func<UsuarioAutenticadoDto, MainWindow>`; tratamento global de exceções com logging e mensagem amigável. 4 novos testes de navegação STA (total 84 passando). Build Debug e Release com 0 erros e 0 warnings.
* [x] **2026-09-03** — Prompt 018 executado. UI WPF alinhada ao padrão visual do Google Stitch. Criado o Design System centralizado `Resources/DesignSystem.xaml` (cores, tipografia Work Sans com fallback, espaçamentos, raios, ícones vetoriais, botões, campos, DataGrid, card, status pill, nav). MainWindow (sidebar com ícones/estado selecionado, header com saudação), Dashboard (cards de indicadores sem dados inventados), Placeholder, UsuarioGerenciamento, UsuarioModal, FuncionarioGerenciamento e FuncionarioModal migrados para o design system. Login conferido contra a referência. 84 testes passando. Build Debug e Release com 0 erros e 0 warnings.
* [x] **2026-09-03** — Prompt 019 executado. Cadastro funcional de SIMCARDs implementado. Criadas as telas `SimcardGerenciamentoView.xaml` (grid com pesquisa, ações, status visual) e `SimcardModalView.xaml` (modal de criação/edição). Menu lateral atualizado: SIMCARDs agora abre o gerenciamento real. Adicionado estilo `DatePickerInput` ao Design System. 28 testes novos (total 110 passando). Build Debug e Release com 0 erros e 0 warnings. Migration `AddSimcardGerenciamento` aplicada. Push realizado para `main`.
* [x] **2026-09-04** — Prompt 020 executado. Cadastro funcional de Operadoras implementado. Entidade Operadora completada com todos os campos e validações. Criados IOperadoraRepository, OperadoraRepository, DTOs dedicados, OperadoraUseCase, Views (OperadoraGerenciamentoView, OperadoraModalView) e ViewModels. Menu Operadoras agora abre o gerenciamento real. Migration `AddOperadoraGerenciamento` criada e aplicada. 35 testes novos (total 142 passando). Build Debug e Release com 0 erros e 0 warnings.

## Checkpoints concluídos

* [x] Prompt 003 — Correção da classificação dos checklists — **2026-08-27**
* [x] Prompt 004 — Definição inicial dos cadastros e campos — **2026-08-27**
* [x] Prompt 005 — Consolidação dos cadastros e campos — **2026-08-28**
* [x] Prompt 006 — Regras de negócio e histórico — **2026-08-28**
* [x] Prompt 007 — Modelo de dados — **2026-08-28**
* [x] Prompt 008 — Arquitetura do sistema — **2026-08-28**
* [x] Prompt 009 — Registro do UX Design existente (Google Stitch) — **2026-08-28**
* [x] Prompt 010 — Revisão final e congelamento da especificação — **2026-08-28**
* [x] Prompt 011 — Fechamento final pré-codificação — **2026-08-28**
* [x] Prompt 012 — Início da implementação — **2026-08-31**
* [x] Prompt 014 — Cadastro de Funcionários — **2026-09-01**
* [x] Prompt 015 — Saneamento e build Release — **2026-09-01**
* [x] Prompt 016 — Correção da inicialização do banco — **2026-09-01**
* [x] Prompt 017 — Correção da navegação WPF — **2026-09-02**
* [x] Prompt 018 — Adequação da UX/UI ao padrão Stitch — **2026-09-03**

## Checkpoints

* [ ] Definir checkpoints de validação
* [ ] Definir checkpoints de aprovação de requisitos
* [ ] Definir checkpoints de aprovação de modelo de dados
* [ ] Definir checkpoints de aprovação de arquitetura
* [ ] Definir checkpoints de aprovação de interface
* [ ] Definir checkpoints de aprovação de relatórios
* [ ] Definir checkpoint de entrega inicial
* [ ] Definir checkpoint de homologação
* [ ] Definir checkpoint de produção

### Prompt 006 — Regras de negócio e histórico — 2026-08-28

- [x] Prompt 006 executado.
- [x] Princípio de preservação do histórico definido.
- [x] Ciclo de vida do SIMCARD documentado.
- [x] Regras de números documentadas.
- [x] Processo de troca de números documentado.
- [x] Substituição de SIMCARD documentada.
- [x] Troca simultânea de SIMCARD e número documentada.
- [x] Histórico de funcionários documentado.
- [x] Histórico de aparelhos documentado.
- [x] Regras de WhatsApp documentadas.
- [x] Cadastro rápido preservado.
- [x] Itens ainda pendentes preservados.

### Prompt 007 — Modelo de dados — 2026-08-28

- [x] Prompt 007 executado.
- [x] Modelo conceitual definido.
- [x] 8 tabelas identificadas (UsuariosSistema, Funcionarios, Operadoras, SIMCards, Aparelhos, HistoricoNumeros, HistoricoUtilizacao, HistoricoSubstituicao).
- [x] Relacionamentos documentados.
- [x] Histórico de números definido.
- [x] Histórico de utilização definido.
- [x] Substituição de SIMCARD suportada.
- [x] Troca de números suportada.
- [x] Compatibilidade futura com PostgreSQL/MySQL considerada.
- [x] Nenhum código criado.

### Prompt 008 — Arquitetura do sistema — 2026-08-28

- [x] Prompt 008 executado.
- [x] WPF definido.
- [x] C# definido.
- [x] Arquitetura em camadas definida (Presentation, Application, Domain, Infrastructure, Persistence).
- [x] SQLite inicial definido.
- [x] PostgreSQL futuro definido.
- [x] MySQL futuro definido.
- [x] Executável separado de configuração do banco definido.
- [x] Autenticação posicionada na arquitetura.
- [x] Segurança de senha registrada (nunca em texto puro, hash com salt).
- [x] Nenhum código criado.
- [x] Tratamento de erros e logging definidos conceitualmente.
- [x] Compatibilidade SQLite/PostgreSQL/MySQL considerada.

### Prompt 009 — Registro do UX Design existente (Google Stitch) — 2026-08-28

- [x] Prompt 009 executado.
- [x] Design existente do Google Stitch incorporado/referenciado.
- [x] Interface documentada.
- [x] Design definido como referência para WPF.
- [x] Nenhuma funcionalidade nova adicionada.
- [x] Nenhum código criado.
- [x] Telas/fluxos identificados: troca de números, substituição de SIMCARD, menu lateral, grids, modais, pesquisa, cadastro rápido relacionado.

### Prompt 011 — Fechamento Final Pré-Codificação — 2026-08-28

- [x] Revisão estrutural final concluída.
- [x] UX Design versionado.
- [x] Modelo de histórico fechado.
- [x] Relação SIMCARD/número fechada.
- [x] Fluxo de troca de números fechado.
- [x] Fluxo de substituição fechado.
- [x] Autenticação fechada.
- [x] Acesso master de testes documentado.
- [x] Banco inicial/futuro definido.
- [x] Relatórios da primeira versão definidos.
- [x] Especificação pronta para implementação.
- [x] Projeto congelado para início da codificação.
