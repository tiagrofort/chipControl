# Interface

> Documento em construção — este arquivo contém apenas a estrutura inicial para preenchimento futuro.
> Não definir telas neste momento. Cada tela/componente deve ser especificado aprovadamente.

## Histórico de alterações

* [x] **2026-08-28** — Prompt 009 executado. UX Design já existente criado no Google Stitch foi registrado como referência visual para a implementação WPF. O design não foi recriado, redesenho ou alterado. Layout com menu lateral, telas com área de ações na parte superior, listagens em grid, edição em modal, pesquisa, cadastro rápido relacionado.

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

### Decisões pendentes (implementação)

- [x] A disposição das informações nas grids ainda poderá receber ajustes durante a implementação. Essa é uma decisão de implementação/UX pendente, sem impacto no design ou no modelo de dados.

> Observação: o design visual foi criado externamente no Google Stitch e está sendo aqui documentado como referência. O Kilo Code não criou o design. O arquivo `docs/stitch_controle_de_chips_ux_design.zip` contém o design existente e deve ser mantido sem modificações.