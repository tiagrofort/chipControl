# Interface

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não definir telas nesta momento. Cada tela/componente deve ser especificado aprovadamente.

## Histórico de alterações

* [x] **2026-08-28** — Prompt 009 executado. UX Design já existente criado no Google Stitch foi registrado como referência visual para a implementação WPF. O design não foi recriado, redesenho ou alterado. Layout com menu lateral, telas com área de ações na parte superior, listagens em grid, edição em modal, pesquisa, cadastro rápido relacionado.
* [x] **2026-08-28** — Prompt 010 executado. Revisão final e congelamento da especificação. Decisões de interface confirmadas e registradas como referência. Grids podem ser ajustados em implementação sem impacto no design.
* [x] **2026-08-28** — Prompt 011 executado. Referência do arquivo do Stitch corrigida para o nome real (`docs/ux_design.zip`, contendo o conteúdo `stitch_controle_de_chips_ux_design/` descompactado em `docs/ux_design/`). UX Design versionado no Git. Interface mantida sem alterações.

## Interface do Usuário

### Decisões confirmadas (Prompt 009)

- [x] O Google Stitch foi utilizado como ferramenta de prototipação.
- [x] O resultado do Stitch é referência visual para a implementação WPF.
- [x] O sistema é desktop Windows.
- [x] O layout utiliza menu lateral para navegação entre telas.
- [x] As telas possuem área de ações na parte superior.
- [x] As listagens utilizam grid na parte inferior.
- [x] A edição e inclusão de registros utilizam modal.
- [x] Existe funcionalidade de pesquisa por campos relevantes.
- [x] Existe o conceito de cadastro rápido relacionado (cadastrar registro relacionado sem sair do formulário atual).
- [x] Existem telas/fluxos específicos para operações do sistema (cadastros, históricos, trocas, substituições).

### Fluxos identificados no design existente

- [x] Fluxo de troca de números por importação: lista da operadora → relacionamento → número antigo → SIMCARD → número novo → conferência → confirmação.
- [x] Fluxo de substituição de SIMCARD: seleção do SIMCARD antigo → motivo → cadastro do novo SIMCARD (quando necessário) → confirmação → preservação do histórico do SIMCARD antigo.

### Decisões de implementação (Prompt 010)

- [x] A disposição das informações nas grids ainda receberá ajustes durante a implementação. Esta é uma decisão de implementação/UX pendente, sem impacto no design ou no modelo de dados.
- [x] O Google Stitch é a referência visual definitiva. Não será redesenhado.
- [x] A implementação será em WPF conforme arquitetura definida no Prompt 008.
- [x] A ordem de prioridade das telas será definida no plano de desenvolvimento (Prompt 011 ou seguinte).

### Decisões Finais (Prompt 010)

- [x] A interface foi confirmada como referência visual.
- [x] Nenhuma funcionalidade nova foi adicionada.
- [x] O design não foi alterado.
- [x] A especificação de interface está CONGELADA.

> Observação: o design visual foi criado externamente no Google Stitch e está sendo aqui documentado como referência. O Kilo Code não criou o design. O arquivo `docs/ux_design.zip` (com o conteúdo descompactado em `docs/ux_design/`, pasta `stitch_controle_de_chips_ux_design/` dentro do ZIP) contém o design existente e deve ser mantido sem modificações.