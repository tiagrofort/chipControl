# Prompt 009 — Registro do UX Design Existente

## Identificação

- [X] Prompt 009
- [X] Data: 2026-08-28

## Objetivo

- [X] Registrar na documentação o UX Design já criado externamente no Google Stitch.

## Prompt completo

PROMPT 009 — REGISTRO DO UX DESIGN EXISTENTE

OBJETIVO

Registrar na documentação do projeto o UX Design que já foi criado
externamente no Google Stitch.

IMPORTANTE:

O design JÁ EXISTE.

NÃO recriar as telas.

NÃO redesenhar a interface.

NÃO alterar o design.

NÃO criar código.

NÃO iniciar o desenvolvimento WPF.

NÃO inventar funcionalidades.

Esta etapa serve apenas para incorporar ao projeto o trabalho já
realizado no Google Stitch e documentar sua utilização como referência.

O projeto continua congelado quanto a novas funcionalidades.


==================================================
1. DOCUMENTOS DE REFERÊNCIA
==================================================

Leia:

- docs/01-REQUISITOS.md
- docs/02-REGRAS-DE-NEGOCIO.md
- docs/03-MODELO-DE-DADOS.md
- docs/04-ARQUITETURA.md
- docs/05-INTERFACE.md
- docs/06-RELATORIOS.md
- docs/08-CHECKPOINTS.md
- docs/09-BACKLOG-FUTURO.md

Também localize o arquivo existente relacionado ao Google Stitch.

Foi informado que existe:

docs/stitch_controle_de_chips_ux_design.zip

Se o arquivo existir, utilize-o como referência.

NÃO modificar o conteúdo do ZIP.


==================================================
2. REGISTRO DO DESIGN
==================================================

Criar:

docs/prompts/009-google-stitch.md

Registrar que o design visual foi criado anteriormente no Google Stitch
e está sendo incorporado ao projeto nesta etapa.

Não afirmar que o Kilo Code criou o design.

Não inventar data, telas ou decisões que não possam ser verificadas.


==================================================
3. ARQUIVO DO STITCH
==================================================

Verificar se existe:

docs/stitch_controle_de_chips_ux_design.zip

Se já existir:

- manter o arquivo;
- não modificar;
- não recriar;
- não duplicar.

Se estiver em outro local do projeto e for claramente o mesmo arquivo,
mover/copiá-lo para:

docs/stitch_controle_de_chips_ux_design.zip

Somente faça isso se for necessário.

Se não for possível identificar com segurança o arquivo correto,
NÃO invente nem crie outro.

Registrar a situação no relatório final.


==================================================
4. DOCUMENTO 05 — INTERFACE
==================================================

Atualizar:

docs/05-INTERFACE.md

Documentar somente aquilo que puder ser confirmado através do design
existente e das decisões já documentadas.

Registrar:

- o Google Stitch foi utilizado como ferramenta de prototipação;
- o resultado é referência visual para a implementação WPF;
- o sistema é desktop Windows;
- o layout utiliza menu lateral;
- as telas possuem área de ações na parte superior;
- as listagens utilizam grid;
- edição utiliza modal;
- existe pesquisa;
- existe conceito de cadastro rápido relacionado;
- existem telas/fluxos específicos para operações do sistema.

NÃO inventar detalhes que não estejam confirmados.


==================================================
5. GRID
==================================================

IMPORTANTE:

A disposição das informações nas grids ainda poderá receber ajustes
durante a implementação.

Registrar isso como uma decisão de implementação/UX pendente, sem
considerar isso um problema no design.

NÃO alterar o modelo de dados por causa disso.

NÃO adicionar essa questão ao backlog de funcionalidades.


==================================================
6. TELA DE TROCA DE NÚMEROS
==================================================

Registrar, se estiver representado no design, o fluxo de:

lista da operadora
→ relacionamento
→ número antigo
→ SIMCARD
→ número novo
→ conferência
→ confirmação.

