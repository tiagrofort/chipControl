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

## Checkpoints concluídos

* [x] Prompt 003 — Correção da classificação dos checklists — **2026-08-27**
* [x] Prompt 004 — Definição inicial dos cadastros e campos — **2026-08-27**
* [x] Prompt 005 — Consolidação dos cadastros e campos — **2026-08-28**
* [x] Prompt 006 — Regras de negócio e histórico — **2026-08-28**
* [x] Prompt 007 — Modelo de dados — **2026-08-28**
* [x] Prompt 008 — Arquitetura do sistema — **2026-08-28**
* [x] Prompt 009 — Registro do UX Design existente (Google Stitch) — **2026-08-28**
* [x] Prompt 010 — Revisão final e congelamento da especificação — **2026-08-28**

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