Essa funcionalidade já faz parte do escopo.

Não criar regras novas.


==================================================
7. SUBSTITUIÇÃO DE SIMCARD
==================================================

Registrar, se estiver representado no design, o fluxo de substituição
do SIMCARD.

O novo SIMCARD pode precisar ser cadastrado.

Deve existir a possibilidade de cadastrar o novo SIMCARD sem abandonar
a operação atual.

Preservar o histórico do SIMCARD antigo.


==================================================
8. REGRAS IMPORTANTES
==================================================

O design NÃO altera:

- modelo de dados;
- regras de negócio;
- status;
- histórico;
- permissões;
- arquitetura;
- funcionalidades aprovadas.

O Stitch é somente referência de UX/UI.


==================================================
9. CHECKPOINTS
==================================================

Atualizar:

docs/08-CHECKPOINTS.md

Adicionar:

- [X] Prompt 009 — registro do UX Design executado.
- [X] Design existente do Google Stitch incorporado/referenciado.
- [X] Interface documentada.
- [X] Design definido como referência para WPF.
- [X] Nenhuma funcionalidade nova adicionada.
- [X] Nenhum código criado.


==================================================
10. BACKLOG FUTURO
==================================================

NÃO adicionar ideias ao backlog somente porque poderiam melhorar o
design.

Somente registrar no backlog algo que realmente seja uma nova ideia
ou funcionalidade identificada durante a análise.

Não alterar o backlog se não houver necessidade.


==================================================
11. PRESERVAÇÃO DA DOCUMENTAÇÃO
==================================================

Nunca apagar conteúdo anterior.

Nunca substituir histórico.

Somente adicionar ou atualizar de forma compatível com o padrão
existente.

Manter os checklists no formato:

- [ ] pendente
- [X] concluído


==================================================
12. GIT
==================================================

Antes do commit:

1. Execute git status.
2. Revise todas as alterações.
3. Confirme que nenhum código foi criado.
4. Confirme que não houve alteração de regras de negócio.
5. Confirme que o arquivo do Stitch está correto.
6. Faça commit.
7. Faça push para o GitHub.
8. Informe o hash completo.
9. Informe a branch.
10. Informe o resultado do push.
11. Informe todos os arquivos alterados.

O commit deve conter somente alterações relacionadas a esta etapa.


==================================================
13. RELATÓRIO FINAL OBRIGATÓRIO
==================================================

ATENÇÃO:

Não considere o Git como substituto do relatório.

O relatório deve ser ESCRITO DIRETAMENTE NA RESPOSTA AO USUÁRIO.

O usuário irá copiar o relatório e enviar de volta para análise.

Ao terminar TODA a execução, apresente:

1. Status da execução.
2. O que foi realizado.
3. Situação do arquivo do Google Stitch.
4. Local onde o arquivo ficou armazenado.
5. Arquivos criados.
6. Arquivos alterados.
7. Arquivos excluídos, se houver.
8. O que foi documentado em docs/05-INTERFACE.md.
9. O que foi registrado em docs/08-CHECKPOINTS.md.
10. O que foi registrado em docs/prompts/009-google-stitch.md.
11. Telas/fluxos identificados no design existente.
12. Pendências de interface.
13. Problemas encontrados.
14. Correções realizadas.
15. Confirmação de que nenhum código foi criado.
16. Confirmação de que nenhuma regra de negócio foi alterada.
17. Confirmação de que nenhuma funcionalidade nova foi inventada.
18. Commit realizado.
19. Hash COMPLETO do commit.
20. Branch.
21. Resultado do push.

IMPORTANTE:

O relatório deve aparecer como texto na resposta final.

NÃO responder apenas "concluído".

NÃO fornecer apenas o hash do Git.

NÃO encerrar a execução sem apresentar o relatório.

NÃO iniciar a implementação WPF.

NÃO avançar para o próximo prompt.